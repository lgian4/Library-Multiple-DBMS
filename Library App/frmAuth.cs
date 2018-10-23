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

    public partial class frmAuth : Form
    {
        public frmAuth()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadCboDbOption();
            
        }
        private void LoadCboDbOption()
        {
            cboDbOption.Items.Add(DBFactoryEnums.MsAccess);
            cboDbOption.Items.Add(DBFactoryEnums.MsAccessPassword);
            cboDbOption.Items.Add(DBFactoryEnums.MDBPassword);
            cboDbOption.Items.Add(DBFactoryEnums.MySql);
            cboDbOption.Items.Add(DBFactoryEnums.MsSQLServerExpress);
            cboDbOption.SelectedIndex = 0;
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            DBFactoryEnums db = (DBFactoryEnums)cboDbOption.SelectedIndex + 1;
            LibraryProxy library = LibraryProxy.GetInstance(db);
            if (!library.Login(txtUsername.Text.Trim(), txtPassword.Text))
                MessageBox.Show("Please Insert Username and Password");
            if (library.account.AccountLevel >= AccountLevelEnums.Patron)
            {
                MessageBox.Show("Login success.");
                GoToMain();
            }
        }

        private void BtnSignIn_Click(object sender, EventArgs e)
        {
            var signInDialog = new frmSignIn();
            signInDialog.Show();

        }

        private void btnGuest_Click(object sender, EventArgs e)
        {
            DBFactoryEnums db = (DBFactoryEnums)cboDbOption.SelectedIndex + 1;
            LibraryProxy library = LibraryProxy.GetInstance(db);
            library.GuestAccount();
            GoToMain();
        }
        private void GoToMain()
        {
            this.Hide();
            var main = new frmMain();
            main.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
