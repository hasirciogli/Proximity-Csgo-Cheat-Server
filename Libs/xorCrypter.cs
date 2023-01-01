using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace xorCrypter
{
    public class openSSL
    {
        string key = "6485689462699694";
        string iv = "6658745596832456";

        public string crypt(string plaintext)
        {
            // Anahtar değerini belirtin
            string key = "PCMK";

            // Şifrelenmiş veri için yer ayırın
            string ciphertext = "";

            // Şifreleme işlemini gerçekleştirin
            for (int i = 0; i < plaintext.Length; i++)
            {
                ciphertext += (char)(plaintext[i] ^ key[i % key.Length]);
            }

            return ciphertext;
        }

        //public string EncryptStringToBytesAes(string plainText, string _key = "6485689462699694", string _iv = "6658745596832456")
        //{
        //    // Check arguments.
        //    if (plainText == null || plainText.Length <= 0)
        //        throw new ArgumentNullException("plainText");
        //    if (_key == null || _key.Length <= 0)
        //        throw new ArgumentNullException("key");
        //    if (_iv == null || _iv.Length <= 0)

        //        throw new ArgumentNullException("iv");


        //    byte[] key = Encoding.UTF8.GetBytes(_key);
        //    byte[] iv = Encoding.UTF8.GetBytes(_iv);


        //    // Declare the stream used to encrypt to an in memory
        //    // array of bytes.
        //    MemoryStream msEncrypt;

        //    // Declare the RijndaelManaged object
        //    // used to encrypt the data.
        //    RijndaelManaged aesAlg = null;

        //    try
        //    {
        //        // Create a RijndaelManaged object
        //        // with the specified key and IV.
        //        aesAlg = new RijndaelManaged { Key = key, IV = iv, Mode = CipherMode.CBC, KeySize = 128, BlockSize = 128 };


        //        // Create an encryptor to perform the stream transform.
        //        ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        //        // Create the streams used for encryption.
        //        msEncrypt = new MemoryStream();
        //        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        //        {
        //            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
        //            {

        //                //Write all data to the stream.
        //                swEncrypt.Write(plainText);
        //                swEncrypt.Flush();
        //                swEncrypt.Close();
        //            }
        //        }
        //    }
        //    finally
        //    {
        //        // Clear the RijndaelManaged object.
        //        if (aesAlg != null)
        //            aesAlg.Clear();
        //    }

        //    // Return the encrypted bytes from the memory stream.
        //    return Encoding.UTF8.GetString(msEncrypt.ToArray());
        //}

        //public string DecryptStringFromBytesAes(string _plainText, string _key = "6485689462699694", string _iv = "6658745596832456")
        //{
        //    // Check arguments.
        //    if (_plainText == null || _plainText.Length <= 0)
        //        throw new ArgumentNullException("plainText");
        //    if (_key == null || _key.Length <= 0)
        //        throw new ArgumentNullException("key");
        //    if (_iv == null || _iv.Length <= 0)

        //        throw new ArgumentNullException("iv");


        //    byte[] cipherText = Encoding.UTF8.GetBytes(_plainText);
        //    byte[] key = Encoding.UTF8.GetBytes(_key);
        //    byte[] iv = Encoding.UTF8.GetBytes(_iv);

        //    // Declare the RijndaelManaged object
        //    // used to decrypt the data.
        //    RijndaelManaged aesAlg = null;

        //    // Declare the string used to hold
        //    // the decrypted text.
        //    string plaintext = "";

        //    try
        //    {
        //        // Create a RijndaelManaged object
        //        // with the specified key and IV.
        //        aesAlg = new RijndaelManaged { Key = key, IV = iv, Mode = CipherMode.CBC, KeySize = 128, BlockSize = 128 };

        //        // Create a decrytor to perform the stream transform.
        //        ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
        //        // Create the streams used for decryption.
        //        using (MemoryStream msDecrypt = new MemoryStream(cipherText))
        //        {
        //            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
        //            {
        //                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
        //                {
        //                    // Read the decrypted bytes from the decrypting stream
        //                    // and place them in a string.
        //                    plaintext = srDecrypt.ReadToEnd();
        //                    srDecrypt.Close();
        //                }
        //            }
        //        }
        //    }
        //    finally
        //    {
        //        // Clear the RijndaelManaged object.
        //        if (aesAlg != null)
        //            aesAlg.Clear();
        //    }

        //    return plaintext;
        //}

        public static openSSL cfun()
        {
            return new openSSL();
        }
    }
}