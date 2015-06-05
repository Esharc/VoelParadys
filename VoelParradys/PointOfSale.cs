using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Drawing.Drawing2D;
using BrightIdeasSoftware;


namespace VoelParadys
{
    struct SSaleInvoiceData
    {
        string m_sItemCode;                 // The code of the item to be sold
        string m_sItemName;                 // The name of the item to be sold
        int m_iItemQuantity;                // The amount of the item to be sold

        public SSaleInvoiceData(string sItemCode, string sItemName, int iItemQuantity)
        {
            m_sItemCode = sItemCode;
            m_sItemName = sItemName;
            m_iItemQuantity = iItemQuantity;
        }

        // Add the item code to the sale item
        public void AddCode(string sCode)
        {
            m_sItemCode = sCode;
        }
        // Add the item name to the sale item
        public void AddName(string sName)
        {
            m_sItemName = sName;
        }
        // Add the quantity to the sale item
        public void AddQuantity(int iQuantity)
        {
            m_iItemQuantity = iQuantity;
        }
        // Clear the sale item for a new entry
        public void ClearItem()
        {
            m_sItemCode = "";
            m_sItemName = "";
            m_iItemQuantity = -1;
        }
        // Get the sale item code
        public string GetItemCode()
        {
            return m_sItemCode;
        }
        // Get the sale item name
        public string GetItemName()
        {
            return m_sItemName;
        }
        // Get the sale item quantity
        public int GetItemQuantity()
        {
            return m_iItemQuantity;
        }
    }

    public partial class PointOfSale : Form
    {
        private float m_fCashReceived;
        float m_fSubTotal;
        string m_sSelectedItemCode;            // The currently selected item in the list
        private List<SSaleInvoiceData> m_lSaleItems;
        private SSaleInvoiceData m_SItem;

        public PointOfSale()
        {
            // This call will make sure all XML files have been created if they do not exist
            VoelParadysDataController.GetInstance();
            InitializeComponent();
            SetUpInvoiceList();
            m_lSaleItems = new List<SSaleInvoiceData>();
            m_SItem = new SSaleInvoiceData("", "", -1);
            m_fCashReceived = 0;
            m_fSubTotal = 0;
            m_sSelectedItemCode = "";
        }
        // What should happen when the text in the code box changes
        private void ItemCodeBox_TextChanged(object sender, EventArgs e)
        {
            string sInput = ((TextBox)sender).Text;
            m_SItem.AddCode(sInput);
        }
        // What should happen when the text in the name box changes
        private void ItemNameBox_TextChanged(object sender, EventArgs e)
        {
            string sInput = ((TextBox)sender).Text;
            m_SItem.AddName(sInput);
        }
        // What should happen when the text in the quantity box changes
        private void QuantityBox_TextChanged(object sender, EventArgs e)
        {
            string sInput = ((TextBox)sender).Text;
            if (sInput != "")
            {
                int iTempVal = -1;
                if (int.TryParse(sInput, out iTempVal))
                    m_SItem.AddQuantity(iTempVal);
                else
                {
                    string sMessage = "Only number values are accepted";
                    string sCaption = "Invalid Input";
                    DialogResult dialogResult;

                    dialogResult = MessageBox.Show(sMessage, sCaption);

                    if (dialogResult == System.Windows.Forms.DialogResult.OK)
                        QuantityBox.Clear();
                }
            }
        }
        // What should happen if the enter key is pressed in the quantity box
        private void OnQuantityEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                // Here we first determine if the entered data is an actual match for the inventory database.
                // If it is then we can add it to the invoice. If it is not, then we reject the input.
                var rDataController = VoelParadysDataController.GetInstance();
                bool bValidInput = false;
                if (m_SItem.GetItemName() != "")        // A name was entered
                {
                    string sItemCode = "";
                    bValidInput = rDataController.DoesItemExistInDatabase(m_SItem.GetItemName(), ref sItemCode);
                    m_SItem.AddCode(sItemCode);
                }
                else
                    bValidInput = rDataController.DoesItemExistInDatabase(m_SItem.GetItemCode());

