namespace UI
{
    partial class GameForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameForm));
            this.btnRun = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnStep = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblGenerationLabel = new System.Windows.Forms.Label();
            this.lblGeneration = new System.Windows.Forms.Label();
            this.SpeedSlider = new System.Windows.Forms.TrackBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.UpDownRows = new System.Windows.Forms.NumericUpDown();
            this.TrackRows = new System.Windows.Forms.TrackBar();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.UpDownCols = new System.Windows.Forms.NumericUpDown();
            this.TrackCols = new System.Windows.Forms.TrackBar();
            this.CheckboxLockRowAndCols = new System.Windows.Forms.CheckBox();
            this.Menu = new System.Windows.Forms.MenuStrip();
            this.GameMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GameLoadMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GameNewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GameRunMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GameStepMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GamePauseMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GameExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.divToolStripMenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.gamePanel = new UI.GamePanel();
            ((System.ComponentModel.ISupportInitialize)(this.SpeedSlider)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackRows)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownCols)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackCols)).BeginInit();
            this.Menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRun
            // 
            this.btnRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRun.Location = new System.Drawing.Point(1088, 37);
            this.btnRun.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(93, 38);
            this.btnRun.TabIndex = 0;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.RunGameHandler);
            // 
            // btnPause
            // 
            this.btnPause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPause.Location = new System.Drawing.Point(1088, 82);
            this.btnPause.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(93, 38);
            this.btnPause.TabIndex = 2;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.PauseGameHandler);
            // 
            // btnNew
            // 
            this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNew.Location = new System.Drawing.Point(1088, 747);
            this.btnNew.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(93, 38);
            this.btnNew.TabIndex = 7;
            this.btnNew.Text = "New Game";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.NewGameHandler);
            // 
            // btnStep
            // 
            this.btnStep.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStep.Location = new System.Drawing.Point(1193, 37);
            this.btnStep.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnStep.Name = "btnStep";
            this.btnStep.Size = new System.Drawing.Size(93, 38);
            this.btnStep.TabIndex = 1;
            this.btnStep.Text = "Step";
            this.btnStep.UseVisualStyleBackColor = true;
            this.btnStep.Click += new System.EventHandler(this.StepGameHandler);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(1193, 747);
            this.btnExit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(93, 38);
            this.btnExit.TabIndex = 8;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.ExitGameHandler);
            // 
            // lblGenerationLabel
            // 
            this.lblGenerationLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblGenerationLabel.AutoSize = true;
            this.lblGenerationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGenerationLabel.Location = new System.Drawing.Point(8, 751);
            this.lblGenerationLabel.Name = "lblGenerationLabel";
            this.lblGenerationLabel.Size = new System.Drawing.Size(108, 25);
            this.lblGenerationLabel.TabIndex = 6;
            this.lblGenerationLabel.Text = "Generation";
            this.lblGenerationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblGeneration
            // 
            this.lblGeneration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblGeneration.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblGeneration.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGeneration.Location = new System.Drawing.Point(128, 746);
            this.lblGeneration.Name = "lblGeneration";
            this.lblGeneration.Size = new System.Drawing.Size(100, 34);
            this.lblGeneration.TabIndex = 7;
            this.lblGeneration.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SpeedSlider
            // 
            this.SpeedSlider.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SpeedSlider.Location = new System.Drawing.Point(5, 37);
            this.SpeedSlider.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SpeedSlider.Name = "SpeedSlider";
            this.SpeedSlider.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.SpeedSlider.Size = new System.Drawing.Size(187, 56);
            this.SpeedSlider.TabIndex = 0;
            this.SpeedSlider.TickFrequency = 100;
            this.SpeedSlider.ValueChanged += new System.EventHandler(this.SpeedSlider_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.SpeedSlider);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(1088, 130);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(199, 103);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Speed";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.UpDownRows);
            this.groupBox2.Controls.Add(this.TrackRows);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(1088, 249);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(93, 193);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Rows";
            // 
            // UpDownRows
            // 
            this.UpDownRows.Location = new System.Drawing.Point(5, 159);
            this.UpDownRows.Margin = new System.Windows.Forms.Padding(4);
            this.UpDownRows.Name = "UpDownRows";
            this.UpDownRows.Size = new System.Drawing.Size(81, 27);
            this.UpDownRows.TabIndex = 9;
            this.UpDownRows.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.UpDownRows.ValueChanged += new System.EventHandler(this.UpDownRows_ValueChanged);
            // 
            // TrackRows
            // 
            this.TrackRows.Location = new System.Drawing.Point(19, 27);
            this.TrackRows.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TrackRows.Name = "TrackRows";
            this.TrackRows.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.TrackRows.Size = new System.Drawing.Size(56, 126);
            this.TrackRows.TabIndex = 0;
            this.TrackRows.ValueChanged += new System.EventHandler(this.TrackRows_ValueChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.UpDownCols);
            this.groupBox3.Controls.Add(this.TrackCols);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(1193, 249);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Size = new System.Drawing.Size(93, 193);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Cols";
            // 
            // UpDownCols
            // 
            this.UpDownCols.Location = new System.Drawing.Point(5, 159);
            this.UpDownCols.Margin = new System.Windows.Forms.Padding(4);
            this.UpDownCols.Name = "UpDownCols";
            this.UpDownCols.Size = new System.Drawing.Size(81, 27);
            this.UpDownCols.TabIndex = 10;
            this.UpDownCols.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.UpDownCols.ValueChanged += new System.EventHandler(this.UpDownCols_ValueChanged);
            // 
            // TrackCols
            // 
            this.TrackCols.Location = new System.Drawing.Point(21, 27);
            this.TrackCols.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TrackCols.Name = "TrackCols";
            this.TrackCols.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.TrackCols.Size = new System.Drawing.Size(56, 126);
            this.TrackCols.TabIndex = 14;
            this.TrackCols.ValueChanged += new System.EventHandler(this.TrackCols_ValueChanged);
            // 
            // CheckboxLockRowAndCols
            // 
            this.CheckboxLockRowAndCols.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckboxLockRowAndCols.AutoSize = true;
            this.CheckboxLockRowAndCols.Checked = true;
            this.CheckboxLockRowAndCols.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckboxLockRowAndCols.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckboxLockRowAndCols.Location = new System.Drawing.Point(1107, 452);
            this.CheckboxLockRowAndCols.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CheckboxLockRowAndCols.Name = "CheckboxLockRowAndCols";
            this.CheckboxLockRowAndCols.Size = new System.Drawing.Size(177, 22);
            this.CheckboxLockRowAndCols.TabIndex = 6;
            this.CheckboxLockRowAndCols.Text = "Match Rows and Cols";
            this.CheckboxLockRowAndCols.UseVisualStyleBackColor = true;
            this.CheckboxLockRowAndCols.CheckedChanged += new System.EventHandler(this.CheckboxLockRowAndCols_CheckedChanged);
            // 
            // Menu
            // 
            this.Menu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GameMenuItem});
            this.Menu.Location = new System.Drawing.Point(0, 0);
            this.Menu.Name = "Menu";
            this.Menu.Size = new System.Drawing.Size(1299, 28);
            this.Menu.TabIndex = 9;
            this.Menu.Text = "menuStrip1";
            // 
            // GameMenuItem
            // 
            this.GameMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GameLoadMenuItem,
            this.GameNewMenuItem,
            this.GameRunMenuItem,
            this.GameStepMenuItem,
            this.GamePauseMenuItem,
            this.divToolStripMenuItem,
            this.GameExitMenuItem});
            this.GameMenuItem.Name = "GameMenuItem";
            this.GameMenuItem.Size = new System.Drawing.Size(60, 24);
            this.GameMenuItem.Text = "Game";
            // 
            // GameLoadMenuItem
            // 
            this.GameLoadMenuItem.Name = "GameLoadMenuItem";
            this.GameLoadMenuItem.Size = new System.Drawing.Size(181, 26);
            this.GameLoadMenuItem.Text = "Load";
            // 
            // GameNewMenuItem
            // 
            this.GameNewMenuItem.Name = "GameNewMenuItem";
            this.GameNewMenuItem.Size = new System.Drawing.Size(181, 26);
            this.GameNewMenuItem.Text = "New";
            this.GameNewMenuItem.Click += new System.EventHandler(this.NewGameHandler);
            // 
            // GameRunMenuItem
            // 
            this.GameRunMenuItem.Name = "GameRunMenuItem";
            this.GameRunMenuItem.Size = new System.Drawing.Size(181, 26);
            this.GameRunMenuItem.Text = "Run";
            this.GameRunMenuItem.Click += new System.EventHandler(this.RunGameHandler);
            // 
            // GameStepMenuItem
            // 
            this.GameStepMenuItem.Name = "GameStepMenuItem";
            this.GameStepMenuItem.Size = new System.Drawing.Size(181, 26);
            this.GameStepMenuItem.Text = "Step";
            this.GameStepMenuItem.Click += new System.EventHandler(this.StepGameHandler);
            // 
            // GamePauseMenuItem
            // 
            this.GamePauseMenuItem.Name = "GamePauseMenuItem";
            this.GamePauseMenuItem.Size = new System.Drawing.Size(181, 26);
            this.GamePauseMenuItem.Text = "Pause";
            this.GamePauseMenuItem.Click += new System.EventHandler(this.PauseGameHandler);
            // 
            // GameExitMenuItem
            // 
            this.GameExitMenuItem.Name = "GameExitMenuItem";
            this.GameExitMenuItem.Size = new System.Drawing.Size(181, 26);
            this.GameExitMenuItem.Text = "Exit";
            this.GameExitMenuItem.Click += new System.EventHandler(this.ExitGameHandler);
            // 
            // divToolStripMenuItem
            // 
            this.divToolStripMenuItem.Name = "divToolStripMenuItem";
            this.divToolStripMenuItem.Size = new System.Drawing.Size(178, 6);
            // 
            // gamePanel
            // 
            this.gamePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gamePanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gamePanel.Cells = null;
            this.gamePanel.Grid = null;
            this.gamePanel.Location = new System.Drawing.Point(12, 36);
            this.gamePanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gamePanel.Name = "gamePanel";
            this.gamePanel.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gamePanel.Size = new System.Drawing.Size(1071, 698);
            this.gamePanel.TabIndex = 8;
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1299, 798);
            this.Controls.Add(this.CheckboxLockRowAndCols);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gamePanel);
            this.Controls.Add(this.lblGeneration);
            this.Controls.Add(this.lblGenerationLabel);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnStep);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.Menu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimumSize = new System.Drawing.Size(499, 598);
            this.Name = "GameForm";
            this.Text = "Game Of Life";
            this.Load += new System.EventHandler(this.GameForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.SpeedSlider)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownRows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackRows)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownCols)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackCols)).EndInit();
            this.Menu.ResumeLayout(false);
            this.Menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnStep;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblGenerationLabel;
        private System.Windows.Forms.Label lblGeneration;
        private GamePanel gamePanel;
        private System.Windows.Forms.TrackBar SpeedSlider;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TrackBar TrackRows;
        private System.Windows.Forms.TrackBar TrackCols;
        private System.Windows.Forms.CheckBox CheckboxLockRowAndCols;
        private System.Windows.Forms.NumericUpDown UpDownRows;
        private System.Windows.Forms.NumericUpDown UpDownCols;
        private System.Windows.Forms.MenuStrip Menu;
        private System.Windows.Forms.ToolStripMenuItem GameMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GameNewMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GameRunMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GameStepMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GamePauseMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GameLoadMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GameExitMenuItem;
        private System.Windows.Forms.ToolStripSeparator divToolStripMenuItem;
    }
}

