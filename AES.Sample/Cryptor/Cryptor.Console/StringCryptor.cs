using System;
using System.Text;

namespace Cryptor.Console
{
    public class StringCryptor : IStringCryptor
    {
        private readonly ICryptor _cryptor;

        public StringCryptor(string key)
        {
            _cryptor = new Cryptor(key);
        }

        public string Decrypt(string cipheredText)
        {
            return GetString(_cryptor.Decrypt(Convert.FromBase64String(cipheredText)));
        }

        public string Encrypt(string openText)
        {
            return Convert.ToBase64String(_cryptor.Encrypt(ToBytes(openText)));
        }


        private byte[] ToBytes(string text) => Encoding.UTF8.GetBytes(text);

        private string GetString(byte[] bytes) => Encoding.UTF8.GetString(bytes);

        public void Dispose()
        {
            _cryptor.Dispose();
        }
    }
}
