using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library_App.Ms_Access;
namespace Library_App.MDBPassword
{
    class LibraryFactory : ILibraryFactory
    {
       
        private MsAccessHelper dbHelper = MsAccessHelper.GetInstance();
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
