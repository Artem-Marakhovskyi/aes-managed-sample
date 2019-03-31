using System;
using System.Security.Cryptography;
using System.Text;

namespace Cryptor.Console
{
    public class Cryptor : ICryptor
    {
        private readonly RijndaelManaged _aesEncryption;
        private readonly ICryptoTransform _encryptor;
        private readonly ICryptoTransform _descryptor;

        public Cryptor(string key)
        {
            _aesEncryption = new RijndaelManaged
            {
                KeySize = 256,
                BlockSize = 128,
                Padding = PaddingMode.PKCS7,
                Key = SHA256.Create().ComputeHash(Encoding.BigEndianUnicode.GetBytes(key))
            };

            _encryptor = _aesEncryption.CreateEncryptor();
            _descryptor = _aesEncryption.CreateDecryptor();
        }

        public byte[] Encrypt(byte[] openBytes)
        {
            return _encryptor.TransformFinalBlock(openBytes, 0, openBytes.Length);
        }

        public byte[] Decrypt(byte[] cipheredBytes)
        {
            return _descryptor.TransformFinalBlock(
                    cipheredBytes, 
                    0,
                    cipheredBytes.Length);
        }

        public void Dispose()
        {
            _aesEncryption.Dispose();
            _descryptor.Dispose();
            _encryptor.Dispose();
        }
    }
}
