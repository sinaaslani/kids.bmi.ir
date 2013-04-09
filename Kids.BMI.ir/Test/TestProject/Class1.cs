using System;
using System.IO;
using System.Security.Cryptography;

namespace TestProject1
{


    public class Cryptography
    {
        public static RSACryptoServiceProvider rsa;

        public static void AssignParameter()
        {
            const int PROVIDER_RSA_FULL = 1;
            const string CONTAINER_NAME = "SpiderContainer";
            CspParameters cspParams = new CspParameters(PROVIDER_RSA_FULL);
            cspParams.KeyContainerName = CONTAINER_NAME;
            cspParams.Flags = CspProviderFlags.UseMachineKeyStore;
            cspParams.ProviderName = "Microsoft Strong Cryptographic Provider";
            rsa = new RSACryptoServiceProvider(cspParams);
        }

        public static string EncryptData(string data2Encrypt)
        {
            AssignParameter();
            StreamReader reader = new StreamReader(@"C:\Inetpub\wwwroot\dotnetspiderencryption\publickey.xml");
            string publicOnlyKeyXML = reader.ReadToEnd();
            rsa.FromXmlString(publicOnlyKeyXML);
            reader.Close();

            //read plaintext, encrypt it to ciphertext

            byte[] plainbytes = System.Text.Encoding.UTF8.GetBytes(data2Encrypt);
            byte[] cipherbytes = rsa.Encrypt(plainbytes, false);
            return Convert.ToBase64String(cipherbytes);
        }

        public static void AssignNewKey()
        {
            AssignParameter();

            //provide public and private RSA params
            StreamWriter writer = new StreamWriter(@"C:\privatekey.xml");
            string publicPrivateKeyXML = rsa.ToXmlString(true);
            writer.Write(publicPrivateKeyXML);
            writer.Close();

            //provide public only RSA params
            writer = new StreamWriter(@"C:\publickey.xml");
            string publicOnlyKeyXML = rsa.ToXmlString(false);
            writer.Write(publicOnlyKeyXML);
            writer.Close();

        }

        public static string DecryptData(string data2Decrypt)
        {
            AssignParameter();

            byte[] getpassword = Convert.FromBase64String(data2Decrypt);

            StreamReader reader = new StreamReader(@"C:\Inetpub\wwwroot\dotnetspiderencryption\privatekey.xml");
            string publicPrivateKeyXML = reader.ReadToEnd();
            rsa.FromXmlString(publicPrivateKeyXML);
            reader.Close();

            //read ciphertext, decrypt it to plaintext
            byte[] plain = rsa.Decrypt(getpassword, false);
            return System.Text.Encoding.UTF8.GetString(plain);

        }
    }

}
