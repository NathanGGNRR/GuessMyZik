using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;

namespace GuessMyZik.Classes
{
    /// <summary>
    /// Provides symmetric encryption and decryption services
    /// </summary>
    public class SymmetricEncryption
    {
        private readonly IBuffer buffGenerated;
        private readonly CryptographicKey cryptographicKey;
        private readonly string algorithmName;
        private readonly SymmetricKeyAlgorithmProvider cryptingProvider;
        private readonly string keyString = "guessmyzik-crypt"; //Encryption key.

        /// <summary>
        /// Instantiate with a random generated buffer (not an option if
        /// you want to persist the encryption to disk)
        /// </summary>
        public SymmetricEncryption()
        {
            algorithmName = SymmetricAlgorithmNames.Rc2CbcPkcs7;
            cryptingProvider = SymmetricKeyAlgorithmProvider.OpenAlgorithm(algorithmName);
            buffGenerated = CryptographicBuffer.CreateFromByteArray(Encoding.Unicode.GetBytes(keyString));
            cryptographicKey = cryptingProvider.CreateSymmetricKey(buffGenerated);
        }

        private bool IsMultipleOfBlockLength(IBuffer binaryData)
        {
            return (binaryData.Length % cryptingProvider.BlockLength) != 0;
        }
        /// <summary>
        /// Encrypts a given string
        /// </summary>
        /// <param name="data">Data to be encrypted</param>
        /// <returns>An encrypted string in Unicode</returns>
        public string Encrypt(string data)
        {
            var binaryData = Encoding.Unicode.GetBytes(data).AsBuffer();
            if (!algorithmName.Contains("PKCS7") && IsMultipleOfBlockLength(binaryData))
                throw new Exception("Message buffer length must be multiple of block length !!");
            var encryptedBinaryData = CryptographicEngine.Encrypt(cryptographicKey, binaryData, null);
            return Encoding.Unicode.GetString(encryptedBinaryData.ToArray());
        }
        /// <summary>
        /// Decrypts a string in Unicode
        /// </summary>
        /// <param name="encryptedData">An encrypted string in Unicode</param>
        /// <returns>The decrypted string in Unicode</returns>
        public string Decrypt(string encryptedData)
        {
            var encryptedBinaryData = Encoding.Unicode.GetBytes(encryptedData).AsBuffer();
            var decryptedData = CryptographicEngine.Decrypt(cryptographicKey, encryptedBinaryData, null);
            return Encoding.Unicode.GetString(decryptedData.ToArray());
        }
    }
}