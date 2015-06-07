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
    public partial class Orders : Form
    {
        int m_iSelectedCustomerID;

        public Orders()
        {
            InitializeComponent();
            m_iSelectedCustomerID = -1;
        }

        private void TheHelpButton_Click(object sender, EventArgs e)
        {
            string sMessage = "Select the customer that placed the order, and the POS form will be loaded, \n" +
                                "if the correct customer was selected and s/he placed an order.\n" +
                                "If no order information is found for the selected customer, then nothing will happen.\n" +
                                "If a new order is placed, select the Add New Order button after selecting the \n" +
                                "customer from the drop down box.\n" +
                                "If the customer is not in the drop down box, then the customer will first have \n" +
                                "to be added to the database.";
            string sCaption = "Order Form Help";
            MessageBox.Show(sMessage, sCaption);
        }

        private void ReturnButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CustomerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
