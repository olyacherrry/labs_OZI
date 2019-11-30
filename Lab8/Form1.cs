using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab8
{
    public partial class Form1 : Form
    {
        private MySign mySign;
        private RSAParameters rsaParameters;
        private Byte[] hash;
        private Byte[] signature;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mySign = new MySign();
        }

        private void signButton_Click(object sender, EventArgs e)
        {
            hash = mySign.ComputeHash(Encoding.UTF8.GetBytes(textBox.Text.ToString()));
            //infoTextBox.Text = hash.ToString();
            hashTextBox.Text = ToHexString(hash);
            signature = mySign.SignHash(hash);
            signTextBox.Text = ToString(signature);
            rsaParameters = mySign.ExportParameters(false);
            ShowInfo(infoTextBox);
        }


        private void ShowInfo(TextBox textBox)
        {
            textBox.Text = String.Empty;
            textBox.Text += "During Hash Size :" + hash.Length + " bytes |" + hash.Length * 8 + "  bits " + Environment.NewLine;
            textBox.Text += "During Key Size : " + mySign.GetKeySize() / 8 + "bytes |" + mySign.GetKeySize().ToString() + " bits" + Environment.NewLine;
            KeySizes[] legalKeySizes = mySign.GetPossibleKeySizes();
            if (legalKeySizes.Length > 0)
            {
                for (int i = 0; i < legalKeySizes.Length; i++)
                {
                    textBox.Text += "Keysize{i} min, max, step: " + legalKeySizes[i].MinSize + " " + legalKeySizes[i].MaxSize + " " + legalKeySizes[i].SkipSize + " bits" + Environment.NewLine;
                }
            }
        }

        private String ToHexString(byte[] array)
        {
            StringBuilder hex = new StringBuilder();
            foreach (Byte b in array)
            {
                hex.AppendFormat("{0:x2} ", b);
            }
            return hex.ToString().Trim();
        }

        private String ToString(byte[] array)
        {
            return String.Join(" ", array);
        }

       
    }
}
