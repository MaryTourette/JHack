/**
 * 
 * checkfilesystem() check if main folders exists and create them if not
 * 
 **/

using System.IO;

namespace filebrowser
{
    class directory
    {
        private static readonly log4net.ILog log = LogHelper.GetLogger();

        //Method checks filesystem if folder exists. If not creat them 
        public void checkfilesystem()
        {
            try
            {
                // Specify a name for your folder.
                string mainFolder = @"c:\crypto";
                string rsakeysFolder = @"c:\crypto\rsakeys";
                string aeskeysFolder = @"c:\crypto\aeskeys";
                string decryptedFolder = @"c:\crypto\decrypted";
                string encryptedFolder = @"c:\crypto\encrypted";

                //Check if folder exist and if not create them
                if (!Directory.Exists(mainFolder))
                {
                    DirectoryInfo main = Directory.CreateDirectory(@"C:\crypto");
                }

                if (!Directory.Exists(rsakeysFolder))
                {
                    DirectoryInfo rsa = Directory.CreateDirectory(@"C:\crypto\rsakeys");
                }

                if (!Directory.Exists(aeskeysFolder))
                {
                    DirectoryInfo rsa = Directory.CreateDirectory(@"C:\crypto\aeskeys");
                }

                if (!Directory.Exists(decryptedFolder))
                {
                    DirectoryInfo rsa = Directory.CreateDirectory(@"C:\crypto\decrypted");
                }

                if (!Directory.Exists(encryptedFolder))
                {
                    DirectoryInfo rsa = Directory.CreateDirectory(@"C:\crypto\encrypted");
                }

                //ToDo Logging
            }

            catch
            {
                //ToDo Logging
            }
        }
    }
}
