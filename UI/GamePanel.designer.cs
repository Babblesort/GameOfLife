namespace UI;

sealed partial class GamePanel
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Component Designer generated code

    private void InitializeComponent()
    {
        this.SuspendLayout();
        this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GamePanel_MouseClick);
        this.ResumeLayout(false);
    }

    #endregion
}
