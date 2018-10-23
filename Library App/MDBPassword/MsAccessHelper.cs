using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace Library_App.MDBPassword { 
    /// <summary>
    ///  Database helper to execute query
    /// </summary>
    public class MsAccessHelper : IDatabaseSHelpertrategy
    {
        private static MsAccessHelper uniqueInstance;
        public string ConnectionString { get; private set; }

        /// <summary>
        /// Constructor to create connection string
        /// </summary>
        /// 
        private MsAccessHelper()
        {
            string cs = ConfigurationManager.ConnectionStrings["MDBPassword"].ConnectionString;
            ConnectionString = cs;
        }

        /// <summary>
        /// create only one instance for one database
        /// </summary>
        /// <param name="databaseFileLocation">Full path of database access file location</param>
        /// <param name="provider">version of the database access that used</param>
        /// <returns>returned database helper</returns>
        public static MsAccessHelper GetInstance()
        {
            bool isValid = uniqueInstance == null ;

            if (isValid)
            {
                uniqueInstance = new MsAccessHelper();
            }
            return uniqueInstance;
        }

        /// <summary>
        /// execute query with a set of parameters
        /// </summary>
        /// <param name="query">string of query</param>
        /// <param name="parameters">queue of oledb parameter</param>
        public void ExecuteQuery(string query, Queue<object> parameters)
        {
            try
            {
                using (OleDbConnection oledbConnection = new OleDbConnection(ConnectionString))
                {

                    oledbConnection.Open();
                    using (OleDbCommand oledbCommand = new OleDbCommand())
                    {
                        oledbCommand.Connection = oledbConnection;
                        oledbCommand.CommandText = query;

                        while (parameters.Count > 0)
                        {
                            oledbCommand.Parameters.Add(parameters.Dequeue());
                        }
                        oledbCommand.ExecuteScalar();

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
        /// <param name="parameters">queue of oledb parameter</param>
        /// <returns>query result</returns>
        public DataSet ExecuteReturnedQuery(string query, Queue<object> parameters)
        {
            try
            {
                using (OleDbConnection oledbConnection = new OleDbConnection(ConnectionString))
                {
                    using (OleDbCommand oledbCommand = new OleDbCommand())
                    {
                        
                        oledbCommand.Connection = oledbConnection;
                        oledbCommand.CommandText = query;

                        while (parameters.Count > 0)
                        {
                            oledbCommand.Parameters.Add(parameters.Dequeue());
                        }

                        using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(oledbCommand))
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
