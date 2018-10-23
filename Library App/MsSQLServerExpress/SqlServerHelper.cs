using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace Library_App.MsSQLServerExpress
{
    public class SqlServerHelper : IDatabaseSHelpertrategy
    {
        private static SqlServerHelper uniqueInstance;
        public string ConnectionString { get; private set; }

        /// <summary>
        /// Constructor to create connection string
        /// </summary>
        /// 
        private SqlServerHelper()
        {
            string cs = ConfigurationManager.ConnectionStrings["MsSQLServerExpress"].ConnectionString;
            ConnectionString = cs;
        }

        /// <summary>
        /// create only one instance for one database
        /// </summary>
        /// <param name="databaseFileLocation">Full path of database access file location</param>
        /// <param name="provider">version of the database access that used</param>
        /// <returns>returned database helper</returns>
        public static SqlServerHelper GetInstance()
        {
            bool isValid = uniqueInstance == null;

            if (isValid)
            {
                uniqueInstance = new SqlServerHelper();
            }
            return uniqueInstance;
        }

        /// <summary>
        /// execute query with a set of parameters
        /// </summary>
        /// <param name="query">string of query</param>
        /// <param name="parameters">queue of Sql parameter</param>
        public void ExecuteQuery(string query, Queue<object> parameters)
        {
            try
            {
                using (SqlConnection SqlConnection = new SqlConnection(ConnectionString))
                {

                    SqlConnection.Open();
                    using (SqlCommand SqlCommand = new SqlCommand())
                    {
                        SqlCommand.Connection = SqlConnection;
                        SqlCommand.CommandText = query;

                        while (parameters.Count > 0)
                        {
                            SqlCommand.Parameters.Add(parameters.Dequeue());
                        }
                        SqlCommand.ExecuteScalar();

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// execute query that returned a result
        /// </summary>
        /// <param name="query">string of query</param>
        /// <param name="parameters">queue of Sql parameter</param>
        /// <returns>query result</returns>
        public DataSet ExecuteReturnedQuery(string query, Queue<object> parameters)
        {
            try
            {
                using (SqlConnection SqlConnection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand SqlCommand = new SqlCommand())
                    {

                        SqlCommand.Connection = SqlConnection;
                        SqlCommand.CommandText = query;

                        while (parameters.Count > 0)
                        {
                            SqlCommand.Parameters.Add(parameters.Dequeue());
                        }

                        using (SqlDataAdapter dataAdapter = new SqlDataAdapter(SqlCommand))
                        {
                            using (DataSet dataSet = new DataSet())
                            {
                                dataAdapter.Fill(dataSet);
                                return dataSet;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
