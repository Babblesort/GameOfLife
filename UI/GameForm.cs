using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Engine;
using System.Threading;

namespace UI
{
    public partial class GameForm : Form
    {
        TaskScheduler _scheduler;

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
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            var grid = new Grid();
            var gaea = new Gaea(grid, new Rules());
            gamePanel.Grid = grid;
            gaea.Run((generationNumber, cells) => UpdateGameVisualization(generationNumber, cells));

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

    }
}
