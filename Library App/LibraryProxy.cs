using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_App
{

    public class LibraryProxy
    {
        public static DBFactoryEnums DbFactory;
        private static LibraryProxy libraryProxy;


        private IAccountMethodStrategy AccountMehod { get; set; }
        private IBookMethodStrategy BookMethod { get; set; }
        private IBorrowMethodStrategy BorrowMehod { get; set; }
        private Account _account;
        public Account account
        {
            get { return _account; }
        }

        public static LibraryProxy GetInstance(DBFactoryEnums db)
        {
            if (libraryProxy == null || DbFactory != db)
            {
                DbFactory = db;
                libraryProxy = new LibraryProxy(db);
            }
            return libraryProxy;
        }
        public static LibraryProxy GetInstance()
        {
            if (libraryProxy == null)
            {
                throw new NotImplementedException();
            }
            return libraryProxy;
        }

        private LibraryProxy(DBFactoryEnums factoryEnums)
        {
            ILibraryFactory libraryFactory;
            switch (factoryEnums)
            {
                case DBFactoryEnums.MsAccess:
                    libraryFactory = new Ms_Access.LibraryFactory();
                    break;
                case DBFactoryEnums.MsAccessPassword:
                    libraryFactory = new MsAccessPassword.LibraryFactory();
                    break;
                case DBFactoryEnums.MDBPassword:
                    libraryFactory = new MDBPassword.LibraryFactory();
                    break;
                case DBFactoryEnums.MySql:
                    libraryFactory = new MySql.LibraryFactory();
                    break;
                case DBFactoryEnums.MsSQLServerExpress:
                    libraryFactory = new MsSQLServerExpress.LibraryFactory();
                    break;
                default:
                    throw new NotImplementedException();
            }

            AccountMehod = libraryFactory.CreateAccount();
            BookMethod = libraryFactory.CreateBook();
            BorrowMehod = libraryFactory.CreateBorrow();
        }
        public void GuestAccount()
        {
            _account = new Account();
        }
        public bool Login(string username, string password)
        {
            bool isValid = !IsStringEmpty(username, password);
            if (!isValid)
                return false;

            int id = AccountMehod.Login(username, password);
            if (id > 0)
            {
                _account = AccountMehod.Read(id);
                return true;
            }
            return false;
        }
        public bool SignIn(string fullname, string address, string phone, AccountLevelEnums level, string username, string password)
        {
            bool isValid = !IsStringEmpty(fullname, username, password) && password.Length >= 8 && level != 0 && (account.AccountLevel >= level || level == AccountLevelEnums.Patron);
            if (!isValid)
                return false;
            if (AccountMehod.CountUsername(username) != 0)
                return false;
            AccountMehod.Add(new Account(fullname, address, phone, DateTime.Today, level, username, password));
            return true;
        }
        public bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            bool isValid = !IsStringEmpty(username, oldPassword, newPassword);
            if (!isValid)
                return false;
            try
            {
                AccountMehod.ChangePassword(username, oldPassword, newPassword);
            }
            catch (NotImplementedException)
            {
                return false;
            }

            return true;
        }
        #region accountMethod
        public List<Account> FindAccount(string search)
        {
            if ((int)account.AccountLevel <= 1)
                throw new NotImplementedException();
            return AccountMehod.Find(search);
        }
        public List<Account> ReadAccount()
        {
            if ((int)account.AccountLevel <= 1)
                throw new NotImplementedException();
            return AccountMehod.Read();
        }
        public Account ReadAccount(int id)
        {
            if ((int)account.AccountLevel <= 1 && id <= 0)
                throw new NotImplementedException();
            return AccountMehod.Read(id);
        }
        public bool RemoveAccount(int id)
        {
            bool isvalid = account.ID != id && account.AccountLevel >= AccountLevelEnums.Librarian;
            if (!isvalid)
                return false;
            AccountMehod.Remove(id);
            return true;
        }
        public bool UpdateAccount(int id, string fullname, string address, string phone)
        {
            bool isValid = !IsStringEmpty(fullname) && id > 0 && (account.ID == id || account.AccountLevel >= AccountLevelEnums.Librarian);
            if (!isValid)
                return false;
            AccountMehod.Update(new Account(fullname, address, phone, id));
            return true;
        }
        #endregion
        #region BookMethod

        public bool AddBook(string title, string author, string publisher, DateTime published)
        {
            bool isValid = !IsStringEmpty(title) && account.AccountLevel >= AccountLevelEnums.Librarian;
            if (!isValid)
                return false;
            BookMethod.Add(new Book(title, author, publisher, published));
            return true;
        }

        public List<Book> FindBook(string searchInput)
        {
            bool isValid = !IsStringEmpty(searchInput);
            if (!isValid)
                return new List<Book>();
            return BookMethod.Find(searchInput);
        }

        public List<Book> ReadBook()
        {
            return BookMethod.Read();
        }
        public Book ReadBook(int id)
        {
            if (id <= 0)
                return new Book();
            return BookMethod.Read(id);
        }
        public bool RemoveBook(int id)
        {
            bool isValid = account.AccountLevel >= AccountLevelEnums.Librarian && id > 0;
            if (!isValid)
                return false;
            BookMethod.Remove(id);
            return true;
        }
        public bool UpdateBook(int id, string title, string author, string publisher, DateTime published)
        {
            bool isValid = !IsStringEmpty(title) && id > 0 && account.AccountLevel >= AccountLevelEnums.Librarian;
            if (!isValid)
                return false;
            BookMethod.Update(new Book(id, title, author, publisher, published));
            return true;
        }
        #endregion

        #region BorrowMethod


        public List<JoinBorrow> CurrentBorrowHistory()
        {
            bool isValid = account.ID > 0;
            if (!isValid)
                throw new NotImplementedException();
            return BorrowMehod.AccountHistory(account.ID);
        }
        public List<JoinBorrow> FindBorrowHistory(string search)
        {
            bool isValid = !IsStringEmpty(search) && account.AccountLevel >= AccountLevelEnums.Librarian;
            if (!isValid)
                throw new NotImplementedException();
            return BorrowMehod.Find(search);
        }
        public JoinBorrow BorrowHistory(int id)
        {
            bool isValid = id > 0 && account.AccountLevel >= AccountLevelEnums.Librarian;
            if (!isValid)
                throw new NotImplementedException();
            return BorrowMehod.Read(id);
        }
        public List<JoinBorrow> BorrowHistory()
        {
            if (account.AccountLevel < AccountLevelEnums.Librarian)
                return new List<JoinBorrow>();
            return BorrowMehod.Read();
        }
        public List<Account> BookHistory(int bookId)
        {
            if (account.AccountLevel >= AccountLevelEnums.Librarian)
                throw new NotImplementedException();
            return BorrowMehod.BookHistory(bookId);
        }
        public bool Borrow(int bookId, int AccountId)
        {
            bool isValid = bookId >= 0 && AccountId >= 0 && account.AccountLevel >= AccountLevelEnums.Librarian;
            if (!isValid)
                return false;
            BorrowMehod.Borrow(new Borrow(bookId, AccountId, DateTime.Today));
            return true;
        }
        public bool Borrow(int bookId)
        {
            bool isValid = bookId >= 0 && account.AccountLevel >= AccountLevelEnums.Patron;
            if (!isValid)
                return false;
            BorrowMehod.Borrow(new Borrow(bookId, account.ID, DateTime.Today));
            return true;
        }
        public bool Return(int borrowId)
        {
            bool isValid = borrowId >= 0 && account.AccountLevel >= AccountLevelEnums.Patron;
            if (!isValid)
                return false;
            BorrowMehod.Return(new Borrow(borrowId, DateTime.Today), false);
            return true;
        }

        public bool ReturnAdjustFine(int borrowId, decimal fine)
        {
            bool isValid = borrowId >= 0 && fine >= 0 && account.AccountLevel >= AccountLevelEnums.Librarian;
            if (!isValid)
                return false;
            BorrowMehod.Return(new Borrow(borrowId, DateTime.Today, fine), false);
            return true;
        }
        public decimal CalculateFine(DateTime borrowed, DateTime returned)
        {
            return BorrowMehod.CalculateFine(borrowed, returned);
        }
        #endregion
        private bool IsStringEmpty(params string[] textlist)
        {
            foreach (var item in textlist)
            {
                if (string.IsNullOrEmpty(item))
                    return true;
            }
            return false;
        }
        public List<BorrowReport> GetReport() {
            if (account.AccountLevel >= AccountLevelEnums.Manager)
            {
            return    BorrowMehod.GetReport();
            }
            throw new NotImplementedException();
        }

    }
}
