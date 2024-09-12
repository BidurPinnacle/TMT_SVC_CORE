using System.Security.Cryptography;
using System.Text;

namespace TMT_Code_Migration1.DataLogics.Utility
{
    public class ManagedAes
    {
        private readonly IConfiguration _configuration;

        public ManagedAes(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GetPlainText = string.Empty;
        public string GetEncryptedText = string.Empty;
        public ManagedAes(string plainText)
        {
            GetEncryptedText = EncryptDataWithAes(plainText);
        }
        public ManagedAes(string cipherText, string keyBase64, string vectorBase64)
        {
            GetPlainText = DecryptDataWithAes(cipherText, keyBase64, vectorBase64);
        }
        private static string EncryptDataWithAes(string plainText)
        {
            using (Aes aesAlgorithm = Aes.Create())
            {
                var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

                // Retrieve configuration values
                string keyBase64 = configuration.GetSection("KeyValues")["keyBase64"];
                string vectorBase64 = configuration.GetSection("KeyValues")["vectorBase64"];

                //string keyBase64 = _configuration.GetSection("KeyValues")["keyBase64"];
                //string vectorBase64 = _configuration.GetSection("KeyValues")["vectorBase64"];

                aesAlgorithm.Key = Encoding.UTF8.GetBytes(keyBase64);
                aesAlgorithm.IV = Encoding.UTF8.GetBytes(vectorBase64);

                // Create encryptor object
                ICryptoTransform encryptor = aesAlgorithm.CreateEncryptor();

                byte[] encryptedData;

                //Encryption will be done in a memory stream through a CryptoStream object
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(plainText);
                        }
                        encryptedData = ms.ToArray();
                    }
                }

                return Convert.ToBase64String(encryptedData);
            }
        }
        private static string DecryptDataWithAes(string cipherText, string keyBase64, string vectorBase64)
        {
            using (Aes aesAlgorithm = Aes.Create())
            {
                aesAlgorithm.Key = Encoding.UTF8.GetBytes(keyBase64);
                aesAlgorithm.IV = Encoding.UTF8.GetBytes(vectorBase64);

                byte[] buffer = Convert.FromBase64String(cipherText);
                MemoryStream memoryStream = new MemoryStream(buffer);
                ICryptoTransform decryptor = aesAlgorithm.CreateDecryptor(aesAlgorithm.Key, aesAlgorithm.IV);
                CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                StreamReader streamReader = new StreamReader(cryptoStream);
                return streamReader.ReadToEnd();

            }
        }
    }
}
