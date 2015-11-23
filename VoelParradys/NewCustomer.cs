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
    public partial class NewCustomer : Form
    {
        private VoelParadysDataStructures.CCustomerDetails m_NewCustomer;
        private string m_sAddress1;
        private string m_sAddress2;
        private string m_sAddress3;
        private string m_sAddress4;
        private string m_sAddress5;
        private bool m_bCustomerIdCreated;

        public NewCustomer()
        {
            InitializeComponent();
            m_NewCustomer = new VoelParadysDataStructures.CCustomerDetails(-1, "-1", "-1", new string[5] { "-1", "-1", "-1", "-1", "-1" }, "-1", -1);
            m_sAddress1 = "";
            m_sAddress2 = "";
            m_sAddress3 = "";
            m_sAddress4 = "";
            m_sAddress5 = "";
            m_bCustomerIdCreated = false;
        }

        private void CreateUniqueCustomerId()
        {
            int iTempID = -1;
            VoelParadysDataController.GetInstance().GetUniqueCustomerID(ref iTempID);
            m_NewCustomer.SetCustomerID(iTempID);
            m_bCustomerIdCreated = true;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void HelpButton_Click(object sender, EventArgs e)
        {
            string sMessage = "Only the name of the customer is required to ensure a valid customer input.\n" +
                                "The address only supplies 5 lines. If the address is more than 5 lines long,\n" +
                                "separate the rest of the address on line 5 with semi-colons\n(e.g Monument park;Pretoria;0181).\n" +
                                "Only South African telephone numbers are accepted (cell or land line).\n" + 
                                "All inputs will be accepted, but no formatting will take place, and the number will be displayed as it was entered.";
            string sCaption = "Customer Input Help";
            MessageBox.Show(sMessage, sCaption);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (m_NewCustomer.GetCustomerName() != "-1" || m_NewCustomer.GetCustomerName() != "")
            {
                string[] saAddress = new string[5] {"-1", "-1", "-1", "-1", "-1"};
                if (m_sAddress1 != "")
                    saAddress[0] = m_sAddress1;
                if (m_sAddress2 != "")
                    saAddress[1] = m_sAddress2;
                if (m_sAddress3 != "")
                    saAddress[2] = m_sAddress3;
                if (m_sAddress4 != "")
                    saAddress[3] = m_sAddress4;
                if (m_sAddress5 != "")
                    saAddress[4] = m_sAddress5;

                m_NewCustomer.SetCustomerAddress(saAddress);
                VoelParadysDataController.GetInstance().AddNewCustomerToList(m_NewCustomer);
                m_NewCustomer = new VoelParadysDataStructures.CCustomerDetails();
                m_sAddress1 = m_sAddress2 = m_sAddress3 = m_sAddress4 = m_sAddress5 = "";
                this.Close();
            }
        }

        private void NameTextBox_LooseFocus(object sender, EventArgs e)
        {
            if (!m_bCustomerIdCreated)
                CreateUniqueCustomerId();
            m_NewCustomer.SetCustomerName(NameTextBox.Text == "" ? "-1" : NameTextBox.Text);
        }

        private void SurnameTextBox_LooseFocus(object sender, EventArgs e)
        {
            if (!m_bCustomerIdCreated)
                CreateUniqueCustomerId();
            m_NewCustomer.SetCustomerSurname(SurnameTextBox.Text == "" ? "-1" : SurnameTextBox.Text);
        }

        private void PhoneTextBox_LooseFocus(object sender, EventArgs e)
        {
            if (!m_bCustomerIdCreated)
                CreateUniqueCustomerId();
            m_NewCustomer.SetCustomerPhoneNumber(PhoneTextBox.Text == "" ? "-1" : PhoneTextBox.Text);
        }

        private void IdNumberTextBox_LooseFocus(object sender, EventArgs e)
        {
            if (!m_bCustomerIdCreated)
                CreateUniqueCustomerId();
            if (IdNumberTextBox.Text != "")
            {
                long iTempIdNumber = -1;
                if (long.TryParse(IdNumberTextBox.Text, out iTempIdNumber))
                    m_NewCustomer.SetCustomerIdNumber(iTempIdNumber);
                else
                {
                    string sMessage = "Only number values are accepted";
                    string sCaption = "Invalid Input";
                    DialogResult dialogResult;

                    dialogResult = MessageBox.Show(sMessage, sCaption);

                    if (dialogResult == System.Windows.Forms.DialogResult.OK)
                    {
                        IdNumberTextBox.Clear();
                    }
                }
            }
        }

        private void AddressTextBox1_LooseFocus(object sender, EventArgs e)
        {
            if (!m_bCustomerIdCreated)
                CreateUniqueCustomerId();
            m_sAddress1 = AddressTextBox1.Text;
        }

        private void AddressTextBox2_LooseFocus(object sender, EventArgs e)
        {
            if (!m_bCustomerIdCreated)
                CreateUniqueCustomerId();
            m_sAddress2 = AddressTextBox2.Text;
        }

        private void AddressTextBox3_LooseFocus(object sender, EventArgs e)
        {
            if (!m_bCustomerIdCreated)
                CreateUniqueCustomerId();
            m_sAddress3 = AddressTextBox3.Text;
        }

        private void AddressTextBox4_LooseFocus(object sender, EventArgs e)
        {
            if (!m_bCustomerIdCreated)
                CreateUniqueCustomerId();
            m_sAddress4 = AddressTextBox4.Text;
        }

        private void AddressTextBox5_LooseFocus(object sender, EventArgs e)
        {
            if (!m_bCustomerIdCreated)
                CreateUniqueCustomerId();
            m_sAddress5 = AddressTextBox5.Text;
        }
    }
}
