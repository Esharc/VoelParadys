namespace VoelParadys
{
    partial class ProgressBar
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
            this.VPProgressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // VPProgressBar
            // 
            this.VPProgressBar.Location = new System.Drawing.Point(12, 12);
            this.VPProgressBar.Name = "VPProgressBar";
            this.VPProgressBar.Size = new System.Drawing.Size(260, 23);
            this.VPProgressBar.TabIndex = 0;
            // 
            // ProgressBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 48);
            this.Controls.Add(this.VPProgressBar);
            this.Name = "ProgressBar";
            this.Text = "Processing Request";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar VPProgressBar;
    }
}