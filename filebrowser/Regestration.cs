using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace filebrowser
{
    public partial class Regestration : Form
    {
        public Regestration()
        {
            InitializeComponent();
        }

        private void but_Regestration_Click(object sender, EventArgs e)
        {
            string username = txb_Username.Text;
            string password = txb_Password.Text;
            string eMail = txb_EMail.Text;

            ApiLoginOperations ops = new ApiLoginOperations();
            User user = ops.RegisterUser(username, password, eMail);
            if (username != String.Empty && password != String.Empty && eMail != String.Empty)
            {
                if (user == null)
                {
                    MessageBox.Show("Username already exists");
                    return;
                }

                Globals.LoggedInUser = user;
                MessageBox.Show("Registration successful");
                Login login = new Login();
                login.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Please file all fileds","Error");
            }
        }
    }
}
