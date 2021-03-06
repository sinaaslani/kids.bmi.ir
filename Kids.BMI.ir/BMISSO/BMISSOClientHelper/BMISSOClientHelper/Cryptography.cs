using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BMISSOClientHelper
{
    public static class CryptographyHelper
    {
        public enum HashMode
        {
            MD5,
            SHA1
        }

        private const String ENCRYPTION_KEY = "@@[^&$جواد$&^]@@"; 
        private const String ENCRYPTION_KEY_RFC289 = "@@[^&$جواد$&^]@@";
        private static readonly byte[] ENCRYPTION_SALT = { 200, 10, 180, 45, 20, 254, 3, 144 };


        public static String Encrypt(string inputText)
        {
            RijndaelManaged RijndaelCipher = new RijndaelManaged();
            Byte[] PlainText = Encoding.Unicode.GetBytes(inputText);

            Rfc2898DeriveBytes pwdGen = new Rfc2898DeriveBytes(ENCRYPTION_KEY, ENCRYPTION_SALT);
            byte[] Key = pwdGen.GetBytes(32);
            byte[] IV = pwdGen.GetBytes(16);


            using (ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(Key, IV))
            {
                using (MemoryStream MemoryStream = new MemoryStream())
                {
                    using (CryptoStream CryptoStream = new CryptoStream(MemoryStream, Encryptor, CryptoStreamMode.Write))
                    {
                        CryptoStream.Write(PlainText, 0, PlainText.Length);
                        CryptoStream.FlushFinalBlock();
                        return Convert.ToBase64String(MemoryStream.ToArray());

                    }
                }
            }
        }


        public static string Decrypt(string inputText)
        {

            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            Byte[] encryptedData = Convert.FromBase64String(inputText);

            Rfc2898DeriveBytes pwdGen = new Rfc2898DeriveBytes(ENCRYPTION_KEY_RFC289, ENCRYPTION_SALT);
            byte[] Key = pwdGen.GetBytes(32);
            byte[] IV = pwdGen.GetBytes(16);


            using (ICryptoTransform decryptor = rijndaelCipher.CreateDecryptor(Key, IV))
            {
                using (MemoryStream memoryStream = new MemoryStream(encryptedData))
                {
                    using (
                        CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read)
                        )
                    {
                        byte[] plainText = new Byte[encryptedData.Length];
                        int decryptedCount = cryptoStream.Read(plainText, 0, plainText.Length);
                        return Encoding.Unicode.GetString(plainText, 0, decryptedCount);

                    }

                }

            }
        }


        public static string Encrypt(string InputText, Byte[] SALT, string cryptoKey)
        {
            RijndaelManaged RijndaelCipher = new RijndaelManaged();
            Byte[] PlainText = Encoding.Unicode.GetBytes(InputText);

            Rfc2898DeriveBytes pwdGen = new Rfc2898DeriveBytes(ENCRYPTION_KEY_RFC289, ENCRYPTION_SALT);
            byte[] Key = pwdGen.GetBytes(32);
            byte[] IV = pwdGen.GetBytes(16);



            using (ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(Key, IV))
            {
                using (MemoryStream MemoryStream = new MemoryStream())
                {
                    using (CryptoStream CryptoStream = new CryptoStream(MemoryStream, Encryptor, CryptoStreamMode.Write))
                    {
                        CryptoStream.Write(PlainText, 0, PlainText.Length);
                        CryptoStream.FlushFinalBlock();
                        return Convert.ToBase64String(MemoryStream.ToArray());

                    }

                }
            }
        }


        public static string Decrypt(string InputText, Byte[] SALT, string cryptoKey)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            byte[] encryptedData = Convert.FromBase64String(InputText);

            Rfc2898DeriveBytes pwdGen = new Rfc2898DeriveBytes(cryptoKey, SALT);
            byte[] Key = pwdGen.GetBytes(32);
            byte[] IV = pwdGen.GetBytes(16);


            using (ICryptoTransform decryptor = rijndaelCipher.CreateDecryptor(Key, IV))
            {
                using (MemoryStream memoryStream = new MemoryStream(encryptedData))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        byte[] plainText = new Byte[encryptedData.Length];
                        int decryptedCount = cryptoStream.Read(plainText, 0, plainText.Length);
                        return Encoding.Unicode.GetString(plainText, 0, decryptedCount);

                    }

                }
            }
        }


        public static String CalculateHash(String SourceString, HashMode encmode)
        {
            Byte[] InputBytes = Encoding.UTF8.GetBytes(SourceString);
            switch (encmode)
            {
                case HashMode.MD5:
                    {
                        MD5CryptoServiceProvider s = new MD5CryptoServiceProvider();
                        byte[] ResultBytes = s.ComputeHash(InputBytes);
                        StringBuilder ret = new StringBuilder();
                        foreach (byte result in ResultBytes)
                            ret.Append(result.ToString("x2").ToLower());
                        return ret.ToString();
                    }
                case HashMode.SHA1:
                    {
                        SHA1CryptoServiceProvider s = new SHA1CryptoServiceProvider();
                        byte[] result = s.ComputeHash(InputBytes);
                        String ret = BitConverter.ToString(result);
                        ret = ret.Replace("-", "");
                        return ret;
                    }
            }

            return "";
        }
    }
}
