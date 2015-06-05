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
    public partial class EnterPassword : Form
    {
        string sEnteredPass;
        public EnterPassword()
        {
            InitializeComponent();
            sEnteredPass = "";
        }

        private void PasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            sEnteredPass = PasswordTextBox.Text;
        }

        private void OnPassEnterPressed(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                PassAcceptButton_Click(sender, e);
        }

        private void PassAcceptButton_Click(object sender, EventArgs e)
        {
            if (VoelParadysDataController.GetInstance().VerifyPassword(sEnteredPass))
            {
                var InventoryForm = new Inventory();
                InventoryForm.Show();
                this.Close();
            }
            else
            {
                string sMessage = "Invalid password entered";
                string sCaption = "Invalid password";

                DialogResult theResult;
                theResult = MessageBox.Show(sMessage, sCaption);

                if (theResult == DialogResult.OK)
                    PasswordTextBox.Clear();
            }
        }

        private void PassCancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
