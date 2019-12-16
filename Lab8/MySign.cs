using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lab8
{
    class MySign
    {
        private RSACryptoServiceProvider asymmetricAlgorithm;
        private SHA512CryptoServiceProvider hashAlgorithm;
        private String hashAlgorithmName;

        public MySign()
        {
            asymmetricAlgorithm = new RSACryptoServiceProvider();
            hashAlgorithm = new SHA512CryptoServiceProvider();
            hashAlgorithmName = "SHA512";
        }

        public Byte[] ComputeHash(Byte[] textBytes)
        {
            return hashAlgorithm.ComputeHash(textBytes);
        }

        public Byte[] SignHash(Byte[] hash)
        {
            return asymmetricAlgorithm.SignHash(hash, CryptoConfig.MapNameToOID(hashAlgorithmName));
            //верифицирует заданную подпись, сравнивая ее с подписью, вычисленной для заданного хеша
        }

        public RSAParameters ExportParameters(Boolean IsPrivateKey) //Экспортирует параметры DSAв объект DSAParameters
        {
            return asymmetricAlgorithm.ExportParameters(IsPrivateKey);
        }

        public void ImportParameters(RSAParameters rsaParameters)//Импортирует параметры DSAиз объектаDSAParameters
        {
            asymmetricAlgorithm.ImportParameters(rsaParameters);
        }

        public Boolean VerifyHash(Byte[] hash, Byte[] signature) //Верифицирует заданную подпись, сравнивая ее с подписью,вычисленной для заданного хеша
        {
            return asymmetricAlgorithm.VerifyHash(hash, CryptoConfig.MapNameToOID(hashAlgorithmName), signature);
            //Для передачи информации между методами создания и верификации подписи используются три параметра:
            //1) text–строка, содержащая исходный текст сообщения.
            //2) RSAParametersrsaparams - объект, инкапсулирующий информацию открытого ключа.
            //3) byte[] signaturebytes – массив, содержащий цифровую подпись сообщения.
        }

        public Int32 GetKeySize()
        {
            return asymmetricAlgorithm.KeySize;
        }

        public KeySizes[] GetPossibleKeySizes()
        {
            return asymmetricAlgorithm.LegalKeySizes;
        }

    }
}

