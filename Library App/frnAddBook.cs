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
    public partial class frmAddBook : Form
    {
        LibraryProxy libraryApp;
        public frmAddBook()
        {
            InitializeComponent();
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text.Trim()))
            {
                MessageBox.Show("One or more field haven't been filled correctly");
                return;
            }

            if (libraryApp.AddBook(txtTitle.Text.Trim(), txtAuthor.Text.Trim(), txtPublisher.Text.Trim(), dtpPublished.Value))
            {
                MessageBox.Show("Success");
                txtTitle.Text = "";
                txtAuthor.Text = "";
                txtPublisher.Text = "";
                return;
            }
            MessageBox.Show("Error occurred\n Please relogin if the error continues!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmAddBook_Load(object sender, EventArgs e)
        {
            libraryApp = LibraryProxy.GetInstance();
        }
    }
}
