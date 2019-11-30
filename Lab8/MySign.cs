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
        }

        public RSAParameters ExportParameters(Boolean IsPrivateKey)
        {
            return asymmetricAlgorithm.ExportParameters(IsPrivateKey);
        }

        public void ImportParameters(RSAParameters rsaParameters)
        {
            asymmetricAlgorithm.ImportParameters(rsaParameters);
        }

        public Boolean VerifyHash(Byte[] hash, Byte[] signature)
        {
            return asymmetricAlgorithm.VerifyHash(hash, CryptoConfig.MapNameToOID(hashAlgorithmName), signature);
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

