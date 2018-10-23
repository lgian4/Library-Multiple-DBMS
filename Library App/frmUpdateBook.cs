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
    public partial class frmUpdateBook : Form
    {
        LibraryProxy libraryApp;
        public frmUpdateBook()
        {
            InitializeComponent();
        }

        public frmUpdateBook(int id) : this()
        {
            lblID.Text = id.ToString();
            libraryApp = LibraryProxy.GetInstance();
            LoadBook(Convert.ToInt32(lblID.Text));
        }
        private void button3_Click(object sender, EventArgs e)
        {
            lblID.Text = frmInput.GetId(TblEnums.Book).ToString();
            LoadBook(Convert.ToInt32(lblID.Text));
        }
        private void LoadBook(int id)
        {
            if (id == 0)
                return;
            var book = libraryApp.ReadBook(id);
            txtTitle.Text = book.Title;
            txtAuthor.Text = book.Author;
            txtPublisher.Text = book.Publisher;
            dtpPublished.Value = book.PublishedDate;
        }
        private void frmUpdateBook_Load(object sender, EventArgs e)
        {
            libraryApp = LibraryProxy.GetInstance();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text) || Convert.ToInt32(lblID.Text) <= 0)
            {
                MessageBox.Show("One or more field haven't been filled correctly");
                return;
            }
            if (libraryApp.UpdateBook(Convert.ToInt32(lblID.Text), txtTitle.Text.Trim(), txtAuthor.Text.Trim(), txtPublisher.Text.Trim(), dtpPublished.Value))
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
