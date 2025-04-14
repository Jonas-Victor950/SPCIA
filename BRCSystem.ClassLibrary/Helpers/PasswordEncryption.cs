using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BRCSystem.ClassLibrary.Helpers
{
   public class PasswordEncryption
    {
        private static readonly string encryptionKey = "6094067890160940";

        public static string EncryptPassword(string password)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(encryptionKey);
                aesAlg.GenerateIV(); // Gera um novo vetor de inicialização (IV) aleatório

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                byte[] encryptedBytes;
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(password);
                        }
                    }
                    encryptedBytes = msEncrypt.ToArray();
                }

                // Combine IV e dados criptografados em uma única string
                byte[] combinedBytes = new byte[aesAlg.IV.Length + encryptedBytes.Length];
                aesAlg.IV.CopyTo(combinedBytes, 0);
                encryptedBytes.CopyTo(combinedBytes, aesAlg.IV.Length);

                return Convert.ToBase64String(combinedBytes);
            }
        }

        public static string DecryptPassword(string encryptedPassword)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(encryptionKey);

                byte[] combinedBytes = Convert.FromBase64String(encryptedPassword);

                byte[] iv = new byte[aesAlg.IV.Length];
                byte[] encryptedBytes = new byte[combinedBytes.Length - aesAlg.IV.Length];

                Array.Copy(combinedBytes, iv, iv.Length);
                Array.Copy(combinedBytes, iv.Length, encryptedBytes, 0, encryptedBytes.Length);

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, iv);

                using (MemoryStream msDecrypt = new MemoryStream(encryptedBytes))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}