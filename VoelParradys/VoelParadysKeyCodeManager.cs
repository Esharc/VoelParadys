using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace VoelParadys
{
    class VoelParadysKeyCodeManager
    {
        public bool IsValidCharacter(char[] cCharacterArray, char cTestCharacter)
        {
            // Here we want to test if the cTestcharacter exists in the cCharacterArray

            for (int i = 0; i < cCharacterArray.Length; ++i)
            {
                if (cTestCharacter == cCharacterArray[i])
                    return true;
            }
            return false;
        }
        private bool IsNumberCharacter(char cCharacter)
        {
            char[] cNumbers = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            return IsValidCharacter(cNumbers, cCharacter);
        }
        private bool IsAlphaCharacter(bool bCheckCaps, char cCharacter)
        {
            char[] cAlpha;

            if (bCheckCaps)
                cAlpha = new char[] {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J'
                                      , 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T'
                                      , 'U', 'V', 'W', 'X', 'Y', 'Z'};
            else
                cAlpha = new char[] {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j'
                                      , 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't'
                                      , 'u', 'v', 'w', 'x', 'y', 'z'};
            return IsValidCharacter(cAlpha, cCharacter);
        }
        private bool IsSpecialCharacter(char cCharacter)
        {
            char[] cSpecialCharacters = new char[] { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')' };
            return IsValidCharacter(cSpecialCharacters, cCharacter);
        }
        private string EncryptNumberCombination(char cCharacter)
        {
            Dictionary<char, string> NumberDictionary = new Dictionary<char, string>();
            NumberDictionary['0'] = "?*<@";
            NumberDictionary['1'] = ">w3P";
            NumberDictionary['2'] = "q(^!";
            NumberDictionary['3'] = "#SaG";
            NumberDictionary['4'] = "$%&c";
            NumberDictionary['5'] = "GtlM";
            NumberDictionary['6'] = "8zWt";
            NumberDictionary['7'] = ".<]{";
            NumberDictionary['8'] = "kIfT";
            NumberDictionary['9'] = "CXte";

            return NumberDictionary[cCharacter];
        }
        private string EncryptLowAlphaCombination(char cCharacter)
        {
            Dictionary<char, string> NumberDictionary = new Dictionary<char, string>();
            NumberDictionary['a'] = "<@?*";
            NumberDictionary['b'] = "3P>w";
            NumberDictionary['c'] = "^!q(";
            NumberDictionary['d'] = "aG#S";
            NumberDictionary['e'] = "&c$%";
            NumberDictionary['f'] = "lMGt";
            NumberDictionary['g'] = "Wt8z";
            NumberDictionary['h'] = "]{.<";
            NumberDictionary['i'] = "fTkI";
            NumberDictionary['j'] = "teCX";
            NumberDictionary['k'] = "?@*<";
            NumberDictionary['l'] = ">Pw3";
            NumberDictionary['m'] = "q!(^";
            NumberDictionary['n'] = "#GSa";
            NumberDictionary['o'] = "$c%&";
            NumberDictionary['p'] = "GMtl";
            NumberDictionary['q'] = "8tzW";
            NumberDictionary['r'] = ".{<]";
            NumberDictionary['s'] = "kTIf";
            NumberDictionary['t'] = "CeXt";
            NumberDictionary['u'] = "(^q!";
            NumberDictionary['v'] = "Sa#G";
            NumberDictionary['w'] = "%&$c";
            NumberDictionary['x'] = "tlGM";
            NumberDictionary['y'] = "zW8t";
            NumberDictionary['z'] = "<].{";

            return NumberDictionary[cCharacter];
        }
        private string EncryptHighAlphaCombination(char cCharacter)
        {
            Dictionary<char, string> NumberDictionary = new Dictionary<char, string>();
            NumberDictionary['A'] = "*@?<";
            NumberDictionary['B'] = "wP>3";
            NumberDictionary['C'] = "(!q^";
            NumberDictionary['D'] = "SG#a";
            NumberDictionary['E'] = "%c$&";
            NumberDictionary['F'] = "tMGl";
            NumberDictionary['G'] = "zt8W";
            NumberDictionary['H'] = "<{.]";
            NumberDictionary['I'] = "ITkf";
            NumberDictionary['J'] = "XeCt";
            NumberDictionary['K'] = "<@*?";
            NumberDictionary['L'] = "3Pw>";
            NumberDictionary['M'] = "^!(q";
            NumberDictionary['N'] = "aGS#";
            NumberDictionary['O'] = "&c%$";
            NumberDictionary['P'] = "lMtG";
            NumberDictionary['Q'] = "Wtz8";
            NumberDictionary['R'] = "]{<.";
            NumberDictionary['S'] = "fTIk";
            NumberDictionary['T'] = "teXC";
            NumberDictionary['U'] = "!^q(";
            NumberDictionary['V'] = "Ga#S";
            NumberDictionary['W'] = "c&$%";
            NumberDictionary['X'] = "MlGt";
            NumberDictionary['Y'] = "tW8z";
            NumberDictionary['Z'] = "{].<";

            return NumberDictionary[cCharacter];
        }
        private string EncryptSpecialCharacterCombination(char cCharacter)
        {
            Dictionary<char, string> NumberDictionary = new Dictionary<char, string>();
            NumberDictionary['!'] = "0a3g";
            NumberDictionary['@'] = "6ynb";
            NumberDictionary['#'] = "3(^t";
            NumberDictionary['$'] = "#%,G";
            NumberDictionary['%'] = "5tDg";
            NumberDictionary['^'] = "37uI";
            NumberDictionary['&'] = "B71W";
            NumberDictionary['*'] = ".rh{";
            NumberDictionary['('] = "3If8";
            NumberDictionary[')'] = "rkte";

            return NumberDictionary[cCharacter];
        }
        private char DecryptCharacterCombination(string sCombination)
        {
            Dictionary<string, char> NumberDictionary = new Dictionary<string, char>();
            NumberDictionary["?*<@"] = '0';
            NumberDictionary[">w3P"] = '1';
            NumberDictionary["q(^!"] = '2';
            NumberDictionary["#SaG"] = '3';
            NumberDictionary["$%&c"] = '4';
            NumberDictionary["GtlM"] = '5';
            NumberDictionary["8zWt"] = '6';
            NumberDictionary[".<]{"] = '7';
            NumberDictionary["kIfT"] = '8';
            NumberDictionary["CXte"] = '9';
            NumberDictionary["<@?*"] = 'a';
            NumberDictionary["3P>w"] = 'b';
            NumberDictionary["^!q("] = 'c';
            NumberDictionary["aG#S"] = 'd';
            NumberDictionary["&c$%"] = 'e';
            NumberDictionary["lMGt"] = 'f';
            NumberDictionary["Wt8z"] = 'g';
            NumberDictionary["]{.<"] = 'h';
            NumberDictionary["fTkI"] = 'i';
            NumberDictionary["teCX"] = 'j';
            NumberDictionary["?@*<"] = 'k';
            NumberDictionary[">Pw3"] = 'l';
            NumberDictionary["q!(^"] = 'm';
            NumberDictionary["#GSa"] = 'n';
            NumberDictionary["$c%&"] = 'o';
            NumberDictionary["GMtl"] = 'p';
            NumberDictionary["8tzW"] = 'q';
            NumberDictionary[".{<]"] = 'r';
            NumberDictionary["kTIf"] = 's';
            NumberDictionary["CeXt"] = 't';
            NumberDictionary["(^q!"] = 'u';
            NumberDictionary["Sa#G"] = 'v';
            NumberDictionary["%&$c"] = 'w';
            NumberDictionary["tlGM"] = 'x';
            NumberDictionary["zW8t"] = 'y';
            NumberDictionary["<].{"] = 'z';
            NumberDictionary["*@?<"] = 'A';
            NumberDictionary["wP>3"] = 'B';
            NumberDictionary["(!q^"] = 'C';
            NumberDictionary["SG#a"] = 'D';
            NumberDictionary["%c$&"] = 'E';
            NumberDictionary["tMGl"] = 'F';
            NumberDictionary["zt8W"] = 'G';
            NumberDictionary["<{.]"] = 'H';
            NumberDictionary["ITkf"] = 'I';
            NumberDictionary["XeCt"] = 'J';
            NumberDictionary["<@*?"] = 'K';
            NumberDictionary["3Pw>"] = 'L';
            NumberDictionary["^!(q"] = 'M';
            NumberDictionary["aGS#"] = 'N';
            NumberDictionary["&c%$"] = 'O';
            NumberDictionary["lMtG"] = 'P';
            NumberDictionary["Wtz8"] = 'Q';
            NumberDictionary["]{<."] = 'R';
            NumberDictionary["fTIk"] = 'S';
            NumberDictionary["teXC"] = 'T';
            NumberDictionary["!^q("] = 'U';
            NumberDictionary["Ga#S"] = 'V';
            NumberDictionary["c&$%"] = 'W';
            NumberDictionary["MlGt"] = 'X';
            NumberDictionary["tW8z"] = 'Y';
            NumberDictionary["{].<"] = 'Z';
            NumberDictionary["0a3g"] = '!';
            NumberDictionary["6ynb"] = '@';
            NumberDictionary["3(^t"] = '#';
            NumberDictionary["#%,G"] = '$';
            NumberDictionary["5tDg"] = '%';
            NumberDictionary["37uI"] = '^';
            NumberDictionary["B71W"] = '&';
            NumberDictionary[".rh{"] = '*';
            NumberDictionary["3If8"] = '(';
            NumberDictionary["rkte"] = ')';

            return NumberDictionary[sCombination];
        }

        public void VerifyFileExistence(string sNewCode)
        {
            string sDatFileName = "../Data/VoelParadysData.dat";
            FileStream myFileStream;

            if (!File.Exists(sDatFileName))
            {
                myFileStream = File.Create(sDatFileName);
                string sEncrypted = EncryptKeyCode(sNewCode);
                byte[] HashCodeInfo = new UTF8Encoding(true).GetBytes(sEncrypted);
                myFileStream.Write(HashCodeInfo, 0, HashCodeInfo.Length);
                myFileStream.Flush();
                myFileStream.Close();
            }
        }

        private void WriteToFile(string sHashCode)
        {
            string sDatFileName = "../Data/VoelParadysData.dat";
            FileStream myFileStream;

            if (File.Exists(sDatFileName))
                myFileStream = File.OpenWrite(sDatFileName);
            else
                myFileStream = File.Create(sDatFileName);

            byte[] HashCodeInfo = new UTF8Encoding(true).GetBytes(sHashCode);
            myFileStream.Write(HashCodeInfo, 0, HashCodeInfo.Length);
            myFileStream.Flush();
            myFileStream.Close();
        }
        private void ReadFromFile(ref string sHashCode)
        {
            sHashCode = "";
            string sDatFileName = "../Data/VoelParadysData.dat";
            FileStream myFileStream = File.OpenRead(sDatFileName);
            byte[] b = new byte[64];
            UTF8Encoding TempHashCode = new UTF8Encoding(true);
            while (myFileStream.Read(b, 0, b.Length) > 0)
            {
                char[] acEncrypted = TempHashCode.GetChars(b);
                for (int i = 0; i < acEncrypted.Length; ++i)
                {
                    if (acEncrypted[i] != '\0')
                        sHashCode += acEncrypted[i];
                }
            }
            myFileStream.Flush();
            myFileStream.Close();
        }
        private string EncryptKeyCode(string sCode)
        {
            // Here we want to map each character in the entered string with 4 other characters.
            string sHashCoded = "";

            for (int i = 0; i < sCode.Length; ++i)
            {
                if (IsNumberCharacter(sCode[i]))
                    sHashCoded += EncryptNumberCombination(sCode[i]);
                else if (IsAlphaCharacter(false, sCode[i]))
                    sHashCoded += EncryptLowAlphaCombination(sCode[i]);
                else if (IsAlphaCharacter(true, sCode[i]))
                    sHashCoded += EncryptHighAlphaCombination(sCode[i]);
                else if (IsSpecialCharacter(sCode[i]))
                    sHashCoded += EncryptSpecialCharacterCombination(sCode[i]);
                else
                {
                    string sMessage = "Illegal character entered for the password";
                    string sCaption = "Illegal Input";
                    MessageBox.Show(sMessage, sCaption);
                }
            }
            return sHashCoded;    
        }
        private string DecryptKeyCode(string sHashCode)
        {
            string sPassword = "";
            // We need to split the hash code into 4 letter strings to decrypt it
            for (int i = 0; i < sHashCode.Length; i += 4)
            {
                string sSplitString = sHashCode.Substring(i, 4); ;
                sPassword += DecryptCharacterCombination(sSplitString);
            }
            return sPassword;
        }
        public bool IsPasswordMatch(string sPassword)
        {
           // We should actually be getting the saved password here and comparing it to sPassword.
           // I am currently just busy checking my logic.
            string sSavedPassword = "";
            ReadFromFile(ref sSavedPassword);
            string sEncryptedPassword = EncryptKeyCode(sPassword);
            return sEncryptedPassword == sSavedPassword;
        }
        public void VerifyOldAddNew(string sOld, string sNew)
        {
            if (IsPasswordMatch(sOld))
                WriteToFile(EncryptKeyCode(sNew));
        }
    }
}
