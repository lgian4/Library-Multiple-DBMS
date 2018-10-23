using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_App.Ms_Access
{
    class BorrowMethods : IBorrowMethodStrategy
    {
        private IDatabaseSHelpertrategy DBHelper;

        public BorrowMethods(IDatabaseSHelpertrategy dbHelpler)
        {
            DBHelper = dbHelpler;
        }


        public List<Account> BookHistory(int bookId)
        {
            if (bookId <= 0)
                throw new NotImplementedException();

            string query = "select  tblAccount.ID, tblAccount.Fullname, tblAccount.Address, tblAccount.PhoneNumber, tblAccount.Opened, tblAccount.Level from ( tblBorrow inner join tblBook on tblBorrow.BookId = tblBook.ID) inner join tblAccount on tblBorrow.AccountId = tblAccount.ID where tblBook.ID = @ID; ";
            Queue<object> parameters = new Queue<object>();
            parameters.Enqueue(new OleDbParameter("@ID", bookId));
            DataSet result = DBHelper.ExecuteReturnedQuery(query, parameters);
            List<Account> resultAccount = new List<Account>();
            foreach (DataRow dataRow in result.Tables[0].Rows)
            {
                resultAccount.Add(new Account(
                    Convert.ToInt32(dataRow[0].ToString()),
                    dataRow[1].ToString(),
                    dataRow[2].ToString(),
                    dataRow[3].ToString(),
                    DateTime.Parse(dataRow[4].ToString()),
                    (AccountLevelEnums)Convert.ToInt32(dataRow[5].ToString())
                    ));
            }
            return resultAccount;
        }
        public List<JoinBorrow> Find(string text)
        {
            string query = "select  tblBorrow.ID, tblBook.Title, tblBook.Author, tblBook.Publisher, tblBook.PublishedDate, tblAccount.FullName, tblAccount.Address, tblAccount.PhoneNumber, tblAccount.Opened, tblAccount.Level, tblBorrow.Borrowed, tblBorrow.Returned, tblBorrow.Fine from ( tblBorrow inner join tblBook on tblBorrow.BookId = tblBook.ID) inner join tblAccount on tblBorrow.AccountId = tblAccount.ID where tblBorrow.ID like '%'+@inputQuery+'%' or tblBook.Title like '%'+@inputQuery+'%' or tblBook.Publisher like '%'+@inputQuery+'%' or tblBook.PublishedDate like '%'+@inputQuery+'%' or tblAccount.FullName like '%'+@inputQuery+'%' or  tblAccount.Address like '%'+@inputQuery+'%' or tblAccount.PhoneNumber like '%'+@inputQuery+'%' or tblAccount.Opened like '%'+@inputQuery+'%' or tblAccount.Level like '%'+@inputQuery+'%' or tblBorrow.Borrowed like '%'+@inputQuery+'%' or tblBorrow.Returned like '%'+@inputQuery+'%' or tblBorrow.Fine like '%'+@inputQuery+'%'  ;";
            Queue<object> parameters = new Queue<object>();
            parameters.Enqueue(new OleDbParameter("@Query", text));
            DataSet result = DBHelper.ExecuteReturnedQuery(query, parameters);
            List<JoinBorrow> resultAccount = new List<JoinBorrow>();
            foreach (DataRow dataRow in result.Tables[0].Rows)
            {
                int id = Convert.ToInt32(dataRow[0].ToString());
                string title = dataRow[1].ToString();
                string author = dataRow[2].ToString();
                string publisher = dataRow[3].ToString();
                DateTime published = DateTime.Parse(dataRow[4].ToString());
                string fullname = dataRow[5].ToString();
                string address = dataRow[6].ToString();
                string phone = dataRow[7].ToString();
                DateTime opened = DateTime.Parse(dataRow[8].ToString());
                AccountLevelEnums level = (AccountLevelEnums)Convert.ToInt32(dataRow[9].ToString());
                DateTime borrowed = DateTime.Parse(dataRow[10].ToString());
                DateTime returned = string.IsNullOrEmpty(dataRow[12].ToString()) ? DateTime.MinValue : Convert.ToDateTime(dataRow[11].ToString());
                decimal fine = string.IsNullOrEmpty(dataRow[12].ToString()) ? 0 : Convert.ToDecimal(dataRow[12].ToString());
                resultAccount.Add(new JoinBorrow(
                id, title, author, publisher, published, fullname, address, phone, opened, level, borrowed, returned, fine));
            }
            return resultAccount;
        }
        public void Borrow(Borrow borrow)
        {
            bool isNotValid = borrow.BookID <= 0 || borrow.AccountID <= 0;
            if (isNotValid)
            {
                throw new NotImplementedException();
            }
            Queue<object> parameters = new Queue<object>();
            parameters.Enqueue(new OleDbParameter("@BookId", borrow.BookID));
            parameters.Enqueue(new OleDbParameter("@AccountId", borrow.AccountID));

            // have to explicitly set to date type
            OleDbParameter publishedParameter = new OleDbParameter("@Borrowed", borrow.Borrowed);
            publishedParameter.OleDbType = OleDbType.Date;
            parameters.Enqueue(publishedParameter);

            string query = "INSERT INTO tblBorrow ( BookId, AccountId, Borrowed ) VALUES (@BookId, @AccountId, @Borrowed)";
            DBHelper.ExecuteQuery(query, parameters);
        }


        public List<JoinBorrow> Read()
        {
            string query = "select  tblBorrow.ID, tblBook.Title, tblBook.Author, tblBook.Publisher, tblBook.PublishedDate, tblAccount.FullName, tblAccount.Address, tblAccount.PhoneNumber, tblAccount.Opened, tblAccount.Level, tblBorrow.Borrowed, tblBorrow.Returned, tblBorrow.Fine from ( tblBorrow inner join tblBook on tblBorrow.BookId = tblBook.ID) inner join tblAccount on tblBorrow.AccountId = tblAccount.ID;";
            Queue<object> parameters = new Queue<object>();
            DataSet result = DBHelper.ExecuteReturnedQuery(query, parameters);
            List<JoinBorrow> resultAccount = new List<JoinBorrow>();
            foreach (DataRow dataRow in result.Tables[0].Rows)
            {
                int id = Convert.ToInt32(dataRow[0].ToString());
                string title = dataRow[1].ToString();
                string author = dataRow[2].ToString();
                string publisher = dataRow[3].ToString();
                DateTime published = DateTime.Parse(dataRow[4].ToString());
                string fullname = dataRow[5].ToString();
                string address = dataRow[6].ToString();
                string phone = dataRow[7].ToString();
                DateTime opened = DateTime.Parse(dataRow[8].ToString());
                AccountLevelEnums level = (AccountLevelEnums)Convert.ToInt32(dataRow[9].ToString());
                DateTime borrowed = DateTime.Parse(dataRow[10].ToString());
                DateTime returned = string.IsNullOrEmpty(dataRow[12].ToString()) ? DateTime.MinValue : Convert.ToDateTime(dataRow[11].ToString());
                decimal fine = string.IsNullOrEmpty(dataRow[12].ToString()) ? 0 : Convert.ToDecimal(dataRow[12].ToString());
                resultAccount.Add(new JoinBorrow(
                id, title, author, publisher, published, fullname, address, phone, opened, level, borrowed, returned, fine));
            }
            return resultAccount;
        }
        public List<JoinBorrow> AccountHistory(int accountId)
        {
            string query = "select  tblBorrow.ID, tblBook.Title, tblBook.Author, tblBook.Publisher, tblBook.PublishedDate, tblAccount.FullName, tblAccount.Address, tblAccount.PhoneNumber, tblAccount.Opened, tblAccount.Level, tblBorrow.Borrowed, tblBorrow.Returned, tblBorrow.Fine from ( tblBorrow inner join tblBook on tblBorrow.BookId = tblBook.ID) inner join tblAccount on tblBorrow.AccountId = tblAccount.ID where tblAccount.ID = @AccountID;";
            Queue<object> parameters = new Queue<object>();
            parameters.Enqueue(new OleDbParameter("@AccountID", accountId));
            DataSet result = DBHelper.ExecuteReturnedQuery(query, parameters);
            List<JoinBorrow> resultAccount = new List<JoinBorrow>();
            foreach (DataRow dataRow in result.Tables[0].Rows)
            {
                int id = Convert.ToInt32(dataRow[0].ToString());
                string title = dataRow[1].ToString();
                string author = dataRow[2].ToString();
                string publisher = dataRow[3].ToString();
                DateTime published = DateTime.Parse(dataRow[4].ToString());
                string fullname = dataRow[5].ToString();
                string address = dataRow[6].ToString();
                string phone = dataRow[7].ToString();
                DateTime opened = DateTime.Parse(dataRow[8].ToString());
                AccountLevelEnums level = (AccountLevelEnums)Convert.ToInt32(dataRow[9].ToString());
                DateTime borrowed = DateTime.Parse(dataRow[10].ToString());
                DateTime returned = string.IsNullOrEmpty(dataRow[12].ToString()) ? DateTime.MinValue : Convert.ToDateTime(dataRow[11].ToString());
                decimal fine = string.IsNullOrEmpty(dataRow[12].ToString()) ? 0 : Convert.ToDecimal(dataRow[12].ToString());
                resultAccount.Add(new JoinBorrow(
                id, title, author, publisher, published, fullname, address, phone, opened, level, borrowed, returned, fine));
            }
            return resultAccount;
        }
        public JoinBorrow Read(int id)
        {
            if (id <= 0)
                throw new NotFiniteNumberException();
            Queue<object> parameters = new Queue<object>();
            parameters.Enqueue(new OleDbParameter("@ID", id));
            string query = "select  tblBorrow.ID, tblBook.Title, tblBook.Author, tblBook.Publisher, tblBook.PublishedDate, tblAccount.FullName, tblAccount.Address, tblAccount.PhoneNumber, tblAccount.Opened, tblAccount.Level, tblBorrow.Borrowed, tblBorrow.Returned, tblBorrow.Fine from ( tblBorrow inner join tblBook on tblBorrow.BookId = tblBook.ID) inner join tblAccount on tblBorrow.AccountId = tblAccount.ID where tblBorrow.ID = @ID;";
            DataSet result = DBHelper.ExecuteReturnedQuery(query, parameters);
            foreach (DataRow dataRow in result.Tables[0].Rows)
            {
                int ida = Convert.ToInt32(dataRow[0].ToString());
                string title = dataRow[1].ToString();
                string author = dataRow[2].ToString();
                string publisher = dataRow[3].ToString();
                DateTime published = DateTime.Parse(dataRow[4].ToString());
                string fullname = dataRow[5].ToString();
                string address = dataRow[6].ToString();
                string phone = dataRow[7].ToString();
                DateTime opened = DateTime.Parse(dataRow[8].ToString());
                AccountLevelEnums level = (AccountLevelEnums)Convert.ToInt32(dataRow[9].ToString());
                DateTime borrowed = DateTime.Parse(dataRow[10].ToString());
                DateTime returned = string.IsNullOrEmpty(dataRow[12].ToString()) ? DateTime.MinValue : Convert.ToDateTime(dataRow[11].ToString());
                decimal fine = string.IsNullOrEmpty(dataRow[12].ToString()) ? 0 : Convert.ToDecimal(dataRow[12].ToString());
                return new JoinBorrow(
                    ida, title, author, publisher, published, fullname, address, phone, opened, level, borrowed, returned, fine);
            }
            return new JoinBorrow();
        }

        private void Return(Borrow borrow)
        {
            if (borrow.ID <= 0)
            {
                throw new NotImplementedException();
            }
            Queue<object> parameters = new Queue<object>();
            // have to explicitly set to date type
            OleDbParameter publishedParameter = new OleDbParameter("@Returned", borrow.Returned);
            publishedParameter.OleDbType = OleDbType.Date;
            parameters.Enqueue(publishedParameter);

            publishedParameter = new OleDbParameter("@Fine", borrow.Fine);
            publishedParameter.OleDbType = OleDbType.Currency;
            parameters.Enqueue(publishedParameter);

            parameters.Enqueue(new OleDbParameter("@ID", borrow.ID));

            string query = "UPDATE tblBorrow SET tblBorrow.Returned = @Returned, tblBorrow.Fine = @Fine where  tblBorrow.ID = @ID;";
            DBHelper.ExecuteQuery(query, parameters);
        }
        public void Return(Borrow borrow, bool wasFineCalculated)
        {
            if (wasFineCalculated)
                Return(borrow);
            var selectedBorrow = this.Read(borrow.ID);
            decimal fine = this.CalculateFine(selectedBorrow.Borrowed, borrow.Returned);
            Return(new Borrow(borrow.ID, borrow.Returned, fine));
        }
        public decimal CalculateFine(DateTime borrowed, DateTime returned)
        {
            bool isNotValid = borrowed == DateTime.MinValue || returned == DateTime.MinValue || borrowed > returned;
            if (isNotValid)
                throw new NotImplementedException();

            // get late return date
            int days = (int)(returned - borrowed).TotalDays - Constants.MAX_BORROW_DAYS;
            days = days > Constants.MAX_BORROW_DAYS ? days : 0;
            // get fine
            decimal fine = days * Constants.FINE_PER_DAYS > Constants.MAX_FINE ? Constants.MAX_FINE : days * Constants.FINE_PER_DAYS;
            return fine;
        }

        public List<BorrowReport> GetReport()
        {
            string query = "SELECT tblBorrow.Borrowed , tblBorrow.Fine  FROM (tblBorrow INNER JOIN tblBook ON tblBorrow.BookId = tblBook.ID) INNER JOIN tblAccount ON tblBorrow.AccountId = tblAccount.ID";
            Queue<object> parameters = new Queue<object>();
            DataSet result = DBHelper.ExecuteReturnedQuery(query, parameters);
            List<BorrowReport> resultReport = new List<BorrowReport>();
            foreach (DataRow dataRow in result.Tables[0].Rows)
            {
                DateTime month = Convert.ToDateTime(dataRow[0].ToString());
                decimal fine = string.IsNullOrEmpty(dataRow[1].ToString()) ? 0 : Convert.ToDecimal(dataRow[1].ToString());
                resultReport.Add(new BorrowReport(month, fine));
            }
            return resultReport;
        }
    }
}
