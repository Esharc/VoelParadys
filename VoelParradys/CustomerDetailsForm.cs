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
    public partial class CustomerDetailsForm : Form
    {
        string m_sSelectedItem;
        int m_iSelectedCustomerID;
        private CustomerDetailsForm()
        {
            InitializeComponent();
            m_sSelectedItem = "";
        }
        public CustomerDetailsForm(int theSelectedCustomerId)
        {
            InitializeComponent();
            m_sSelectedItem = "";
            m_iSelectedCustomerID = theSelectedCustomerId;

            string sName = "", sSurname = "", sAddress = "", sPhoneNumber = "";
            long lIDNumber = -1;
            VoelParadysDataController.GetInstance().GetCustomerData(m_iSelectedCustomerID, ref sName, ref sSurname, ref sAddress, ref sPhoneNumber, ref lIDNumber);

            HeadingLabel.Text = "Wish list items for " + sName;
            SetUpWishList();
        }

        // Add the column headers to the customers list
        private void SetUpWishList()
        {
            // check http://csharp.net-informations.com/gui/cs-listview.htm for a tutorial of using a list view control
            WishListView.View = View.Details;
            WishListView.GridLines = true;
            WishListView.FullRowSelect = true;

            WishListView.Columns.Add("Product Name", 162);
        }

        // Add wish list items to the list view
        private void AddWishListItemsToList()
        {
            // Here we should get all the stock items and add them to the list.
            WishListView.Items.Clear();
            string[] arr = new string[7];
            ListViewItem itm;
            var rDataController = VoelParadysDataController.GetInstance();

            for (int i = 0; i < rDataController.GetCustomerWishListCount(m_iSelectedCustomerID); ++i)
            {
                arr[0] = rDataController.GetCustomerWishListItemAt(m_iSelectedCustomerID, i);
                itm = new ListViewItem(arr);
                WishListView.Items.Add(itm);
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            // Add a wish list item here
            var AddWishListItem = new NewWishListItem(m_iSelectedCustomerID);
            AddWishListItem.Show();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            var rDataController = VoelParadysDataController.GetInstance();
            DialogResult theResult = rDataController.DisplayWarningMessageForDelete(m_sSelectedItem);

            if (theResult == DialogResult.Yes)
            {
                rDataController.DeleteCustomerWishListItem(m_iSelectedCustomerID, m_sSelectedItem);
                this.Refresh();
                m_sSelectedItem = "";
                DeleteButton.Enabled = false;
            }   
        }

        private void ReturnButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public override void Refresh()
        {
            AddWishListItemsToList();
            base.Refresh();
        }

        private void CustomerDetailsForm_Activated(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void OnWishListItemSelected(object sender, EventArgs e)
        {
            m_sSelectedItem = WishListView.SelectedItems[0].SubItems[0].Text;
            DeleteButton.Enabled = true;
        }
    }
}
