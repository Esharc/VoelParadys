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
    public partial class NewWishListItem : Form
    {
        int m_iSelectedCustomerID;
        private NewWishListItem()
        {
            InitializeComponent();
        }
        public NewWishListItem(int theSelectedCustomerID)
        {
            InitializeComponent();
            m_iSelectedCustomerID = theSelectedCustomerID;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (ProductNameTextBox.Text != "")
            {
                VoelParadysDataController.GetInstance().AddCustomerWishListItem(m_iSelectedCustomerID, ProductNameTextBox.Text);
                this.Close();
            }
        }
    }
}
