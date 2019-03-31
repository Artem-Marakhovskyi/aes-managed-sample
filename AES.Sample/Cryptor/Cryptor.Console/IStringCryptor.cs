using System;
namespace Cryptor.Console
{
    public interface IStringCryptor : IDisposable
    {
        string Encrypt(string openText);

        string Decrypt(string cipheredText);
    }
}
