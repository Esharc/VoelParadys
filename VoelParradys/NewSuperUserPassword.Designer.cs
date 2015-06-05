namespace VoelParadys
{
    partial class NewSuperUserPassword
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
            this.OldPasswordLabel = new System.Windows.Forms.Label();
            this.OldPasswordTextBox = new System.Windows.Forms.TextBox();
            this.NewPasswordTextBox = new System.Windows.Forms.TextBox();
            this.NewPasswordLabel = new System.Windows.Forms.Label();
            this.RetypPasswordTextBox = new System.Windows.Forms.TextBox();
            this.RetypePasswordLabel = new System.Windows.Forms.Label();
            this.SavePasswordButton = new System.Windows.Forms.Button();
            this.CancelPasswordButton = new System.Windows.Forms.Button();
            this.HelpWithPasswordButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // OldPasswordLabel
            // 
            this.OldPasswordLabel.AutoSize = true;
            this.OldPasswordLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OldPasswordLabel.Location = new System.Drawing.Point(12, 9);
            this.OldPasswordLabel.Name = "OldPasswordLabel";
            this.OldPasswordLabel.Size = new System.Drawing.Size(127, 24);
            this.OldPasswordLabel.TabIndex = 0;
            this.OldPasswordLabel.Text = "Old Password";
            // 
            // OldPasswordTextBox
            // 
            this.OldPasswordTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OldPasswordTextBox.Location = new System.Drawing.Point(16, 37);
            this.OldPasswordTextBox.Name = "OldPasswordTextBox";
            this.OldPasswordTextBox.PasswordChar = '*';
            this.OldPasswordTextBox.Size = new System.Drawing.Size(196, 26);
            this.OldPasswordTextBox.TabIndex = 1;
            this.OldPasswordTextBox.UseSystemPasswordChar = true;
            this.OldPasswordTextBox.Leave += new System.EventHandler(this.OldPasswordTextBox_LeaveFocus);
            // 
            // NewPasswordTextBox
            // 
            this.NewPasswordTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewPasswordTextBox.Location = new System.Drawing.Point(16, 93);
            this.NewPasswordTextBox.Name = "NewPasswordTextBox";
            this.NewPasswordTextBox.PasswordChar = '*';
            this.NewPasswordTextBox.Size = new System.Drawing.Size(196, 26);
            this.NewPasswordTextBox.TabIndex = 3;
            this.NewPasswordTextBox.UseSystemPasswordChar = true;
            this.NewPasswordTextBox.TextChanged += new System.EventHandler(this.NewPasswordTextBox_TextChanged);
            this.NewPasswordTextBox.Leave += new System.EventHandler(this.NewPasswordTextBox_LeaveFocus);
            // 
            // NewPasswordLabel
            // 
            this.NewPasswordLabel.AutoSize = true;
            this.NewPasswordLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewPasswordLabel.Location = new System.Drawing.Point(12, 66);
            this.NewPasswordLabel.Name = "NewPasswordLabel";
            this.NewPasswordLabel.Size = new System.Drawing.Size(136, 24);
            this.NewPasswordLabel.TabIndex = 2;
            this.NewPasswordLabel.Text = "New Password";
            // 
            // RetypPasswordTextBox
            // 
            this.RetypPasswordTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RetypPasswordTextBox.Location = new System.Drawing.Point(16, 149);
            this.RetypPasswordTextBox.Name = "RetypPasswordTextBox";
            this.RetypPasswordTextBox.PasswordChar = '*';
            this.RetypPasswordTextBox.Size = new System.Drawing.Size(196, 26);
            this.RetypPasswordTextBox.TabIndex = 5;
            this.RetypPasswordTextBox.UseSystemPasswordChar = true;
            this.RetypPasswordTextBox.TextChanged += new System.EventHandler(this.RetypPasswordTextBox_TextChanged);
            // 
            // RetypePasswordLabel
            // 
            this.RetypePasswordLabel.AutoSize = true;
            this.RetypePasswordLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RetypePasswordLabel.Location = new System.Drawing.Point(12, 122);
            this.RetypePasswordLabel.Name = "RetypePasswordLabel";
            this.RetypePasswordLabel.Size = new System.Drawing.Size(200, 24);
            this.RetypePasswordLabel.TabIndex = 4;
            this.RetypePasswordLabel.Text = "Retype New Password";
            // 
            // SavePasswordButton
            // 
            this.SavePasswordButton.Enabled = false;
            this.SavePasswordButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SavePasswordButton.Location = new System.Drawing.Point(16, 182);
            this.SavePasswordButton.Name = "SavePasswordButton";
            this.SavePasswordButton.Size = new System.Drawing.Size(90, 35);
            this.SavePasswordButton.TabIndex = 6;
            this.SavePasswordButton.Text = "Save";
            this.SavePasswordButton.UseVisualStyleBackColor = true;
            this.SavePasswordButton.Click += new System.EventHandler(this.SavePasswordButton_Click);
            // 
            // CancelPasswordButton
            // 
            this.CancelPasswordButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelPasswordButton.Location = new System.Drawing.Point(112, 181);
            this.CancelPasswordButton.Name = "CancelPasswordButton";
            this.CancelPasswordButton.Size = new System.Drawing.Size(90, 35);
            this.CancelPasswordButton.TabIndex = 7;
            this.CancelPasswordButton.Text = "Cancel";
            this.CancelPasswordButton.UseVisualStyleBackColor = true;
            this.CancelPasswordButton.Click += new System.EventHandler(this.CancelPasswordButton_Click);
            // 
            // HelpWithPasswordButton
            // 
            this.HelpWithPasswordButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HelpWithPasswordButton.Location = new System.Drawing.Point(208, 182);
            this.HelpWithPasswordButton.Name = "HelpWithPasswordButton";
            this.HelpWithPasswordButton.Size = new System.Drawing.Size(90, 35);
            this.HelpWithPasswordButton.TabIndex = 8;
            this.HelpWithPasswordButton.Text = "Help";
            this.HelpWithPasswordButton.UseVisualStyleBackColor = true;
            this.HelpWithPasswordButton.Click += new System.EventHandler(this.HelpWithPasswordButton_Click);
            // 
            // NewSuperUserPasswordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 229);
            this.Controls.Add(this.HelpWithPasswordButton);
            this.Controls.Add(this.CancelPasswordButton);
            this.Controls.Add(this.SavePasswordButton);
            this.Controls.Add(this.RetypPasswordTextBox);
            this.Controls.Add(this.RetypePasswordLabel);
            this.Controls.Add(this.NewPasswordTextBox);
            this.Controls.Add(this.NewPasswordLabel);
            this.Controls.Add(this.OldPasswordTextBox);
            this.Controls.Add(this.OldPasswordLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "NewSuperUserPasswordForm";
            this.Text = "NewSuperUserPasswordForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label OldPasswordLabel;
        private System.Windows.Forms.TextBox OldPasswordTextBox;
        private System.Windows.Forms.TextBox NewPasswordTextBox;
        private System.Windows.Forms.Label NewPasswordLabel;
        private System.Windows.Forms.TextBox RetypPasswordTextBox;
        private System.Windows.Forms.Label RetypePasswordLabel;
        private System.Windows.Forms.Button SavePasswordButton;
        private System.Windows.Forms.Button CancelPasswordButton;
        private System.Windows.Forms.Button HelpWithPasswordButton;
    }
}