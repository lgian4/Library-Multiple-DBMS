using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_App.MySql
{
    class LibraryFactory : ILibraryFactory
    {
        private MysqlHelper dbHelper = MysqlHelper.GetInstance();
        public LibraryFactory()
        {

        }
        public IAccountMethodStrategy CreateAccount()
        {
            return new AccountMethods(dbHelper);
        }

        public IBookMethodStrategy CreateBook()
        {
            return new BookMethods(dbHelper);
        }

        public IBorrowMethodStrategy CreateBorrow()
        {
            return new BorrowMethods(dbHelper);
        }
    }
}
