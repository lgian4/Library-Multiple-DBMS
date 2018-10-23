
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_App.MsSQLServerExpress
{
    class LibraryFactory : ILibraryFactory
    {
        private SqlServerHelper dbHelper = SqlServerHelper.GetInstance();
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
