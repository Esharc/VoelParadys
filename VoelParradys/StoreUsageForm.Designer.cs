namespace VoelParadys
{
    partial class StoreUsageForm
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
            this.UsageListView = new System.Windows.Forms.ListView();
            this.ReturnButton = new System.Windows.Forms.Button();
            this.UpdateUsageButton = new System.Windows.Forms.Button();
            this.SelectedItemLabel = new System.Windows.Forms.Label();
            this.ItemNameLabel = new System.Windows.Forms.Label();
            this.UsageLabel = new System.Windows.Forms.Label();
            this.UsageTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // UsageListView
            // 
            this.UsageListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UsageListView.Location = new System.Drawing.Point(12, 12);
            this.UsageListView.MultiSelect = false;
            this.UsageListView.Name = "UsageListView";
            this.UsageListView.Size = new System.Drawing.Size(306, 289);
            this.UsageListView.TabIndex = 0;
            this.UsageListView.UseCompatibleStateImageBehavior = false;
            this.UsageListView.Click += new System.EventHandler(this.OnItemSelected);
            // 
            // ReturnButton
            // 
            this.ReturnButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReturnButton.Location = new System.Drawing.Point(323, 267);
            this.ReturnButton.Name = "ReturnButton";
            this.ReturnButton.Size = new System.Drawing.Size(146, 35);
            this.ReturnButton.TabIndex = 2;
            this.ReturnButton.Text = "Return";
            this.ReturnButton.UseVisualStyleBackColor = true;
            this.ReturnButton.Click += new System.EventHandler(this.ReturnButton_Click);
            // 
            // UpdateUsageButton
            // 
            this.UpdateUsageButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpdateUsageButton.Location = new System.Drawing.Point(323, 204);
            this.UpdateUsageButton.Name = "UpdateUsageButton";
            this.UpdateUsageButton.Size = new System.Drawing.Size(146, 57);
            this.UpdateUsageButton.TabIndex = 1;
            this.UpdateUsageButton.Text = "Update Usage";
            this.UpdateUsageButton.UseVisualStyleBackColor = true;
            this.UpdateUsageButton.Click += new System.EventHandler(this.UpdateUsageButton_Click);
            // 
            // SelectedItemLabel
            // 
            this.SelectedItemLabel.AutoSize = true;
            this.SelectedItemLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectedItemLabel.Location = new System.Drawing.Point(324, 13);
            this.SelectedItemLabel.Name = "SelectedItemLabel";
            this.SelectedItemLabel.Size = new System.Drawing.Size(137, 24);
            this.SelectedItemLabel.TabIndex = 3;
            this.SelectedItemLabel.Text = "Selected Item";
            // 
            // ItemNameLabel
            // 
            this.ItemNameLabel.AutoSize = true;
            this.ItemNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemNameLabel.Location = new System.Drawing.Point(328, 41);
            this.ItemNameLabel.Name = "ItemNameLabel";
            this.ItemNameLabel.Size = new System.Drawing.Size(87, 20);
            this.ItemNameLabel.TabIndex = 4;
            this.ItemNameLabel.Text = "Item Name";
            // 
            // UsageLabel
            // 
            this.UsageLabel.AutoSize = true;
            this.UsageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UsageLabel.Location = new System.Drawing.Point(324, 70);
            this.UsageLabel.Name = "UsageLabel";
            this.UsageLabel.Size = new System.Drawing.Size(69, 24);
            this.UsageLabel.TabIndex = 5;
            this.UsageLabel.Text = "Usage";
            // 
            // UsageTextBox
            // 
            this.UsageTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UsageTextBox.Location = new System.Drawing.Point(328, 98);
            this.UsageTextBox.Name = "UsageTextBox";
            this.UsageTextBox.Size = new System.Drawing.Size(133, 26);
            this.UsageTextBox.TabIndex = 0;
            this.UsageTextBox.TextChanged += new System.EventHandler(this.UsageTextBox_TextChanged);
            // 
            // StoreUsageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 311);
            this.Controls.Add(this.UsageTextBox);
            this.Controls.Add(this.UsageLabel);
            this.Controls.Add(this.ItemNameLabel);
            this.Controls.Add(this.SelectedItemLabel);
            this.Controls.Add(this.UpdateUsageButton);
            this.Controls.Add(this.ReturnButton);
            this.Controls.Add(this.UsageListView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Location = new System.Drawing.Point(1, 1);
            this.Name = "StoreUsageForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "StoreUsageForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView UsageListView;
        private System.Windows.Forms.Button ReturnButton;
        private System.Windows.Forms.Button UpdateUsageButton;
        private System.Windows.Forms.Label SelectedItemLabel;
        private System.Windows.Forms.Label ItemNameLabel;
        private System.Windows.Forms.Label UsageLabel;
        private System.Windows.Forms.TextBox UsageTextBox;
    }
}