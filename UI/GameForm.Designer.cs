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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.TrackRows = new System.Windows.Forms.TrackBar();
            this.TrackCols = new System.Windows.Forms.TrackBar();
            this.LabelRowsValue = new System.Windows.Forms.Label();
            this.LabelColsValue = new System.Windows.Forms.Label();
            this.CheckboxLockRowAndCols = new System.Windows.Forms.CheckBox();
            this.gamePanel = new UI.GamePanel();
            ((System.ComponentModel.ISupportInitialize)(this.SpeedSlider)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackCols)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRun
            // 
            this.btnRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRun.Location = new System.Drawing.Point(1088, 12);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(94, 38);
            this.btnRun.TabIndex = 1;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnPause
            // 
            this.btnPause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPause.Location = new System.Drawing.Point(1193, 12);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(94, 38);
            this.btnPause.TabIndex = 2;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(1193, 65);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(94, 38);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // btnStep
            // 
            this.btnStep.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStep.Location = new System.Drawing.Point(1088, 65);
            this.btnStep.Name = "btnStep";
            this.btnStep.Size = new System.Drawing.Size(94, 38);
            this.btnStep.TabIndex = 3;
            this.btnStep.Text = "Step";
            this.btnStep.UseVisualStyleBackColor = true;
            this.btnStep.Click += new System.EventHandler(this.btnStep_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(1193, 747);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(94, 38);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblGenerationLabel
            // 
            this.lblGenerationLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblGenerationLabel.AutoSize = true;
            this.lblGenerationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGenerationLabel.Location = new System.Drawing.Point(15, 758);
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
            this.lblGeneration.Location = new System.Drawing.Point(134, 753);
            this.lblGeneration.Name = "lblGeneration";
            this.lblGeneration.Size = new System.Drawing.Size(100, 35);
            this.lblGeneration.TabIndex = 7;
            this.lblGeneration.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SpeedSlider
            // 
            this.SpeedSlider.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SpeedSlider.Location = new System.Drawing.Point(6, 40);
            this.SpeedSlider.Name = "SpeedSlider";
            this.SpeedSlider.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.SpeedSlider.Size = new System.Drawing.Size(187, 56);
            this.SpeedSlider.TabIndex = 9;
            this.SpeedSlider.TickFrequency = 100;
            this.SpeedSlider.ValueChanged += new System.EventHandler(this.SpeedSlider_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.SpeedSlider);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(1088, 131);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(199, 103);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Speed";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.LabelRowsValue);
            this.groupBox2.Controls.Add(this.TrackRows);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(1088, 263);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(94, 193);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Rows";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.LabelColsValue);
            this.groupBox3.Controls.Add(this.TrackCols);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(1193, 263);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(94, 193);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Cols";
            // 
            // TrackRows
            // 
            this.TrackRows.Location = new System.Drawing.Point(19, 19);
            this.TrackRows.Name = "TrackRows";
            this.TrackRows.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.TrackRows.Size = new System.Drawing.Size(56, 134);
            this.TrackRows.TabIndex = 0;
            this.TrackRows.ValueChanged += new System.EventHandler(this.TrackRows_ValueChanged);
            // 
            // TrackCols
            // 
            this.TrackCols.Location = new System.Drawing.Point(21, 19);
            this.TrackCols.Name = "TrackCols";
            this.TrackCols.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.TrackCols.Size = new System.Drawing.Size(56, 134);
            this.TrackCols.TabIndex = 14;
            this.TrackCols.ValueChanged += new System.EventHandler(this.TrackCols_ValueChanged);
            // 
            // LabelRowsValue
            // 
            this.LabelRowsValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LabelRowsValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LabelRowsValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelRowsValue.Location = new System.Drawing.Point(6, 151);
            this.LabelRowsValue.Name = "LabelRowsValue";
            this.LabelRowsValue.Size = new System.Drawing.Size(82, 35);
            this.LabelRowsValue.TabIndex = 14;
            this.LabelRowsValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LabelColsValue
            // 
            this.LabelColsValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LabelColsValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LabelColsValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelColsValue.Location = new System.Drawing.Point(6, 151);
            this.LabelColsValue.Name = "LabelColsValue";
            this.LabelColsValue.Size = new System.Drawing.Size(82, 35);
            this.LabelColsValue.TabIndex = 15;
            this.LabelColsValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CheckboxLockRowAndCols
            // 
            this.CheckboxLockRowAndCols.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckboxLockRowAndCols.AutoSize = true;
            this.CheckboxLockRowAndCols.Checked = true;
            this.CheckboxLockRowAndCols.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckboxLockRowAndCols.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckboxLockRowAndCols.Location = new System.Drawing.Point(1107, 462);
            this.CheckboxLockRowAndCols.Name = "CheckboxLockRowAndCols";
            this.CheckboxLockRowAndCols.Size = new System.Drawing.Size(177, 22);
            this.CheckboxLockRowAndCols.TabIndex = 14;
            this.CheckboxLockRowAndCols.Text = "Match Rows and Cols";
            this.CheckboxLockRowAndCols.UseVisualStyleBackColor = true;
            this.CheckboxLockRowAndCols.CheckedChanged += new System.EventHandler(this.CheckboxLockRowAndCols_CheckedChanged);
            // 
            // gamePanel
            // 
            this.gamePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gamePanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gamePanel.Cells = null;
            this.gamePanel.Grid = null;
            this.gamePanel.Location = new System.Drawing.Point(12, 12);
            this.gamePanel.Name = "gamePanel";
            this.gamePanel.Padding = new System.Windows.Forms.Padding(2);
            this.gamePanel.Size = new System.Drawing.Size(1070, 738);
            this.gamePanel.TabIndex = 8;
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1299, 797);
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
            this.Name = "GameForm";
            this.Text = "Game Of Life";
            this.Load += new System.EventHandler(this.GameForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.SpeedSlider)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackRows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackCols)).EndInit();
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
        private System.Windows.Forms.Label LabelRowsValue;
        private System.Windows.Forms.TrackBar TrackRows;
        private System.Windows.Forms.Label LabelColsValue;
        private System.Windows.Forms.TrackBar TrackCols;
        private System.Windows.Forms.CheckBox CheckboxLockRowAndCols;
    }
}

