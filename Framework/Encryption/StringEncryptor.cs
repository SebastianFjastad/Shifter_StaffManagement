using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Framework.Encryption
{
    public static class StringEncryptor
    {
        private const string salt = "GRxdlvKS3Co4lEGKmOHBZv9cK41nuPY6xkv5VJN/t+1DOA==";
        
        // This size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be
        // 32 bytes long.  Using a 16 character string here gives us 32 bytes when converted to a byte array.
        private const string initVector = "dc39gcci35tx89m0";

        //private const string passPhrase = "9E22E5B126CD4837A974CB71759C7F62";
        private const string passPhrase = "GRXDLVKS3CO4LEGKMOHBZV9CK41NUPY6";

        // This constant is used to determine the keysize of the encryption algorithm.
        private const int keysize = 256;

        /// <summary>
        /// Overload for the Decrypt method which doesn't require a key and rather uses the default pass phrase
        /// </summary>
        public static string Decrypt(this string text)
        {
            return Decrypt(text, passPhrase);
        }

        /// <summary>
        /// Overload for the Encrypt method which doesn't require a key and rather uses the default pass phrase
        /// </summary>
        public static string Encrypt(this string text)
        {
            return Encrypt(text, passPhrase);
        }

        /// <summary>
        /// Encrypts a string using a passphrase
        /// </summary>
        public static string Encrypt(this string text, string encryptToken)
        {
            var encryptedText = string.Empty;

            using (var memoryStream = new MemoryStream())
            {
                using (var encryptor = CreateCipher(CipherType.Encryption, encryptToken))
                {
                    var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
                    var plainTextBytes = Encoding.Default.GetBytes(text);

                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    encryptedText = Convert.ToBase64String(Encoding.Default.GetBytes(Convert.ToBase64String(memoryStream.ToArray())));
                }
            }

            return encryptedText;
        }

        /// <summary>
        /// Decrypts a string using a passphrase
        /// </summary>     
        public static string Decrypt(this string text, string decryptToken)
        {
            var decryptedText = string.Empty;

            try
            {
                var cipherTextBytes = Convert.FromBase64String(Encoding.Default.GetString(Convert.FromBase64String(text)));

                using (var memoryStream = new MemoryStream(cipherTextBytes))
                {
                    using (var decryptor = CreateCipher(CipherType.Decryption, decryptToken))
                    {
                        var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

                        var plainTextBytes = new byte[cipherTextBytes.Length];
                        var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                        decryptedText = Encoding.Default.GetString(plainTextBytes, 0, decryptedByteCount);
                    }
                }
            }
            catch
            {
                return text;
            }

            return decryptedText;
        }

        private static ICryptoTransform CreateCipher(CipherType cipherType, string token)
        {
            byte[] keyBytes;
            var initVectorBytes = Encoding.Default.GetBytes(initVector);

            var saltBytes = Encoding.Default.GetBytes(salt);

            using (var password = new PasswordDeriveBytes(token, saltBytes))
            {
                keyBytes = password.GetBytes(keysize / 8);
            }

            using (var symmetricKey = new RijndaelManaged())
            {
                symmetricKey.Mode = CipherMode.CBC;

                if (cipherType == CipherType.Encryption)
                {
                    return symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
                }

                return symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            }
        }
    }

    enum CipherType { Encryption, Decryption }
}
