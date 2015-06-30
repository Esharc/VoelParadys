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
    public partial class NewSupplier : Form
    {
        private VoelParadysDataStructures.CSupplierDetails m_NewSupplier;
        private string m_sAddress1;
        private string m_sAddress2;
        private string m_sAddress3;
        private string m_sAddress4;
        private string m_sAddress5;
        private bool m_bSupplierIdCreated;

        public NewSupplier()
        {
            InitializeComponent();
            m_NewSupplier = new VoelParadysDataStructures.CSupplierDetails(-1, "-1", "-1", "-1", new string[5] { "-1", "-1", "-1", "-1", "-1" }, "-1");
            m_sAddress1 = "";
            m_sAddress2 = "";
            m_sAddress3 = "";
            m_sAddress4 = "";
            m_sAddress5 = "";
            m_bSupplierIdCreated = false;
        }

        private void CreateUniqueSupplierId()
        {
            int iTempID = -1;
            VoelParadysDataController.GetInstance().GetUniqueSupplierID(ref iTempID);
            m_NewSupplier.SetSupplierID(iTempID);
            m_bSupplierIdCreated = true;
        }

        private void TheHelpButton_Click(object sender, EventArgs e)
        {
            string sMessage = "Only the name of the supplier is required to ensure a valid supplier input.\n" +
                                "The address only supplies 5 lines. If the address is more than 5 lines long,\n" +
                                "separate the rest of the address on line 5 with semi colons\n(e.g Monument park;Pretoria;0181).\n" +
                                "Only South African telephone numbers are accepted (cell or land line).\n" +
                                "All inputs will be accepted, but no formatting will take place, and the number will be displayed as it was entered.";
            string sCaption = "Supplier Input Help";
            MessageBox.Show(sMessage, sCaption);
        }

        private void TheCancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TheSaveButton_Click(object sender, EventArgs e)
        {
            if (m_NewSupplier.GetSupplierName() != "-1" || m_NewSupplier.GetSupplierName() != "")
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

                m_NewSupplier.SetSupplierAddress(saAddress);
                VoelParadysDataController.GetInstance().AddNewSupplierToList(m_NewSupplier);
                m_NewSupplier = new VoelParadysDataStructures.CSupplierDetails();
                m_sAddress1 = m_sAddress2 = m_sAddress3 = m_sAddress4 = m_sAddress5 = "";
                this.Close();
            }
        }
        private void NameTextBox_LooseFocus(object sender, EventArgs e)
        {
            if (!m_bSupplierIdCreated)
                CreateUniqueSupplierId();
            m_NewSupplier.SetSupplierName(NameTextBox.Text);
        }

        private void RepNameTextBox_LooseFocus(object sender, EventArgs e)
        {
            if (!m_bSupplierIdCreated)
                CreateUniqueSupplierId();
            m_NewSupplier.SetRepName(RepNameTextBox.Text);
        }

        private void RepSurnameTextBox_LooseFocus(object sender, EventArgs e)
        {
            if (!m_bSupplierIdCreated)
                CreateUniqueSupplierId();
            m_NewSupplier.SetRepSurname(RepSurnameTextBox.Text);
        }

        private void PhoneTextBox_LooseFocus(object sender, EventArgs e)
        {
            if (!m_bSupplierIdCreated)
                CreateUniqueSupplierId();
            m_NewSupplier.SetSupplierPhoneNumber(PhoneTextBox.Text);
        }

        private void AddressTextBox1_LooseFocus(object sender, EventArgs e)
        {
            if (!m_bSupplierIdCreated)
                CreateUniqueSupplierId();
            m_sAddress1 = AddressTextBox1.Text;
        }

        private void AddressTextBox2_LooseFocus(object sender, EventArgs e)
        {
            if (!m_bSupplierIdCreated)
                CreateUniqueSupplierId();
            m_sAddress2 = AddressTextBox2.Text;
        }

        private void AddressTextBox3_LooseFocus(object sender, EventArgs e)
        {
            if (!m_bSupplierIdCreated)
                CreateUniqueSupplierId();
            m_sAddress3 = AddressTextBox3.Text;
        }

        private void AddressTextBox4_LooseFocus(object sender, EventArgs e)
        {
            if (!m_bSupplierIdCreated)
                CreateUniqueSupplierId();
            m_sAddress4 = AddressTextBox4.Text;
        }

        private void AddressTextBox5_LooseFocus(object sender, EventArgs e)
        {
            if (!m_bSupplierIdCreated)
                CreateUniqueSupplierId();
            m_sAddress5 = AddressTextBox5.Text;
        }
    }
}
