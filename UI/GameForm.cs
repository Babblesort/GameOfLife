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

        public GameForm()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            _scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            _grid = new Grid(40, 40);
            gamePanel.Grid = _grid;
            _gaea = new Gaea(_grid, new Rules());
            SpeedSlider.Minimum = Gaea.MinDelayMilliseconds;
            SpeedSlider.Maximum = Gaea.MaxDelayMilliseconds;
            SpeedSlider.TickFrequency = 100;
            SpeedSlider.SmallChange = 50;
            SpeedSlider.Value = Gaea.DefaultDelayMilliseconds;
            SpeedSlider.TickStyle = TickStyle.BottomRight;
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            _gaea.Run(UpdateGameVisualization);
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            _gaea.Pause();
        }

        private void btnStep_Click(object sender, EventArgs e)
        {
            _gaea.Step(UpdateGameVisualization);
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
            _gaea.DelayMilliseconds = SpeedSlider.Value;
        }
    }
}
