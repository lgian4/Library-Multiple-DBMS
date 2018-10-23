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
    public partial class frmDeleteAccount : Form
    {
        LibraryProxy libraryApp;
        public frmDeleteAccount()
        {
            InitializeComponent();
        }
        public frmDeleteAccount(int id) : this()
        {
            lblID.Text = id.ToString();
        }
        private void frmDeleteAccount_Load(object sender, EventArgs e)
        {
            libraryApp = LibraryProxy.GetInstance();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            lblID.Text = frmInput.GetId(TblEnums.Account).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lblID.Text) <= 0)
            {
                MessageBox.Show("Please Choose One Account");
                return;
            }
            if (libraryApp.RemoveAccount(Convert.ToInt32(lblID.Text)))
            {
                MessageBox.Show("Delete Success");
                lblID.Text = "0";
            }
            else
            {
                MessageBox.Show("Error occurred\n Please relogin if the error continues!");
            }
        }
    }
}
