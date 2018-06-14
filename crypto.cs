/**
 * 
 * The class crypto provide the main crypto functionality for the asymetric en- and decryption 
 * 
 * Keys() generate a new rsa keypair 
         * @param string publicKeyFileName
         * @param string privateKeyFileName
 *
 * Encrypt() encrypt a rsa file with the public key and store the new file to the given path
         * @param string publicKeyFileName
         * @param string plainFileName
         * @param string encryptedFileName
* 
 * Decrypt() decrypt a rsa file with the private key and store the new file to the given path
         * @param string privateKeyFileName
         * @param string encryptedFileName
         * @param string plainFileName
* 
 * The class_symetric crypto provide the main symetric crypto functionality for the en- and decryption 
 *
 * symetrEncrypt() encrypt a aes file with the public key and store the new file to the given path
 * The Key and the IV will be also stored in a file 
         * @param string fileInputPath
         * @param string fileOutputPath
         * @param string fileOutputKeyPath
         * @param string fileOutputIVPath
* 
 * symetrDecrypt() decrypt a aes file with the private key and store the new file to the given path
         * @param string fileInputPath
         * @param string fileOutputPath
* 
 * getKey() get the aes key from a file 
         * @param string fileInputKeyPath
 *
 * getIV() get the aes IV from a file
         * @param string fileInputIVPath
 **/

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;


namespace filebrowser
{
    class crypto
    {
        private static readonly log4net.ILog log = LogHelper.GetLogger();

        // Generate a new rsa keypair
        public static void Keys(string publicKeyFileName, string privateKeyFileName)

        {

            // Variables

            CspParameters cspParams = null;

            RSACryptoServiceProvider rsaProvider = null;

            StreamWriter publicKeyFile = new StreamWriter(Path.Combine(@"C:\crypto\rsakeys", publicKeyFileName));

            StreamWriter privateKeyFile = new StreamWriter(Path.Combine(@"C:\crypto\rsakeys", privateKeyFileName));

            string publicKey = "";

            string privateKey = "";


            try

            {

                // Create a new key pair on target CSP

                cspParams = new CspParameters();

                cspParams.ProviderType = 1; // PROV_RSA_FULL

                //cspParams.ProviderName; // CSP name

                cspParams.Flags = CspProviderFlags.UseArchivableKey;

                cspParams.KeyNumber = (int)KeyNumber.Exchange;

                rsaProvider = new RSACryptoServiceProvider(cspParams);


                // Export public key

                publicKey = rsaProvider.ToXmlString(false);


                // Write public key to file

                publicKeyFile.Write(publicKey);


                // Export private/public key pair

                privateKey = rsaProvider.ToXmlString(true);


                // Write private/public key pair to file

                privateKeyFile.Write(privateKey);

                //ToDo Logging

            }

            catch (Exception ex)

            {
                //ToDo Logging
                // Any errors? Show them

            }

            finally

            {

                // Do some clean up if needed

                if (publicKeyFile != null)

                {

                    publicKeyFile.Close();

                }

                if (privateKeyFile != null)

                {

                    privateKeyFile.Close();

                }

            }


        }

        // Encrypt a rsa file
        public static void Encrypt(string publicKeyFileName, string plainFileName, string encryptedFileName)

