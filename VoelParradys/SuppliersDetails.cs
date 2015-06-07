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
    public partial class SuppliersDetails : Form
    {
        int m_iSelectedCustomerID;
        public SuppliersDetails()
        {
            InitializeComponent();
        }
        public SuppliersDetails(int theSelectedCustomerId)
        {
            InitializeComponent();
            m_iSelectedCustomerID = theSelectedCustomerId;

            string sName = "", sRepName = "", sRepSurname = "", sAddress = "", sPhoneNumber = "";
            VoelParadysDataController.GetInstance().GetSupplierData(m_iSelectedCustomerID, ref sName, ref sRepName, ref sRepSurname, ref sAddress, ref sPhoneNumber);

            HeadingLabel.Text = "Supplied items for " + sName;
            SetUpSuppliedItems();
            AddSuppliedItemsToList();
        }
        // Add the column headers to the customers list
        private void SetUpSuppliedItems()
        {
            // check http://csharp.net-informations.com/gui/cs-listview.htm for a tutorial of using a list view control
            SuppliedItemsListView.View = View.Details;
            SuppliedItemsListView.GridLines = true;
            SuppliedItemsListView.FullRowSelect = true;

            SuppliedItemsListView.Columns.Add("Product Code", 100);
            SuppliedItemsListView.Columns.Add("Quantity", 100);
        }

        // Add supplied items to the list view
        private void AddSuppliedItemsToList()
        {
            // Here we should get all the stock items and add them to the list.
            SuppliedItemsListView.Items.Clear();
            string[] arr = new string[2];
            ListViewItem itm;
            var rDataController = VoelParadysDataController.GetInstance();

            for (int i = 0; i < rDataController.GetSuppliedListCount(m_iSelectedCustomerID); ++i)
            {
                string sItemCode = "";
                int iQuantity = -1;
                rDataController.GetSuppliedListItemAt(m_iSelectedCustomerID, i, ref sItemCode, ref iQuantity);
                arr[0] = sItemCode;
                arr[1] = iQuantity.ToString();
                itm = new ListViewItem(arr);
                SuppliedItemsListView.Items.Add(itm);
            }
        }

        private void ReturnButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
