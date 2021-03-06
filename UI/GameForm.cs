﻿using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Engine;
using System.Threading;
using System.ComponentModel;
using System.Linq;

namespace UI
{
    public partial class GameForm : Form
    {
        private TaskScheduler _scheduler;
        private Grid _grid;
        private Gaea _gaea;

        public enum GameStates
        {
            Idle = 0,
            Run = 1,
            Step = 2,
            Pause = 3
        }

        public Generation PregameCells { get; private set; }
        public GameStates GameState { get; private set; } = GameStates.Idle;

        public GameForm()
        {
            InitializeComponent();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            _scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            _grid = new Grid();
            gamePanel.Grid = _grid;
            PregameCells = _grid.CreateEmptyGeneration();
            _grid.GridSizeIncreased += OnGridSizeIncreased;
            _grid.GridSizeDecreased += OnGridSizeDecreased;
            gamePanel.GridCellClicked += OnGridCellClicked;

            GameLoadMenuItem.Enabled = true;

            SpeedSlider.Minimum = Gaea.MinDelayMilliseconds;
            SpeedSlider.Maximum = Gaea.MaxDelayMilliseconds;
            SpeedSlider.TickFrequency = 100;
            SpeedSlider.SmallChange = 50;
            SpeedSlider.Value = Gaea.DefaultDelayMilliseconds;
            SpeedSlider.TickStyle = TickStyle.BottomRight;

            TrackRows.Minimum = Grid.MinRows;
            TrackRows.Maximum = Grid.MaxRows;
            TrackRows.TickFrequency = 25;
            TrackRows.SmallChange = 10;
            UpDownRows.DecimalPlaces = 0;
            UpDownRows.Minimum = Grid.MinRows;
            UpDownRows.Maximum = Grid.MaxRows;

            TrackCols.Minimum = Grid.MinCols;
            TrackCols.Maximum = Grid.MaxCols;
            TrackCols.TickFrequency = 25;
            TrackCols.SmallChange = 10;
            UpDownCols.DecimalPlaces = 0;
            UpDownCols.Minimum = Grid.MinCols;
            UpDownCols.Maximum = Grid.MaxCols;

            TrackRows.Value = Grid.DefaultRows;
            TrackCols.Value = Grid.DefaultCols;
            UpDownRows.Value = Grid.DefaultRows;
            UpDownCols.Value = Grid.DefaultCols;

            SetUiForGameState(GameStates.Idle);
        }

        private void OnGridSizeIncreased(Object sender, EventArgs e)
        {
            AddMissingPregameCells();
            UpdateGameVisualization(0, PregameCells);
        }

        private void OnGridSizeDecreased(Object sender, EventArgs e)
        {
            RemoveExtraPregameCells();
            UpdateGameVisualization(0, PregameCells);
        }

        private void AddMissingPregameCells()
        {
            _grid.Cells
                .Except(PregameCells.Keys)
                .ToList()
                .ForEach(k => PregameCells.Add(k, false));
        }

        private void RemoveExtraPregameCells()
        {
            PregameCells.Keys
                .Except(_grid.Cells)
                .ToList()
                .ForEach(k => PregameCells.Remove(k));
        }

        private void OnGridCellClicked(object sender, CellClickedEventArgs e)
        {
            if(GameState == GameStates.Idle)
            {
                PregameCells[e.Cell] = !PregameCells[e.Cell];
                UpdateGameVisualization(0, PregameCells);
            }
        }

        private void RaiseGaeaOnDemand()
        {
            if(_gaea == null)
            {
                var generationZero = PregameCells.HasLiveCells ? PregameCells : _grid.CreateRandomGeneration();
                _gaea = new Gaea(_grid, new Rules(), UpdateGameVisualization, generationZero);
            }
            _gaea.DelayMilliseconds = SpeedSlider.Value;
        }

        private void NewGameHandler(object sender, EventArgs e)
        {
            GameNew();
        }

        private void RunGameHandler(object sender, EventArgs e)
        {
            GameRun();
        }

        private void StepGameHandler(object sender, EventArgs e)
        {
            GameStep();
        }

        private void PauseGameHandler(object sender, EventArgs e)
        {
            GamePause();
        }

        private void ExitGameHandler(object sender, EventArgs e)
        {
            GameExit();
        }

        private void GameNew()
        {
            SetUiForGameState(GameStates.Idle);
            PregameCells = _grid.CreateEmptyGeneration();
            _gaea?.Clear();
            _gaea = null;
        }

        private void GameRun()
        {
            SetUiForGameState(GameStates.Run);
            RaiseGaeaOnDemand();
            _gaea.Run();
        }

        private void GamePause()
        {
            SetUiForGameState(GameStates.Pause);
            RaiseGaeaOnDemand();
            _gaea.Pause();
        }

        private void GameStep()
        {
            SetUiForGameState(GameStates.Step);
            RaiseGaeaOnDemand();
            _gaea.Step();
        }

        private static void GameExit()
        {
            Environment.Exit(exitCode: 0);
        }

