using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Lab3
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button3_Click(sender, e);
            if (openTextBox.Text.ToString().Length == 0)
            {
                MessageBox.Show("Input text", "Error");
                return;
            }
            if (keyBox.Text.ToString().Length == 0)
            {
                MessageBox.Show("Input key", "Error");
                return;
            }
            Tuple<List<BitArray>, String> tuple = S_DES.MakeS_DES(
                openTextBox.Text.ToString(),
                keyBox.Text.ToString(),
                radioButton1.Checked
                );
            closeTextBox.Text += S_DES.ToString(tuple.Item1);
            logBox.Text += tuple.Item2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            logBox.Text = String.Empty;
            closeTextBox.Text = String.Empty;
        }

        
    }
}
