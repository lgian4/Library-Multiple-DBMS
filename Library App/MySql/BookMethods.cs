using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Library_App.MySql
{
    class BookMethods : IBookMethodStrategy
    {
        private IDatabaseSHelpertrategy DBHelper;


        public BookMethods(IDatabaseSHelpertrategy dbHelpler)
        {
            DBHelper = dbHelpler;
        }

        /// <summary>
        /// Add book to Table Book
        /// </summary>
        /// <param name="book">Book to Add</param>
        public void Add(Book book)
        {
            bool isNotValid = string.IsNullOrEmpty(book.Title);
            if (isNotValid)
            {
                throw new NotImplementedException();
            }
            Queue<object> parameters = new Queue<object>();
            parameters.Enqueue(new MySqlParameter("@Title", book.Title));
            parameters.Enqueue(new MySqlParameter("@Author", book.Author));
            parameters.Enqueue(new MySqlParameter("@Publisher", book.Publisher));

            // have to explicitly set to date type
            MySqlParameter publishedParameter = new MySqlParameter("@PublishedDate", book.PublishedDate);
            publishedParameter.DbType = DbType.Date;
            parameters.Enqueue(publishedParameter);

            string query = "INSERT INTO tblBook ( Title, Author, Publisher, PublishedDate ) VALUES (@Title, @Author, @Publisher, @PublishedDate)";
            DBHelper.ExecuteQuery(query, parameters);
        }
        /// <summary>
        /// Find list of book 
        /// </summary>
        /// <param name="inputquery">Search query</param>
        /// <returns>List of book with field similar with inputQuery</returns>
        public List<Book> Find(string inputquery)
        {
            if (string.IsNullOrEmpty(inputquery.Trim()))
                throw new NotImplementedException();

            Queue<object> parameters = new Queue<object>();
            parameters.Enqueue(new MySqlParameter("@inputQuery","%" +inputquery.Trim()+"%"));

            string query = "SELECT tblBook.ID, tblBook.Title, tblBook.Author, tblBook.Publisher, tblBook.PublishedDate FROM tblBook WHERE  tblBook.ID like @inputQuery or  tblBook.Title like @inputQuery  or  tblBook.Author like @inputQuery or  tblBook.Publisher like @inputQuery or  tblBook.PublishedDate like @inputQuery;";
            DataSet result = DBHelper.ExecuteReturnedQuery(query, parameters);
            List<Book> resultBook = new List<Book>();
            foreach (DataRow dataRow in result.Tables[0].Rows)
            {
                resultBook.Add(new Book(
                    Convert.ToInt32(dataRow[0].ToString()),
                    dataRow[1].ToString(),
                    dataRow[2].ToString(),
                    dataRow[3].ToString(),
                    DateTime.Parse(dataRow[4].ToString())
                    ));
            }
            return resultBook;
        }
        /// <summary>
        /// Read all rows of the book table
        /// </summary>
        /// <returns>List of book</returns>
        public List<Book> Read()
        {
            string query = "SELECT tblBook.ID, tblBook.Title, tblBook.Author, tblBook.Publisher, tblBook.PublishedDate FROM tblBook;";
            Queue<object> parameters = new Queue<object>();
            DataSet result = DBHelper.ExecuteReturnedQuery(query, parameters);
            List<Book> resultBook = new List<Book>();
            foreach (DataRow dataRow in result.Tables[0].Rows)
            {
                resultBook.Add(new Book(
                    Convert.ToInt32(dataRow[0].ToString()),
                    dataRow[1].ToString(),
                    dataRow[2].ToString(),
                    dataRow[3].ToString(),
                    DateTime.Parse(dataRow[4].ToString())
                    ));
            }
            return resultBook;
        }

        /// <summary>
        /// Read one book of a specific ID
        /// </summary>
        /// <param name="id">ID of the book</param>
        /// <returns>Book with the input ID</returns>
        public Book Read(int id)
        {
            if (id <= 0)
                throw new NotImplementedException();

            Queue<object> parameters = new Queue<object>();
            parameters.Enqueue(new MySqlParameter("@ID", id));

            string query = "SELECT tblBook.ID, tblBook.Title, tblBook.Author, tblBook.Publisher, tblBook.PublishedDate FROM tblBook WHERE tblBook.ID = @ID;";
            DataRow result = DBHelper.ExecuteReturnedQuery(query, parameters).Tables[0].Rows[0];
            return new Book(
               Convert.ToInt32(result[0].ToString()),
               result[1].ToString(),
               result[2].ToString(),
               result[3].ToString(),
               DateTime.Parse(result[4].ToString())
            );
        }
        /// <summary>
        /// Remove a specific book
        /// </summary>
        /// <param name="id">id of book that want to remove</param>
        public void Remove(int id)
        {
            if (id <= 0)
                throw new NotImplementedException();

            Queue<object> parameters = new Queue<object>();
            parameters.Enqueue(new MySqlParameter("@ID", id));

            string query = "DELETE tblBook.* FROM tblBook WHERE(((tblBook.ID) = @ID)); ";
            DBHelper.ExecuteQuery(query, parameters);
        }
        /// <summary>
        /// update a book based of input ID
        /// </summary>
        /// <param name="book">changed book</param>
        public void Update(Book book)
        {
            if (book.ID <= 0)
                throw new NotImplementedException();

            Queue<object> parameters = new Queue<object>();
            parameters.Enqueue(new MySqlParameter("@Title", book.Title));
            parameters.Enqueue(new MySqlParameter("@Author", book.Author));
            parameters.Enqueue(new MySqlParameter("@Publisher", book.Publisher));

            // have to explicitly set to date type
            MySqlParameter publishedParameter = new MySqlParameter("@PublishedDate", book.PublishedDate);
            publishedParameter.DbType = DbType.Date;
            parameters.Enqueue(publishedParameter);
            parameters.Enqueue(new MySqlParameter("@ID", book.ID));

            string query = "UPDATE tblBook SET tblBook.Title = @Title, tblBook.Author = @Author, tblBook.Publisher = @Publisher, tblBook.PublishedDate = @PublishedDate WHERE(((tblBook.ID) =  @ID));";
            DBHelper.ExecuteQuery(query, parameters);

        }
    }
}
