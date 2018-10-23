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
    public partial class frmMain : Form
    {
        public LibraryProxy libraryapp;
        public frmMain()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
        private void HideTab()
        {
            //if released
            switch (libraryapp.account.AccountLevel)
            {
                case AccountLevelEnums.Guest:
                    this.tabControl1.Controls.Add(this.tabPage1);
                    break;
                case AccountLevelEnums.Patron:
                    this.tabControl1.Controls.Add(this.tabPage1);
                    this.tabControl1.Controls.Add(this.tabPage2);
                    break;
                case AccountLevelEnums.Librarian:
                    this.tabControl1.Controls.Add(this.tabPage1);
                    this.tabControl1.Controls.Add(this.tabPage2);
                    this.tabControl1.Controls.Add(this.tabPage3);
                    this.tabControl1.Controls.Add(this.tabPage4);
                    break;
                case AccountLevelEnums.Manager:
                    this.tabControl1.Controls.Add(this.tabPage1);
                    this.tabControl1.Controls.Add(this.tabPage2);
                    this.tabControl1.Controls.Add(this.tabPage3);
                    this.tabControl1.Controls.Add(this.tabPage4);
                    this.tabControl1.Controls.Add(this.tabPage5);
                    break;
                default:
                    break;
            }

        }
        private void btnLogOut_Click(object sender, EventArgs e)
        {
            libraryapp.GuestAccount();
            this.Hide();
            var login = new frmAuth();
            login.Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void setLabelAccount()
        {
            lblname.Text = libraryapp.account.FullName;
            lbladdress.Text = libraryapp.account.Address;
            lblphone.Text = libraryapp.account.PhoneNumber;
            lblopened.Text = libraryapp.account.Opened.ToShortDateString();
            lbllevel.Text = libraryapp.account.AccountLevel.ToString();
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            libraryapp = LibraryProxy.GetInstance();
            setLabelAccount();
            HideTab();
        }
        #region buttonClick

       
        private void button1_Click(object sender, EventArgs e)
        {
            var searchbook = new frmSearchBook();
            searchbook.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmChangePassword frmchange = new frmChangePassword();
            frmchange.ShowDialog();
            btnLogOut_Click(this, e);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmBorrowHistory frmborrow = new frmBorrowHistory();
            frmborrow.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmBorrowBook frmBorrow = new frmBorrowBook();
            frmBorrow.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            frmReturnedBook frmReturn = new frmReturnedBook();
            frmReturn.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            frmAddBook frmAdd = new frmAddBook();
            frmAdd.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            frmUpdateBook frmUpdate = new frmUpdateBook();
            frmUpdate.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            frmDeleteBook frmDelete = new frmDeleteBook();
            frmDelete.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            frmBookList frmBookList = new frmBookList();
            frmBookList.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            frmSignIn frmSign = new frmSignIn(false);
            frmSign.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            frmUpdateAccount frmUpdate = new frmUpdateAccount();
            frmUpdate.Show();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            frmDeleteAccount frmDelete = new frmDeleteAccount();
            frmDelete.Show();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            frmAccountList frmAccountList = new frmAccountList();
            frmAccountList.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            frmBorrowList frmBorrowList = new frmBorrowList();
            frmBorrowList.Show();
        }
        #endregion

        private void button15_Click(object sender, EventArgs e)
        {
           
        }

        private void button15_Click_1(object sender, EventArgs e)
        {
            frmBorrowReprot frm = new frmBorrowReprot();
            frm.Show();
        }
    }
}
