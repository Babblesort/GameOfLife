using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Engine;
using System.Threading;

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

        public GameStates GameState { get; private set; } = GameStates.Idle;

        public GameForm()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(exitCode: 0);
        }

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
        }

        private void RaiseGaeaOnDemand()
        {
            if(_gaea == null)
            {
                _gaea = new Gaea(_grid, new Rules(), _grid.CreateRandomGeneration());
            }
            _gaea.DelayMilliseconds = SpeedSlider.Value;
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            SetUiForGameState(GameStates.Run);
            RaiseGaeaOnDemand();
            _gaea.Run(UpdateGameVisualization);
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            SetUiForGameState(GameStates.Pause);
            RaiseGaeaOnDemand();
            _gaea.Pause();
        }

        private void btnStep_Click(object sender, EventArgs e)
        {
            SetUiForGameState(GameStates.Step);
            RaiseGaeaOnDemand();
            _gaea.Step(UpdateGameVisualization);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            SetUiForGameState(GameStates.Idle);
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
                    btnRun.Enabled = true;
                    btnStep.Enabled = true;
                    btnPause.Enabled = false;
                    btnClear.Enabled = true;
                    SpeedSlider.Enabled = true;
                    CheckboxLockRowAndCols.Enabled = true;
                    TrackRows.Enabled = true;
                    TrackCols.Enabled = true;
                    UpDownRows.Enabled = true;
                    UpDownCols.Enabled = true;
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
                    UpDownRows.Enabled = false;
                    UpDownCols.Enabled = false;
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
                    UpDownRows.Enabled = false;
                    UpDownCols.Enabled = false;
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
                    UpDownRows.Enabled = false;
                    UpDownCols.Enabled = false;
                    break;
            }
        }
    }
}
