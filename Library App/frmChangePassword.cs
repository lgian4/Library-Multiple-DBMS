using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library_App
{
    public partial class frmChangePassword : Form
    {
        LibraryProxy libraryApp;
        public frmChangePassword()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool isValid = !string.IsNullOrEmpty(txtUsername.Text.Trim()) && !string.IsNullOrEmpty(txtNew.Text.Trim()) && !string.IsNullOrEmpty(txtOld.Text.Trim());
            string message;
            if (!isValid)
            {
                message = "One or more field haven't been filled correctly";
            }
            else
            {
                var result = libraryApp.ChangePassword(txtUsername.Text.Trim(), txtOld.Text.Trim(), txtNew.Text.Trim());
                message = result ? "Changed Success" : "Username and Password Not found";
            }
            MessageBox.Show(message);
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            libraryApp = LibraryProxy.GetInstance();
        }
    }
}
