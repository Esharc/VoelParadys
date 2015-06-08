namespace VoelParadys
{
    partial class WishListItems
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
            this.WishListBox = new System.Windows.Forms.ListBox();
            this.ItemsLabel = new System.Windows.Forms.Label();
            this.TheAcceptButton = new System.Windows.Forms.Button();
            this.TheCancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // WishListBox
            // 
            this.WishListBox.FormattingEnabled = true;
            this.WishListBox.Location = new System.Drawing.Point(12, 40);
            this.WishListBox.Name = "WishListBox";
            this.WishListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.WishListBox.Size = new System.Drawing.Size(174, 212);
            this.WishListBox.TabIndex = 0;
            // 
            // ItemsLabel
            // 
            this.ItemsLabel.AutoSize = true;
            this.ItemsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemsLabel.Location = new System.Drawing.Point(13, 13);
            this.ItemsLabel.Name = "ItemsLabel";
            this.ItemsLabel.Size = new System.Drawing.Size(256, 24);
            this.ItemsLabel.TabIndex = 1;
            this.ItemsLabel.Text = "Select the items to be sold";
            // 
            // TheAcceptButton
            // 
            this.TheAcceptButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TheAcceptButton.Location = new System.Drawing.Point(192, 177);
            this.TheAcceptButton.Name = "TheAcceptButton";
            this.TheAcceptButton.Size = new System.Drawing.Size(80, 35);
            this.TheAcceptButton.TabIndex = 2;
            this.TheAcceptButton.Text = "Accept";
            this.TheAcceptButton.UseVisualStyleBackColor = true;
            this.TheAcceptButton.Click += new System.EventHandler(this.TheAcceptButton_Click);
            // 
            // TheCancelButton
            // 
            this.TheCancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TheCancelButton.Location = new System.Drawing.Point(192, 218);
            this.TheCancelButton.Name = "TheCancelButton";
            this.TheCancelButton.Size = new System.Drawing.Size(80, 35);
            this.TheCancelButton.TabIndex = 3;
            this.TheCancelButton.Text = "Cancel";
            this.TheCancelButton.UseVisualStyleBackColor = true;
            this.TheCancelButton.Click += new System.EventHandler(this.TheCancelButton_Click);
            // 
            // WishListItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.TheCancelButton);
            this.Controls.Add(this.TheAcceptButton);
            this.Controls.Add(this.ItemsLabel);
            this.Controls.Add(this.WishListBox);
            this.Name = "WishListItems";
            this.Text = "WishListItems";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox WishListBox;
        private System.Windows.Forms.Label ItemsLabel;
        private System.Windows.Forms.Button TheAcceptButton;
        private System.Windows.Forms.Button TheCancelButton;
    }
}