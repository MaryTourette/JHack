using System.IO;

namespace filebrowser
{
    class directory
    {
        public void checkfilesystem()
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
        }
    }
}
