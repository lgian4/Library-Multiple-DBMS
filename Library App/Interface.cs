using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_App
{
    public interface IDatabaseSHelpertrategy
    {
        void ExecuteQuery(string query, Queue<object> parameters);
        DataSet ExecuteReturnedQuery(string query, Queue<object> parameters);
    }
    public interface IBookMethodStrategy
    {
        void Add(Book book);
        void Remove(int id);
        void Update(Book book);
        Book Read(int id);
        List<Book> Find(string query);
        List<Book> Read();
    }
    public enum TblEnums
    {
        Book = 1, Account = 2, Borrow = 3
    }
    public interface IAccountMethodStrategy
    {
        void Add(Account account);
        void Remove(int id);
        void Update(Account account);
        void ChangePassword(string username, string oldPassword, string newPassword);
        Account Read(int id);
        List<Account> Find(string query);
        List<Account> Read();
        int Login(string username, string password);
        int CountUsername(string username);
    }
    public interface IBorrowMethodStrategy
    {
        void Borrow(Borrow borrow);
        void Return(Borrow borrow, bool wasFineCalculated);
        JoinBorrow Read(int id);
        List<JoinBorrow> Find(string searchtext);
        List<JoinBorrow> Read();
        List<JoinBorrow> AccountHistory(int accountId);
        List<Account> BookHistory(int bookId);
        decimal CalculateFine(DateTime borrowed, DateTime returned);
        List<BorrowReport> GetReport();

    }
    
    public interface ILibraryFactory
    {
        IBookMethodStrategy CreateBook();
        IAccountMethodStrategy CreateAccount();
        IBorrowMethodStrategy CreateBorrow();
    }

}
