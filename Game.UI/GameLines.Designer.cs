namespace Game.UI
{
    partial class GameLines
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // GameLines
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(661, 649);
            this.DoubleBuffered = true;
            this.Name = "GameLines";
            this.Text = "GameLines";
            this.Load += new System.EventHandler(this.GameLines_Load);
            this.Click += new System.EventHandler(this.GameLines_Click);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.GameLines_Paint_1);
            this.Resize += new System.EventHandler(this.GameLines_Resize);
            this.ResumeLayout(false);

        }

        #endregion
    }
}

