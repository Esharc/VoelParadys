namespace VoelParadys
{
    partial class Inventory
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
            this.InventoryReturn = new System.Windows.Forms.Button();
            this.InventoryListView = new System.Windows.Forms.ListView();
            this.AddNewStockItem = new System.Windows.Forms.Button();
            this.QuantBoughtLabel = new System.Windows.Forms.Label();
            this.BuyPriceLabel = new System.Windows.Forms.Label();
            this.SellPriceLabel = new System.Windows.Forms.Label();
            this.QuantityTextBox = new System.Windows.Forms.TextBox();
            this.BuyPriceTextBox = new System.Windows.Forms.TextBox();
            this.SellPriceTextBox = new System.Windows.Forms.TextBox();
            this.SelectedItemLabel = new System.Windows.Forms.Label();
            this.UpdateItemButton = new System.Windows.Forms.Button();
            this.ItemNameLabel = new System.Windows.Forms.Label();
            this.DeleteItemButton = new System.Windows.Forms.Button();
            this.SupplierComboBox = new System.Windows.Forms.ComboBox();
            this.SupplierLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // InventoryReturn
            // 
            this.InventoryReturn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InventoryReturn.Location = new System.Drawing.Point(659, 666);
            this.InventoryReturn.Name = "InventoryReturn";
            this.InventoryReturn.Size = new System.Drawing.Size(146, 35);
            this.InventoryReturn.TabIndex = 6;
            this.InventoryReturn.Text = "Return";
            this.InventoryReturn.UseVisualStyleBackColor = true;
            this.InventoryReturn.Click += new System.EventHandler(this.InventoryReturn_Click);
            // 
            // InventoryListView
            // 
            this.InventoryListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InventoryListView.Location = new System.Drawing.Point(12, 12);
            this.InventoryListView.MultiSelect = false;
            this.InventoryListView.Name = "InventoryListView";
            this.InventoryListView.Size = new System.Drawing.Size(636, 689);
            this.InventoryListView.TabIndex = 1;
            this.InventoryListView.UseCompatibleStateImageBehavior = false;
            this.InventoryListView.Click += new System.EventHandler(this.InventoryOnSelected);
            // 
            // AddNewStockItem
            // 
            this.AddNewStockItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddNewStockItem.Location = new System.Drawing.Point(659, 540);
            this.AddNewStockItem.Name = "AddNewStockItem";
            this.AddNewStockItem.Size = new System.Drawing.Size(146, 57);
            this.AddNewStockItem.TabIndex = 4;
            this.AddNewStockItem.Text = "Add";
            this.AddNewStockItem.UseVisualStyleBackColor = true;
            this.AddNewStockItem.Click += new System.EventHandler(this.AddNewStockItem_Click);
            // 
            // QuantBoughtLabel
            // 
            this.QuantBoughtLabel.AutoSize = true;
            this.QuantBoughtLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuantBoughtLabel.Location = new System.Drawing.Point(654, 87);
            this.QuantBoughtLabel.Name = "QuantBoughtLabel";
            this.QuantBoughtLabel.Size = new System.Drawing.Size(158, 24);
            this.QuantBoughtLabel.TabIndex = 3;
            this.QuantBoughtLabel.Text = "Quantity Bought";
            // 
            // BuyPriceLabel
            // 
            this.BuyPriceLabel.AutoSize = true;
            this.BuyPriceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BuyPriceLabel.Location = new System.Drawing.Point(654, 154);
            this.BuyPriceLabel.Name = "BuyPriceLabel";
            this.BuyPriceLabel.Size = new System.Drawing.Size(99, 24);
            this.BuyPriceLabel.TabIndex = 4;
            this.BuyPriceLabel.Text = "Buy Price";
            // 
            // SellPriceLabel
            // 
            this.SellPriceLabel.AutoSize = true;
            this.SellPriceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SellPriceLabel.Location = new System.Drawing.Point(654, 220);
            this.SellPriceLabel.Name = "SellPriceLabel";
            this.SellPriceLabel.Size = new System.Drawing.Size(99, 24);
            this.SellPriceLabel.TabIndex = 5;
            this.SellPriceLabel.Text = "Sell Price";
            // 
            // QuantityTextBox
            // 
            this.QuantityTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuantityTextBox.Location = new System.Drawing.Point(658, 114);
            this.QuantityTextBox.Name = "QuantityTextBox";
            this.QuantityTextBox.Size = new System.Drawing.Size(146, 26);
            this.QuantityTextBox.TabIndex = 0;
            this.QuantityTextBox.TextChanged += new System.EventHandler(this.QuantityTextBox_TextChanged);
            // 
            // BuyPriceTextBox
            // 
            this.BuyPriceTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BuyPriceTextBox.Location = new System.Drawing.Point(658, 181);
            this.BuyPriceTextBox.Name = "BuyPriceTextBox";
            this.BuyPriceTextBox.Size = new System.Drawing.Size(146, 26);
            this.BuyPriceTextBox.TabIndex = 1;
            this.BuyPriceTextBox.TextChanged += new System.EventHandler(this.BuyPriceTextBox_TextChanged);
            // 
            // SellPriceTextBox
            // 
            this.SellPriceTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SellPriceTextBox.Location = new System.Drawing.Point(658, 247);
            this.SellPriceTextBox.Name = "SellPriceTextBox";
            this.SellPriceTextBox.Size = new System.Drawing.Size(146, 26);
            this.SellPriceTextBox.TabIndex = 2;
            this.SellPriceTextBox.TextChanged += new System.EventHandler(this.SellPriceTextBox_TextChanged);
            // 
            // SelectedItemLabel
            // 
            this.SelectedItemLabel.AutoSize = true;
            this.SelectedItemLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectedItemLabel.Location = new System.Drawing.Point(654, 13);
            this.SelectedItemLabel.Name = "SelectedItemLabel";
            this.SelectedItemLabel.Size = new System.Drawing.Size(92, 24);
            this.SelectedItemLabel.TabIndex = 9;
            this.SelectedItemLabel.Text = "Selected";
            // 
            // UpdateItemButton
            // 
            this.UpdateItemButton.Enabled = false;
            this.UpdateItemButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpdateItemButton.Location = new System.Drawing.Point(659, 477);
            this.UpdateItemButton.Name = "UpdateItemButton";
            this.UpdateItemButton.Size = new System.Drawing.Size(146, 57);
            this.UpdateItemButton.TabIndex = 3;
            this.UpdateItemButton.Text = "Update";
            this.UpdateItemButton.UseVisualStyleBackColor = true;
            this.UpdateItemButton.Click += new System.EventHandler(this.UpdateItemButton_Click);
            // 
            // ItemNameLabel
            // 
            this.ItemNameLabel.AutoSize = true;
            this.ItemNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemNameLabel.Location = new System.Drawing.Point(655, 46);
            this.ItemNameLabel.Name = "ItemNameLabel";
            this.ItemNameLabel.Size = new System.Drawing.Size(87, 20);
            this.ItemNameLabel.TabIndex = 11;
            this.ItemNameLabel.Text = "Item Name";
            // 
            // DeleteItemButton
            // 
            this.DeleteItemButton.Enabled = false;
            this.DeleteItemButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeleteItemButton.Location = new System.Drawing.Point(659, 603);
            this.DeleteItemButton.Name = "DeleteItemButton";
            this.DeleteItemButton.Size = new System.Drawing.Size(146, 57);
            this.DeleteItemButton.TabIndex = 5;
            this.DeleteItemButton.Text = "Delete";
            this.DeleteItemButton.UseVisualStyleBackColor = true;
            this.DeleteItemButton.Click += new System.EventHandler(this.DeleteItemButton_Click);
            // 
            // SupplierComboBox
            // 
            this.SupplierComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SupplierComboBox.FormattingEnabled = true;
            this.SupplierComboBox.Location = new System.Drawing.Point(658, 303);
            this.SupplierComboBox.Name = "SupplierComboBox";
            this.SupplierComboBox.Size = new System.Drawing.Size(145, 28);
            this.SupplierComboBox.TabIndex = 12;
            this.SupplierComboBox.SelectedIndexChanged += new System.EventHandler(this.SupplierComboBox_SelectedIndexChanged);
            // 
            // SupplierLabel
            // 
            this.SupplierLabel.AutoSize = true;
            this.SupplierLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SupplierLabel.Location = new System.Drawing.Point(655, 276);
            this.SupplierLabel.Name = "SupplierLabel";
            this.SupplierLabel.Size = new System.Drawing.Size(88, 24);
            this.SupplierLabel.TabIndex = 13;
            this.SupplierLabel.Text = "Supplier";
            // 
            // Inventory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(818, 715);
            this.Controls.Add(this.SupplierLabel);
            this.Controls.Add(this.SupplierComboBox);
            this.Controls.Add(this.DeleteItemButton);
            this.Controls.Add(this.ItemNameLabel);
            this.Controls.Add(this.UpdateItemButton);
            this.Controls.Add(this.SelectedItemLabel);
            this.Controls.Add(this.SellPriceTextBox);
            this.Controls.Add(this.BuyPriceTextBox);
            this.Controls.Add(this.QuantityTextBox);
            this.Controls.Add(this.SellPriceLabel);
            this.Controls.Add(this.BuyPriceLabel);
            this.Controls.Add(this.QuantBoughtLabel);
            this.Controls.Add(this.AddNewStockItem);
            this.Controls.Add(this.InventoryListView);
            this.Controls.Add(this.InventoryReturn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Inventory";
            this.Text = "Inventory";
            this.Activated += new System.EventHandler(this.Inventory_Activated);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button InventoryReturn;
        private System.Windows.Forms.ListView InventoryListView;
        private System.Windows.Forms.Button AddNewStockItem;
        private System.Windows.Forms.Label QuantBoughtLabel;
        private System.Windows.Forms.Label BuyPriceLabel;
        private System.Windows.Forms.Label SellPriceLabel;
        private System.Windows.Forms.TextBox QuantityTextBox;
        private System.Windows.Forms.TextBox BuyPriceTextBox;
        private System.Windows.Forms.TextBox SellPriceTextBox;
        private System.Windows.Forms.Label SelectedItemLabel;
        private System.Windows.Forms.Button UpdateItemButton;
        private System.Windows.Forms.Label ItemNameLabel;
        private System.Windows.Forms.Button DeleteItemButton;
        private System.Windows.Forms.ComboBox SupplierComboBox;
        private System.Windows.Forms.Label SupplierLabel;
    }
}