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
    public partial class NewSuperUserPasswordForm : Form
    {
        string m_sEnteredNewPassword;
        string m_sOldPassword;
        bool m_bOldMatch;
        bool m_bNewValid;
        public NewSuperUserPasswordForm()
        {
            InitializeComponent();
            m_sEnteredNewPassword = "";
            m_sOldPassword = "";
            m_bOldMatch = m_bNewValid = false;
        }

        private void OldPasswordTextBox_LeaveFocus(object sender, EventArgs e)
        {
            if (OldPasswordTextBox.Text != "")
            if (VoelParadysDataController.GetInstance().VerifyPassword(OldPasswordTextBox.Text))
            {
                m_sOldPassword = OldPasswordTextBox.Text;
                m_bOldMatch = true;
            }
            else
            {
                string sMessage = "Incorrect password entered";
                string sCaption = "Passwords do not match";
                DialogResult dialogResult;

                dialogResult = MessageBox.Show(sMessage, sCaption);

                if (dialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    OldPasswordTextBox.Text = "";
                }
            }
        }

        private void NewPasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            // Verify new password character input here
            char[] acIllegalCharacters = new char[] {'[', ']', '{', '}', '|', '\\', '\'', '"', ';', ':', '/', '?', '>', '.', '<', ',', '-', '_', '+', '='};
            string sInput = NewPasswordTextBox.Text;
            string sNewInput = "";
            bool bInvalidCahracterEntered = false;
            char cIllegalCharacter = ' ';

            for (int i = 0; i < sInput.Length; ++i)
            {
                if (VoelParadysDataController.GetInstance().IsCharacterValid(acIllegalCharacters, sInput[i]))
                {
                    bInvalidCahracterEntered = true;
                    cIllegalCharacter = sInput[i];
                    break;
                }
                else
                    sNewInput += sInput[i];
            }

            if (bInvalidCahracterEntered)
            {
                string sMessage = "Illegal character entered. " + cIllegalCharacter + " was entered and may not be in the password";
                string sCaption = "Illegal Input";
                DialogResult dialogResult;

                dialogResult = MessageBox.Show(sMessage, sCaption);

                if (dialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    NewPasswordTextBox.Text = sNewInput;
                    NewPasswordTextBox.Select(NewPasswordTextBox.Text.Length, 0);
                }
            }
            else
                m_sEnteredNewPassword = sInput;
        }

        private void NewPasswordTextBox_LeaveFocus(object sender, EventArgs e)
        {
            if (m_sEnteredNewPassword != "")
                m_bNewValid = true;
        }

        private void RetypPasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            if (RetypPasswordTextBox.Text == m_sEnteredNewPassword)
                SavePasswordButton.Enabled = m_bOldMatch && m_bNewValid;
            else
                SavePasswordButton.Enabled = false;
        }

        private void SavePasswordButton_Click(object sender, EventArgs e)
        {
            VoelParadysDataController.GetInstance().AddNewPassword(m_sOldPassword, m_sEnteredNewPassword);
            this.Close();
        }

        private void CancelPasswordButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void HelpWithPasswordButton_Click(object sender, EventArgs e)
        {
            string sMessage = "Any password can be entered excpet for the following characters:\n" + 
                        "[, ], {, }, |, \\, \', \", ;, :, /, ?, >, ., <, ,, -, _, +, =";
            string sCaption = "Password Help";

            MessageBox.Show(sMessage, sCaption);
        }
    }
}
