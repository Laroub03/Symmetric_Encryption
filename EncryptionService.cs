using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
namespace Symmetric_Encryption
{
    class EncryptionService
    {
        private readonly SymmetricAlgorithm _symmetricAlgorithm;

        public EncryptionService(SymmetricAlgorithm symmetricAlgorithm)
        {
            _symmetricAlgorithm = symmetricAlgorithm;
        }

        public byte[] Encrypt(string message)
        {
            // Generate a random key and initialization vector (IV).
            _symmetricAlgorithm.GenerateKey();
            _symmetricAlgorithm.GenerateIV();

            // Create an encryptor using the selected algorithm.
            using (ICryptoTransform encryptor = _symmetricAlgorithm.CreateEncryptor())
            {
                // Convert the message to bytes and perform encryption.
                byte[] plaintextBytes = Encoding.UTF8.GetBytes(message);
                return encryptor.TransformFinalBlock(plaintextBytes, 0, plaintextBytes.Length);
            }
        }

        public string Decrypt(byte[] ciphertextBytes)
        {
            // Create a decryptor using the selected algorithm.
            using (ICryptoTransform decryptor = _symmetricAlgorithm.CreateDecryptor())
            {
                // Perform decryption and convert the decrypted bytes to string.
                byte[] decryptedBytes = decryptor.TransformFinalBlock(ciphertextBytes, 0, ciphertextBytes.Length);
                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }
    }
}