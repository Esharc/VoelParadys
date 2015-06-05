namespace VoelParadys
{
    partial class EnterPassword
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
            this.EnterPasswordLabel = new System.Windows.Forms.Label();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.PassAcceptButton = new System.Windows.Forms.Button();
            this.PassCancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // EnterPasswordLabel
            // 
            this.EnterPasswordLabel.AutoSize = true;
            this.EnterPasswordLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EnterPasswordLabel.Location = new System.Drawing.Point(13, 13);
            this.EnterPasswordLabel.Name = "EnterPasswordLabel";
            this.EnterPasswordLabel.Size = new System.Drawing.Size(142, 24);
            this.EnterPasswordLabel.TabIndex = 0;
            this.EnterPasswordLabel.Text = "Enter Password";
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PasswordTextBox.Location = new System.Drawing.Point(17, 41);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.Size = new System.Drawing.Size(138, 26);
            this.PasswordTextBox.TabIndex = 1;
            this.PasswordTextBox.UseSystemPasswordChar = true;
            this.PasswordTextBox.TextChanged += new System.EventHandler(this.PasswordTextBox_TextChanged);
            this.PasswordTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnPassEnterPressed);
            // 
            // PassAcceptButton
            // 
            this.PassAcceptButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PassAcceptButton.Location = new System.Drawing.Point(13, 74);
            this.PassAcceptButton.Name = "PassAcceptButton";
            this.PassAcceptButton.Size = new System.Drawing.Size(89, 35);
            this.PassAcceptButton.TabIndex = 2;
            this.PassAcceptButton.Text = "Accept";
            this.PassAcceptButton.UseVisualStyleBackColor = true;
            this.PassAcceptButton.Click += new System.EventHandler(this.PassAcceptButton_Click);
            // 
            // PassCancelButton
            // 
            this.PassCancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PassCancelButton.Location = new System.Drawing.Point(108, 74);
            this.PassCancelButton.Name = "PassCancelButton";
            this.PassCancelButton.Size = new System.Drawing.Size(89, 35);
            this.PassCancelButton.TabIndex = 3;
            this.PassCancelButton.Text = "Cancel";
            this.PassCancelButton.UseVisualStyleBackColor = true;
            this.PassCancelButton.Click += new System.EventHandler(this.PassCancelButton_Click);
            // 
            // EnterPasswordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(209, 120);
            this.Controls.Add(this.PassCancelButton);
            this.Controls.Add(this.PassAcceptButton);
            this.Controls.Add(this.PasswordTextBox);
            this.Controls.Add(this.EnterPasswordLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "EnterPasswordForm";
            this.Text = "EnterPasswordForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label EnterPasswordLabel;
        private System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.Button PassAcceptButton;
        private System.Windows.Forms.Button PassCancelButton;
    }
}