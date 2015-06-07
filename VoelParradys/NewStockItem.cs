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
    public partial class NewStockItem : Form
    {
        private string m_sItemCode;
        private string m_sItemName;
        private int m_iItemQuantity;
        private float m_fBuyPrice;
        private float m_fSellPrice;
        private bool m_bMessageBoxShown;
        int m_iSelectedSupplier;

        public NewStockItem()
        {
            InitializeComponent();
            PopulateSupplierComboBox();
            m_sItemCode = "";
            m_sItemName = "";
            m_iItemQuantity = -1;
            m_fBuyPrice = -1;
            m_fSellPrice = -1;
            m_bMessageBoxShown = false;
            m_iSelectedSupplier = -1;
        }

        private bool IsValidCode()
        {
            return m_sItemCode != "" && m_sItemCode != "0" && !VoelParadysDataController.GetInstance().DoesItemExistInDatabase(m_sItemCode);
        }

        private bool IsValidName()
        {
            string sItemCode = "";
            var rDataController = VoelParadysDataController.GetInstance();
            return m_sItemName != "" && m_sItemName != "0" && !rDataController.DoesItemExistInDatabase(m_sItemName, ref sItemCode);
        }

        private bool IsValidItem()
        {
            return IsValidCode() && IsValidName() && m_iItemQuantity != -1 && m_fBuyPrice != -1 && m_fSellPrice != -1;
        }

        private void CancelInput_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NewStockItem_Deactivate(object sender, EventArgs e)
        {
            if (!m_bMessageBoxShown)
                this.Close();
        }

        private void CodeTextBox_TextChanged(object sender, EventArgs e)
        {
            // If the text box is empty, do nothing
            if (CodeTextBox.Text != "")
                m_sItemCode = CodeTextBox.Text;
        }

        private void NameTextBox_TextChanged(object sender, EventArgs e)
        {
            m_sItemName = NameTextBox.Text;
        }

        private void QuantityTextBox_TextChanged(object sender, EventArgs e)
        {
            // If the text box is empty, do nothing
            if (QuantityTextBox.Text != "")
            {
                int iTempItem = -1;
                if (int.TryParse(QuantityTextBox.Text, out iTempItem))
                    m_iItemQuantity = iTempItem;
                else
                {
                    m_bMessageBoxShown = true;
                    string sMessage = "Only number values are accepted";
                    string sCaption = "Invalid Input";
                    DialogResult dialogResult;

                    dialogResult = MessageBox.Show(sMessage, sCaption);

                    if (dialogResult == System.Windows.Forms.DialogResult.OK)
                    {
                        m_bMessageBoxShown = false;
                        QuantityTextBox.Clear();
                    }
                }
            }
        }

        private void BuyPriceTextBox_TextChanged(object sender, EventArgs e)
        {
            // If the text box is empty, do nothing
            if (BuyPriceTextBox.Text != "")
            {
                float fTempItem = -1;
                if (float.TryParse(BuyPriceTextBox.Text, out fTempItem))
                    m_fBuyPrice = fTempItem;
                else
                {
                    m_bMessageBoxShown = true;
                    string sMessage = "Only number values are accepted";
                    string sCaption = "Invalid Input";
                    DialogResult dialogResult;

                    dialogResult = MessageBox.Show(sMessage, sCaption);

                    if (dialogResult == System.Windows.Forms.DialogResult.OK)
                    {
                        m_bMessageBoxShown = false;
                        BuyPriceTextBox.Clear();
                    }
                }
            }
        }

        private void SellPriceTextBox_TextChanged(object sender, EventArgs e)
        {
            // If the text box is empty, do nothing
            if (SellPriceTextBox.Text != "")
            {
                float fTempItem = -1;
                if (float.TryParse(SellPriceTextBox.Text, out fTempItem))
                    m_fSellPrice = fTempItem;
                else
                {
                    m_bMessageBoxShown = true;
                    string sMessage = "Only number values are accepted";
                    string sCaption = "Invalid Input";
                    DialogResult dialogResult;

                    dialogResult = MessageBox.Show(sMessage, sCaption);

                    if (dialogResult == System.Windows.Forms.DialogResult.OK)
                    {
                        m_bMessageBoxShown = false;
                        SellPriceTextBox.Clear();
                    }
                }
            }
        }

        private void HelpButton_Click(object sender, EventArgs e)
        {
            m_bMessageBoxShown = true;
            string sMessage = "All items should have an input to ensure that a valid inventory item is added.\n" +
                                "The code and name should be unique to ensure a match with database searches.\n" +
                                "Fields that do not require an input can be entered as zero (Sell price, Buy price and Quantity).\n" +
                                "The name and code fields are required and a value of 0 is not accepted";
            string sCaption = "Inventory Input Help";
            DialogResult dialogResult;

            dialogResult = MessageBox.Show(sMessage, sCaption);

            if (dialogResult == System.Windows.Forms.DialogResult.OK)
            {
                m_bMessageBoxShown = false;
            }
        }

        private void SaveStockItem_Click(object sender, EventArgs e)
        {
            if (IsValidItem())
            {
                VoelParadysDataController.GetInstance().AddNewInventoryItemToList(m_sItemCode, m_sItemName, m_iItemQuantity, m_fBuyPrice, m_fSellPrice);
                if (SupplierComboBox.SelectedItem.ToString() != "None")
                    UpdateSupplierQuantity(m_iItemQuantity);
                this.Close();
            }
            else
            {
                m_bMessageBoxShown = true;
                string sMessage = "The item entered is not valid";
                string sCaption = "Invalid Item Entered";
                DialogResult dialogResult;

                dialogResult = MessageBox.Show(sMessage, sCaption);

                if (dialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    m_bMessageBoxShown = false;
                }
            }
        }

        private void PopulateSupplierComboBox()
        {
            List<string> lsSupplierNames = VoelParadysDataController.GetInstance().GetAllSupplierNames();
            List<string> lsDataSource = new List<string>();
            lsDataSource.Add("None");
            for (int i = 0; i < lsSupplierNames.Count; ++i)
                lsDataSource.Add(lsSupplierNames[i]);
            SupplierComboBox.DataSource = lsDataSource;
        }

        private void SupplierComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sSelectedSupplier = SupplierComboBox.SelectedItem.ToString();
            m_iSelectedSupplier = VoelParadysDataController.GetInstance().GetSupplierIDFromName(sSelectedSupplier);
        }

        private void UpdateSupplierQuantity(int iIncrement)
        {
            VoelParadysDataController.GetInstance().UpdateSuppliedItem(m_iSelectedSupplier, m_sItemCode, iIncrement);
        }
    }
}
