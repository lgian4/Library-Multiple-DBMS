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
    public partial class frmBorrowBook : Form
    {
        LibraryProxy libraryApp;
        public frmBorrowBook()
        {
            InitializeComponent();
        }

        private void frmBorrowBook_Load(object sender, EventArgs e)
        {
            libraryApp = LibraryProxy.GetInstance();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            lblAccount.Text = frmInput.GetId(TblEnums.Account).ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lblBook.Text = frmInput.GetId(TblEnums.Book).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (libraryApp.Borrow(Convert.ToInt32(lblBook.Text), Convert.ToInt32(lblAccount.Text)))
            {
                MessageBox.Show("Borrow Success");
            }
            else
            {
                MessageBox.Show("One or more field haven't been filled correctly");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
