using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ECB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Clear()
        {
            logBox.Text = "";
            encryptedText.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clear();
            if (plainText.Text.ToString().Length == 0)
            {
                MessageBox.Show("Input text", "Error");
                return;
            }
            if (keyBox.Text.ToString().Length == 0)
            {
                MessageBox.Show("Input key", "Error");
                return;
            }
            if (nunceBox.Text.ToString().Length == 0)
            {
                MessageBox.Show("Input counter", "Error");
                return;
            }
            CTRCrypt(plainText.Text,keyBox.Text, nunceBox.Text);
           
        }

        private void CTRCrypt(string text, string key, string nunce)
        {
            StringBuilder encryptedTextBuilder = new StringBuilder();
            StringBuilder logBuilder = new StringBuilder();
            IList<BitArray> textBits = ParseCryptedText(text) ? GetBitArrayFromBits(text) : GetBitArrayFromCharacters(text);
            for (int i = 0; i < textBits.Count; i++)
            {
                Tuple<BitArray, String> tuple = SDES.MakeS_DES(textBits[i], key, nunce, i, radioButton1.Checked, radioButton4.Checked);

                encryptedTextBuilder.Append(SDES.ToString(tuple.Item1));
                logBuilder.Append(tuple.Item2);
            }
            encryptedText.Text = encryptedTextBuilder.ToString();
            logBox.Text = logBuilder.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clear();
        }
        
        private bool ParseCryptedText(string text)
        {
            bool parseResult = false;
            Regex regex = new Regex("^[0-1]+$");
            if (regex.IsMatch(text))
            {
                parseResult = true;
            }
            return parseResult;
        }

        private IList<BitArray> GetBitArrayFromCharacters(string text)
        {
            IList<BitArray> textBits = new List<BitArray>();
            foreach(char character in text)
            {
                BitArray characterBits = new BitArray(new byte[] { (byte)character});
                characterBits = SDES.Reverse(characterBits);
                textBits.Add(characterBits);
            }
            return textBits;
        }

        private IList<BitArray> GetBitArrayFromBits(string text)
        {
            IList<BitArray> textBits = new List<BitArray>();
            int blockLength = 8;
            for (int i = 0; i < text.Length; i += blockLength)
            {
                int difference = text.Length - i;
                string textBitsPart = null;
                if (difference > blockLength)
                {
                    textBitsPart = text.Substring(i, blockLength);
                }
                else
                {
                    textBitsPart = text.Substring(i, difference) + new String('0', blockLength - difference);
                }
                BitArray textBit = new BitArray(blockLength);
                for (int j = 0; j < blockLength; j++)
                {
                    textBit.Set(j, textBitsPart[j].Equals('1') ? true : false);
                }
                textBits.Add(textBit);
            }
            return textBits;
        }

        public void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
                label2.Visible = radioButton4.Checked;
                nunceBox.Visible = radioButton4.Checked;
        }
    }
}
