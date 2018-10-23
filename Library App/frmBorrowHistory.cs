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
    public partial class frmBorrowHistory : Form
    {
        LibraryProxy libraryApp;
        public frmBorrowHistory()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void LoadAll()
        {
            dataGridView1.DataSource = libraryApp.CurrentBorrowHistory();
        }
        private void frmBorrowHistory_Load(object sender, EventArgs e)
        {
            libraryApp = LibraryProxy.GetInstance();
            LoadAll();
        }
    }
}