                if (bValidInput)
                {
                    m_lSaleItems.Add(m_SItem);
                    m_SItem.ClearItem();
                    ItemCodeBox.Clear();
                    ItemNameBox.Clear();
                    QuantityBox.Clear();
                    UpdateSaleInvoice();
                    ItemCodeBox.Focus();
                }
            }
        }
        // What should happen when enter is pressed on the cash received box
        private void OnCashEntered(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                string sInput = ((TextBox)sender).Text;
                if (sInput != "")
                {
                    float fTempCashReceived = -1;
      
                    if (float.TryParse(sInput, out fTempCashReceived))
                    {
                        m_fCashReceived = fTempCashReceived;
                        CashReceivedBox.Clear();
                        UpdateSaleInvoice();
                    }
                    else
                    {
                        string sMessage = "Only number values are accepted";
                        string sCaption = "Invalid Input";
                        DialogResult dialogResult;

                        dialogResult = MessageBox.Show(sMessage, sCaption);

                        if (dialogResult == System.Windows.Forms.DialogResult.OK)
                            CashReceivedBox.Clear();
                    }
                }
            }
        }
        // Update the sale invoice with the new data
        private void UpdateSaleInvoice()
        {
            InvoiceListView.Items.Clear();
            string[] arr = new string[5];
            m_fSubTotal = 0;
            ListViewItem itm;

            //add items to ListView. This must come from the database once it has been set up

            for (int i = 0; i < m_lSaleItems.Count; ++i )
            {
                string sItemCode = m_lSaleItems[i].GetItemCode(), sName = "";
                int iQuantitySold = -1, iQuantityBought = -1, iQuantityUsed = -1;
                float fCostPrice = -1, fSellPrice = -1;

                VoelParadysDataController.GetInstance().GetStockItemData(sItemCode, ref sName, ref iQuantitySold, ref iQuantityBought, ref iQuantityUsed, ref fCostPrice, ref fSellPrice);

                arr[0] = sItemCode;
                arr[1] = sName;
                arr[2] = "R " + fSellPrice.ToString("F2");
                arr[3] = m_lSaleItems[i].GetItemQuantity().ToString();
                arr[4] = "R " + (m_lSaleItems[i].GetItemQuantity() * fSellPrice).ToString("F2");
                m_fSubTotal += m_lSaleItems[i].GetItemQuantity() * fSellPrice;
                itm = new ListViewItem(arr);
                InvoiceListView.Items.Add(itm);
            }
            AddFooterToInvoice(m_fSubTotal);
        }
        // Add the column headers to the sale invoice list
        private void SetUpInvoiceList()
        {
            // check http://csharp.net-informations.com/gui/cs-listview.htm for a tutorial of using a list view control
            InvoiceListView.View = View.Details;
            InvoiceListView.GridLines = true;
            InvoiceListView.FullRowSelect = true;

            InvoiceListView.Columns.Add("Code", 80);
            InvoiceListView.Columns.Add("Name", 120);
            InvoiceListView.Columns.Add("Price", 70);
            InvoiceListView.Columns.Add("Qty", 55);
            InvoiceListView.Columns.Add("Total", 86);
        }
        // Add the footer to the sale invoice list
        private void AddFooterToInvoice(float fSubTotal)
        {
            string[] arr = new string[5];
            ListViewItem itm;
            float fGrandTotal = fSubTotal; // Without VAT consideration.

            arr[0] = "";
            arr[1] = "";
            arr[2] = "";
            arr[3] = "";
            arr[4] = "";
            itm = new ListViewItem(arr);
            InvoiceListView.Items.Add(itm);

            /* We are not taking VAT into consideration? If at a later stage we do take it into consideration, uncomment the code below
            
            fGrandTotal = fSubTotal + ((fSubTotal * 14 / 100));
            arr[0] = "";
            arr[1] = "Sub Total";
            arr[2] = "";
            arr[3] = "";
            arr[4] = "R " + fSubTotal.ToString("F2");
            itm = new ListViewItem(arr);
            InvoiceListView.Items.Add(itm);
            
            arr[0] = "";
            arr[1] = "Vat @ 14%";
            arr[2] = "";
            arr[3] = "";
            arr[4] = "R " + (fSubTotal * 14 / 100).ToString("F2");
            itm = new ListViewItem(arr);
            InvoiceListView.Items.Add(itm); */

            arr[0] = "";
            arr[1] = "Grand Total";
            arr[2] = "";
            arr[3] = "";
            arr[4] = "R " + fGrandTotal.ToString("F2");
            itm = new ListViewItem(arr);
            InvoiceListView.Items.Add(itm);

            arr[0] = "";
            arr[1] = "Cash Received";
            arr[2] = "";
            arr[3] = "";
            arr[4] = "R " + m_fCashReceived.ToString("F2");
            itm = new ListViewItem(arr);
            InvoiceListView.Items.Add(itm);

            arr[0] = "";
            arr[1] = "Change";
            arr[2] = "";
            arr[3] = "";
            arr[4] = "R " + (m_fCashReceived - fGrandTotal).ToString("F2");
            itm = new ListViewItem(arr);
            InvoiceListView.Items.Add(itm);
        }
        // Open the inventory list when the button is clicked
        private void Inventory_Click(object sender, EventArgs e)
        {
            // We still have to add user verification here. Still need to figure this part out.
            var rDataController = VoelParadysDataController.GetInstance();

            if (rDataController.UsePassword())
            {
                var PassForm = new EnterPasswordForm();
                PassForm.Show();
            }
            else
            {
                var InventoryForm = new Inventory();
                InventoryForm.Show();
            }
        }

        private void PrintButton_Click(object sender, EventArgs e)
        {
            // When this button is pressed, then we must print the receipt. Print both to file and the printer so that a record of all sales can be kept
            // As well as printing the receipt, this action will consider the sale to be final, and the entered quantities can then be removed from the
            // inventory database and saved.

            PrintReceipt();
            string sPaymentType = "";
            if (cashRadioButton.Checked)
                sPaymentType = "Cash";
            else if (EftRadioButton.Checked)
                sPaymentType = "EFT";
            else
                sPaymentType = "Bank Transfer";
            VoelParadysDataController.GetInstance().WriteDailySaleDataToDB(m_lSaleItems, m_fSubTotal, m_fCashReceived, sPaymentType);

            for (int i = 0; i < m_lSaleItems.Count; ++i)
            {
                var rDataController = VoelParadysDataController.GetInstance();
                string sCode = m_lSaleItems[i].GetItemCode(), sName = "";
                int iQuantitySold = -1, iQuantityBought = -1, iQuantityUsed = -1;
                float fCostPrice = -1, fSellPrice = -1;

                rDataController.GetStockItemData(sCode, ref sName, ref iQuantitySold, ref iQuantityBought, ref iQuantityUsed, ref fCostPrice, ref fSellPrice);
                iQuantitySold += m_lSaleItems[i].GetItemQuantity();
                rDataController.UpdateInventoryItem(sCode, sName, iQuantitySold, iQuantityBought, iQuantityUsed, fCostPrice, fSellPrice);
            }
            m_lSaleItems.Clear();
            InvoiceListView.Clear();
            m_fCashReceived = 0;
            m_fSubTotal = 0;
            cashRadioButton.Checked = true;
            SetUpInvoiceList();
        }

        public void PrintReceipt()
        {
            // Reformat the list view so that has a standard column width. It does not matter that it will be going over the page border, we just shrink to fit it,
            // and then we are sure that the form is uniformed.
            for (int i = 0; i < InvoiceListView.Columns.Count; ++i)
                InvoiceListView.Columns[i].Width = 150;

            // Create the ListViewPrinter, and assign the list to it
            ListViewPrinter lListPrinter = new ListViewPrinter();
            lListPrinter.ListView = InvoiceListView;
            // Give the page a heading and set the ShrinkToFit true
            lListPrinter.Header = "\tVoel Paradys Invoice";
            lListPrinter.IsShrinkToFit = true;
            // Remove the standard gradient effects on the header background, and draw a line at the bottom of the header
            lListPrinter.HeaderFormat = BlockFormat.Header();
            lListPrinter.HeaderFormat.TextBrush = Brushes.Black;
            lListPrinter.HeaderFormat.BackgroundBrush = null;
            lListPrinter.HeaderFormat.SetBorderPen(Sides.Bottom, new Pen(Color.Black, 0.5f));
            // Set the grid pens colour and the header border colour
            lListPrinter.ListHeaderFormat = BlockFormat.ListHeader();
            lListPrinter.ListHeaderFormat.BackgroundBrush = null;
            lListPrinter.ListHeaderFormat.SetBorderPen(Sides.All, new Pen(Color.Black, 0.5f));
            lListPrinter.ListGridPen = new Pen(Color.Black);

            lListPrinter.PrintPreview();
            // TODO: Once the product is complete, uncomment this to actually print the page, and comment out the PrintPreview to print directly without first viewing it
            // lListPrinter.PrintWithDialog();
        }

        private void StoreUsageButton_Click(object sender, EventArgs e)
        {
            var UsageForm = new StoreUsageForm();
            UsageForm.Show();
        }

        private void CancelSaleButton_Click(object sender, EventArgs e)
        {
            m_sSelectedItemCode = "";
            m_fSubTotal = 0;
            m_fCashReceived = 0;
            m_lSaleItems.Clear();
            InvoiceListView.Clear();
            cashRadioButton.Checked = true;
            SetUpInvoiceList();
        }

        private void OnItemSelected(object sender, EventArgs e)
        {
            m_sSelectedItemCode = InvoiceListView.SelectedItems[0].SubItems[0].Text;
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            // Get the currently selected item, and remove it from the sale list
            for (int i = 0; i < m_lSaleItems.Count; ++i)
            {
                if (m_sSelectedItemCode == m_lSaleItems[i].GetItemCode())
                {
                    m_lSaleItems.RemoveAt(i);
                    // If there are no more items in the list, cancel the sale, otherwise just update the sale invoice
                    if (m_lSaleItems.Count == 0)
                        CancelSaleButton_Click(sender, e);
                    else
                        UpdateSaleInvoice();
                    break;
                }
            }
        }

        private void CustomersButton_Click(object sender, EventArgs e)
        {
            var CustomersForm = new CustomersForm();
            CustomersForm.Show();
        }

        private void ChangePasswordButton_Click(object sender, EventArgs e)
        {
            var SuperUserPasswordForm = new NewSuperUserPasswordForm();
            SuperUserPasswordForm.Show();
        }

        private void SuppliersButton_Click(object sender, EventArgs e)
        {
            var SuppliersForm = new SuppliersForm();
            SuppliersForm.Show();
        }
    }
}
