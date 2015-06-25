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
    public partial class Customers : Form
    {
        private int m_iSelectedCustomerID;                          // The ID of the selected customer [can be edited]
        private string m_sTempName;                                 // The name of the selected customer [can be edited]
        private string m_sTempSurname;                              // The surname of the selected customer [can be edited]
        private string m_sTempPhone;                                // The phone number of the selected customer [can be edited]
        private string[] m_saTempAddress;                           // The address of the selected customer [can be edited]
        private long m_lTempIDNumber;                               // The ID number of the selected customer [can be edited]

        public Customers()
        {
            InitializeComponent();
            SetUpCustomersList();
            m_iSelectedCustomerID = -1;
            m_sTempName = "-1";
            m_sTempSurname = "-1";
            m_sTempPhone = "-1";
            m_saTempAddress = new string[5] {"-1", "-1", "-1", "-1", "-1"};
            m_lTempIDNumber = -1;
        }

        // Add the column headers to the customers list
        private void SetUpCustomersList()
        {
            // check http://csharp.net-informations.com/gui/cs-listview.htm for a tutorial of using a list view control
            CustomersListView.View = View.Details;
            CustomersListView.GridLines = true;
            CustomersListView.FullRowSelect = true;

            CustomersListView.Columns.Add("ID", 50);
            CustomersListView.Columns.Add("Name", 80);
            CustomersListView.Columns.Add("Surname", 80);
            CustomersListView.Columns.Add("Phone", 85);
            CustomersListView.Columns.Add("Address", 120);
            CustomersListView.Columns.Add("ID Number", 106);
        }
        private void AddCustomersToList()
        {
            // Here we should get all the cutomers and add them to the list.
            CustomersListView.Items.Clear();
            string[] arr = new string[6];
            ListViewItem itm;
            var rDataController = VoelParadysDataController.GetInstance();

            for (int i = 0; i < rDataController.GetCustomerListSize(); ++i)
            {
                int iID = -1;
                string sName = "", sSurname = "", sPhoneNumber = "";
                string[] saAddress = new string[5] { "", "", "", "", "" };
                List<string> sAddressList = new List<string>();
                long lIDNumber = -1;
                rDataController.GetCustomerData(i, ref iID, ref sName, ref sSurname, ref saAddress, ref sPhoneNumber, ref lIDNumber);

                string sFormattedAddress = "";
                for (int j = 0; j < saAddress.Length; ++j)
                {
                    if (saAddress[j] != "-1")
                        sAddressList.Add(saAddress[j]);
                }
                for (int k = 0; k < sAddressList.Count; ++k)
                {
                    if (k == sAddressList.Count - 1)
                        sFormattedAddress += sAddressList[k];
                    else
                        sFormattedAddress += sAddressList[k] + ", ";
                }

                arr[0] = iID.ToString();
                arr[1] = sName == "-1" ? "" : sName;
                arr[2] = sSurname == "-1" ? "" : sSurname;
                arr[3] = sPhoneNumber == "-1" ? "" : sPhoneNumber;
                arr[4] = sFormattedAddress;
                arr[5] = lIDNumber == -1 ? "" : lIDNumber.ToString();
                itm = new ListViewItem(arr);
                CustomersListView.Items.Add(itm);
            }
        }
        private void ReturnButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddCustomerButton_Click(object sender, EventArgs e)
        {
            var NewCustomerForm = new NewCustomer();
            NewCustomerForm.Show();
        }

        private void CustomerOnSelected(object sender, EventArgs e)
        {
            if (!UpdateCustomerButton.Enabled)
                UpdateCustomerButton.Enabled = true;
            if (!DeleteCustomerButton.Enabled)
                DeleteCustomerButton.Enabled = true;
            if (!CustomerDetailsButton.Enabled)
                CustomerDetailsButton.Enabled = true;

            var rDataController = VoelParadysDataController.GetInstance();
            string sName = "", sSurname = "", sPhoneNumber = "";
            string[] saAddress = new string[5];
            long lIDNumber = -1;
            m_iSelectedCustomerID = int.Parse(CustomersListView.SelectedItems[0].SubItems[0].Text.ToString());
            rDataController.GetCustomerData(m_iSelectedCustomerID, ref sName, ref sSurname, ref saAddress, ref sPhoneNumber, ref lIDNumber);
            
            m_sTempName = sName;
            m_sTempSurname = sSurname;
            m_sTempPhone = sPhoneNumber;
            m_saTempAddress[0] = saAddress[0];
            m_saTempAddress[1] = saAddress[1];
            m_saTempAddress[2] = saAddress[2];
            m_saTempAddress[3] = saAddress[3];
            m_saTempAddress[4] = saAddress[4];
            m_lTempIDNumber = lIDNumber;

            NameTextBox.Text = m_sTempName == "-1" ? "" : m_sTempName;
            SurnameTextBox.Text = m_sTempSurname == "-1" ? "" : m_sTempSurname;
            PhoneNumberTextBox.Text = m_sTempPhone == "-1" ? "" : m_sTempPhone;
            AddressTextBox1.Text = m_saTempAddress[0] == "-1" ? "" : m_saTempAddress[0];
            AddressTextBox2.Text = m_saTempAddress[1] == "-1" ? "" : m_saTempAddress[1];
            AddressTextBox3.Text = m_saTempAddress[2] == "-1" ? "" : m_saTempAddress[2];
            AddressTextBox4.Text = m_saTempAddress[3] == "-1" ? "" : m_saTempAddress[3];
            AddressTextBox5.Text = m_saTempAddress[4] == "-1" ? "" : m_saTempAddress[4];
            IdNumberTextBox.Text = m_lTempIDNumber == -1 ? "" : m_lTempIDNumber.ToString();
        }

        private void NameTextBox_LeaveFocus(object sender, EventArgs e)
        {
            m_sTempName = NameTextBox.Text;
        }

        private void SurnameTextBox_LeaveFocus(object sender, EventArgs e)
        {
            m_sTempSurname = SurnameTextBox.Text;
        }

        private void PhoneNumberTextBox_LeaveFocus(object sender, EventArgs e)
        {
            m_sTempPhone = PhoneNumberTextBox.Text;
        }

        private void AddressTextBox1_LeaveFocus(object sender, EventArgs e)
        {
            m_saTempAddress[0] = AddressTextBox1.Text == "" ? "-1" : AddressTextBox1.Text;
        }

        private void AddressTextBox2_LeaveFocus(object sender, EventArgs e)
        {
            m_saTempAddress[1] = AddressTextBox2.Text == "" ? "-1" : AddressTextBox2.Text;
        }

        private void AddressTextBox3_LeaveFocus(object sender, EventArgs e)
        {
            m_saTempAddress[2] = AddressTextBox3.Text == "" ? "-1" : AddressTextBox3.Text;
        }

        private void AddressTextBox4_LeaveFocus(object sender, EventArgs e)
        {
            m_saTempAddress[3] = AddressTextBox4.Text == "" ? "-1" : AddressTextBox4.Text;
        }

        private void AddressTextBox5_LeaveFocus(object sender, EventArgs e)
        {
            m_saTempAddress[4] = AddressTextBox5.Text == "" ? "-1" : AddressTextBox5.Text;
        }

        private void IdNumberTextBox_LeaveFocus(object sender, EventArgs e)
        {
            long lConvertedIDNumber = -1;
            if (long.TryParse(IdNumberTextBox.Text, out lConvertedIDNumber))
                m_lTempIDNumber = lConvertedIDNumber;
            else
            {
                string sMessage = "Only number values are accepted";
                string sCaption = "Invalid Input";
                DialogResult dialogResult;

                dialogResult = MessageBox.Show(sMessage, sCaption);

                if (dialogResult == System.Windows.Forms.DialogResult.OK)
                    IdNumberTextBox.Clear();
            }
        }

        private void UpdateCustomerButton_Click(object sender, EventArgs e)
        {
            var rDataController = VoelParadysDataController.GetInstance();
            if (rDataController.DoesCustomerExistInDatabase(m_iSelectedCustomerID))
            {
                string sName = "", sSurname = "", sPhoneNumber = "";
                string[] saAddress = new string[5] { "-1", "-1", "-1", "-1", "-1" };
                long lIDNumber = -1;
                rDataController.GetCustomerData(m_iSelectedCustomerID, ref sName, ref sSurname, ref saAddress, ref sPhoneNumber, ref lIDNumber);

                bool bChanged = false;
                if (m_sTempName != "-1" && m_sTempName != sName)
                {
                    sName = m_sTempName;
                    bChanged = true;
                }
                if (m_sTempSurname != "-1" && sSurname != m_sTempSurname)
                {
                    sSurname = m_sTempSurname;
                    bChanged = true;
                }
                if (m_sTempPhone != "-1" && sPhoneNumber != m_sTempPhone)
                {
                    sPhoneNumber = m_sTempPhone;
                    bChanged = true;
                }
                if (m_saTempAddress != null && saAddress != m_saTempAddress)
                {
                    saAddress = m_saTempAddress;
                    bChanged = true;
                }
                if (m_lTempIDNumber != -1 && lIDNumber != m_lTempIDNumber)
                {
                    lIDNumber = m_lTempIDNumber;
                    bChanged = true;
                }
                if (bChanged)
                {
                    rDataController.UpdateCustomerDetails(m_iSelectedCustomerID, sName, sSurname, saAddress, sPhoneNumber, lIDNumber);
                    ClearMemberVairiables();
                    ClearTextBoxes();
                    this.Refresh();
                }
            }
        }

        private void DeleteCustomerButton_Click(object sender, EventArgs e)
        {
            var rDataController = VoelParadysDataController.GetInstance();
            string sName = "", sSurname = "", sPhoneNumber = "";
            string[] saAddress = new string[5] { "", "", "", "", "" };
            long lIDNumber = -1;
            rDataController.GetCustomerData(m_iSelectedCustomerID, ref sName, ref sSurname, ref saAddress, ref sPhoneNumber, ref lIDNumber);

            DialogResult messageBoxResult = rDataController.DisplayWarningMessageForDelete(sName);
          
            if (messageBoxResult == DialogResult.Yes)
            {
                if (rDataController.DoesCustomerExistInDatabase(m_iSelectedCustomerID))
                {
                    rDataController.DeleteCustomerFromDB(m_iSelectedCustomerID);
                    ClearMemberVairiables();
                    ClearTextBoxes();
                    this.Refresh();
                }
            }
        }

        private void ClearMemberVairiables()
        {
            m_iSelectedCustomerID = -1;
            OnNoCustomerSelected();
            m_sTempName = "-1";
            m_sTempSurname = "-1";
            m_sTempPhone = "-1";
            m_saTempAddress = new string[5] {"-1", "-1", "-1", "-1", "-1"};
            m_lTempIDNumber = -1;
        }

        private void ClearTextBoxes()
        {
            NameTextBox.Clear();
            SurnameTextBox.Clear();
            PhoneNumberTextBox.Clear();
            AddressTextBox1.Clear();
            AddressTextBox2.Clear();
            AddressTextBox3.Clear();
            AddressTextBox4.Clear();
            AddressTextBox5.Clear();
            IdNumberTextBox.Clear();
        }

        public override void Refresh()
        {
            AddCustomersToList();
            base.Refresh();
        }

        private void CustomerForm_Activated(object sender, EventArgs e)
        {
            // We want to refresh once when we have focus again.
            this.Refresh();
            ClearMemberVairiables();
            ClearTextBoxes();
        }

        private void CustomerDetailsButton_Click(object sender, EventArgs e)
        {
            var theCustomerDetailsForm = new CustomerDetails(m_iSelectedCustomerID);
            theCustomerDetailsForm.Show();
        }

        private void OnNoCustomerSelected()
        {
            DeleteCustomerButton.Enabled = false;
            CustomerDetailsButton.Enabled = false;
            UpdateCustomerButton.Enabled = false;
        }
    }
}
