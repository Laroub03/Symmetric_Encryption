using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace Symmetric_Encryption
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Display the encryption algorithm menu and get user's choice.
                ConsoleUI.DisplayMenu();
                Console.Write("Select choice: ");
                int choice;

                // Try parsing the user's choice as an integer.
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid choice.");
                    return;
                }

                SymmetricAlgorithm symmetricAlgorithm;

                // Initialize the chosen symmetric encryption algorithm based on user input.
                switch (choice)
                {
                    case 1:
                        symmetricAlgorithm = Aes.Create();  // Advanced Encryption Standard (AES)
                        break;
                    case 2:
                        symmetricAlgorithm = DES.Create();  // Data Encryption Standard (DES)
                        break;
                    case 3:
                        symmetricAlgorithm = TripleDES.Create();  // Triple DES
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        return;
                }

                EncryptionService encryptionService = new EncryptionService(symmetricAlgorithm);

                // Prompt the user to enter the message to encrypt.
                Console.Write("Enter message to encrypt: ");
                string message = Console.ReadLine();

                Stopwatch stopwatch = new Stopwatch();

                // Encrypt the user's message using the chosen algorithm.
                stopwatch.Start();
                byte[] ciphertextBytes = encryptionService.Encrypt(message);
                stopwatch.Stop();

                // Display encryption details including cipher text, key, IV, and encryption time.
                ConsoleUI.DisplayEncryptionDetails(ciphertextBytes, symmetricAlgorithm.Key, symmetricAlgorithm.IV, stopwatch.ElapsedMilliseconds);

                stopwatch.Restart();
                // Decrypt the encrypted message using the same algorithm.
                string decryptedMessage = encryptionService.Decrypt(ciphertextBytes);
                stopwatch.Stop();

                // Display decryption details including decrypted text and decryption time.
                ConsoleUI.DisplayDecryptionDetails(decryptedMessage, stopwatch.ElapsedTicks);
            }
            catch (Exception ex)
            {
                // Display an error message if an exception occurs.
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}