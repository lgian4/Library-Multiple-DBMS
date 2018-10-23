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
    public partial class frmBookList : Form
    {
        LibraryProxy libraryapp;
        public frmBookList()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmBookList_Load(object sender, EventArgs e)
        {
            libraryapp = LibraryProxy.GetInstance();
            loadAll();
        }
        private void loadAll()
        {
            dataGridView1.DataSource = libraryapp.ReadBook();
            switch (dataGridView1.Rows.Count)
            {
                case 0:
                    EnableBtn(false);
                    break;
                default:
                    EnableBtn(true);
                    break;
            }
        }

        private void loadSearch(string text)
        {
            dataGridView1.DataSource = libraryapp.FindBook(text);
            switch (dataGridView1.Rows.Count)
            {
                case 0:
                    EnableBtn(false);
                    break;
                default:
                    EnableBtn(true);
                    break;
            }
        }
        private void EnableBtn(bool isEnable)
        {
            btnDelete.Enabled = isEnable;
            btnUpdate.Enabled = isEnable;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Search();
        }
        private void Search() {
            if (string.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                loadAll();
            }
            else
            {
                loadSearch(textBox1.Text.Trim());
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddBook frmAdd = new frmAddBook();
            frmAdd.ShowDialog();
            Search();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            frmUpdateBook frmUpdate = new frmUpdateBook(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
            frmUpdate.ShowDialog();
            Search();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            frmDeleteBook frmDelete = new frmDeleteBook(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
            frmDelete.ShowDialog();
           Search();
        }
    }
}
