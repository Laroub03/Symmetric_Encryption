﻿using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Vælg en krypteringsalgoritme:");
        Console.WriteLine("1. AES");
        Console.WriteLine("2. DES");
        Console.WriteLine("3. TripleDES");
        Console.Write("Indtast dit valg: ");
        int choice = int.Parse(Console.ReadLine());

        SymmetricAlgorithm symmetricAlgorithm;

        switch (choice)
        {
            case 1:
                symmetricAlgorithm = Aes.Create();
                break;
            case 2:
                symmetricAlgorithm = DES.Create();
                break;
            case 3:
                symmetricAlgorithm = TripleDES.Create();
                break;
            default:
                Console.WriteLine("Ugyldigt valg.");
                return;
        }

        symmetricAlgorithm.GenerateKey();
        symmetricAlgorithm.GenerateIV();

        Console.Write("Indtast besked til kryptering: ");
        string message = Console.ReadLine();

        byte[] ciphertextBytes;

        Stopwatch stopwatch = new Stopwatch();

        using (ICryptoTransform encryptor = symmetricAlgorithm.CreateEncryptor())
        {
            byte[] plaintextBytes = Encoding.UTF8.GetBytes(message);

            stopwatch.Start();
            ciphertextBytes = encryptor.TransformFinalBlock(plaintextBytes, 0, plaintextBytes.Length);
            stopwatch.Stop();

            Console.WriteLine("\nKryptering:");
            Console.WriteLine("Plain Text (ASCII): " + message);
            Console.WriteLine("Plain Text (HEX): " + BitConverter.ToString(plaintextBytes).Replace("-", " "));
            Console.WriteLine("Cipher Text (ASCII): " + Convert.ToBase64String(ciphertextBytes));
            Console.WriteLine("Cipher Text (HEX): " + BitConverter.ToString(ciphertextBytes).Replace("-", " "));
            Console.WriteLine("Key (ASCII): " + Convert.ToBase64String(symmetricAlgorithm.Key));
            Console.WriteLine("Key (HEX): " + BitConverter.ToString(symmetricAlgorithm.Key).Replace("-", " "));
            Console.WriteLine("Initialization Vector (ASCII): " + Convert.ToBase64String(symmetricAlgorithm.IV));
            Console.WriteLine("Initialization Vector (HEX): " + BitConverter.ToString(symmetricAlgorithm.IV).Replace("-", " "));
            Console.WriteLine("Tid for kryptering: " + stopwatch.Elapsed);
        }

        using (ICryptoTransform decryptor = symmetricAlgorithm.CreateDecryptor())
        {
            stopwatch.Restart();
            byte[] decryptedBytes = decryptor.TransformFinalBlock(ciphertextBytes, 0, ciphertextBytes.Length);
            stopwatch.Stop();

            string decryptedMessage = Encoding.UTF8.GetString(decryptedBytes);

            Console.WriteLine("\nDekryptering:");
            Console.WriteLine("Decrypted Text: " + decryptedMessage);
            Console.WriteLine("Tid for dekryptering: " + stopwatch.Elapsed);
        }
    }
}