        private void UpdateGameVisualization(int generationNumber, Generation cells)
        {
            Task.Factory.StartNew(() => UpdateVisualization(generationNumber, cells), 
                CancellationToken.None, 
                TaskCreationOptions.None, 
                _scheduler);
        }

        private void UpdateVisualization(int generation, Generation cells)
        {
            lblGeneration.Text = generation.ToString();
            gamePanel.Cells = cells;
        }

        private void SpeedSlider_ValueChanged(object sender, EventArgs e)
        {
            if (_gaea != null)
            {
                _gaea.DelayMilliseconds = SpeedSlider.Value;
            }
        }

        private void TrackRows_ValueChanged(object sender, EventArgs e)
        {
            SynchronizeRowsValue(TrackRows.Value);
        }

        private void TrackCols_ValueChanged(object sender, EventArgs e)
        {
            SynchronizeColsValue(TrackCols.Value);
        }

        private void UpDownRows_ValueChanged(object sender, EventArgs e)
        {
            SynchronizeRowsValue((int) UpDownRows.Value);
        }

        private void UpDownCols_ValueChanged(object sender, EventArgs e)
        {
            SynchronizeColsValue((int) UpDownCols.Value);
        }

        private void SynchronizeRowsValue(int value)
        {
            if (CheckboxLockRowAndCols.Checked)
            {
                TrackCols.Value = value;
                UpDownCols.Value = value;
            }
            UpDownRows.Value = value;
            TrackRows.Value = value;
            _grid.RowCount = value;
        }

        private void SynchronizeColsValue(int value)
        {
            if (CheckboxLockRowAndCols.Checked)
            {
                TrackRows.Value = value;
                UpDownRows.Value = value;
            }
            UpDownCols.Value = value;
            TrackCols.Value = value;
            _grid.ColCount = value;
        }

        private void CheckboxLockRowAndCols_CheckedChanged(object sender, EventArgs e)
        {
            if (!CheckboxLockRowAndCols.Checked) return;

            var lower = Math.Min(TrackRows.Value, TrackCols.Value);
            TrackRows.Value = lower;
            TrackCols.Value = lower;
        }

        private void SetUiForGameState(GameStates state)
        {
            GameState = state;
            switch (GameState)
            {
                case GameStates.Idle:
                    GameLoadMenuItem.Enabled = true;
                    GameNewMenuItem.Enabled = true;
                    GameRunMenuItem.Enabled = true;
                    GameStepMenuItem.Enabled = true;
                    GamePauseMenuItem.Enabled = false;
                    btnRun.Enabled = true;
                    btnStep.Enabled = true;
                    btnPause.Enabled = false;
                    btnNew.Enabled = true;
                    SpeedSlider.Enabled = true;
                    CheckboxLockRowAndCols.Enabled = true;
                    TrackRows.Enabled = true;
                    TrackCols.Enabled = true;
                    UpDownRows.Enabled = true;
                    UpDownCols.Enabled = true;
                    break;
                case GameStates.Run:
                    GameLoadMenuItem.Enabled = true;
                    GameNewMenuItem.Enabled = true;
                    GameRunMenuItem.Enabled = false;
                    GameStepMenuItem.Enabled = true;
                    GamePauseMenuItem.Enabled = true;
                    btnRun.Enabled = false;
                    btnStep.Enabled = true;
                    btnPause.Enabled = true;
                    btnNew.Enabled = true;
                    SpeedSlider.Enabled = true;
                    CheckboxLockRowAndCols.Enabled = false;
                    TrackRows.Enabled = false;
                    TrackCols.Enabled = false;
                    UpDownRows.Enabled = false;
                    UpDownCols.Enabled = false;
                    break;
                case GameStates.Step:
                    GameLoadMenuItem.Enabled = true;
                    GameNewMenuItem.Enabled = true;
                    GameRunMenuItem.Enabled = true;
                    GameStepMenuItem.Enabled = true;
                    GamePauseMenuItem.Enabled = false;
                    btnRun.Enabled = true;
                    btnStep.Enabled = true;
                    btnPause.Enabled = false;
                    btnNew.Enabled = true;
                    SpeedSlider.Enabled = true;
                    CheckboxLockRowAndCols.Enabled = false;
                    TrackRows.Enabled = false;
                    TrackCols.Enabled = false;
                    UpDownRows.Enabled = false;
                    UpDownCols.Enabled = false;
                    break;
                case GameStates.Pause:
                    GameLoadMenuItem.Enabled = true;
                    GameNewMenuItem.Enabled = true;
                    GameRunMenuItem.Enabled = true;
                    GameStepMenuItem.Enabled = true;
                    GamePauseMenuItem.Enabled = false;
                    btnRun.Enabled = true;
                    btnStep.Enabled = true;
                    btnPause.Enabled = false;
                    btnNew.Enabled = true;
                    SpeedSlider.Enabled = true;
                    CheckboxLockRowAndCols.Enabled = false;
                    TrackRows.Enabled = false;
                    TrackCols.Enabled = false;
                    UpDownRows.Enabled = false;
                    UpDownCols.Enabled = false;
                    break;
            }
        }
    }
}
