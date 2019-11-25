using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LR5_AES
{
    public partial class Form1 : Form
    {
        private const string ENCRYPTION_KEY = "testEnkriptionKey";
        private readonly static byte[] SALT = Encoding.ASCII.GetBytes(ENCRYPTION_KEY);

        private AesCryptoServiceProvider aesManaged = new AesCryptoServiceProvider();
        public Form1()
        {
            InitializeComponent();
        }

        private byte[] RandomKey()
        {
            int size = 16;
            Rfc2898DeriveBytes keyGenerator = new Rfc2898DeriveBytes(ENCRYPTION_KEY, SALT);
            if (comboBox2.Text == "128")
                size = 16;
            if (comboBox2.Text == "192")
                size = 24;
            if (comboBox2.Text == "256")
                size = 32;
            return keyGenerator.GetBytes(size);
        }
        private byte[] RandomIV()
        {

            Rfc2898DeriveBytes keyGenerator = new Rfc2898DeriveBytes(ENCRYPTION_KEY, SALT);
            return keyGenerator.GetBytes(16);
        }

        private void buttonEncrypt_Click(object sender, EventArgs e)
        {
            AesManaged myAes = new AesManaged
            {
                KeySize = Convert.ToInt32(comboBox2.Text),
                BlockSize = Convert.ToInt32(comboBox3.Text),
                //Mode = CipherMode.CBC,
                Padding = PaddingMode.None,
                Key = RandomKey(),
                IV = RandomIV()
            };

            byte[] encrypted = EncryptStringToBytes_Aes(richTextBox1.Text, myAes.Key, myAes.IV);

                richTextBox2.Text = Convert.ToBase64String(encrypted);
        }

        private void buttonDecrypt_Click(object sender, EventArgs e)
        {
            AesManaged myAes = new AesManaged
            {
                KeySize = Convert.ToInt32(comboBox2.Text),
                BlockSize = Convert.ToInt32(comboBox3.Text),
                Padding = PaddingMode.None,
                Key = RandomKey(),
                IV = RandomIV()
            };

            string roundtrip = DecryptStringFromBytes_Aes(Convert.FromBase64String(richTextBox1.Text), myAes.Key, myAes.IV);

            richTextBox2.Text = roundtrip;
        }

        static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;

            // Create an AesManaged object
            // with the specified key and IV.
            using (AesManaged aesAlg = new AesManaged())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }

        static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an AesManaged object
            // with the specified key and IV.
            using (AesManaged aesAlg = new AesManaged())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }
            return plaintext;
        }
    }
}
