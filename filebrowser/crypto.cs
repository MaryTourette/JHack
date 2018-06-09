using System;
using System.IO;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Text;


namespace filebrowser
{
    class crypto
    {
        // Generate a new key pair
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

            }

            catch (Exception ex)

            {

                // Any errors? Show them

                Console.WriteLine("Exception generating a new key pair! More info:");

                Console.WriteLine(ex.Message);

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

        // Encrypt a file
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

            }

            catch (Exception ex)

            {

                // Any errors? Show them

                Console.WriteLine("Exception encrypting file! More info:");

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

        // Decrypt a file
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

            }

            catch (Exception ex)

            {

                // Any errors? Show them

                Console.WriteLine("Exception decrypting file! More info:");

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
        public void symetrEncrypt(string fileInputPath, string fileOutputPath, string fileOutputKeyPath, string fileOutputIVPath)
        {
            try
            {
                /**
                //Create FileStream to open and Save data
                FileStream fsInputSymEncrypt = new FileStream(@fileInputPath, FileMode.Open);
                FileStream fsOutputSymEncrypt = new FileStream(@fileOutputPath, FileMode.Create);

                //Create a new instance of the RijndaelManaged class and encrypt the stream.  
                RijndaelManaged RMCrypto = new RijndaelManaged();

                //Generate RijndaelManaged Key & IV and save it in a string
                RMCrypto.GenerateKey();
                string Key = Convert.ToBase64String(RMCrypto.Key);
                RMCrypto.GenerateIV();
                string IV = Convert.ToBase64String(RMCrypto.IV);

                //Create a CryptoStream, pass it the FileStream and encrypt it with the Rijndael class.  
                CryptoStream CryptStream = new CryptoStream(fsOutputSymEncrypt, RMCrypto.CreateEncryptor(RMCrypto.Key, RMCrypto.IV), CryptoStreamMode.Write);

                //Write Key to file
                File.WriteAllText(fileOutputKeyPath,Key);
                File.WriteAllText(fileOutputIVPath, IV);

                //Close all the connections. 
                CryptStream.Close();
                fsInputSymEncrypt.Close();
                fsOutputSymEncrypt.Close();
                **/

                using (RijndaelManaged RMCrypto = new RijndaelManaged())
                {
                    RMCrypto.GenerateKey();
                    string Key = Convert.ToBase64String(RMCrypto.Key);
                    RMCrypto.GenerateIV();
                    string IV = Convert.ToBase64String(RMCrypto.IV);

                    File.WriteAllText(fileOutputKeyPath, Key);
                    File.WriteAllText(fileOutputIVPath, IV);

                    using (FileStream fsCrypt = new FileStream(fileOutputPath, FileMode.Create))
                    {
                        using (ICryptoTransform encryptor = RMCrypto.CreateEncryptor(RMCrypto.Key, RMCrypto.IV))
                        {
                            using (CryptoStream cs = new CryptoStream(fsCrypt, encryptor, CryptoStreamMode.Write))
                            {
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

            }
            catch
            {
                //Inform the user that an exception was raised.  
                Console.WriteLine("The encryption failed.");
            }
        }

        public void symetrDecrypt(string fileInputPath, string fileOutputPath)
        {

            try
            {
                using (RijndaelManaged aes = new RijndaelManaged())
                {
                    byte[] Key = Convert.FromBase64String(getKey(@"C:\crypto\aeskeys\Key.txt"));
                    byte[] IV = Convert.FromBase64String(getKey(@"C:\crypto\aeskeys\IV.txt"));

                    using (FileStream fsCrypt = new FileStream(fileInputPath, FileMode.Open))
                    {
                        using (FileStream fsOut = new FileStream(fileOutputPath, FileMode.Create))
                        {
                            using (ICryptoTransform decryptor = aes.CreateDecryptor(Key, IV))
                            {
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
            }
            catch (Exception ex)
            {
                // failed to decrypt file
            }
            /**
            byte[] Key = Convert.FromBase64String(getKey(@"C:\crypto\aeskeys\Key.txt"));
            byte[] IV = Convert.FromBase64String(getKey(@"C:\crypto\aeskeys\IV.txt"));
            try
            {
                //Create FileStream to open and Save data
                FileStream fsInputSymEncrypt = new FileStream(@fileInputPath, FileMode.Open);
                FileStream fsOutputSymEncrypt = new FileStream(@fileOutputPath, FileMode.Create);

                //Create a new instance of the RijndaelManaged class  
                // and decrypt the stream.  
                RijndaelManaged RMCrypto = new RijndaelManaged();

                //Create a CryptoStream, pass it the NetworkStream, and decrypt   
                //it with the Rijndael class using the key and IV.  
                CryptoStream CryptStream = new CryptoStream(fsOutputSymEncrypt, RMCrypto.CreateDecryptor(Key, IV), CryptoStreamMode.Read);

                int data;
                while ((data = CryptStream.ReadByte()) != -1)
                {
                    fsOutputSymEncrypt.WriteByte((byte)data);
                }


                //Close the streams. 
                CryptStream.Close();
                fsOutputSymEncrypt.Close();
                fsInputSymEncrypt.Close();

            }
            //Catch any exceptions.   
            catch
            {
                Console.WriteLine("The Listener Failed.");
            }
    **/
        }

        public string getKey(string fileInputKeyPath)
        {
            string Key = "";
            StreamReader srKey = new StreamReader(fileInputKeyPath);
            Key = srKey.ReadToEnd();
            return Key;
        }

        public string getIV(string fileInputIVPath)
        {
            string IV = "";
            StreamReader srIV = new StreamReader(fileInputIVPath);
            IV = srIV.ReadToEnd();
            return IV;
        }

    }

}
