﻿namespace VoelParadys
{
    partial class Customers
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
            this.CustomersListView = new System.Windows.Forms.ListView();
            this.ReturnButton = new System.Windows.Forms.Button();
            this.DeleteCustomerButton = new System.Windows.Forms.Button();
            this.AddCustomerButton = new System.Windows.Forms.Button();
            this.UpdateCustomerButton = new System.Windows.Forms.Button();
            this.SelectedCustomerLabel = new System.Windows.Forms.Label();
            this.NameLabel = new System.Windows.Forms.Label();
            this.SurnameLabel = new System.Windows.Forms.Label();
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.SurnameTextBox = new System.Windows.Forms.TextBox();
            this.PhoneNumberTextBox = new System.Windows.Forms.TextBox();
            this.PhoneNumberLabel = new System.Windows.Forms.Label();
            this.AddressLabel = new System.Windows.Forms.Label();
            this.IdNumberTextBox = new System.Windows.Forms.TextBox();
            this.IdNumberLabel = new System.Windows.Forms.Label();
            this.CustomerDetailsButton = new System.Windows.Forms.Button();
            this.AddressTextBox1 = new System.Windows.Forms.TextBox();
            this.AddressTextBox2 = new System.Windows.Forms.TextBox();
            this.AddressTextBox3 = new System.Windows.Forms.TextBox();
            this.AddressTextBox4 = new System.Windows.Forms.TextBox();
            this.AddressTextBox5 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // CustomersListView
            // 
            this.CustomersListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CustomersListView.Location = new System.Drawing.Point(13, 13);
            this.CustomersListView.Name = "CustomersListView";
            this.CustomersListView.Size = new System.Drawing.Size(526, 722);
            this.CustomersListView.TabIndex = 0;
            this.CustomersListView.UseCompatibleStateImageBehavior = false;
            this.CustomersListView.Click += new System.EventHandler(this.CustomerOnSelected);
            // 
            // ReturnButton
            // 
            this.ReturnButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReturnButton.Location = new System.Drawing.Point(548, 700);
            this.ReturnButton.Name = "ReturnButton";
            this.ReturnButton.Size = new System.Drawing.Size(146, 35);
            this.ReturnButton.TabIndex = 8;
            this.ReturnButton.Text = "Return";
            this.ReturnButton.UseVisualStyleBackColor = true;
            this.ReturnButton.Click += new System.EventHandler(this.ReturnButton_Click);
            // 
            // DeleteCustomerButton
            // 
            this.DeleteCustomerButton.Enabled = false;
            this.DeleteCustomerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeleteCustomerButton.Location = new System.Drawing.Point(548, 637);
            this.DeleteCustomerButton.Name = "DeleteCustomerButton";
            this.DeleteCustomerButton.Size = new System.Drawing.Size(146, 57);
            this.DeleteCustomerButton.TabIndex = 7;
            this.DeleteCustomerButton.Text = "Delete";
            this.DeleteCustomerButton.UseVisualStyleBackColor = true;
            this.DeleteCustomerButton.Click += new System.EventHandler(this.DeleteCustomerButton_Click);
            // 
            // AddCustomerButton
            // 
            this.AddCustomerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddCustomerButton.Location = new System.Drawing.Point(548, 574);
            this.AddCustomerButton.Name = "AddCustomerButton";
            this.AddCustomerButton.Size = new System.Drawing.Size(146, 57);
            this.AddCustomerButton.TabIndex = 6;
            this.AddCustomerButton.Text = "Add";
            this.AddCustomerButton.UseVisualStyleBackColor = true;
            this.AddCustomerButton.Click += new System.EventHandler(this.AddCustomerButton_Click);
            // 
            // UpdateCustomerButton
            // 
            this.UpdateCustomerButton.Enabled = false;
            this.UpdateCustomerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpdateCustomerButton.Location = new System.Drawing.Point(548, 511);
            this.UpdateCustomerButton.Name = "UpdateCustomerButton";
            this.UpdateCustomerButton.Size = new System.Drawing.Size(146, 57);
            this.UpdateCustomerButton.TabIndex = 5;
            this.UpdateCustomerButton.Text = "Update";
            this.UpdateCustomerButton.UseVisualStyleBackColor = true;
            this.UpdateCustomerButton.Click += new System.EventHandler(this.UpdateCustomerButton_Click);
            // 
            // SelectedCustomerLabel
            // 
            this.SelectedCustomerLabel.AutoSize = true;
            this.SelectedCustomerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectedCustomerLabel.Location = new System.Drawing.Point(542, 13);
            this.SelectedCustomerLabel.Name = "SelectedCustomerLabel";
            this.SelectedCustomerLabel.Size = new System.Drawing.Size(92, 24);
            this.SelectedCustomerLabel.TabIndex = 5;
            this.SelectedCustomerLabel.Text = "Selected";
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameLabel.Location = new System.Drawing.Point(542, 37);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(65, 24);
            this.NameLabel.TabIndex = 6;
            this.NameLabel.Text = "Name";
            // 
            // SurnameLabel
            // 
            this.SurnameLabel.AutoSize = true;
            this.SurnameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SurnameLabel.Location = new System.Drawing.Point(542, 93);
            this.SurnameLabel.Name = "SurnameLabel";
            this.SurnameLabel.Size = new System.Drawing.Size(94, 24);
            this.SurnameLabel.TabIndex = 7;
            this.SurnameLabel.Text = "Surname";
            // 
            // NameTextBox
            // 
            this.NameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameTextBox.Location = new System.Drawing.Point(546, 64);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.Size = new System.Drawing.Size(146, 26);
            this.NameTextBox.TabIndex = 0;
            this.NameTextBox.Leave += new System.EventHandler(this.NameTextBox_LeaveFocus);
            // 
            // SurnameTextBox
            // 
            this.SurnameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SurnameTextBox.Location = new System.Drawing.Point(546, 120);
            this.SurnameTextBox.Name = "SurnameTextBox";
            this.SurnameTextBox.Size = new System.Drawing.Size(146, 26);
            this.SurnameTextBox.TabIndex = 1;
            this.SurnameTextBox.Leave += new System.EventHandler(this.SurnameTextBox_LeaveFocus);
            // 
            // PhoneNumberTextBox
            // 
            this.PhoneNumberTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PhoneNumberTextBox.Location = new System.Drawing.Point(546, 176);
            this.PhoneNumberTextBox.Name = "PhoneNumberTextBox";
            this.PhoneNumberTextBox.Size = new System.Drawing.Size(146, 26);
            this.PhoneNumberTextBox.TabIndex = 2;
            this.PhoneNumberTextBox.Leave += new System.EventHandler(this.PhoneNumberTextBox_LeaveFocus);
            // 
            // PhoneNumberLabel
            // 
            this.PhoneNumberLabel.AutoSize = true;
            this.PhoneNumberLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PhoneNumberLabel.Location = new System.Drawing.Point(542, 149);
            this.PhoneNumberLabel.Name = "PhoneNumberLabel";
            this.PhoneNumberLabel.Size = new System.Drawing.Size(152, 24);
            this.PhoneNumberLabel.TabIndex = 10;
            this.PhoneNumberLabel.Text = "Phone Number";
            // 
            // AddressLabel
            // 
            this.AddressLabel.AutoSize = true;
            this.AddressLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddressLabel.Location = new System.Drawing.Point(542, 205);
            this.AddressLabel.Name = "AddressLabel";
            this.AddressLabel.Size = new System.Drawing.Size(87, 24);
            this.AddressLabel.TabIndex = 12;
            this.AddressLabel.Text = "Address";
            // 
            // IdNumberTextBox
            // 
            this.IdNumberTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdNumberTextBox.Location = new System.Drawing.Point(546, 416);
            this.IdNumberTextBox.Name = "IdNumberTextBox";
            this.IdNumberTextBox.Size = new System.Drawing.Size(146, 26);
            this.IdNumberTextBox.TabIndex = 4;
            this.IdNumberTextBox.Leave += new System.EventHandler(this.IdNumberTextBox_LeaveFocus);
            // 
            // IdNumberLabel
            // 
            this.IdNumberLabel.AutoSize = true;
            this.IdNumberLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdNumberLabel.Location = new System.Drawing.Point(545, 389);
            this.IdNumberLabel.Name = "IdNumberLabel";
            this.IdNumberLabel.Size = new System.Drawing.Size(110, 24);
            this.IdNumberLabel.TabIndex = 14;
            this.IdNumberLabel.Text = "ID Number";
            // 
            // CustomerDetailsButton
            // 
            this.CustomerDetailsButton.Enabled = false;
            this.CustomerDetailsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CustomerDetailsButton.Location = new System.Drawing.Point(548, 448);
            this.CustomerDetailsButton.Name = "CustomerDetailsButton";
            this.CustomerDetailsButton.Size = new System.Drawing.Size(146, 57);
            this.CustomerDetailsButton.TabIndex = 15;
            this.CustomerDetailsButton.Text = "Details";
            this.CustomerDetailsButton.UseVisualStyleBackColor = true;
            this.CustomerDetailsButton.Click += new System.EventHandler(this.CustomerDetailsButton_Click);
            // 
            // AddressTextBox1
            // 
            this.AddressTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddressTextBox1.Location = new System.Drawing.Point(546, 232);
            this.AddressTextBox1.Name = "AddressTextBox1";
            this.AddressTextBox1.Size = new System.Drawing.Size(146, 26);
            this.AddressTextBox1.TabIndex = 16;
            this.AddressTextBox1.Leave += new System.EventHandler(this.AddressTextBox1_LeaveFocus);
            // 
            // AddressTextBox2
            // 
            this.AddressTextBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddressTextBox2.Location = new System.Drawing.Point(546, 264);
            this.AddressTextBox2.Name = "AddressTextBox2";
            this.AddressTextBox2.Size = new System.Drawing.Size(146, 26);
            this.AddressTextBox2.TabIndex = 17;
            this.AddressTextBox2.Leave += new System.EventHandler(this.AddressTextBox2_LeaveFocus);
            // 
            // AddressTextBox3
            // 
            this.AddressTextBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddressTextBox3.Location = new System.Drawing.Point(546, 296);
            this.AddressTextBox3.Name = "AddressTextBox3";
            this.AddressTextBox3.Size = new System.Drawing.Size(146, 26);
            this.AddressTextBox3.TabIndex = 18;
            this.AddressTextBox3.Leave += new System.EventHandler(this.AddressTextBox3_LeaveFocus);
            // 
            // AddressTextBox4
            // 
            this.AddressTextBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddressTextBox4.Location = new System.Drawing.Point(546, 328);
            this.AddressTextBox4.Name = "AddressTextBox4";
            this.AddressTextBox4.Size = new System.Drawing.Size(146, 26);
            this.AddressTextBox4.TabIndex = 19;
            this.AddressTextBox4.Leave += new System.EventHandler(this.AddressTextBox4_LeaveFocus);
            // 
            // AddressTextBox5
            // 
            this.AddressTextBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddressTextBox5.Location = new System.Drawing.Point(546, 360);
            this.AddressTextBox5.Name = "AddressTextBox5";
            this.AddressTextBox5.Size = new System.Drawing.Size(146, 26);
            this.AddressTextBox5.TabIndex = 20;
            this.AddressTextBox5.Leave += new System.EventHandler(this.AddressTextBox5_LeaveFocus);
            // 
            // Customers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(705, 747);
            this.Controls.Add(this.AddressTextBox5);
            this.Controls.Add(this.AddressTextBox4);
            this.Controls.Add(this.AddressTextBox3);
            this.Controls.Add(this.AddressTextBox2);
            this.Controls.Add(this.AddressTextBox1);
            this.Controls.Add(this.CustomerDetailsButton);
            this.Controls.Add(this.IdNumberTextBox);
            this.Controls.Add(this.IdNumberLabel);
            this.Controls.Add(this.AddressLabel);
            this.Controls.Add(this.PhoneNumberTextBox);
            this.Controls.Add(this.PhoneNumberLabel);
            this.Controls.Add(this.SurnameTextBox);
            this.Controls.Add(this.NameTextBox);
            this.Controls.Add(this.SurnameLabel);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.SelectedCustomerLabel);
            this.Controls.Add(this.UpdateCustomerButton);
            this.Controls.Add(this.AddCustomerButton);
            this.Controls.Add(this.DeleteCustomerButton);
            this.Controls.Add(this.ReturnButton);
            this.Controls.Add(this.CustomersListView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Customers";
            this.Text = "Customers";
            this.Activated += new System.EventHandler(this.CustomerForm_Activated);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView CustomersListView;
        private System.Windows.Forms.Button ReturnButton;
        private System.Windows.Forms.Button DeleteCustomerButton;
        private System.Windows.Forms.Button AddCustomerButton;
        private System.Windows.Forms.Button UpdateCustomerButton;
        private System.Windows.Forms.Label SelectedCustomerLabel;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.Label SurnameLabel;
        private System.Windows.Forms.TextBox NameTextBox;
        private System.Windows.Forms.TextBox SurnameTextBox;
        private System.Windows.Forms.TextBox PhoneNumberTextBox;
        private System.Windows.Forms.Label PhoneNumberLabel;
        private System.Windows.Forms.Label AddressLabel;
        private System.Windows.Forms.TextBox IdNumberTextBox;
        private System.Windows.Forms.Label IdNumberLabel;
        private System.Windows.Forms.Button CustomerDetailsButton;
        private System.Windows.Forms.TextBox AddressTextBox1;
        private System.Windows.Forms.TextBox AddressTextBox2;
        private System.Windows.Forms.TextBox AddressTextBox3;
        private System.Windows.Forms.TextBox AddressTextBox4;
        private System.Windows.Forms.TextBox AddressTextBox5;
    }
}