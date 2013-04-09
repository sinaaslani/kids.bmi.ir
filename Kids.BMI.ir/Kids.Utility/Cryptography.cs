using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Kids.Utility
{
    public static class CryptographyHelper
    {
        #region HashMode enum

        public enum HashMode
        {
            MD5,
            SHA1,
            HAMC
        } ;

        #endregion

        private static String ENCRYPTION()
        {
            return "@@[^&$جواد$&^]@@";
        }

        private static Byte[] ENCRYPTION_SALT()
        {
            StringBuilder a = new StringBuilder();
            a = new StringBuilder();
            a = new StringBuilder();
            a = new StringBuilder();
            return new Byte[] { 200, 10, 180, 45, 20, 254, 3, 144 };
        }



        public static String Encrypt(string inputText)
        {
            RijndaelManaged RijndaelCipher = new RijndaelManaged();
            Byte[] PlainText = Encoding.Unicode.GetBytes(inputText);

            //Rfc2898DeriveBytes pwdGen = new Rfc2898DeriveBytes(ENCRYPTION_KEY, ENCRYPTION_SALT);
            PasswordDeriveBytes pwdGen = new PasswordDeriveBytes(ENCRYPTION(), ENCRYPTION_SALT());
            byte[] Key = pwdGen.GetBytes(32);
            byte[] IV = pwdGen.GetBytes(16);

            using (ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(Key, IV))
            {
                using (MemoryStream MemoryStream = new MemoryStream())
                {
                    using (CryptoStream CryptoStream = new CryptoStream(MemoryStream, Encryptor, CryptoStreamMode.Write)
                        )
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

            //Rfc2898DeriveBytes pwdGen = new Rfc2898DeriveBytes(ENCRYPTION_KEY, ENCRYPTION_SALT, 1000);
            PasswordDeriveBytes pwdGen = new PasswordDeriveBytes(ENCRYPTION(), ENCRYPTION_SALT());
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

            //Rfc2898DeriveBytes pwdGen = new Rfc2898DeriveBytes(cryptoKey, SALT);
            PasswordDeriveBytes pwdGen = new PasswordDeriveBytes(cryptoKey, SALT);
            Byte[] Key = pwdGen.GetBytes(32);
            Byte[] IV = pwdGen.GetBytes(16);

            using (ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(Key, IV))
            {
                using (MemoryStream MemoryStream = new MemoryStream())
                {
                    using (CryptoStream CryptoStream = new CryptoStream(MemoryStream, Encryptor, CryptoStreamMode.Write)
                        )
                    {
                        CryptoStream.Write(PlainText, 0, PlainText.Length);
                        CryptoStream.FlushFinalBlock();
                        return Convert.ToBase64String(MemoryStream.ToArray());
                    }
                }
            }
        }
        public static string Encrypt(string InputText, string cryptoKey)
        {
            RijndaelManaged RijndaelCipher = new RijndaelManaged();
            Byte[] PlainText = Encoding.Unicode.GetBytes(InputText);

            //Rfc2898DeriveBytes pwdGen = new Rfc2898DeriveBytes(cryptoKey, SALT);
            PasswordDeriveBytes pwdGen = new PasswordDeriveBytes(cryptoKey, ENCRYPTION_SALT());
            Byte[] Key = pwdGen.GetBytes(32);
            Byte[] IV = pwdGen.GetBytes(16);

            using (ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(Key, IV))
            {
                using (MemoryStream MemoryStream = new MemoryStream())
                {
                    using (CryptoStream CryptoStream = new CryptoStream(MemoryStream, Encryptor, CryptoStreamMode.Write)
                        )
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

            //Rfc2898DeriveBytes pwdGen = new Rfc2898DeriveBytes(cryptoKey, SALT, 1000);
            PasswordDeriveBytes pwdGen = new PasswordDeriveBytes(cryptoKey, SALT);
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

        public static string Decrypt(string InputText, string cryptoKey)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            byte[] encryptedData = Convert.FromBase64String(InputText);

            //Rfc2898DeriveBytes pwdGen = new Rfc2898DeriveBytes(cryptoKey, SALT, 1000);
            PasswordDeriveBytes pwdGen = new PasswordDeriveBytes(cryptoKey, ENCRYPTION_SALT());
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




        public static String CalculateHash(String SourceString, String HashKey = null, HashMode encmode = HashMode.MD5)
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
                case HashMode.HAMC:
                    {
                        Byte[] SourceStringByte = Encoding.ASCII.GetBytes(SourceString);
                        Byte[] HashKeyByte = Encoding.ASCII.GetBytes(HashKey);

                        HMACMD5 hmacMD5 = new HMACMD5(HashKeyByte);
                        Byte[] computedHash = hmacMD5.ComputeHash(SourceStringByte);

                        StringBuilder s = new StringBuilder(2 * computedHash.Length);

                        foreach (byte t in computedHash)
                            s.AppendFormat("{0:x2}", t);

                        return s.ToString();
                    }
                default:
                    throw new NotSupportedException();
            }
        }


        //#region RSAHelper

        //const int RSAKeySize = 2048;

        //public static string RSAEncryptString(string InputString, string XMLPublicKeyString)
        //{
        //    RSACryptoServiceProvider rsaCryptoServiceProvider = new RSACryptoServiceProvider(RSAKeySize);
        //    rsaCryptoServiceProvider.FromXmlString(XMLPublicKeyString);
        //    int keySize = RSAKeySize / 8;
        //    byte[] bytes = Encoding.UTF32.GetBytes(InputString);
        //    // The hash function in use by the .NET RSACryptoServiceProvider here is SHA1
        //    // int maxLength = ( keySize ) - 2 - ( 2 * SHA1.Create().ComputeHash( rawBytes ).Length );
        //    int maxLength = keySize - 42;
        //    int dataLength = bytes.Length;
        //    int iterations = dataLength / maxLength;
        //    StringBuilder stringBuilder = new StringBuilder();
        //    for (int i = 0; i <= iterations; i++)
        //    {
        //        byte[] tempBytes = new byte[(dataLength - maxLength * i > maxLength) ? maxLength : dataLength - maxLength * i];
        //        Buffer.BlockCopy(bytes, maxLength * i, tempBytes, 0, tempBytes.Length);
        //        byte[] encryptedBytes = rsaCryptoServiceProvider.Encrypt(tempBytes, true);
        //        // Be aware the RSACryptoServiceProvider reverses the order of encrypted bytes after encryption and before decryption.
        //        // If you do not require compatibility with Microsoft Cryptographic API (CAPI) and/or other vendors.
        //        // Comment out the next line and the corresponding one in the DecryptString function.
        //        Array.Reverse(encryptedBytes);
        //        // Why convert to base 64?
        //        // Because it is the largest power-of-two base printable using only ASCII characters
        //        stringBuilder.Append(Convert.ToBase64String(encryptedBytes));
        //    }
        //    return stringBuilder.ToString();
        //}

        //public static string RSADecryptString(string InputString, string XMLPrivateKeyString)
        //{
        //    RSACryptoServiceProvider rsaCryptoServiceProvider = new RSACryptoServiceProvider(RSAKeySize);
        //    rsaCryptoServiceProvider.FromXmlString(XMLPrivateKeyString);
        //    const int base64BlockSize = ((RSAKeySize / 8) % 3 != 0) ? (((RSAKeySize / 8) / 3) * 4) + 4 : ((RSAKeySize / 8) / 3) * 4;
        //    int iterations = InputString.Length / base64BlockSize;
        //    ArrayList arrayList = new ArrayList();
        //    for (int i = 0; i < iterations; i++)
        //    {
        //        byte[] encryptedBytes = Convert.FromBase64String(InputString.Substring(base64BlockSize * i, base64BlockSize));
        //        // Be aware the RSACryptoServiceProvider reverses the order of encrypted bytes after encryption and before decryption.
        //        // If you do not require compatibility with Microsoft Cryptographic API (CAPI) and/or other vendors.
        //        // Comment out the next line and the corresponding one in the EncryptString function.
        //        Array.Reverse(encryptedBytes);
        //        arrayList.AddRange(rsaCryptoServiceProvider.Decrypt(encryptedBytes, true));
        //    }
        //    return Encoding.UTF32.GetString(arrayList.ToArray(Type.GetType("System.Byte")) as byte[]);
        //}

        //public static void RSAGenerateKeys(out string XMLPublicKey, out string XMLPulicPrivateKey)
        //{
        //    RSACryptoServiceProvider RSAProvider = new RSACryptoServiceProvider(RSAKeySize);
        //    XMLPulicPrivateKey = string.Format("<BitStrength>{0}</BitStrength>{1}", RSAKeySize, RSAProvider.ToXmlString(true));
        //    XMLPublicKey = string.Format("<BitStrength>{0}</BitStrength>{1}", RSAKeySize, RSAProvider.ToXmlString(false));

        //}

        //#endregion

        #region ISC HEXDigit DES Codeing
        public static string DES_HEXDigit_Encode(string input, string Key)
        {
            byte[] bytKey = ConvertToBytes(Key);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            byte[] Code = ConvertToBytes(input);
            byte[] Res1 = des.CreateEncryptor(bytKey, new byte[8]).TransformFinalBlock(Code, 0, Code.Length);

            string encrypted = Res1.Aggregate("", (current, b) => current + string.Format("{0:X2}", b));

            return encrypted.Substring(0, 16);
        }

        private static byte[] ConvertToBytes(string Key)
        {
            byte[] Res = new byte[8];
            for (int i = 0; i < Key.Length; i = i + 2)
            {
                string Block = Key.Substring(i, 2);
                Res[i / 2] = Convert.ToByte(Block, 16);
            }
            return Res;
        }



        public static string DESEncrypt(string originalString, string Key)
        {
            if (String.IsNullOrEmpty(originalString))
                throw new ArgumentNullException(@"The string which needs to be encrypted can not be null.");


            byte[] bytes = Encoding.ASCII.GetBytes(Key);

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateEncryptor(bytes, bytes), CryptoStreamMode.Write);

            StreamWriter writer = new StreamWriter(cryptoStream);
            writer.Write(originalString);
            writer.Flush();
            cryptoStream.FlushFinalBlock();
            writer.Flush();

            return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
        }


        public static string DESDecrypt(string cryptedString, string Key)
        {
            if (String.IsNullOrEmpty(cryptedString))
                throw new ArgumentNullException("The string which needs to be decrypted can not be null.");



            byte[] bytes = Encoding.ASCII.GetBytes(Key);

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(cryptedString));
            CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateDecryptor(bytes, bytes), CryptoStreamMode.Read);
            StreamReader reader = new StreamReader(cryptoStream);

            return reader.ReadToEnd();
        }


        #endregion




    }

    public static class ASymetricCryptoHelper
    {
        public static X509Certificate2 LoadCertificate(StoreName storeName, StoreLocation storeLocation, string certificateName)
        {
            X509Store store = new X509Store(storeName, storeLocation);

            try
            {
                store.Open(OpenFlags.ReadOnly);

                X509Certificate2Collection certificateCollection =
                    store.Certificates.Find(X509FindType.FindBySubjectName,
                    certificateName, false);
                if (certificateCollection.Count > 0)
                {
                    //  We ignore if there is more than one matching cert,    
                    //  we just return the first one.   
                    return certificateCollection[0];
                }
                else
                {
                    throw new ArgumentException("Certificate not found");
                }
            }
            finally
            {
                store.Close();
            }
        }


        public static string Sign(string data, X509Certificate2 certificate, object algorithm)
        {
            RSACryptoServiceProvider provider1 = (RSACryptoServiceProvider)certificate.PrivateKey;
            byte[] buffer1 = (new UnicodeEncoding()).GetBytes(data);
            byte[] result = provider1.SignData(buffer1, algorithm);
            string b64s = Convert.ToBase64String(result);
            return b64s;
        }

        public static bool Verify(string data, string signature, X509Certificate2 certificate, object algorithm)
        {
            RSACryptoServiceProvider provider1 = (RSACryptoServiceProvider)certificate.PublicKey.Key;
            byte[] buffer1 = (new UnicodeEncoding()).GetBytes(data);
            byte[] buffer2 = Convert.FromBase64String(signature);
            bool result = provider1.VerifyData(buffer1, algorithm, buffer2);
            return result;
        }

        public static string Decrypt(string data, X509Certificate2 certificate, Encoding coding = null)
        {
            if (coding == null)
                coding = Encoding.Unicode;
            RSACryptoServiceProvider provider1 = (RSACryptoServiceProvider)certificate.PrivateKey;
            byte[] buffer1 = Convert.FromBase64String(data);
            byte[] result = provider1.Decrypt(buffer1, false);
            return coding.GetString(result);
        }

        public static string Encrypt(string data, X509Certificate2 certificate, Encoding coding = null)
        {
            if (coding == null)
                coding = Encoding.Unicode;
            RSACryptoServiceProvider provider1 = (RSACryptoServiceProvider)certificate.PublicKey.Key;
            byte[] buffer1 = coding.GetBytes(data);
            byte[] result = provider1.Encrypt(buffer1, false);
            string b64s = Convert.ToBase64String(result);
            return b64s;
        }


        public static byte[] Encrypt(byte[] plainData, bool fOAEP,
            X509Certificate2 certificate)
        {
            if (plainData == null)
            {
                throw new ArgumentNullException("plainData");
            }
            if (certificate == null)
            {
                throw new ArgumentNullException("certificate");
            }

            using (RSACryptoServiceProvider provider = new RSACryptoServiceProvider())
            {
                // Note that we use the public key to encrypt   
                provider.FromXmlString(GetPublicKey(certificate));

                return provider.Encrypt(plainData, fOAEP);
            }
        }

        public static byte[] Decrypt(byte[] encryptedData, bool fOAEP,
            X509Certificate2 certificate)
        {
            if (encryptedData == null)
            {
                throw new ArgumentNullException("encryptedData");
            }
            if (certificate == null)
            {
                throw new ArgumentNullException("certificate");
            }

            using (RSACryptoServiceProvider provider = new RSACryptoServiceProvider())
            {
                // Note that we use the private key to decrypt   
                provider.FromXmlString(GetXmlKeyPair(certificate));

                return provider.Decrypt(encryptedData, fOAEP);
            }
        }

        private static string GetPublicKey(X509Certificate2 certificate)
        {
            if (certificate == null)
            {
                throw new ArgumentNullException("certificate");
            }

            return certificate.PublicKey.Key.ToXmlString(false);
        }

        private static string GetXmlKeyPair(X509Certificate2 certificate)
        {
            if (certificate == null)
            {
                throw new ArgumentNullException("certificate");
            }

            if (!certificate.HasPrivateKey)
            {
                throw new ArgumentException("certificate does not have a private key");
            }

            return certificate.PrivateKey.ToXmlString(true);

        }
    }
}