        {

            // Variables

            CspParameters cspParams = null;

            RSACryptoServiceProvider rsaProvider = null;

            StreamReader publicKeyFile = null;

            StreamReader plainFile = new StreamReader(@"C:\crypto\decrypted\" + plainFileName);

            FileStream encryptedFile = null;

            string publicKeyText = "";

            string plainText = "";

            byte[] plainBytes = null;

            byte[] encryptedBytes = null;


            try

            {

                // Select target CSP

                cspParams = new CspParameters();

                cspParams.ProviderType = 1; // PROV_RSA_FULL

                //cspParams.ProviderName; // CSP name

                rsaProvider = new RSACryptoServiceProvider(cspParams);


                // Read public key from file

                publicKeyFile = File.OpenText(@"C:\crypto\rsakeys\" + publicKeyFileName);

                publicKeyText = publicKeyFile.ReadToEnd();


                // Import public key

                rsaProvider.FromXmlString(publicKeyText);


                // Read plain file 
                // ToDo Read file and convert it into byte array 

                plainText = plainFile.ReadToEnd();


                // Encrypt plain file

                plainBytes = Encoding.Unicode.GetBytes(plainText);

                encryptedBytes = rsaProvider.Encrypt(plainBytes, false);


                // Write encrypted text to file

                encryptedFile = File.Create(encryptedFileName);

                encryptedFile.Write(encryptedBytes, 0, encryptedBytes.Length);

                //ToDo Logging 
            }

            catch (Exception ex)

            {
                //ToDo Logging
                // Any errors? Show them
                Console.WriteLine(ex.Message);

            }

            finally

            {

                // Do some clean up if needed

                if (publicKeyFile != null)

                {

                    publicKeyFile.Close();

                }

                if (plainFile != null)

                {

                    plainFile.Close();

                }

                if (encryptedFile != null)

                {

                    encryptedFile.Close();

                }

            }


        }

        // Decrypt a rsa file
        public static void Decrypt(string privateKeyFileName, string encryptedFileName, string plainFileName)

        {

            // Variables

            CspParameters cspParams = null;

            RSACryptoServiceProvider rsaProvider = null;

            StreamReader privateKeyFile = null;

            FileStream encryptedFile = null;

            StreamWriter plainFile = null;

            string privateKeyText = "";

            string plainText = "";

            byte[] encryptedBytes = null;

            byte[] plainBytes = null;


            try

            {

                // Select target CSP

                cspParams = new CspParameters();

                cspParams.ProviderType = 1; // PROV_RSA_FULL

                //cspParams.ProviderName; // CSP name

                rsaProvider = new RSACryptoServiceProvider(cspParams);


                // Read private/public key pair from file

                privateKeyFile = File.OpenText(privateKeyFileName);

                privateKeyText = privateKeyFile.ReadToEnd();


                // Import private/public key pair

                rsaProvider.FromXmlString(privateKeyText);


                // Read encrypted text from file

                encryptedFile = File.OpenRead(encryptedFileName);

                encryptedBytes = new byte[encryptedFile.Length];

                encryptedFile.Read(encryptedBytes, 0, (int)encryptedFile.Length);


                // Decrypt text

                plainBytes = rsaProvider.Decrypt(encryptedBytes, false);


                // Write decrypted text to file

                plainFile = File.CreateText(plainFileName);

                plainText = Encoding.Unicode.GetString(plainBytes);

                plainFile.Write(plainText);

                //ToDo Logging

            }

            catch (Exception ex)

            {
                //ToDo Logging
                // Any errors? Show them
                Console.WriteLine(ex.Message);

            }

            finally

            {

                // Do some clean up if needed

                if (privateKeyFile != null)

                {

                    privateKeyFile.Close();

                }

                if (encryptedFile != null)

                {

                    encryptedFile.Close();

                }

                if (plainFile != null)

                {

                    plainFile.Close();

                }

            }


        }

    }

    class crypto_symmetric
    {
        //Method to encrypt files with symetric aes
        public void symetrEncrypt(string fileInputPath, string fileOutputPath, string fileOutputKeyPath, string fileOutputIVPath)
        {
            try
            {
                using (RijndaelManaged RMCrypto = new RijndaelManaged())
                {
                    //ToDo crypt key & IV with rsa and save into a HashMap
                    RMCrypto.GenerateKey();
                    string Key = Convert.ToBase64String(RMCrypto.Key);
                    RMCrypto.GenerateIV();
                    string IV = Convert.ToBase64String(RMCrypto.IV);

                    //Create file for Key & IV 
                    File.WriteAllText(fileOutputKeyPath, Key);
                    File.WriteAllText(fileOutputIVPath, IV);

                    //Create file for new crypt document 
                    using (FileStream fsCrypt = new FileStream(fileOutputPath, FileMode.Create))
                    {
                        using (ICryptoTransform encryptor = RMCrypto.CreateEncryptor(RMCrypto.Key, RMCrypto.IV))
                        {
                            //new CryptoStream to stream a file like a filestream but used for crypto data 
                            using (CryptoStream cs = new CryptoStream(fsCrypt, encryptor, CryptoStreamMode.Write))
                            {
                                //Create FileStream to open and Save data in the while statment
                                using (FileStream fsIn = new FileStream(fileInputPath, FileMode.Open))
                                {
                                    int data;
                                    while ((data = fsIn.ReadByte()) != -1)
                                    {
                                        cs.WriteByte((byte)data);
                                    }
                                }
                            }
                        }
                    }
                }

                //ToDo Logging
            }
            catch
            {
                //ToDO logging
                //Inform the user that an exception was raised.  
            }
        }

        //Method to decrypt files with a symetric aes
        public void symetrDecrypt(string fileInputPath, string fileOutputPath)
        {
            try
            {
                using (RijndaelManaged aes = new RijndaelManaged())
                {
                    //Read aes keys
                    //ToDo implement a Hashmap or Dictionary in C#
                    byte[] Key = Convert.FromBase64String(getKey(@"C:\crypto\aeskeys\Key.txt"));
                    byte[] IV = Convert.FromBase64String(getKey(@"C:\crypto\aeskeys\IV.txt"));

                    //Read exist crypted file 
                    using (FileStream fsCrypt = new FileStream(fileInputPath, FileMode.Open))
                    {
                        //New filestream to save the decrypted file as a new file
                        using (FileStream fsOut = new FileStream(fileOutputPath, FileMode.Create))
                        {
                            using (ICryptoTransform decryptor = aes.CreateDecryptor(Key, IV))
                            {
                                //read encrypted file and save it in a new file withe the same name 
                                using (CryptoStream cs = new CryptoStream(fsCrypt, decryptor, CryptoStreamMode.Read))
                                {
                                    int data;
                                    while ((data = cs.ReadByte()) != -1)
                                    {
                                        fsOut.WriteByte((byte)data);
                                    }
                                }
                            }
                        }
                    }
                }

                //ToDo Logging
            }

            catch (Exception ex)
            {
                //ToDo logging
                // failed to decrypt file
            }
        }

        //Get key from a file in a given path
        public string getKey(string fileInputKeyPath)
        {
            string Key = "";
            StreamReader srKey = new StreamReader(fileInputKeyPath);
            Key = srKey.ReadToEnd();
            return Key;

            //ToDo Logging
        }

        //Get IV from a file in a given path
        public string getIV(string fileInputIVPath)
        {
            string IV = "";
            StreamReader srIV = new StreamReader(fileInputIVPath);
            IV = srIV.ReadToEnd();
            return IV;

            //ToDo Logging
        }

    }

}
