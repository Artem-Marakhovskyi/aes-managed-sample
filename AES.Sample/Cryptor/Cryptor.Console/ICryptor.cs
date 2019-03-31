using System;
namespace Cryptor.Console
{
    public interface ICryptor : IDisposable
    {
        byte[] Encrypt(byte[] openText);

        byte[] Decrypt(byte[] cipheredText);
    }
}
