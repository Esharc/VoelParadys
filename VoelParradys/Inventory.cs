using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VoelParadys
{
    public partial class Inventory : Form
    {
        private string m_sSelectedItemCode;                 // The code of the currently selected item
        // private SStockItemDetails theSelectedItemDetails;   // The details of the currently selected item
        private string m_sTempQuantityBought;               // The temporary quantity bought that was entered by the user
        private string m_sTempBuyPrice;                     // The temporary buy price entered by the user
        private string m_sTempSellPrice;                    // The temporary sell price entered by the user
        private bool m_bNewItemWindowLoaded;                // Has the add new item window been opened?

        public Inventory()
        {
            InitializeComponent();
            m_sSelectedItemCode = "";
            // theSelectedItemDetails = new SStockItemDetails();
            m_sTempQuantityBought = "";
            m_sTempBuyPrice = "";
            m_sTempSellPrice = "";
            m_bNewItemWindowLoaded = false;
            SetUpInventoryList();
            AddItemsToList();
        }

        private void InventoryReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddItemsToList()
        {
            // Here we should get all the stock items and add them to the list.
            InventoryListView.Items.Clear();
            string[] arr = new string[7];
            ListViewItem itm;
            var rDataController = VoelParadysDataController.GetInstance();
            string sCode = "", sName = "";
            int iQuantitySold = -1, iQuantityBought = -1, iQuantityUsed = -1;
            float fCostPrice = -1, fSellPrice = -1;

            for (int i = 0; i < rDataController.GetInventoryListSize(); ++i)
            {
                rDataController.GetStockItemData(i, ref sCode, ref sName, ref iQuantitySold, ref iQuantityBought, ref iQuantityUsed, ref fCostPrice, ref fSellPrice);
                arr[0] = sCode;
                arr[1] = sName;
                arr[2] = iQuantityBought.ToString();
                arr[3] = iQuantitySold.ToString();
                arr[4] = iQuantityUsed.ToString();
                arr[5] = (iQuantityBought - (iQuantitySold + iQuantityUsed)).ToString();
                arr[6] = "R " + (fSellPrice - fCostPrice).ToString("F2");
                itm = new ListViewItem(arr);
                InventoryListView.Items.Add(itm);
            }
        }

        private void SetUpInventoryList()
        {
            // check http://csharp.net-informations.com/gui/cs-listview.htm for a tutorial of using a list view control
            InventoryListView.View = View.Details;
            InventoryListView.GridLines = true;
            InventoryListView.FullRowSelect = true;

            InventoryListView.Columns.Add("Code", 80);
            InventoryListView.Columns.Add("Description", 140);
            InventoryListView.Columns.Add("Qty Bought", 80);
            InventoryListView.Columns.Add("Qty Sold", 80);
            InventoryListView.Columns.Add("Qty Used", 80);
            InventoryListView.Columns.Add("Qty In Stock", 90);
            InventoryListView.Columns.Add("Profit", 80);
        }

        private void InventoryOnSelected(object sender, EventArgs e)
        {
            if (!UpdateItemButton.Enabled)
                UpdateItemButton.Enabled = true;
            if (!DeleteItemButton.Enabled)
                DeleteItemButton.Enabled = true;

            m_sSelectedItemCode = InventoryListView.SelectedItems[0].SubItems[0].Text.ToString();

            string sName = "";
            int iQuantitySold = -1, iQuantityBought = -1, iQuantityUsed = -1;
            float fCostPrice = -1, fSellPrice = -1;

            VoelParadysDataController.GetInstance().GetStockItemData(m_sSelectedItemCode, ref sName, ref iQuantitySold, ref iQuantityBought, ref iQuantityUsed, ref fCostPrice, ref fSellPrice);
            ItemNameLabel.Text = sName;
            m_sTempQuantityBought = "";
            m_sTempBuyPrice = fCostPrice.ToString("F2");
            m_sTempSellPrice = fSellPrice.ToString("F2");

            QuantityTextBox.Text = m_sTempQuantityBought;
            BuyPriceTextBox.Text = m_sTempBuyPrice;
            SellPriceTextBox.Text = m_sTempSellPrice;
        }

        private void AddNewStockItem_Click(object sender, EventArgs e)
        {
            var NewStockItemForm = new NewStockItem();
            NewStockItemForm.Show();
            m_bNewItemWindowLoaded = true;
        }

        private void QuantityTextBox_TextChanged(object sender, EventArgs e)
        {
            int iTempInput = -1;
            if (QuantityTextBox.Text.ToString() != "")
            {
                if (int.TryParse(QuantityTextBox.Text.ToString(), out iTempInput))
                    m_sTempQuantityBought = QuantityTextBox.Text.ToString();
                else
                {
                    string sMessage = "Only number values are accepted";
                    string sCaption = "Invalid Input";
                    DialogResult dialogResult;

                    dialogResult = MessageBox.Show(sMessage, sCaption);

                    if (dialogResult == System.Windows.Forms.DialogResult.OK)
                        QuantityTextBox.Clear();
                }
            }
        }

        private void BuyPriceTextBox_TextChanged(object sender, EventArgs e)
        {
            float fTempInput = -1;
            if (BuyPriceTextBox.Text.ToString() != "")
            {
                if (float.TryParse(BuyPriceTextBox.Text.ToString(), out fTempInput))
                    m_sTempBuyPrice = BuyPriceTextBox.Text.ToString();
                else
                {
                    string sMessage = "Only number values are accepted";
                    string sCaption = "Invalid Input";
                    DialogResult dialogResult;
            
                    dialogResult = MessageBox.Show(sMessage, sCaption);
            
                    if (dialogResult == System.Windows.Forms.DialogResult.OK)
                        BuyPriceTextBox.Clear();
                }
            }
        }

        private void SellPriceTextBox_TextChanged(object sender, EventArgs e)
        {
            float fTempInput = -1;
            if (SellPriceTextBox.Text.ToString() != "")
            {
                if (float.TryParse(SellPriceTextBox.Text.ToString(), out fTempInput))
                    m_sTempSellPrice = SellPriceTextBox.Text.ToString();
                else
                {
                    string sMessage = "Only number values are accepted";
                    string sCaption = "Invalid Input";
                    DialogResult dialogResult;

                    dialogResult = MessageBox.Show(sMessage, sCaption);

                    if (dialogResult == System.Windows.Forms.DialogResult.OK)
                        SellPriceTextBox.Clear();
                }
            }
        }

        private void UpdateItemButton_Click(object sender, EventArgs e)
        {
            var rDataController = VoelParadysDataController.GetInstance();
            if (rDataController.DoesItemExistInDatabase(m_sSelectedItemCode))
            {
                string sName = "";
                int iQuantitySold = -1, iQuantityBought = -1, iQuantityUsed = -1;
                float fCostPrice = -1, fSellPrice = -1;

                rDataController.GetStockItemData(m_sSelectedItemCode, ref sName, ref iQuantitySold, ref iQuantityBought, ref iQuantityUsed, ref fCostPrice, ref fSellPrice);

                int iTempVal = -1;
                bool bChanged = false;
                if (m_sTempQuantityBought != "" && int.TryParse(m_sTempQuantityBought, out iTempVal))
                {
                    iQuantityBought += iTempVal;
                    bChanged = true;
                }
                float fTempValue = -1;
                if (float.TryParse(m_sTempBuyPrice, out fTempValue) && fCostPrice != fTempValue)
                {
                    fCostPrice = fTempValue;
                    bChanged = true;
                }
                if (float.TryParse(m_sTempSellPrice, out fTempValue) && fSellPrice != fTempValue)
                {
                    fSellPrice = fTempValue;
                    bChanged = true;
                }
                if (bChanged)
                {
                    rDataController.UpdateInventoryItem(m_sSelectedItemCode, sName, iQuantitySold, iQuantityBought, iQuantityUsed, fCostPrice, fSellPrice);
                    this.Refresh();
                }
            }
        }

        private void Inventory_Activated(object sender, EventArgs e)
        {
            // We want to refresh once when we have focus again.
            if (m_bNewItemWindowLoaded)
            {
                this.Refresh();
                m_bNewItemWindowLoaded = false;
            }
        }

        private void DeleteItemButton_Click(object sender, EventArgs e)
        {
            var rDataController = VoelParadysDataController.GetInstance();
            string sName = "";
            int iQuantitySold = -1, iQuantityBought = -1, iQuantityUsed = -1;
            float fCostPrice = -1, fSellPrice = -1;

            rDataController.GetStockItemData(m_sSelectedItemCode, ref sName, ref iQuantitySold, ref iQuantityBought, ref iQuantityUsed, ref fCostPrice, ref fSellPrice);

            DialogResult messageBoxResult = rDataController.DisplayWarningMessageForDelete(sName);

            if (messageBoxResult == DialogResult.Yes)
            {
                if (rDataController.DoesItemExistInDatabase(m_sSelectedItemCode))
                {
                    UpdateItemButton.Enabled = false;
                    DeleteItemButton.Enabled = false;
                    rDataController.DeleteInventoryItem(m_sSelectedItemCode);
                    m_sSelectedItemCode = "";
                    BuyPriceTextBox.Clear();
                    SellPriceTextBox.Clear();
                    ItemNameLabel.Text = "Item Name";
                    this.Refresh();
                }
            }
        }

        public override void Refresh()
        {
            AddItemsToList();
            base.Refresh();
        }
    }
}
