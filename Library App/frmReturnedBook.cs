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
    public partial class frmReturnedBook : Form
    {
        LibraryProxy libraryApp;
        public frmReturnedBook()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            lblBorrow.Text = frmInput.GetId(TblEnums.Borrow).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (libraryApp.Return(Convert.ToInt32(lblBorrow.Text)))
            {
                
                MessageBox.Show(string.Format( "Return Success: \n{0}", libraryApp.BorrowHistory(Convert.ToInt32( lblBorrow.Text))));
            }
            else
            {
                MessageBox.Show("One or more field haven't been filled correctly");
            }
        }

        private void frmReturnedBook_Load(object sender, EventArgs e)
        {
            libraryApp = LibraryProxy.GetInstance();
        }
    }
}
