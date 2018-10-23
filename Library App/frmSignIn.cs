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

    public partial class frmSignIn : Form
    {
        public frmSignIn()
        {
            InitializeComponent();
        }
        public frmSignIn(bool enablecbodb = true):this()
        {
            switch (enablecbodb)
            {
                case false:
                    cboDbOption.Enabled = false;
                    break;
                case true:
                default:
                    cboDbOption.Enabled = true;
                    break;
            }
        }
        private void btnSignIn_Click(object sender, EventArgs e)
        {
            var libraryApp = LibraryProxy.GetInstance((DBFactoryEnums)cboDbOption.SelectedIndex + 1);
            string address = txtAddress.Text.Trim();
            string phone = txtPhone.Text.Trim();
            AccountLevelEnums level = (AccountLevelEnums)cboLevel.SelectedIndex + 1;
            string fullname = txtFullname.Text.Trim();
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            if (libraryApp.SignIn(fullname, address, phone, level, username, password))
            {
                MessageBox.Show("Sign In Success");
                txtAddress.Text = "";
                txtFullname.Text = "";
                txtPassword.Text = "";
                txtPhone.Text = "";
                txtUsername.Text = "";
            }
            else
            {
                MessageBox.Show("One or more field haven't been filled correctly");
            }
        }
        private void LoadCboDbOption()
        {
            cboDbOption.Items.Add(DBFactoryEnums.MsAccess);
            cboDbOption.Items.Add(DBFactoryEnums.MsAccessPassword);
            cboDbOption.Items.Add(DBFactoryEnums.MDBPassword);
            cboDbOption.Items.Add(DBFactoryEnums.MySql);
            cboDbOption.Items.Add(DBFactoryEnums.MsSQLServerExpress);
            if (LibraryProxy.DbFactory > 0)
            {
                cboDbOption.SelectedIndex = (int)LibraryProxy.DbFactory - 1;
            }
            else
            {
                cboDbOption.SelectedIndex = 0;
            }
        }
        public void LoadLevel()
        {
            switch (LibraryProxy.GetInstance((DBFactoryEnums)cboDbOption.SelectedIndex + 1).account.AccountLevel)
            {
                case AccountLevelEnums.Librarian:
                    cboLevel.Items.Add(AccountLevelEnums.Patron);
                    cboLevel.Items.Add(AccountLevelEnums.Librarian);
                    break;
                case AccountLevelEnums.Manager:
                    cboLevel.Items.Add(AccountLevelEnums.Patron);
                    cboLevel.Items.Add(AccountLevelEnums.Librarian);
                    cboLevel.Items.Add(AccountLevelEnums.Manager);
                    break;
                default:
                    cboLevel.Items.Add(AccountLevelEnums.Patron);
                    break;
            }
            cboLevel.SelectedIndex = 0;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void frmSignIn_Load(object sender, EventArgs e)
        {
            LoadCboDbOption();
            LoadLevel();
        }

        private void cboDbOption_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
