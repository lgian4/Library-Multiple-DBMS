using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Library_App.MySql
{
    class AccountMethods : IAccountMethodStrategy
    {
        private IDatabaseSHelpertrategy DBHelper;


        public AccountMethods(IDatabaseSHelpertrategy dbHelpler)
        {
            DBHelper = dbHelpler;
        }
        public void Add(Account account)
        {
            bool isNotValid = string.IsNullOrEmpty(account.FullName) || string.IsNullOrEmpty(account.Username) || string.IsNullOrEmpty(account.Password);
            if (isNotValid)
            {
                throw new NotImplementedException();
            }

            #region setParameters
            Queue<object> parameters = new Queue<object>();
            parameters.Enqueue(new MySqlParameter("@FullName", account.FullName));
            parameters.Enqueue(new MySqlParameter("@Address", account.Address));
            parameters.Enqueue(new MySqlParameter("@PhoneNumber", account.PhoneNumber));

            // have to explicitly set to date type
            MySqlParameter publishedParameter = new MySqlParameter("@Opened", account.Opened);
            publishedParameter.DbType = DbType.Date;
            parameters.Enqueue(publishedParameter);
            parameters.Enqueue(new MySqlParameter("@Level", account.AccountLevel));
            parameters.Enqueue(new MySqlParameter("@Username", account.Username));

            string salt = AuthHelper.CreateSalt(32);
            string password = AuthHelper.CreatePasswordHash(account.Password, salt);
            parameters.Enqueue(new MySqlParameter("@Password", password));
            parameters.Enqueue(new MySqlParameter("@Salt", salt));
            #endregion
            string query = "INSERT INTO tblAccount ( Fullname, Address, PhoneNumber, Opened, Level, Username, Password, Salt ) VALUES (@FullName, @Address, @PhoneNumber, @Opened, @Level, @Username, @Password, @Salt)";
            DBHelper.ExecuteQuery(query, parameters);
        }

        public void ChangePassword(string username, string oldPassword, string newPassword)
        {
            int id = this.Login(username, oldPassword);
            if (id <= 0)
                throw new NotImplementedException();

            Queue<object> parameters = new Queue<object>();
            parameters.Enqueue(new MySqlParameter("@Username", username));
            string salt = AuthHelper.CreateSalt(32);
            string password = AuthHelper.CreatePasswordHash(newPassword, salt);
            parameters.Enqueue(new MySqlParameter("@Password", password));
            parameters.Enqueue(new MySqlParameter("@Salt", salt));
            parameters.Enqueue(new MySqlParameter("@ID", id));
            string query = "UPDATE tblAccount SET tblAccount.Username = @Username, tblAccount.Password = @Password, tblAccount.Salt = @Salt WHERE(((tblAccount.ID) =  @ID));";
            DBHelper.ExecuteQuery(query, parameters);
        }

        public int CountUsername(string username)
        {
            if (string.IsNullOrEmpty(username.Trim()))
                throw new NotImplementedException();

            Queue<object> parameters = new Queue<object>();
            parameters.Enqueue(new MySqlParameter("@Username", username));

            string query = "SELECT Count(*) from tblAccount WHERE tblAccount.Username = @Username";
            DataRow result = DBHelper.ExecuteReturnedQuery(query, parameters).Tables[0].Rows[0];
            return Convert.ToInt32(result[0].ToString());
        }

        public List<Account> Find(string inputquery)
        {
            if (string.IsNullOrEmpty(inputquery.Trim()))
                throw new NotImplementedException();

            Queue<object> parameters = new Queue<object>();
            parameters.Enqueue(new MySqlParameter("@inputQuery", "%" + inputquery.Trim() + "%"));

            string query = "SELECT tblAccount.ID, tblAccount.Fullname, tblAccount.Address, tblAccount.PhoneNumber, tblAccount.Opened, tblAccount.Level FROM tblAccount WHERE  tblAccount.ID like @inputQuery or tblAccount.Fullname like @inputQuery or tblAccount.Address like @inputQuery or tblAccount.PhoneNumber like @inputQuery or tblAccount.Opened like @inputQuery or tblAccount.Username like @inputQuery or tblAccount.Level like @inputQuery ;";

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

        public int Login(string username, string password)
        {
            bool isNotValid = string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password);
            if (isNotValid)
                throw new NotImplementedException();

            Queue<object> parameters = new Queue<object>();

            parameters.Enqueue(new MySqlParameter("@Username", username));

            string query = "SELECT tblAccount.ID, tblAccount.Username, tblAccount.Password, tblAccount.Salt FROM tblAccount WHERE tblAccount.Username = @Username;";

            var result = DBHelper.ExecuteReturnedQuery(query, parameters);
            foreach (DataRow row in result.Tables[0].Rows)
            {
                var Account = new Account(
                    Convert.ToInt32(row[0].ToString()),
                    row[1].ToString(),
                    row[2].ToString(),
                    row[3].ToString()
                    );
                bool isPassword = AuthHelper.CreatePasswordHash(password, Account.Salt) == Account.Password;
                if (isPassword)
                {
                    return Account.ID;
                }
            }
            return 0;
        }

        public List<Account> Read()
        {

            string query = "SELECT tblAccount.ID, tblAccount.Fullname, tblAccount.Address, tblAccount.PhoneNumber, tblAccount.Opened, tblAccount.Level FROM tblAccount;";
            Queue<object> parameters = new Queue<object>();
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

        public Account Read(int id)
        {
            if (id <= 0)
                throw new NotImplementedException();

            Queue<object> parameters = new Queue<object>();
            parameters.Enqueue(new MySqlParameter("@ID", id));

            string query = "SELECT tblAccount.ID, tblAccount.Fullname, tblAccount.Address, tblAccount.PhoneNumber, tblAccount.Opened, tblAccount.Level FROM tblAccount WHERE tblAccount.ID = @ID;";
            DataRow result = DBHelper.ExecuteReturnedQuery(query, parameters).Tables[0].Rows[0];
            return new Account(
               Convert.ToInt32(result[0].ToString()),
               result[1].ToString(),
               result[2].ToString(),
               result[3].ToString(),
               DateTime.Parse(result[4].ToString()),
               (AccountLevelEnums)Convert.ToInt32(result[5].ToString())
            );
        }

        public void Remove(int id)
        {
            if (id <= 0)
                throw new NotImplementedException();

            Queue<object> parameters = new Queue<object>();
            parameters.Enqueue(new MySqlParameter("@ID", id));

            string query = "DELETE tblAccount.* FROM tblAccount WHERE(((tblAccount.ID) = @ID)); ";
            DBHelper.ExecuteQuery(query, parameters);
        }

        public void Update(Account account)
        {
            if (account.ID <= 0)
                throw new NotImplementedException();

            Queue<object> parameters = new Queue<object>();
            parameters.Enqueue(new MySqlParameter("@FullName", account.FullName));
            parameters.Enqueue(new MySqlParameter("@Address", account.Address));
            parameters.Enqueue(new MySqlParameter("@PhoneNumber", account.PhoneNumber));


            parameters.Enqueue(new MySqlParameter("@ID", account.ID));

            string query = "UPDATE tblAccount SET tblAccount.Fullname = @Fullname, tblAccount.Address = @Address, tblAccount.PhoneNumber = @PhoneNumber WHERE(((tblAccount.ID) =  @ID));";
            DBHelper.ExecuteQuery(query, parameters);
        }
    }
}