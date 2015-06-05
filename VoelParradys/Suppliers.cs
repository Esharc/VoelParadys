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
    public partial class Suppliers : Form
    {
        int m_iSelectedSupplierID;
        string m_sTempName;
        string m_sTempRepName;
        string m_sTempRepSurname;
        string m_sTempPhone;
        string m_sTempAddress;
            
        public Suppliers()
        {
            InitializeComponent();
            SetUpSuppliersList();
            m_iSelectedSupplierID = -1;
            m_sTempName = "-1";
            m_sTempRepName = "-1";
            m_sTempRepSurname = "-1";
            m_sTempPhone = "-1";
            m_sTempAddress = "-1";
        }

        // Add the column headers to the suppliers list
        private void SetUpSuppliersList()
        {
            // check http://csharp.net-informations.com/gui/cs-listview.htm for a tutorial of using a list view control
            SuppliersListView.View = View.Details;
            SuppliersListView.GridLines = true;
            SuppliersListView.FullRowSelect = true;
            
            SuppliersListView.Columns.Add("ID", 50);
            SuppliersListView.Columns.Add("Name", 80);
            SuppliersListView.Columns.Add("Rep Name", 80);
            SuppliersListView.Columns.Add("Rep Surname", 100);
            SuppliersListView.Columns.Add("Phone", 85);
            SuppliersListView.Columns.Add("Address", 120);
        }

        private void AddSuppliersToList()
        {
            // Here we should get all the stock items and add them to the list.
            SuppliersListView.Items.Clear();
            string[] arr = new string[6];
            ListViewItem itm;
            var rDataController = VoelParadysDataController.GetInstance();

            for (int i = 0; i < rDataController.GetSupplierListSize(); ++i)
            {
                int iID = -1;
                string sName = "", sRepName = "", sRepSurname = "", sAddress = "", sPhoneNumber = "";
                rDataController.GetSupplierData(i, ref iID, ref sName, ref sRepName, ref sRepSurname, ref sAddress, ref sPhoneNumber);

                string sFormattedAddress = "";
                if (sAddress != "-1")
                    sFormattedAddress = sAddress.Replace(";", ", ");

                arr[0] = iID.ToString();
                arr[1] = sName == "-1" ? "" : sName;
                arr[2] = sRepName == "-1" ? "" : sRepName;
                arr[3] = sRepSurname == "-1" ? "" : sRepSurname;
                arr[4] = sPhoneNumber == "-1" ? "" : sPhoneNumber;
                arr[5] = sFormattedAddress;
                
                itm = new ListViewItem(arr);
                SuppliersListView.Items.Add(itm);
            }
        }

        private void SupplierOnSelected(object sender, EventArgs e)
        {
            if (!UpdateButton.Enabled)
                UpdateButton.Enabled = true;
            if (!DeleteButton.Enabled)
                DeleteButton.Enabled = true;
           if (!DetailsButton.Enabled)
                DetailsButton.Enabled = true;

            var rDataController = VoelParadysDataController.GetInstance();
            string sName = "", sRepName = "", sRepSurname = "", sAddress = "", sPhoneNumber = "";
            m_iSelectedSupplierID = int.Parse(SuppliersListView.SelectedItems[0].SubItems[0].Text.ToString());
            rDataController.GetSupplierData(m_iSelectedSupplierID, ref sName, ref sRepName, ref sRepSurname, ref sAddress, ref sPhoneNumber);
            string sFormattedAddress = "-1";
            if (sAddress != "-1")
            {
                sFormattedAddress = "";
                char[] acDelimiterArray = { ';' };
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
            m_sTempRepName = sRepName;
            m_sTempRepSurname = sRepSurname;
            m_sTempPhone = sPhoneNumber;
            m_sTempAddress = sFormattedAddress;
            
            NameTextBox.Text = m_sTempName == "-1" ? "" : m_sTempName;
            RepNameTextBox.Text = m_sTempRepName == "-1" ? "" : m_sTempRepName;
            RepSurnameTextBox.Text = m_sTempRepSurname == "-1" ? "" : m_sTempRepSurname;
            PhoneNumberTextBox.Text = m_sTempPhone == "-1" ? "" : m_sTempPhone;
            AddressTextBox.Text = m_sTempAddress == "-1" ? "" : m_sTempAddress;
        }

        private void NameTextBox_LeaveFocus(object sender, EventArgs e)
        {
            m_sTempName = NameTextBox.Text;
        }

        private void RepNameTextBox_LeaveFocus(object sender, EventArgs e)
        {
            m_sTempRepName = RepNameTextBox.Text;
        }

        private void RepSurnameTextBox_LeaveFocus(object sender, EventArgs e)
        {
            m_sTempRepSurname = RepSurnameTextBox.Text;
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

        private void ReturnButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            var rDataController = VoelParadysDataController.GetInstance();
            if (rDataController.DoesSupplierExistInDatabase(m_iSelectedSupplierID))
            {
                string sName = "", sRepName = "", sRepSurname = "", sAddress = "", sPhoneNumber = "";
                rDataController.GetSupplierData(m_iSelectedSupplierID, ref sName, ref sRepName, ref sRepSurname, ref sAddress, ref sPhoneNumber);

                bool bChanged = false;
                if (m_sTempName != "-1" && m_sTempName != sName)
                {
                    sName = m_sTempName;
                    bChanged = true;
                }
                if (m_sTempRepName != "-1" && m_sTempRepName != sRepName)
                {
                    sRepName = m_sTempRepName;
                    bChanged = true;
                }
                if (m_sTempRepSurname != "-1" && sRepSurname != m_sTempRepSurname)
                {
                    sRepSurname = m_sTempRepSurname;
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
                if (bChanged)
                {
                    rDataController.UpdateSupplierDetails(m_iSelectedSupplierID, sName, sRepName, sRepSurname, sAddress, sPhoneNumber);
                    this.Refresh();
                }
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            var rDataController = VoelParadysDataController.GetInstance();
            string sName = "", sRepName = "", sRepSurname = "", sAddress = "", sPhoneNumber = "";
            
            rDataController.GetSupplierData(m_iSelectedSupplierID, ref sName, ref sRepName, ref sRepSurname, ref sAddress, ref sPhoneNumber);

            DialogResult messageBoxResult = rDataController.DisplayWarningMessageForDelete(sName);

            if (messageBoxResult == DialogResult.Yes)
            {
                if (rDataController.DoesSupplierExistInDatabase(m_iSelectedSupplierID))
                {
                    rDataController.DeleteCustomerFromDB(m_iSelectedSupplierID);
                    ClearMemberVairiables();
                    ClearTextBoxes();
                    this.Refresh();
                }
            }
        }

        private void ClearMemberVairiables()
        {
            m_iSelectedSupplierID = -1;
            OnNoSupplierSelected();
            m_sTempName = "-1";
            m_sTempRepName = "-1";
            m_sTempRepSurname = "-1";
            m_sTempPhone = "-1";
            m_sTempAddress = "-1";
            
        }

        private void ClearTextBoxes()
        {
            NameTextBox.Clear();
            RepNameTextBox.Clear();
            RepSurnameTextBox.Clear();
            PhoneNumberTextBox.Clear();
            AddressTextBox.Clear();
        }

        private void OnNoSupplierSelected()
        {
            DeleteButton.Enabled = false;
            DetailsButton.Enabled = false;
            UpdateButton.Enabled = false;
        }
        public override void Refresh()
        {
            AddSuppliersToList();
            base.Refresh();
        }

        private void SuppliersForm_Activated(object sender, EventArgs e)
        {
            // We want to refresh once when we have focus again.
            this.Refresh();
            ClearMemberVairiables();
            ClearTextBoxes();
        }

        private void DetailsButton_Click(object sender, EventArgs e)
        {
            var theSupplierDetailsForm = new SuppliersDetails(m_iSelectedSupplierID);
            theSupplierDetailsForm.Show();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            var AddNewSupplierForm = new NewSupplier();
            AddNewSupplierForm.Show();
        }
    }
}
