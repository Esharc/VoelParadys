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
    public partial class StoreUsageForm : Form
    {
        private string m_sSelectedStockItemCode;
        private int m_iAmountToBeUsed;

        public StoreUsageForm()
        {
            InitializeComponent();
            m_sSelectedStockItemCode = "";
            m_iAmountToBeUsed = -1;
            SetUpUsageList();
            AddItemsToList();
        }

        private void SetUpUsageList()
        {
            // check http://csharp.net-informations.com/gui/cs-listview.htm for a tutorial of using a list view control
            UsageListView.View = View.Details;
            UsageListView.GridLines = true;
            UsageListView.FullRowSelect = true;

            UsageListView.Columns.Add("Code", 80);
            UsageListView.Columns.Add("Description", 140);
            UsageListView.Columns.Add("Qty Used", 80);
        }

        private void AddItemsToList()
        {
            // Here we should get all the stock items and add them to the list.
            UsageListView.Items.Clear();
            string[] arr = new string[3];
            ListViewItem itm;
            var rDataController = VoelParadysDataController.GetInstance();
            string sCode = "",  sName = "";
            int iQuantitySold = -1, iQuantityBought = -1, iQuantityUsed = -1;
            float fCostPrice = -1, fSellPrice = -1;

            for (int i = 0; i < rDataController.GetInventoryListSize(); ++i)
            {
                rDataController.GetStockItemData(i, ref sCode, ref sName, ref iQuantitySold, ref iQuantityBought, ref iQuantityUsed, ref fCostPrice, ref fSellPrice);
                
                arr[0] = sCode;
                arr[1] = sName;
                arr[2] = iQuantityUsed.ToString();
                itm = new ListViewItem(arr);
                UsageListView.Items.Add(itm);
            }
        }

        private void ReturnButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UsageTextBox_TextChanged(object sender, EventArgs e)
        {
            int iTempInput;

            if (UsageTextBox.Text.ToString() != "")
            {
                if (int.TryParse(UsageTextBox.Text.ToString(), out iTempInput))
                    m_iAmountToBeUsed = iTempInput;
                else
                {
                    string sMessage = "Only number values are accepted";
                    string sCaption = "Invalid Input";
                    DialogResult dialogResult;

                    dialogResult = MessageBox.Show(sMessage, sCaption);

                    if (dialogResult == System.Windows.Forms.DialogResult.OK)
                        UsageTextBox.Clear();
                }
            }
        }

        private void UpdateUsageButton_Click(object sender, EventArgs e)
        {
            if (m_iAmountToBeUsed != -1 && m_sSelectedStockItemCode != "")
            {
                var rDataController = VoelParadysDataController.GetInstance();
                string sName = "";
                int iQuantitySold = -1, iQuantityBought = -1, iQuantityUsed = -1;
                float fCostPrice = -1, fSellPrice = -1;

                rDataController.GetStockItemData(m_sSelectedStockItemCode, ref sName, ref iQuantitySold, ref iQuantityBought, ref iQuantityUsed, ref fCostPrice, ref fSellPrice);
                iQuantityUsed += m_iAmountToBeUsed;
                rDataController.UpdateInventoryItem(m_sSelectedStockItemCode, sName, iQuantitySold, iQuantityBought, iQuantityUsed, fCostPrice, fSellPrice);
                m_iAmountToBeUsed = -1;
                m_sSelectedStockItemCode = "";
                UsageTextBox.Clear();
                ItemNameLabel.Text = "Item Name";
                this.Refresh();
            }
        }

        public override void Refresh()
        {
            AddItemsToList();
            base.Refresh();
        }

        private void OnItemSelected(object sender, EventArgs e)
        {
            m_sSelectedStockItemCode = UsageListView.SelectedItems[0].SubItems[0].Text;
            string sName = "";
            int iQuantitySold = -1, iQuantityBought = -1, iQuantityUsed = -1;
            float fCostPrice = -1, fSellPrice = -1;

            VoelParadysDataController.GetInstance().GetStockItemData(m_sSelectedStockItemCode, ref sName, ref iQuantitySold, ref iQuantityBought, ref iQuantityUsed, ref fCostPrice, ref fSellPrice);
            ItemNameLabel.Text = sName;
        }
    }
}
