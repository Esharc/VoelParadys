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
        CIntStringMap m_SelectedCustomer;
        List<CIntStringMap> m_CustomersList;
        List<string> m_lsWishListItems;
        private PointOfSale m_Parent;

        public Orders(PointOfSale theParent)
        {
            InitializeComponent();
            m_Parent = theParent;
            m_SelectedCustomer = new CIntStringMap(-1, "");
            m_lsWishListItems = new List<string>();
            PopulateCustomersList();
        }

        private void PopulateCustomersList()
        {
            List<CIntStringMap> TempCustomerList = VoelParadysDataController.GetInstance().GetAllCustomersNameAndID();
            List<CIntStringMap> DataSourceList = new List<CIntStringMap>();
            DataSourceList.Add(new CIntStringMap(-1, "None"));

            for (int i = 0; i < TempCustomerList.Count; ++i)
                DataSourceList.Add(TempCustomerList[i]);

            BindingList<CIntStringMap> ListSource = new BindingList<CIntStringMap>(DataSourceList);

            CustomerComboBox.DataSource = DataSourceList;
            CustomerComboBox.DisplayMember = "m_sCustomerName";
            CustomerComboBox.ValueMember = "m_iCustomerID";
        }

        private void TheHelpButton_Click(object sender, EventArgs e)
        {
            string sMessage = "Select the customer that placed the order, and the POS form will be loaded, \n" +
                                "if the correct customer was selected and s/he placed an order.\n" +
                                "If no order information is found for the selected customer, then nothing will happen.\n" +
                                "If a new order is placed, select the Add New Order button and the Customers \n" +
                                "form  will be loaded where a customer can be selected to add wish list items, \n" +
                                "or if the customer does not exist in the database, s/he can be added first, then \n" +
                                "the wish list item(s) added.\n\n" +
                                "NOTE!!! The wish list item name must exist in the stock inventory database with \n" +
                                "the exact same name otherwise nothing will happen.";
            string sCaption = "Order Form Help";
            MessageBox.Show(sMessage, sCaption);
        }

        private void ReturnButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void SetWishListItems(List<string> theItems)
        {
            m_lsWishListItems = theItems;
            CallParentWindow();
        }

        private void CustomerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_SelectedCustomer = CustomerComboBox.SelectedItem as CIntStringMap;
            if (m_SelectedCustomer.m_iCustomerID != -1)
            {
                var rDataController = VoelParadysDataController.GetInstance();

                if (rDataController.GetCustomerWishListCount(m_SelectedCustomer.m_iCustomerID) > 1)
                {
                    List<string> TempList = new List<string>();
                    for (int i = 0; i < rDataController.GetCustomerWishListCount(m_SelectedCustomer.m_iCustomerID); ++i)
                        TempList.Add(rDataController.GetCustomerWishListItemAt(m_SelectedCustomer.m_iCustomerID, i));
                    var theWishListItems = new WishListItems(this, TempList);
                    theWishListItems.Show();
                }
                else if (rDataController.GetCustomerWishListCount(m_SelectedCustomer.m_iCustomerID) == 1)
                {
                    m_lsWishListItems.Add(rDataController.GetCustomerWishListItemAt(m_SelectedCustomer.m_iCustomerID, 0));
                    CallParentWindow();
                }
                else
                {
                    string sMessage = DetermieErrorOnCustomerSelect();
                    string sCaption = "Error!!!";
                    MessageBox.Show(sMessage, sCaption);
                }
            }
        }

        private string DetermieErrorOnCustomerSelect()
        {
            var rDataController = VoelParadysDataController.GetInstance();

            if (rDataController.GetCustomerWishListCount(m_SelectedCustomer.m_iCustomerID) == 0)
                return "No wish list items found for " + m_SelectedCustomer.m_sCustomerName;
            else if (rDataController.GetCustomerWishListCount(m_SelectedCustomer.m_iCustomerID) == 1)
            {
                string sStockItem = rDataController.GetCustomerWishListItemAt(m_SelectedCustomer.m_iCustomerID, 0);
                string sItemCode = "";
                if (!rDataController.DoesItemExistInDatabase(sStockItem, ref sItemCode))
                    return sStockItem + " does not exist in the database";
                else
                    return "I am here because the item exists in the database but \n" +
                        "that means that I should not be here. Contact the developer";
            }
            else
            {
                string sTempCode = "";
                List<string> sItems = new List<string>();
                for (int i = 0; i < m_lsWishListItems.Count; ++i)
                {
                    if (rDataController.DoesItemExistInDatabase(m_lsWishListItems[i], ref sTempCode))
                        sItems.Add(m_lsWishListItems[i]);
                }

                if (sItems.Count > 0)
                {
                    string sRetVal = "";
                    for (int j = 0; j < sItems.Count; ++j)
                    {
                        if (j == sItems.Count - 1)
                            sRetVal += sItems[j];
                        else
                            sRetVal += sItems[j] + ", ";
                    }

                    return "Non existent item(s) in the database: " + sRetVal;
                }
                else
                    return "I am here because there is more than one item in the list \n" +
                        "and technically I should not be here. Contact the developer";
            }
        }

        private void CallParentWindow()
        {
            if (m_lsWishListItems.Count > 0 && VoelParadysDataController.GetInstance().AllItemsExistInDatabase(m_lsWishListItems))
            {
                m_Parent.SetWishListSaleData(m_lsWishListItems, m_SelectedCustomer.m_iCustomerID);
                this.Close();
            }
            else
            {
                string sMessage = DetermieErrorOnCustomerSelect();
                string sCaption = "Error!!!";
                MessageBox.Show(sMessage, sCaption);
            }

        }

        private void AddNewOrderButton_Click(object sender, EventArgs e)
        {
            var theCustomersForm = new Customers();
            theCustomersForm.Show();
            this.Close();
        }
    }
}
