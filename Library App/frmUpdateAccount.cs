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
    public partial class frmUpdateAccount : Form
    {
        LibraryProxy libraryApp;
        public frmUpdateAccount()
        {
            InitializeComponent();
        }
        public frmUpdateAccount(int id) : this()
        {
            lblID.Text = id.ToString();
            libraryApp = LibraryProxy.GetInstance();
            LoadAccount(Convert.ToInt32(lblID.Text));
        }
        private void frmUpdateAccount_Load(object sender, EventArgs e)
        {
            libraryApp = LibraryProxy.GetInstance();
        }
        private void LoadAccount(int id)
        {
            if (id == 0)
                return;
            var account = libraryApp.ReadAccount(id);
            txtFullname.Text = account.FullName;
            txtAddress.Text = account.Address;
            txtPhone.Text = account.PhoneNumber;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            lblID.Text = frmInput.GetId(TblEnums.Account).ToString();
            LoadAccount(Convert.ToInt32(lblID.Text));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFullname.Text) || Convert.ToInt32(lblID.Text) <= 0)
            {
                MessageBox.Show("One or more field haven't been filled correctly");
                return;
            }
            if (libraryApp.UpdateAccount(Convert.ToInt32(lblID.Text), txtFullname.Text.Trim(), txtAddress.Text.Trim(), txtPhone.Text.Trim()))
            {
                MessageBox.Show("Update Success");
            }
            else
            {
                MessageBox.Show("Error occurred\n Please relogin if the error continues!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
