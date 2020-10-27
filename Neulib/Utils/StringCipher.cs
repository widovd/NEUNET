using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Linq;

namespace Neulib.Utils
// http://stackoverflow.com/questions/10168240/encrypting-decrypting-a-string-in-c-sharp

{

    public static class StringCipher
    {
        private const int keySize = 256;

        public static string Encrypt(string text, string passPhrase, byte[] saltBytes, byte[] ivBytes)
        {
            byte[] textBytes = Encoding.UTF8.GetBytes(text);
            string cipherText;
            using (Rfc2898DeriveBytes password = new Rfc2898DeriveBytes(passPhrase, saltBytes))
            {
                var keyBytes = password.GetBytes(keySize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivBytes))
                    using (var memoryStream = new MemoryStream())
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(textBytes, 0, textBytes.Length);
                        cryptoStream.FlushFinalBlock();
                        cipherText = Convert.ToBase64String(memoryStream.ToArray());
                    }
                }
            }
            return cipherText;
        }

        public static string Encrypt(string text, string passPhrase, string salt, string iv)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);
            byte[] ivBytes = Convert.FromBase64String(iv);
            return Encrypt(text, passPhrase, saltBytes, ivBytes);
        }

        public static string Decrypt(string cipherText, string passPhrase, byte[] saltBytes, byte[] ivBytes)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            string text;
            using (var password = new Rfc2898DeriveBytes(passPhrase, saltBytes))
            {
                var keyBytes = password.GetBytes(keySize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivBytes))
                    using (var memoryStream = new MemoryStream(cipherBytes))
                    using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        byte[] textBytes = new byte[cipherBytes.Length];
                        int byteCount = cryptoStream.Read(textBytes, 0, textBytes.Length);
                        text = Encoding.UTF8.GetString(textBytes, 0, byteCount);
                    }
                }
            }
            return text;
        }

        public static string Decrypt(string cipherText, string passPhrase, string salt, string iv)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);
            byte[] ivBytes = Convert.FromBase64String(iv);
            return Decrypt(cipherText, passPhrase, saltBytes, ivBytes);
        }

            public static byte[] Generate32Bytes(int byteCount)
        {
            var randomBytes = new byte[byteCount]; // 32 Bytes will give us 256 bits.
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                // Fill the array with cryptographically secure random bytes.
                rngCsp.GetBytes(randomBytes);
            }
            return randomBytes;
        }

        public static string Generate32BytesString(int byteCount, bool remove)
        {
            byte[] bytes = Generate32Bytes(byteCount);
            string text = Convert.ToBase64String(bytes, 0, bytes.Length);

            if (remove)
            {
                int i = text.IndexOf('=');
                if (i > 0) text = text.Substring(0, i);
            }
            return text;
        }

    }
}

