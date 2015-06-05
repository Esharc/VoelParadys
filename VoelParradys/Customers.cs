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
        private string m_sTempAddress;                              // The address of the selected customer [can be edited]
        private long m_lTempIDNumber;                               // The ID number of the selected customer [can be edited]

        public Customers()
        {
            InitializeComponent();
            SetUpCustomersList();
            m_iSelectedCustomerID = -1;
            m_sTempName = "-1";
            m_sTempSurname = "-1";
            m_sTempPhone = "-1";
            m_sTempAddress = "-1";
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
            // Here we should get all the stock items and add them to the list.
            CustomersListView.Items.Clear();
            string[] arr = new string[6];
            ListViewItem itm;
            var rDataController = VoelParadysDataController.GetInstance();

            for (int i = 0; i < rDataController.GetCustomerListSize(); ++i)
            {
                int iID = -1;
                string sName = "", sSurname = "", sAddress = "", sPhoneNumber = "";
                long lIDNumber = -1;
                rDataController.GetCustomerData(i, ref iID, ref sName, ref sSurname, ref sAddress, ref sPhoneNumber, ref lIDNumber);

                string sFormattedAddress = "";
                if (sAddress != "-1")
                    sFormattedAddress = sAddress.Replace(";", ", ");

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
            string sName = "", sSurname = "", sAddress = "", sPhoneNumber = "";
            long lIDNumber = -1;
            m_iSelectedCustomerID = int.Parse(CustomersListView.SelectedItems[0].SubItems[0].Text.ToString());
            rDataController.GetCustomerData(m_iSelectedCustomerID, ref sName, ref sSurname, ref sAddress, ref sPhoneNumber, ref lIDNumber);
            string sFormattedAddress = "-1";
            if (sAddress != "-1")
            {
                sFormattedAddress = "";
                char[] acDelimiterArray = {';'};
                string[] aAddressLines = sAddress.Split(acDelimiterArray, StringSplitOptions.RemoveEmptyEntries);
                int iCount = aAddressLines.Length < 5 ? aAddressLines.Length : 5;
                for (int i = 0; i < iCount; ++i)
                {
                    if (i == iCount - 1)
                        sFormattedAddress += aAddressLines[i];
                    else
                        sFormattedAddress += aAddressLines[i] + "\r\n";
                }

                if (aAddressLines.Length > 5)
                {
                    iCount = aAddressLines.Length;
                    for (int j = 5; j < iCount; ++j)
                    {
                        if (j == 5)
                            sFormattedAddress += ", " + aAddressLines[j];
                        else
                            sFormattedAddress += aAddressLines[j] + ",";
                    }
                }
            }
            m_sTempName = sName;
            m_sTempSurname = sSurname;
            m_sTempPhone = sPhoneNumber;
            m_sTempAddress = sFormattedAddress;
            m_lTempIDNumber = lIDNumber;

            NameTextBox.Text = m_sTempName == "-1" ? "" : m_sTempName;
            SurnameTextBox.Text = m_sTempSurname == "-1" ? "" : m_sTempSurname;
            PhoneNumberTextBox.Text = m_sTempPhone == "-1" ? "" : m_sTempPhone;
            AddressTextBox.Text = m_sTempAddress == "-1" ? "" : m_sTempAddress;
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

        private void AddressTextBox_LeaveFocus(object sender, EventArgs e)
        {
            // For inserting members into the address text box, remember to add the /r/n delimeters for the different lines.
            string sAddress = "";
            m_sTempAddress = "";
            sAddress = AddressTextBox.Text;
            char[] acDelimiterArray = { '\r', '\n' };
            string[] aAddressLines = sAddress.Split(acDelimiterArray, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < aAddressLines.Length; ++i)
            {
                if (i == aAddressLines.Length - 1)
                    m_sTempAddress += aAddressLines[i];
                else
                    m_sTempAddress += aAddressLines[i] + ";";
            }
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
                string sName = "", sSurname = "", sAddress = "", sPhoneNumber = "";
                long lIDNumber = -1;
                rDataController.GetCustomerData(m_iSelectedCustomerID, ref sName, ref sSurname, ref sAddress, ref sPhoneNumber, ref lIDNumber);

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
                if (m_sTempAddress != "-1" && sAddress != m_sTempAddress)
                {
                    sAddress = m_sTempAddress;
                    bChanged = true;
                }
                if (m_lTempIDNumber != -1 && lIDNumber != m_lTempIDNumber)
                {
                    lIDNumber = m_lTempIDNumber;
                    bChanged = true;
                }
                if (bChanged)
                {
                    rDataController.UpdateCustomerDetails(m_iSelectedCustomerID, sName, sSurname, sAddress, sPhoneNumber, lIDNumber);
                    this.Refresh();
                }
            }
        }

        private void DeleteCustomerButton_Click(object sender, EventArgs e)
        {
            var rDataController = VoelParadysDataController.GetInstance();
            string sName = "", sSurname = "", sAddress = "", sPhoneNumber = "";
            long lIDNumber = -1;
            rDataController.GetCustomerData(m_iSelectedCustomerID, ref sName, ref sSurname, ref sAddress, ref sPhoneNumber, ref lIDNumber);

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
            m_sTempAddress = "-1";
            m_lTempIDNumber = -1;
        }

        private void ClearTextBoxes()
        {
            NameTextBox.Clear();
            SurnameTextBox.Clear();
            PhoneNumberTextBox.Clear();
            AddressTextBox.Clear();
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
