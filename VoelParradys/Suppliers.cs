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
        private bool m_bDeleteWarningShown;                         // Has the delete message box warning been shown
        int m_iSelectedSupplierID;                                  // The selected supplier's ID
        string m_sTempName;                                         // The selected supplier's name
        string m_sTempRepName;                                      // The selected supplier's representative name
        string m_sTempRepSurname;                                   // The selected supplier's representative surname
        string m_sTempPhone;                                        // The selected supplier's phone number
        string[] m_saTempAddress;                                   // The selected supplier's address
            
        public Suppliers()
        {
            InitializeComponent();
            SetUpSuppliersList();
            m_iSelectedSupplierID = -1;
            m_sTempName = "-1";
            m_sTempRepName = "-1";
            m_sTempRepSurname = "-1";
            m_sTempPhone = "-1";
            m_saTempAddress = new string[5] {"-1", "-1", "-1", "-1", "-1"};
            m_bDeleteWarningShown = false;
        }

        // Add the column headers to the suppliers list
        private void SetUpSuppliersList()
        {
            // check http://csharp.net-informations.com/gui/cs-listview.htm for a tutorial of using a list view control
            SuppliersListView.View = View.Details;
            SuppliersListView.GridLines = true;
            SuppliersListView.FullRowSelect = true;
            
            SuppliersListView.Columns.Add("ID", 30);
            SuppliersListView.Columns.Add("Name", 100);
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
                string sName = "", sRepName = "", sRepSurname = "", sPhoneNumber = "";
                string[] saAddress = new string[5] { "-1", "-1", "-1", "-1", "-1" };
                 
                rDataController.GetSupplierData(i, ref iID, ref sName, ref sRepName, ref sRepSurname, ref saAddress, ref sPhoneNumber);
                string sFormattedAddress = "";

                for (int j = 0; j < saAddress.Length; ++j)
                {
                    if (j == saAddress.Length - 1)
                        sFormattedAddress += saAddress[j];
                    else
                        sFormattedAddress += saAddress[j] + ", ";
                }

                arr[0] = iID.ToString();
                arr[1] = sName == "-1" ? "" : sName;
                arr[2] = sRepName == "-1" ? "" : sRepName;
                arr[3] = sRepSurname == "-1" ? "" : sRepSurname;
                arr[4] = sPhoneNumber == "-1" ? "" : sPhoneNumber;
                arr[5] = sFormattedAddress.Contains("-1") ? "" : sFormattedAddress;
                
                itm = new ListViewItem(arr);
                SuppliersListView.Items.Add(itm);
            }
        }

        private void SupplierOnSelected(object sender, EventArgs e)
        {
            ClearMemberVairiables();
            if (!UpdateButton.Enabled)
                UpdateButton.Enabled = true;
            if (!DeleteButton.Enabled)
                DeleteButton.Enabled = true;
            if (!DetailsButton.Enabled)
                DetailsButton.Enabled = true;

            var rDataController = VoelParadysDataController.GetInstance();
            string sName = "", sRepName = "", sRepSurname = "", sPhoneNumber = "";
            string[] saAddress = new string[5] { "-1", "-1", "-1", "-1", "-1" };
            m_iSelectedSupplierID = int.Parse(SuppliersListView.SelectedItems[0].SubItems[0].Text.ToString());
            rDataController.GetSupplierData(m_iSelectedSupplierID, ref sName, ref sRepName, ref sRepSurname, ref saAddress, ref sPhoneNumber);
   
            m_sTempName = sName;
            m_sTempRepName = sRepName;
            m_sTempRepSurname = sRepSurname;
            m_sTempPhone = sPhoneNumber;
            for (int i = 0; i < saAddress.Length; ++i)
                m_saTempAddress[i] = saAddress[i];
            
            NameTextBox.Text = m_sTempName == "-1" ? "" : m_sTempName;
            RepNameTextBox.Text = m_sTempRepName == "-1" ? "" : m_sTempRepName;
            RepSurnameTextBox.Text = m_sTempRepSurname == "-1" ? "" : m_sTempRepSurname;
            PhoneNumberTextBox.Text = m_sTempPhone == "-1" ? "" : m_sTempPhone;
            AddressTextBox1.Text = m_saTempAddress[0] == "-1" ? "" : saAddress[0];
            AddressTextBox2.Text = m_saTempAddress[1] == "-1" ? "" : saAddress[1];
            AddressTextBox3.Text = m_saTempAddress[2] == "-1" ? "" : saAddress[2];
            AddressTextBox4.Text = m_saTempAddress[3] == "-1" ? "" : saAddress[3];
            AddressTextBox5.Text = m_saTempAddress[4] == "-1" ? "" : saAddress[4];
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

        private void ReturnButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            var rDataController = VoelParadysDataController.GetInstance();
            if (rDataController.DoesSupplierExistInDatabase(m_iSelectedSupplierID))
            {
                string sName = "", sRepName = "", sRepSurname = "", sPhoneNumber = "";
                string[] saAddress = new string[5] { "-1", "-1", "-1", "-1", "-1" };
                rDataController.GetSupplierData(m_iSelectedSupplierID, ref sName, ref sRepName, ref sRepSurname, ref saAddress, ref sPhoneNumber);

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
                if (m_saTempAddress != null && saAddress != m_saTempAddress)
                {
                    saAddress = m_saTempAddress;
                    bChanged = true;
                }              
                if (bChanged)
                {
                    rDataController.UpdateSupplierDetails(m_iSelectedSupplierID, sName, sRepName, sRepSurname, saAddress, sPhoneNumber);
                    ClearMemberVairiables();
                    ClearTextBoxes();
                    this.Refresh();
                }
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            var rDataController = VoelParadysDataController.GetInstance();
            string sName = "", sRepName = "", sRepSurname = "", sPhoneNumber = "";
            string[] saAddress = new string[5] { "-1", "-1", "-1", "-1", "-1" };
            
            rDataController.GetSupplierData(m_iSelectedSupplierID, ref sName, ref sRepName, ref sRepSurname, ref saAddress, ref sPhoneNumber);

            m_bDeleteWarningShown = true;
            DialogResult messageBoxResult = rDataController.DisplayWarningMessageForDelete(sName);

            if (messageBoxResult == DialogResult.Yes)
            {
                if (rDataController.DoesSupplierExistInDatabase(m_iSelectedSupplierID))
                {
                    rDataController.DeleteSupplierFromDB(m_iSelectedSupplierID);
                    m_bDeleteWarningShown = false;
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
            m_saTempAddress = new string[5] {"-1", "-1", "-1", "-1", "-1"};
            
        }

        private void ClearTextBoxes()
        {
            NameTextBox.Clear();
            RepNameTextBox.Clear();
            RepSurnameTextBox.Clear();
            PhoneNumberTextBox.Clear();
            AddressTextBox1.Clear();
            AddressTextBox2.Clear();
            AddressTextBox3.Clear();
            AddressTextBox4.Clear();
            AddressTextBox5.Clear();
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
            if (!m_bDeleteWarningShown)
            {
                // We want to refresh once when we have focus again.
                this.Refresh();
                ClearMemberVairiables();
                ClearTextBoxes();
            }
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
