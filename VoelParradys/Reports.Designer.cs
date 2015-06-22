namespace VoelParadys
{
    partial class Reports
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
            this.ReturnButton = new System.Windows.Forms.Button();
            this.CashUpRadioButton = new System.Windows.Forms.RadioButton();
            this.UsageRadioButton = new System.Windows.Forms.RadioButton();
            this.ProfitRadioButton = new System.Windows.Forms.RadioButton();
            this.InventoryRadioButton = new System.Windows.Forms.RadioButton();
            this.ExportButton = new System.Windows.Forms.Button();
            this.PrintButton = new System.Windows.Forms.Button();
            this.FromLabel = new System.Windows.Forms.Label();
            this.ToLabel = new System.Windows.Forms.Label();
            this.FromDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.ToDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.CustomersRadioButton = new System.Windows.Forms.RadioButton();
            this.SuppliersRadioButton = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // ReturnButton
            // 
            this.ReturnButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReturnButton.Location = new System.Drawing.Point(264, 216);
            this.ReturnButton.Name = "ReturnButton";
            this.ReturnButton.Size = new System.Drawing.Size(105, 35);
            this.ReturnButton.TabIndex = 0;
            this.ReturnButton.Text = "Return";
            this.ReturnButton.UseVisualStyleBackColor = true;
            this.ReturnButton.Click += new System.EventHandler(this.ReturnButton_Click);
            // 
            // CashUpRadioButton
            // 
            this.CashUpRadioButton.AutoSize = true;
            this.CashUpRadioButton.Checked = true;
            this.CashUpRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CashUpRadioButton.Location = new System.Drawing.Point(13, 13);
            this.CashUpRadioButton.Name = "CashUpRadioButton";
            this.CashUpRadioButton.Size = new System.Drawing.Size(100, 28);
            this.CashUpRadioButton.TabIndex = 1;
            this.CashUpRadioButton.TabStop = true;
            this.CashUpRadioButton.Text = "Cash Up";
            this.CashUpRadioButton.UseVisualStyleBackColor = true;
            this.CashUpRadioButton.CheckedChanged += new System.EventHandler(this.CashUpRadioButton_CheckedChanged);
            // 
            // UsageRadioButton
            // 
            this.UsageRadioButton.AutoSize = true;
            this.UsageRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UsageRadioButton.Location = new System.Drawing.Point(13, 47);
            this.UsageRadioButton.Name = "UsageRadioButton";
            this.UsageRadioButton.Size = new System.Drawing.Size(82, 28);
            this.UsageRadioButton.TabIndex = 2;
            this.UsageRadioButton.Text = "Usage";
            this.UsageRadioButton.UseVisualStyleBackColor = true;
            // 
            // ProfitRadioButton
            // 
            this.ProfitRadioButton.AutoSize = true;
            this.ProfitRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProfitRadioButton.Location = new System.Drawing.Point(13, 81);
            this.ProfitRadioButton.Name = "ProfitRadioButton";
            this.ProfitRadioButton.Size = new System.Drawing.Size(69, 28);
            this.ProfitRadioButton.TabIndex = 4;
            this.ProfitRadioButton.Text = "Profit";
            this.ProfitRadioButton.UseVisualStyleBackColor = true;
            // 
            // InventoryRadioButton
            // 
            this.InventoryRadioButton.AutoSize = true;
            this.InventoryRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InventoryRadioButton.Location = new System.Drawing.Point(13, 115);
            this.InventoryRadioButton.Name = "InventoryRadioButton";
            this.InventoryRadioButton.Size = new System.Drawing.Size(104, 28);
            this.InventoryRadioButton.TabIndex = 10;
            this.InventoryRadioButton.Text = "Inventory";
            this.InventoryRadioButton.UseVisualStyleBackColor = true;
            this.InventoryRadioButton.CheckedChanged += new System.EventHandler(this.InventoryRadioButton_CheckedChanged);
            // 
            // ExportButton
            // 
            this.ExportButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExportButton.Location = new System.Drawing.Point(12, 216);
            this.ExportButton.Name = "ExportButton";
            this.ExportButton.Size = new System.Drawing.Size(156, 35);
            this.ExportButton.TabIndex = 13;
            this.ExportButton.Text = "Export To Excel";
            this.ExportButton.UseVisualStyleBackColor = true;
            this.ExportButton.Click += new System.EventHandler(this.ExportButton_Click);
            // 
            // PrintButton
            // 
            this.PrintButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PrintButton.Location = new System.Drawing.Point(174, 216);
            this.PrintButton.Name = "PrintButton";
            this.PrintButton.Size = new System.Drawing.Size(84, 35);
            this.PrintButton.TabIndex = 14;
            this.PrintButton.Text = "Print";
            this.PrintButton.UseVisualStyleBackColor = true;
            this.PrintButton.Click += new System.EventHandler(this.PrintButton_Click);
            // 
            // FromLabel
            // 
            this.FromLabel.AutoSize = true;
            this.FromLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FromLabel.Location = new System.Drawing.Point(221, 16);
            this.FromLabel.Name = "FromLabel";
            this.FromLabel.Size = new System.Drawing.Size(59, 24);
            this.FromLabel.TabIndex = 15;
            this.FromLabel.Text = "From";
            // 
            // ToLabel
            // 
            this.ToLabel.AutoSize = true;
            this.ToLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToLabel.Location = new System.Drawing.Point(221, 72);
            this.ToLabel.Name = "ToLabel";
            this.ToLabel.Size = new System.Drawing.Size(35, 24);
            this.ToLabel.TabIndex = 16;
            this.ToLabel.Text = "To";
            // 
            // FromDateTimePicker
            // 
            this.FromDateTimePicker.CustomFormat = "dd MMM yyyy";
            this.FromDateTimePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FromDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.FromDateTimePicker.Location = new System.Drawing.Point(225, 43);
            this.FromDateTimePicker.MinDate = new System.DateTime(2015, 1, 1, 0, 0, 0, 0);
            this.FromDateTimePicker.Name = "FromDateTimePicker";
            this.FromDateTimePicker.Size = new System.Drawing.Size(144, 26);
            this.FromDateTimePicker.TabIndex = 17;
            // 
            // ToDateTimePicker
            // 
            this.ToDateTimePicker.CustomFormat = "dd MMM yyyy";
            this.ToDateTimePicker.Enabled = false;
            this.ToDateTimePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ToDateTimePicker.Location = new System.Drawing.Point(225, 96);
            this.ToDateTimePicker.MinDate = new System.DateTime(2015, 1, 1, 0, 0, 0, 0);
            this.ToDateTimePicker.Name = "ToDateTimePicker";
            this.ToDateTimePicker.Size = new System.Drawing.Size(144, 26);
            this.ToDateTimePicker.TabIndex = 18;
            // 
            // CustomersRadioButton
            // 
            this.CustomersRadioButton.AutoSize = true;
            this.CustomersRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CustomersRadioButton.Location = new System.Drawing.Point(13, 149);
            this.CustomersRadioButton.Name = "CustomersRadioButton";
            this.CustomersRadioButton.Size = new System.Drawing.Size(118, 28);
            this.CustomersRadioButton.TabIndex = 19;
            this.CustomersRadioButton.Text = "Customers";
            this.CustomersRadioButton.UseVisualStyleBackColor = true;
            this.CustomersRadioButton.CheckedChanged += new System.EventHandler(this.CustomersRadioButton_CheckedChanged);
            // 
            // SuppliersRadioButton
            // 
            this.SuppliersRadioButton.AutoSize = true;
            this.SuppliersRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SuppliersRadioButton.Location = new System.Drawing.Point(13, 182);
            this.SuppliersRadioButton.Name = "SuppliersRadioButton";
            this.SuppliersRadioButton.Size = new System.Drawing.Size(107, 28);
            this.SuppliersRadioButton.TabIndex = 20;
            this.SuppliersRadioButton.Text = "Suppliers";
            this.SuppliersRadioButton.UseVisualStyleBackColor = true;
            this.SuppliersRadioButton.CheckedChanged += new System.EventHandler(this.SuppliersRadioButton_CheckedChanged);
            // 
            // Reports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 264);
            this.Controls.Add(this.SuppliersRadioButton);
            this.Controls.Add(this.CustomersRadioButton);
            this.Controls.Add(this.ToDateTimePicker);
            this.Controls.Add(this.FromDateTimePicker);
            this.Controls.Add(this.ToLabel);
            this.Controls.Add(this.FromLabel);
            this.Controls.Add(this.PrintButton);
            this.Controls.Add(this.ExportButton);
            this.Controls.Add(this.InventoryRadioButton);
            this.Controls.Add(this.ProfitRadioButton);
            this.Controls.Add(this.UsageRadioButton);
            this.Controls.Add(this.CashUpRadioButton);
            this.Controls.Add(this.ReturnButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Reports";
            this.Text = "Reports";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ReturnButton;
        private System.Windows.Forms.RadioButton CashUpRadioButton;
        private System.Windows.Forms.RadioButton UsageRadioButton;
        private System.Windows.Forms.RadioButton ProfitRadioButton;
        private System.Windows.Forms.RadioButton InventoryRadioButton;
        private System.Windows.Forms.Button ExportButton;
        private System.Windows.Forms.Button PrintButton;
        private System.Windows.Forms.Label FromLabel;
        private System.Windows.Forms.Label ToLabel;
        private System.Windows.Forms.DateTimePicker FromDateTimePicker;
        private System.Windows.Forms.DateTimePicker ToDateTimePicker;
        private System.Windows.Forms.RadioButton CustomersRadioButton;
        private System.Windows.Forms.RadioButton SuppliersRadioButton;
    }
}