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
    public partial class frmDeleteBook : Form
    {
        LibraryProxy libraryApp;
        public frmDeleteBook()
        {
            InitializeComponent();
        }

        public frmDeleteBook(int id) : this()
        {
            lblID.Text = id.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            lblID.Text = frmInput.GetId(TblEnums.Book).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lblID.Text) <= 0)
            {
                MessageBox.Show("Please Choose One Book");
                return;
            }
            if (libraryApp.RemoveBook(Convert.ToInt32(lblID.Text)))
            {
                MessageBox.Show("Delete Success");
                lblID.Text = "0";
            }
            else
            {
                MessageBox.Show("Error occurred\n Please relogin if the error continues!");
            }

        }

        private void frmDeleteBook_Load(object sender, EventArgs e)
        {
            libraryApp = LibraryProxy.GetInstance();
        }
    }
}
