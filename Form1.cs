using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace filebrowser
{
    public partial class Form1 : Form
    {
        crypto crypt = new crypto();
        crypto_symmetric symcrypt = new crypto_symmetric();
        directory checkAndCreateFolder = new directory();

        public Form1()
        {
            checkAndCreateFolder.checkfilesystem();
            InitializeComponent();
            PopulateTreeView();
        }

        private void PopulateTreeView()
        {
            TreeNode rootNode;

            DirectoryInfo info = new DirectoryInfo(@"C:\crypto");
            if (info.Exists)
            {
                rootNode = new TreeNode(info.Name);
                rootNode.Tag = info;
                GetDirectories(info.GetDirectories(), rootNode);
                treeView1.Nodes.Add(rootNode);
            }
        }

        private void GetDirectories(DirectoryInfo[] subDirs, TreeNode nodeToAddTo)
        {
            TreeNode aNode;
            DirectoryInfo[] subSubDirs;
            foreach (DirectoryInfo subDir in subDirs)
            {
                aNode = new TreeNode(subDir.Name, 0, 0);
                aNode.Tag = subDir;
                aNode.ImageKey = "folder";
                subSubDirs = subDir.GetDirectories();
                if (subSubDirs.Length != 0)
                {
                    GetDirectories(subSubDirs, aNode);
                }
                nodeToAddTo.Nodes.Add(aNode);
            }
        }

        void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode newSelected = e.Node;
            listView1.Items.Clear();
            DirectoryInfo nodeDirInfo = (DirectoryInfo)newSelected.Tag;
            ListViewItem.ListViewSubItem[] subItems;
            ListViewItem item = null;

            foreach (DirectoryInfo dir in nodeDirInfo.GetDirectories())
            {
                item = new ListViewItem(dir.Name, 0);
                subItems = new ListViewItem.ListViewSubItem[]
                          {new ListViewItem.ListViewSubItem(item, "Directory"),
                   new ListViewItem.ListViewSubItem(item,
                dir.LastAccessTime.ToShortDateString())};
                item.SubItems.AddRange(subItems);
                listView1.Items.Add(item);
            }
            foreach (FileInfo file in nodeDirInfo.GetFiles())
            {
                item = new ListViewItem(file.Name, 1);
                subItems = new ListViewItem.ListViewSubItem[]
                          { new ListViewItem.ListViewSubItem(item, "File"),
                   new ListViewItem.ListViewSubItem(item,
                file.LastAccessTime.ToShortDateString())};

                item.SubItems.AddRange(subItems);
                listView1.Items.Add(item);
            }

            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.treeView1.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
        }

        private void but_generate_keypair_Click(object sender, EventArgs e)
        {
            crypto.Keys(System.Environment.MachineName + ".pub", System.Environment.MachineName + ".ppk");
            MessageBox.Show("Generate Keypair", "Success");
        }

        private void but_encrypt_Click(object sender, EventArgs e)
        {
            symcrypt.symetrEncrypt(@"C:\crypto\decrypted\bild.jpg", @"C:\crypto\encrypted\bild.jpg", @"C:\crypto\aeskeys\Key.txt", @"C:\crypto\aeskeys\IV.txt");
            MessageBox.Show("Encrypt File", "Success");
        }

        private void but_decrypt_Click(object sender, EventArgs e)
        {
            symcrypt.symetrDecrypt(@"C:\crypto\encrypted\bild.jpg", @"C:\crypto\decrypted\bild.jpg");
            MessageBox.Show("Decrypt File", "Success");
        }
    }
}
