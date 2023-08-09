using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace Symmetric_Encryption
{
    class ConsoleUI
    {
        public static void DisplayMenu()
        {
            // Display the encryption algorithm menu options.
            Console.WriteLine("Select an encryption algorithm: ");
            Console.WriteLine("1. AES");
            Console.WriteLine("2. DES");
            Console.WriteLine("3. TripleDES");
        }

        public static void DisplayEncryptionDetails(byte[] ciphertextBytes, byte[] key, byte[] iv, long encryptionTimeMs)
        {
            // Display details of the encryption process.
            Console.WriteLine("\nEncrypting:");
            Console.WriteLine("Cipher Text (ASCII): " + Convert.ToBase64String(ciphertextBytes));
            Console.WriteLine("Cipher Text (HEX): " + BitConverter.ToString(ciphertextBytes).Replace("-", " "));
            Console.WriteLine("Key (ASCII): " + Convert.ToBase64String(key));
            Console.WriteLine("Key (HEX): " + BitConverter.ToString(key).Replace("-", " "));
            Console.WriteLine("Initialization Vector (ASCII): " + Convert.ToBase64String(iv));
            Console.WriteLine("Initialization Vector (HEX): " + BitConverter.ToString(iv).Replace("-", " "));
            Console.WriteLine("Encryption time (ms): " + encryptionTimeMs);
        }

        public static void DisplayDecryptionDetails(string decryptedMessage, long decryptionTimeTicks)
        {
            // Display details of the decryption process.
            Console.WriteLine("\nDecrypting:");
            Console.WriteLine("Decrypted Text: " + decryptedMessage);
            Console.WriteLine("Decryption time (ticks): " + decryptionTimeTicks);
        }
    }
}