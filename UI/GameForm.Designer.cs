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
            this.btnClear = new System.Windows.Forms.Button();
            this.btnStep = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblGenerationLabel = new System.Windows.Forms.Label();
            this.lblGeneration = new System.Windows.Forms.Label();
            this.SpeedSlider = new System.Windows.Forms.TrackBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TrackRows = new System.Windows.Forms.TrackBar();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.TrackCols = new System.Windows.Forms.TrackBar();
            this.CheckboxLockRowAndCols = new System.Windows.Forms.CheckBox();
            this.UpDownRows = new System.Windows.Forms.NumericUpDown();
            this.UpDownCols = new System.Windows.Forms.NumericUpDown();
            this.gamePanel = new UI.GamePanel();
            ((System.ComponentModel.ISupportInitialize)(this.SpeedSlider)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackRows)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackCols)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownCols)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRun
            // 
            this.btnRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRun.Location = new System.Drawing.Point(816, 10);
            this.btnRun.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(70, 31);
            this.btnRun.TabIndex = 0;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnPause
            // 
            this.btnPause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPause.Location = new System.Drawing.Point(816, 46);
            this.btnPause.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(70, 31);
            this.btnPause.TabIndex = 2;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(816, 607);
            this.btnClear.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(70, 31);
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "New Game";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnStep
            // 
            this.btnStep.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStep.Location = new System.Drawing.Point(895, 10);
            this.btnStep.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnStep.Name = "btnStep";
            this.btnStep.Size = new System.Drawing.Size(70, 31);
            this.btnStep.TabIndex = 1;
            this.btnStep.Text = "Step";
            this.btnStep.UseVisualStyleBackColor = true;
            this.btnStep.Click += new System.EventHandler(this.btnStep_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(895, 607);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(70, 31);
            this.btnExit.TabIndex = 8;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblGenerationLabel
            // 
            this.lblGenerationLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblGenerationLabel.AutoSize = true;
            this.lblGenerationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGenerationLabel.Location = new System.Drawing.Point(6, 610);
            this.lblGenerationLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblGenerationLabel.Name = "lblGenerationLabel";
            this.lblGenerationLabel.Size = new System.Drawing.Size(89, 20);
            this.lblGenerationLabel.TabIndex = 6;
            this.lblGenerationLabel.Text = "Generation";
            this.lblGenerationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblGeneration
            // 
            this.lblGeneration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblGeneration.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblGeneration.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGeneration.Location = new System.Drawing.Point(96, 606);
            this.lblGeneration.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblGeneration.Name = "lblGeneration";
            this.lblGeneration.Size = new System.Drawing.Size(75, 28);
            this.lblGeneration.TabIndex = 7;
            this.lblGeneration.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SpeedSlider
            // 
            this.SpeedSlider.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SpeedSlider.Location = new System.Drawing.Point(4, 30);
            this.SpeedSlider.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SpeedSlider.Name = "SpeedSlider";
            this.SpeedSlider.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.SpeedSlider.Size = new System.Drawing.Size(140, 45);
            this.SpeedSlider.TabIndex = 0;
            this.SpeedSlider.TickFrequency = 100;
            this.SpeedSlider.ValueChanged += new System.EventHandler(this.SpeedSlider_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.SpeedSlider);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(816, 85);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(149, 84);
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
            this.groupBox2.Location = new System.Drawing.Point(816, 182);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Size = new System.Drawing.Size(70, 157);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Rows";
            // 
            // TrackRows
            // 
            this.TrackRows.Location = new System.Drawing.Point(14, 22);
            this.TrackRows.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TrackRows.Name = "TrackRows";
            this.TrackRows.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.TrackRows.Size = new System.Drawing.Size(45, 102);
            this.TrackRows.TabIndex = 0;
            this.TrackRows.ValueChanged += new System.EventHandler(this.TrackRows_ValueChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.UpDownCols);
            this.groupBox3.Controls.Add(this.TrackCols);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(895, 182);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Size = new System.Drawing.Size(70, 157);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Cols";
            // 
            // TrackCols
            // 
            this.TrackCols.Location = new System.Drawing.Point(16, 22);
            this.TrackCols.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TrackCols.Name = "TrackCols";
            this.TrackCols.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.TrackCols.Size = new System.Drawing.Size(45, 102);
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
            this.CheckboxLockRowAndCols.Location = new System.Drawing.Point(818, 347);
            this.CheckboxLockRowAndCols.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.CheckboxLockRowAndCols.Name = "CheckboxLockRowAndCols";
            this.CheckboxLockRowAndCols.Size = new System.Drawing.Size(145, 19);
            this.CheckboxLockRowAndCols.TabIndex = 6;
            this.CheckboxLockRowAndCols.Text = "Match Rows and Cols";
            this.CheckboxLockRowAndCols.UseVisualStyleBackColor = true;
            this.CheckboxLockRowAndCols.CheckedChanged += new System.EventHandler(this.CheckboxLockRowAndCols_CheckedChanged);
            // 
            // UpDownRows
            // 
            this.UpDownRows.Location = new System.Drawing.Point(4, 129);
            this.UpDownRows.Name = "UpDownRows";
            this.UpDownRows.Size = new System.Drawing.Size(61, 23);
            this.UpDownRows.TabIndex = 9;
            this.UpDownRows.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.UpDownRows.ValueChanged += new System.EventHandler(this.UpDownRows_ValueChanged);
            // 
            // UpDownCols
            // 
            this.UpDownCols.Location = new System.Drawing.Point(4, 129);
            this.UpDownCols.Name = "UpDownCols";
            this.UpDownCols.Size = new System.Drawing.Size(61, 23);
            this.UpDownCols.TabIndex = 10;
            this.UpDownCols.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.UpDownCols.ValueChanged += new System.EventHandler(this.UpDownCols_ValueChanged);
            // 
            // gamePanel
            // 
            this.gamePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gamePanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gamePanel.Cells = null;
            this.gamePanel.Grid = null;
            this.gamePanel.Location = new System.Drawing.Point(9, 10);
            this.gamePanel.Margin = new System.Windows.Forms.Padding(2);
            this.gamePanel.Name = "gamePanel";
            this.gamePanel.Padding = new System.Windows.Forms.Padding(2);
            this.gamePanel.Size = new System.Drawing.Size(804, 587);
            this.gamePanel.TabIndex = 8;
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(974, 648);
            this.Controls.Add(this.CheckboxLockRowAndCols);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gamePanel);
            this.Controls.Add(this.lblGeneration);
            this.Controls.Add(this.lblGenerationLabel);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnStep);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnRun);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MinimumSize = new System.Drawing.Size(379, 495);
            this.Name = "GameForm";
            this.Text = "Game Of Life";
            this.Load += new System.EventHandler(this.GameForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.SpeedSlider)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackRows)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackCols)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownRows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownCols)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnClear;
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
    }
}

