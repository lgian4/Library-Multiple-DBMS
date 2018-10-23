using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace Library_App.MySql
{
    class MysqlHelper : IDatabaseSHelpertrategy
    {
        private static MysqlHelper uniqueInstance;
        public string ConnectionString { get; private set; }
        private MysqlHelper()
        {
            string cs = ConfigurationManager.ConnectionStrings["MySql"].ConnectionString;
            ConnectionString = cs;
        }
        public void ExecuteQuery(string query, Queue<object> parameters)
        {
            try
            {
                using (MySqlConnection MySqlConnection = new MySqlConnection(ConnectionString))
                {

                    MySqlConnection.Open();
                    using (MySqlCommand MySqlCommand = new MySqlCommand())
                    {
                        MySqlCommand.Connection = MySqlConnection;
                        MySqlCommand.CommandText = query;

                        while (parameters.Count > 0)
                        {
                            MySqlCommand.Parameters.Add(parameters.Dequeue());
                        }
                        MySqlCommand.ExecuteScalar();

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static MysqlHelper GetInstance()
        {
            bool isValid = uniqueInstance == null;

            if (isValid)
            {
                uniqueInstance = new MysqlHelper();
            }
            return uniqueInstance;
        }
        public DataSet ExecuteReturnedQuery(string query, Queue<object> parameters)
        {
            try
            {
                using (MySqlConnection MySqlConnection = new MySqlConnection(ConnectionString))
                {
                    using (MySqlCommand MySqlCommand = new MySqlCommand())
                    {

                        MySqlCommand.Connection = MySqlConnection;
                        MySqlCommand.CommandText = query;

                        while (parameters.Count > 0)
                        {
                            MySqlCommand.Parameters.Add(parameters.Dequeue());
                        }

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(MySqlCommand))
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
