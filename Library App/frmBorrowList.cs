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
    public partial class frmBorrowList : Form
    {
        LibraryProxy libraryapp;
        public frmBorrowList()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmBorrowList_Load(object sender, EventArgs e)
        {
            libraryapp = LibraryProxy.GetInstance();
            loadAll();
        }
        private void loadAll()
        {
            dataGridView1.DataSource = libraryapp.BorrowHistory();
        }
        private void Search()
        {
            if (string.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                loadAll();
            }
            else
            {
                loadSearch(textBox1.Text.Trim());
            }
        }
        private void loadSearch(string text)
        {
            dataGridView1.DataSource = libraryapp.FindBorrowHistory(text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Search();
        }
    }
}
