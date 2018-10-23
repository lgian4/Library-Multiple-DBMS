using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_App
{
    public struct BorrowReport
    {
        public DateTime Dates { get; set; }
        public decimal SumOfFine { get; set; }
        public BorrowReport(DateTime date,  decimal fine)
        {
            Dates = date;
            SumOfFine = fine;
        }
        
    }
    public struct Book
    {
        public int ID { get; private set; }
        public string Title { get; private set; }
        public string Author { get; private set; }
        public string Publisher { get; private set; }
        public DateTime PublishedDate { get; private set; }
        public Book(int id, string title, string author, string publisher, DateTime publishedDate)
        {
            ID = id;
            Title = title.Trim();
            Author = author.Trim();
            Publisher = publisher.Trim();
            PublishedDate = publishedDate;
        }
        public Book(string title, string author, string publisher, DateTime publishedDate) : this(0, title, author, publisher, publishedDate) { }
        public override string ToString()
        {
            return string.Format("BOOK ID       : {0}\nTitle         : {1}\nAuthor        : {2}\nPublisher     : {3}\nPublished Date: {4}\n", ID, Title, Author, Publisher, PublishedDate);
        }
    }

    public enum AccountLevelEnums
    {
        Guest = 0,
        Patron = 1,
        Librarian = 2,
        Manager = 3
    }
    public enum DBFactoryEnums
    {
        MsAccess = 1, MsAccessPassword = 2, MDBPassword = 3, MySql = 4, MsSQLServerExpress = 5
    }
    public struct Account
    {
        public int ID { get; private set; }
        public string FullName { get; private set; }
        public string Address { get; private set; }
        public string PhoneNumber { get; private set; }
        public DateTime Opened { get; private set; }
        public AccountLevelEnums AccountLevel { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Salt { get; private set; }
        public Account(int id, string fullName, string address, string phoneNumber, DateTime opened, AccountLevelEnums accountLevel, string username, string password, string salt)
        {
            ID = id;
            FullName = fullName.Trim();
            Address = address.Trim();
            PhoneNumber = phoneNumber.Trim();
            Opened = opened;
            AccountLevel = accountLevel;
            Username = username;
            Password = password;
            Salt = salt;
        }

        public Account(string fullName, string address, string phoneNumber, DateTime opened, AccountLevelEnums accountLevel, string username, string password, string salt) : this(0, fullName, address, phoneNumber, opened, accountLevel, username, password, salt) { }


        public Account(string fullName, string address, string phoneNumber, DateTime opened, AccountLevelEnums accountLevel, string username, string password) : this(0, fullName, address, phoneNumber, opened, accountLevel, username, password, "") { }

        public Account(int id, string fullName, string address, string phoneNumber, DateTime opened, AccountLevelEnums accountLevel) : this(id, fullName, address, phoneNumber, opened, accountLevel, "", "", "") { }
        public Account(string fullName, string address, string phoneNumber, int id) : this(id, fullName, address, phoneNumber, DateTime.Today, AccountLevelEnums.Guest, "", "", "") { }

        public Account(string username, string password) : this(0, "", "", "", DateTime.MinValue, AccountLevelEnums.Guest, username, password, "") { }

        public Account(int id, string username, string password, string salt) : this(id, "", "", "", DateTime.MinValue, AccountLevelEnums.Guest, username, password, salt) { }

        public Account(int id) : this(0, "", "", "", DateTime.MinValue, AccountLevelEnums.Guest, "", "", "") { }
        public override string ToString()
        {
            return string.Format("ID {0} Account\t{1}\nAddress: {2}\nPhone  : {3}\nOpened : {4}\nLevel  : {5}\n", ID, FullName, Address, PhoneNumber, Opened, AccountLevel);
        }
    }

    public struct Borrow
    {
        public int ID { get; private set; }
        public int BookID { get; private set; }
        public int AccountID { get; private set; }
        public DateTime Borrowed { get; private set; }
        public DateTime Returned { get; private set; }
        public decimal Fine;

        public Borrow(int id, int bookId, int accountId, DateTime borrowed, DateTime returned, decimal fine)
        {
            ID = id;
            BookID = bookId;
            AccountID = accountId;
            Borrowed = borrowed;
            Returned = returned;
            Fine = fine;
        }
        public Borrow(int bookId, int accountId, DateTime borrowed) : this(0, bookId, accountId, borrowed, DateTime.MinValue, 0) { }
        public Borrow(int id, DateTime returned, decimal fine) : this(id, 0, 0, DateTime.MinValue, returned, fine) { }
        public Borrow(int id, DateTime returned) : this(id, 0, 0, DateTime.MinValue, returned, 0) { }

    }
    public enum MonthOfYear
    {
        January = 1, Febuary = 2, March = 3, April = 4, May = 5, June = 6, July = 7, August = 8, September = 9, October = 10, November = 11, December = 12
    }
   
    public struct JoinBorrow
    {
        public int ID { get; private set; }
        public string Title { get; private set; }
        public string Author { get; private set; }
        public string Publisher { get; private set; }
        public DateTime PublishedDate { get; private set; }
        public string FullName { get; private set; }
        public string Address { get; private set; }
        public string PhoneNumber { get; private set; }
        public DateTime Opened { get; private set; }
        public AccountLevelEnums AccountLevel { get; private set; }
        public DateTime Borrowed { get; private set; }
        public DateTime Returned { get; private set; }
        public decimal Fine;

        public JoinBorrow(int id, string title, string author, string publisher, DateTime publishedDate, string fullName, string address, string phone, DateTime opened, AccountLevelEnums level, DateTime borrowed, DateTime returned, decimal fine)
        {
            ID = id;
            Title = title;
            Author = author;
            Publisher = publisher;
            PublishedDate = publishedDate;
            FullName = fullName;
            Address = address;
            PhoneNumber = phone;
            Opened = opened;
            AccountLevel = level;
            Borrowed = borrowed;
            Returned = returned;
            Fine = fine;

        }

        public override string ToString()
        {
            return string.Format("ID {0} \nName   : {1,-20} | Book's Title {2}\nAddress: {3,-20} | Author: {4}\nPhone  : {5,-20} | Publisher: {6}\nLevel  : {7,-20} | Published: {8}\nAccount Opened: {9}\nBorrowed: {10}\nReturned: {11}\nFine: {12}\n", ID, FullName, Title, Address, Author, PhoneNumber, Publisher, AccountLevel, PublishedDate, Opened, Borrowed, Returned, Fine);
        }

    }
}