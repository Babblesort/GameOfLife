using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Engine;
using System.Threading;
using System.Linq;
using System.Collections.Generic;

namespace UI
{
    public partial class GameForm : Form
    {
        private TaskScheduler _scheduler;
        private Grid _grid;
        private Gaea _gaea;

        public GameForm()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        public enum GameStates
        {
            Idle = 0,
            Run = 1,
            Step = 2,
            Pause = 3
        }

        public GameStates GameState { get; private set; } = GameStates.Idle;

        private void GameForm_Load(object sender, EventArgs e)
        {
            _scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            _grid = new Grid();
            gamePanel.Grid = _grid;

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
            TrackCols.Minimum = Grid.MinCols;
            TrackCols.Maximum = Grid.MaxCols;
            TrackCols.TickFrequency = 25;
            TrackCols.SmallChange = 10;

            TrackRows.Value = Grid.DefaultRows;
            TrackCols.Value = Grid.DefaultCols;

            RefreshRowColLabels();
        }

        private void RaiseGaeaOnDemand()
        {
            if(_gaea == null)
            {
                _gaea = new Gaea(_grid, new Rules());
            }
            _gaea.DelayMilliseconds = SpeedSlider.Value;
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            GameState = GameStates.Run;
            SetUiForGameState();
            RaiseGaeaOnDemand();
            _gaea.Run(UpdateGameVisualization);
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            GameState = GameStates.Pause;
            SetUiForGameState();
            RaiseGaeaOnDemand();
            _gaea.Pause();
        }

        private void btnStep_Click(object sender, EventArgs e)
        {
            GameState = GameStates.Step;
            SetUiForGameState();
            RaiseGaeaOnDemand();
            _gaea.Step(UpdateGameVisualization);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            GameState = GameStates.Idle;
            SetUiForGameState();
            _gaea?.Clear(UpdateGameVisualization);
            _gaea = null;
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

        private void RefreshRowColLabels()
        {
            LabelRowsValue.Text = TrackRows.Value.ToString();
            LabelColsValue.Text = TrackCols.Value.ToString();
        }

        private void TrackRows_ValueChanged(object sender, EventArgs e)
        {
            SetUiForGameState();
            if (CheckboxLockRowAndCols.Checked)
            {
                TrackCols.Value = TrackRows.Value;
            }
            RefreshRowColLabels();
            _grid.RowCount = TrackRows.Value;
            gamePanel.Refresh();
        }

        private void TrackCols_ValueChanged(object sender, EventArgs e)
        {
            SetUiForGameState();
            if (CheckboxLockRowAndCols.Checked)
            {
                TrackRows.Value = TrackCols.Value;
            }
            RefreshRowColLabels();
            _grid.ColCount = TrackCols.Value;
            gamePanel.Refresh();
        }

        private void CheckboxLockRowAndCols_CheckedChanged(object sender, EventArgs e)
        {
            SetUiForGameState();
            if (CheckboxLockRowAndCols.Checked)
            {
                var lower = new List<int> { TrackRows.Value, TrackCols.Value }.Min();
                TrackRows.Value = lower;
                TrackCols.Value = lower;
            }
        }

        private void SetUiForGameState()
        {
            switch (GameState)
            {
                case GameStates.Idle:
                    btnRun.Enabled = true;
                    btnStep.Enabled = true;
                    btnPause.Enabled = false;
                    btnClear.Enabled = true;
                    SpeedSlider.Enabled = true;
                    CheckboxLockRowAndCols.Enabled = true;
                    TrackRows.Enabled = true;
                    TrackCols.Enabled = true;
                    break;
                case GameStates.Run:
                    btnRun.Enabled = false;
                    btnStep.Enabled = true;
                    btnPause.Enabled = true;
                    btnClear.Enabled = true;
                    SpeedSlider.Enabled = true;
                    CheckboxLockRowAndCols.Enabled = false;
                    TrackRows.Enabled = false;
                    TrackCols.Enabled = false;
                    break;
                case GameStates.Step:
                    btnRun.Enabled = true;
                    btnStep.Enabled = true;
                    btnPause.Enabled = false;
                    btnClear.Enabled = true;
                    SpeedSlider.Enabled = true;
                    CheckboxLockRowAndCols.Enabled = false;
                    TrackRows.Enabled = false;
                    TrackCols.Enabled = false;
                    break;
                case GameStates.Pause:
                    btnRun.Enabled = true;
                    btnStep.Enabled = true;
                    btnPause.Enabled = false;
                    btnClear.Enabled = true;
                    SpeedSlider.Enabled = true;
                    CheckboxLockRowAndCols.Enabled = false;
                    TrackRows.Enabled = false;
                    TrackCols.Enabled = false;
                    break;
                default:
                    break;
            }
        }
    }
}
