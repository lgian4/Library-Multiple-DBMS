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
    public partial class frmSearchBook : Form
    {
        LibraryProxy libraryapp;
        public frmSearchBook()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty( textBox1.Text.Trim()))
            {
                loadAllBook();
            }
            else
            {
                loadSearchBook();
            }
        }
        private void loadSearchBook()
        {
            dataGridView1.DataSource = libraryapp.FindBook(textBox1.Text.Trim());
        }
        private void loadAllBook()
        {
            dataGridView1.DataSource = libraryapp.ReadBook();
            //foreach (var book in libraryapp.ReadBook())
            //{
            //    dataGridView1.Rows.Add(book);
            //}
        }

        private void frmSearchBook_Load(object sender, EventArgs e)
        {
            libraryapp = LibraryProxy.GetInstance();
            loadAllBook();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
    }
}
