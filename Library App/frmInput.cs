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
    public partial class frmInput : Form
    {
        LibraryProxy libraryapp;
        int selectedId;

        TblEnums selectedTable;
        private frmInput()
        {
            InitializeComponent();
        }

        private void InputBook_Load(object sender, EventArgs e)
        {
            btnSelect.Enabled = false;
            libraryapp = LibraryProxy.GetInstance();
            loadDataGrid(loadAll());
        }
        private object loadAll()
        {
            switch (selectedTable)
            {
                case TblEnums.Book:
                    return libraryapp.ReadBook();
                case TblEnums.Account:
                    return libraryapp.ReadAccount();
                case TblEnums.Borrow:
                    return libraryapp.BorrowHistory();
                default:
                    throw new NotImplementedException();
            }
        }
        private void loadDataGrid(object list)
        {
            dataGridView1.DataSource = list;
            switch (dataGridView1.Rows.Count)
            {
                case 0:
                    btnSelect.Enabled = false;
                    break;
                default:
                    btnSelect.Enabled = true;
                    break;
            }
        }
        private object loadSearch(string text)
        {
            switch (selectedTable)
            {
                case TblEnums.Book:
                    return libraryapp.FindBook(text);
                case TblEnums.Account:
                    return libraryapp.FindAccount(text);
                case TblEnums.Borrow:
                    return libraryapp.FindBorrowHistory(text);
                default:
                    throw new NotImplementedException();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                loadDataGrid(loadAll());
            }
            else
            {
                loadDataGrid(loadSearch(textBox1.Text.Trim()));
            }
        }
        public static int GetId(TblEnums tbl)
        {
            var inputdialog = new frmInput();
            inputdialog.selectedTable = tbl;
            inputdialog.ShowDialog();
            return inputdialog.selectedId;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            selectedId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            selectedId = 0;
            Close();
        }
    }
}
