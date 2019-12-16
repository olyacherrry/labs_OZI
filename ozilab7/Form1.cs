using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//в машинном контейнере ключей, размер ключей в битах 2048 5 вар

namespace ozilab7
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public byte[] encryptedData;
        public byte[] decryptedData;
        RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(2048, RSAPersistKeyInCsp("MachineKeyStore"));
        UnicodeEncoding ByteConverter = new UnicodeEncoding();

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {    
                byte[] dataToEncrypt = ByteConverter.GetBytes(textBox1.Text);
                encryptedData = RSAEncrypt(dataToEncrypt, RSA.ExportParameters(false), false);
                textBox3.Text = ByteConverter.GetString(encryptedData);
                var p = RSA.ExportParameters(true);
                textBox2.Text = "Текущий размер ключа:" + RSA.KeySize.ToString() +" Модуль:" + p.Modulus[0].ToString() + " Показатель степени:" + p.Exponent[0].ToString();
                //textBox6.Text = RSA.LegalKeySizes[0].MinSize.ToString() + "..." + RSA.LegalKeySizes[0].MaxSize.ToString();
            }
            catch (ArgumentNullException)
            { 
                Console.WriteLine("Encryption failed.");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            decryptedData = RSADecrypt(encryptedData, RSA.ExportParameters(true), false);
            textBox5.Text = ByteConverter.GetString(decryptedData);
            var p = RSA.ExportParameters(true);
            //textBox2.Text += "\n  P:" + p.P[0].ToString() + " Q:" + p.Q[0].ToString() + " N:" + p.Modulus[0].ToString() + " d:" + p.D[0].ToString(); //параметры алгоритма rsa 
            /*textBox2.Text += p.P.LongLength;
            textBox2.Text += p.P.Length;*/
            textBox2.Text += " P: ";
            for(int i = 0; i < 50; i++)
            {
                textBox2.Text += p.P[i];
            }
            textBox2.Text += " Q: ";
            for (int i = 0; i < 50; i++)
            {
                textBox2.Text += p.Q[i];
            }
            textBox2.Text += " N: ";
            for (int i = 0; i < 50; i++)
            {
                textBox2.Text += p.Modulus[i];
            }

            textBox2.Text += " e: ";
            for (int i = 0; i < p.Exponent.Length; i++)
            {
                textBox2.Text += p.Exponent[i];
            }
            
            textBox2.Text += "d: ";
            for (int i = 0; i < 50; i++)
            {
                textBox2.Text += p.D[i];
            }
            textBox2.Text += "length "+  p.P.Length;
        }


        public static CspParameters RSAPersistKeyInCsp(string ContainerName)
        {
            try
            {
                CspParameters cspParams = new CspParameters();
                cspParams.Flags = CspProviderFlags.UseMachineKeyStore; //информация о ключах будет считываться из контейнера ключей компьютера.
                                cspParams.KeyContainerName = ContainerName;               
                return cspParams;
            }
            catch(CryptographicException e)
            {
                return null;
            }
            
        }

        static public byte[] RSAEncrypt(byte[] DataToEncrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] encryptedData;

                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(2048, RSAPersistKeyInCsp("MachineKeyStore")))
                {
                    RSA.ImportParameters(RSAKeyInfo);
                    encryptedData = RSA.Encrypt(DataToEncrypt, DoOAEPPadding);                  
                }
                return encryptedData;
            }

            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

        }

        public byte[] RSADecrypt(byte[] DataToDecrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] decryptedData;

                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(2048, RSAPersistKeyInCsp("KeyContainer")))
                {
                    RSA.ImportParameters(RSAKeyInfo); 
                    decryptedData = RSA.Decrypt(DataToDecrypt, DoOAEPPadding);
                }
                return decryptedData;
            }

            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
    }
}
