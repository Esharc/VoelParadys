namespace VoelParadys
{
    partial class PointOfSale
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
            this.Title = new System.Windows.Forms.Label();
            this.ItemCodeBox = new System.Windows.Forms.TextBox();
            this.ItemCode = new System.Windows.Forms.Label();
            this.ItemName = new System.Windows.Forms.Label();
            this.ItemNameBox = new System.Windows.Forms.TextBox();
            this.QuantityLabel = new System.Windows.Forms.Label();
            this.QuantityBox = new System.Windows.Forms.TextBox();
            this.CashReceived = new System.Windows.Forms.Label();
            this.CashReceivedBox = new System.Windows.Forms.TextBox();
            this.PaymentTypeLabel = new System.Windows.Forms.Label();
            this.cashRadioButton = new System.Windows.Forms.RadioButton();
            this.EftRadioButton = new System.Windows.Forms.RadioButton();
            this.BankTransferRadioButton = new System.Windows.Forms.RadioButton();
            this.MenuLabel = new System.Windows.Forms.Label();
            this.PrintButton = new System.Windows.Forms.Button();
            this.ClearButton = new System.Windows.Forms.Button();
            this.CancelSaleButton = new System.Windows.Forms.Button();
            this.InvoiceListView = new System.Windows.Forms.ListView();
            this.Inventory = new System.Windows.Forms.Button();
            this.StoreUsageButton = new System.Windows.Forms.Button();
            this.CustomersButton = new System.Windows.Forms.Button();
            this.ChangePasswordButton = new System.Windows.Forms.Button();
            this.SuppliersButton = new System.Windows.Forms.Button();
            this.OrdersButton = new System.Windows.Forms.Button();
            this.ReportsButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Title.Location = new System.Drawing.Point(177, 8);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(253, 42);
            this.Title.TabIndex = 0;
            this.Title.Text = "Voël Paradys";
            // 
            // ItemCodeBox
            // 
            this.ItemCodeBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemCodeBox.Location = new System.Drawing.Point(434, 124);
            this.ItemCodeBox.Name = "ItemCodeBox";
            this.ItemCodeBox.Size = new System.Drawing.Size(178, 26);
            this.ItemCodeBox.TabIndex = 0;
            this.ItemCodeBox.TextChanged += new System.EventHandler(this.ItemCodeBox_TextChanged);
            this.ItemCodeBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnQuantityEnter);
            // 
            // ItemCode
            // 
            this.ItemCode.AutoSize = true;
            this.ItemCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemCode.Location = new System.Drawing.Point(434, 101);
            this.ItemCode.Name = "ItemCode";
            this.ItemCode.Size = new System.Drawing.Size(92, 20);
            this.ItemCode.TabIndex = 3;
            this.ItemCode.Text = "Item Code";
            // 
            // ItemName
            // 
            this.ItemName.AutoSize = true;
            this.ItemName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemName.Location = new System.Drawing.Point(434, 163);
            this.ItemName.Name = "ItemName";
            this.ItemName.Size = new System.Drawing.Size(96, 20);
            this.ItemName.TabIndex = 4;
            this.ItemName.Text = "Item Name";
            // 
            // ItemNameBox
            // 
            this.ItemNameBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemNameBox.Location = new System.Drawing.Point(434, 186);
            this.ItemNameBox.Name = "ItemNameBox";
            this.ItemNameBox.Size = new System.Drawing.Size(178, 26);
            this.ItemNameBox.TabIndex = 1;
            this.ItemNameBox.TextChanged += new System.EventHandler(this.ItemNameBox_TextChanged);
            this.ItemNameBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnQuantityEnter);
            // 
            // QuantityLabel
            // 
            this.QuantityLabel.AutoSize = true;
            this.QuantityLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuantityLabel.Location = new System.Drawing.Point(438, 219);
            this.QuantityLabel.Name = "QuantityLabel";
            this.QuantityLabel.Size = new System.Drawing.Size(76, 20);
            this.QuantityLabel.TabIndex = 23;
            this.QuantityLabel.Text = "Quantity";
            // 
            // QuantityBox
            // 
            this.QuantityBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuantityBox.Location = new System.Drawing.Point(434, 238);
            this.QuantityBox.Name = "QuantityBox";
            this.QuantityBox.Size = new System.Drawing.Size(178, 26);
            this.QuantityBox.TabIndex = 2;
            this.QuantityBox.Text = "1";
            this.QuantityBox.TextChanged += new System.EventHandler(this.QuantityBox_TextChanged);
            this.QuantityBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnQuantityEnter);
            // 
            // CashReceived
            // 
            this.CashReceived.AutoSize = true;
            this.CashReceived.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CashReceived.Location = new System.Drawing.Point(434, 269);
            this.CashReceived.Name = "CashReceived";
            this.CashReceived.Size = new System.Drawing.Size(129, 20);
            this.CashReceived.TabIndex = 19;
            this.CashReceived.Text = "Cash Received";
            // 
            // CashReceivedBox
            // 
            this.CashReceivedBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CashReceivedBox.Location = new System.Drawing.Point(434, 292);
            this.CashReceivedBox.Name = "CashReceivedBox";
            this.CashReceivedBox.Size = new System.Drawing.Size(178, 26);
            this.CashReceivedBox.TabIndex = 3;
            this.CashReceivedBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnCashEntered);
            // 
            // PaymentTypeLabel
            // 
            this.PaymentTypeLabel.AutoSize = true;
            this.PaymentTypeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PaymentTypeLabel.Location = new System.Drawing.Point(434, 330);
            this.PaymentTypeLabel.Name = "PaymentTypeLabel";
            this.PaymentTypeLabel.Size = new System.Drawing.Size(121, 20);
            this.PaymentTypeLabel.TabIndex = 27;
            this.PaymentTypeLabel.Text = "Payment Type";
            // 
            // cashRadioButton
            // 
            this.cashRadioButton.AutoSize = true;
            this.cashRadioButton.Checked = true;
            this.cashRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cashRadioButton.Location = new System.Drawing.Point(442, 353);
            this.cashRadioButton.Name = "cashRadioButton";
            this.cashRadioButton.Size = new System.Drawing.Size(57, 20);
            this.cashRadioButton.TabIndex = 4;
            this.cashRadioButton.TabStop = true;
            this.cashRadioButton.Text = "Cash";
            this.cashRadioButton.UseVisualStyleBackColor = true;
            // 
            // EftRadioButton
            // 
            this.EftRadioButton.AutoSize = true;
            this.EftRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EftRadioButton.Location = new System.Drawing.Point(498, 353);
            this.EftRadioButton.Name = "EftRadioButton";
            this.EftRadioButton.Size = new System.Drawing.Size(52, 20);
            this.EftRadioButton.TabIndex = 5;
            this.EftRadioButton.Text = "EFT";
            this.EftRadioButton.UseVisualStyleBackColor = true;
            // 
            // BankTransferRadioButton
            // 
            this.BankTransferRadioButton.AutoSize = true;
            this.BankTransferRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BankTransferRadioButton.Location = new System.Drawing.Point(442, 379);
            this.BankTransferRadioButton.Name = "BankTransferRadioButton";
            this.BankTransferRadioButton.Size = new System.Drawing.Size(110, 20);
            this.BankTransferRadioButton.TabIndex = 6;
            this.BankTransferRadioButton.Text = "Bank Transfer";
            this.BankTransferRadioButton.UseVisualStyleBackColor = true;
            // 
            // MenuLabel
            // 
            this.MenuLabel.AutoSize = true;
            this.MenuLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MenuLabel.Location = new System.Drawing.Point(255, 51);
            this.MenuLabel.Name = "MenuLabel";
            this.MenuLabel.Size = new System.Drawing.Size(87, 37);
            this.MenuLabel.TabIndex = 6;
            this.MenuLabel.Text = "POS";
            // 
            // PrintButton
            // 
            this.PrintButton.BackColor = System.Drawing.Color.Lime;
            this.PrintButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PrintButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PrintButton.Location = new System.Drawing.Point(434, 435);
            this.PrintButton.Name = "PrintButton";
            this.PrintButton.Size = new System.Drawing.Size(178, 61);
            this.PrintButton.TabIndex = 7;
            this.PrintButton.Text = "Print Receipt";
            this.PrintButton.UseVisualStyleBackColor = false;
            this.PrintButton.Click += new System.EventHandler(this.PrintButton_Click);
            // 
            // ClearButton
            // 
            this.ClearButton.BackColor = System.Drawing.Color.Red;
            this.ClearButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ClearButton.Enabled = false;
            this.ClearButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClearButton.Location = new System.Drawing.Point(434, 502);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(178, 61);
            this.ClearButton.TabIndex = 8;
            this.ClearButton.Text = "Clear Item";
            this.ClearButton.UseVisualStyleBackColor = false;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // CancelSaleButton
            // 
            this.CancelSaleButton.BackColor = System.Drawing.Color.Red;
            this.CancelSaleButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CancelSaleButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelSaleButton.Location = new System.Drawing.Point(434, 569);
            this.CancelSaleButton.Name = "CancelSaleButton";
            this.CancelSaleButton.Size = new System.Drawing.Size(178, 61);
            this.CancelSaleButton.TabIndex = 9;
            this.CancelSaleButton.Text = "Cancel Sale";
            this.CancelSaleButton.UseVisualStyleBackColor = false;
            this.CancelSaleButton.Click += new System.EventHandler(this.CancelSaleButton_Click);
            // 
            // InvoiceListView
            // 
            this.InvoiceListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InvoiceListView.Location = new System.Drawing.Point(13, 101);
            this.InvoiceListView.MultiSelect = false;
            this.InvoiceListView.Name = "InvoiceListView";
            this.InvoiceListView.Size = new System.Drawing.Size(417, 529);
            this.InvoiceListView.TabIndex = 25;
            this.InvoiceListView.UseCompatibleStateImageBehavior = false;
            this.InvoiceListView.Click += new System.EventHandler(this.OnItemSelected);
            // 
            // Inventory
            // 
            this.Inventory.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Inventory.Location = new System.Drawing.Point(13, 642);
            this.Inventory.Name = "Inventory";
            this.Inventory.Size = new System.Drawing.Size(99, 38);
            this.Inventory.TabIndex = 10;
            this.Inventory.Text = "Inventory";
            this.Inventory.UseVisualStyleBackColor = true;
            this.Inventory.Click += new System.EventHandler(this.Inventory_Click);
            // 
            // StoreUsageButton
            // 
            this.StoreUsageButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StoreUsageButton.Location = new System.Drawing.Point(118, 642);
            this.StoreUsageButton.Name = "StoreUsageButton";
            this.StoreUsageButton.Size = new System.Drawing.Size(123, 38);
            this.StoreUsageButton.TabIndex = 11;
            this.StoreUsageButton.Text = "Store Usage";
            this.StoreUsageButton.UseVisualStyleBackColor = true;
            this.StoreUsageButton.Click += new System.EventHandler(this.StoreUsageButton_Click);
            // 
            // CustomersButton
            // 
            this.CustomersButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CustomersButton.Location = new System.Drawing.Point(247, 642);
            this.CustomersButton.Name = "CustomersButton";
            this.CustomersButton.Size = new System.Drawing.Size(123, 38);
            this.CustomersButton.TabIndex = 28;
            this.CustomersButton.Text = "Customers";
            this.CustomersButton.UseVisualStyleBackColor = true;
            this.CustomersButton.Click += new System.EventHandler(this.CustomersButton_Click);
            // 
            // ChangePasswordButton
            // 
            this.ChangePasswordButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChangePasswordButton.Location = new System.Drawing.Point(247, 686);
            this.ChangePasswordButton.Name = "ChangePasswordButton";
            this.ChangePasswordButton.Size = new System.Drawing.Size(365, 38);
            this.ChangePasswordButton.TabIndex = 29;
            this.ChangePasswordButton.Text = "Change Super User Password";
            this.ChangePasswordButton.UseVisualStyleBackColor = true;
            this.ChangePasswordButton.Click += new System.EventHandler(this.ChangePasswordButton_Click);
            // 
            // SuppliersButton
            // 
            this.SuppliersButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SuppliersButton.Location = new System.Drawing.Point(376, 642);
            this.SuppliersButton.Name = "SuppliersButton";
            this.SuppliersButton.Size = new System.Drawing.Size(123, 38);
            this.SuppliersButton.TabIndex = 30;
            this.SuppliersButton.Text = "Suppliers";
            this.SuppliersButton.UseVisualStyleBackColor = true;
            this.SuppliersButton.Click += new System.EventHandler(this.SuppliersButton_Click);
            // 
            // OrdersButton
            // 
            this.OrdersButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OrdersButton.Location = new System.Drawing.Point(505, 642);
            this.OrdersButton.Name = "OrdersButton";
            this.OrdersButton.Size = new System.Drawing.Size(107, 38);
            this.OrdersButton.TabIndex = 32;
            this.OrdersButton.Text = "Orders";
            this.OrdersButton.UseVisualStyleBackColor = true;
            this.OrdersButton.Click += new System.EventHandler(this.OrdersButton_Click);
            // 
            // ReportsButton
            // 
            this.ReportsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReportsButton.Location = new System.Drawing.Point(13, 686);
            this.ReportsButton.Name = "ReportsButton";
            this.ReportsButton.Size = new System.Drawing.Size(228, 38);
            this.ReportsButton.TabIndex = 33;
            this.ReportsButton.Text = "Reports";
            this.ReportsButton.UseVisualStyleBackColor = true;
            // 
            // PointOfSale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 736);
            this.Controls.Add(this.ReportsButton);
            this.Controls.Add(this.OrdersButton);
            this.Controls.Add(this.SuppliersButton);
            this.Controls.Add(this.ChangePasswordButton);
            this.Controls.Add(this.CustomersButton);
            this.Controls.Add(this.StoreUsageButton);
            this.Controls.Add(this.BankTransferRadioButton);
            this.Controls.Add(this.EftRadioButton);
            this.Controls.Add(this.cashRadioButton);
            this.Controls.Add(this.PaymentTypeLabel);
            this.Controls.Add(this.Inventory);
            this.Controls.Add(this.InvoiceListView);
            this.Controls.Add(this.QuantityBox);
            this.Controls.Add(this.QuantityLabel);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.CashReceivedBox);
            this.Controls.Add(this.CashReceived);
            this.Controls.Add(this.PrintButton);
            this.Controls.Add(this.CancelSaleButton);
            this.Controls.Add(this.MenuLabel);
            this.Controls.Add(this.ItemNameBox);
            this.Controls.Add(this.ItemName);
            this.Controls.Add(this.ItemCode);
            this.Controls.Add(this.ItemCodeBox);
            this.Controls.Add(this.Title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "PointOfSale";
            this.Text = "Point Of Sale";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.TextBox ItemCodeBox;
        private System.Windows.Forms.Label ItemCode;
        private System.Windows.Forms.Label ItemName;
        private System.Windows.Forms.TextBox ItemNameBox;
        private System.Windows.Forms.Label MenuLabel;
        private System.Windows.Forms.Button CancelSaleButton;
        private System.Windows.Forms.Button PrintButton;
        private System.Windows.Forms.Label CashReceived;
        private System.Windows.Forms.TextBox CashReceivedBox;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.Label QuantityLabel;
        private System.Windows.Forms.TextBox QuantityBox;
        private System.Windows.Forms.ListView InvoiceListView;
        private System.Windows.Forms.Button Inventory;
        private System.Windows.Forms.Label PaymentTypeLabel;
        private System.Windows.Forms.RadioButton cashRadioButton;
        private System.Windows.Forms.RadioButton EftRadioButton;
        private System.Windows.Forms.RadioButton BankTransferRadioButton;
        private System.Windows.Forms.Button StoreUsageButton;
        private System.Windows.Forms.Button CustomersButton;
        private System.Windows.Forms.Button ChangePasswordButton;
        private System.Windows.Forms.Button SuppliersButton;
        private System.Windows.Forms.Button OrdersButton;
        private System.Windows.Forms.Button ReportsButton;
    }
}

