using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Cryptor.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = null;
            Regex encryptRegex = new Regex(@"e\s\"".+\""\s\"".+\""");
            Regex decryptRegex = new Regex(@"d\s\"".+\""\s\"".+\""");
            Regex fullRegex = new Regex(@"r\s\"".+\""\s\"".+\""");
            do
            {
                System.Console.WriteLine(@"For encrypting input 'e ""open text"" ""key""'");
                System.Console.WriteLine(@"For decrypting input 'd ""ciphered text"" ""key""'");
                System.Console.WriteLine(@"For full cycle (decrypt+encrypt) input 'r ""open text"" ""key""'");
                input = System.Console.ReadLine();

                var match = input
                    .Split('"', '\'')
                    .Where(e => !string.IsNullOrWhiteSpace(e))
                    .ToArray();

                if (encryptRegex.IsMatch(input))
                {
                    Encrypt(match);
                }
                else if (decryptRegex.IsMatch(input))
                {
                    Decrypt(match);
                }
                else if (fullRegex.IsMatch(input))
                {
                    FullCycle(match);
                }

            } while (input != Environment.NewLine || !string.IsNullOrWhiteSpace(input));
        }

        private static void FullCycle(string[] v)
        {
            using (var cryptor = new StringCryptor(v[2]))
            {
                var encrypted = cryptor.Encrypt(v[1]);
                var decrypted = cryptor.Decrypt(encrypted);

                System.Console.WriteLine(
                    $"Before encryption: {v[1]}{Environment.NewLine}" +
                    	$"After encryption: {encrypted}{Environment.NewLine}" +
                    		$"After decryption: {decrypted}{Environment.NewLine}" +
                    			$"Key: {v[2]}{Environment.NewLine}");
                if (v[1] == decrypted)
                {
                    System.Console.WriteLine("Open texts are EQUAL. Success.");
                }
                else
                {
                    System.Console.WriteLine("Open texts are not EQUAL. Failure.");
                }
            }
        }

        private static void Decrypt(string[] v)
        {
            using (var cryptor = new StringCryptor(v[2]))
            {
                var decrypted = cryptor.Decrypt(v[1]);

                System.Console.WriteLine(
                    $"Before decryption: {v[1]}{Environment.NewLine}" +
                        $"After decryption: {decrypted}{Environment.NewLine}" +
                                $"Key: {v[2]}{Environment.NewLine}");
            }
        }

        private static void Encrypt(string[] v)
        {
            using (var cryptor = new StringCryptor(v[2]))
            {
                var encrypted = cryptor.Encrypt(v[1]);

                System.Console.WriteLine(
                    $"Before encryption: {v[1]}{Environment.NewLine}" +
                        $"After encryption: {encrypted}{Environment.NewLine}" +
                                $"Key: {v[2]}{Environment.NewLine}");
            }
        }
    }
}
