using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ToolBox.DataBaseConnectionTools
{
    public class Connection
    {
        private readonly string _ConnectionString;

        public Connection(string connectionString)
        {
            _ConnectionString = connectionString;

            using (SqlConnection connection = CreateConnection())
            {
                connection.Open();
            }
        }

        public int ExecuteNonQuery(Command command)
        {
            using (SqlConnection connection = CreateConnection())
            {
                using (SqlCommand Command = CreateCommand(command, connection))
                {
                    connection.Open();
                    int result = Command.ExecuteNonQuery();
                    return result;
                }
            }
        }

        public object ExecuteScalar(Command command)
        {
            using (SqlConnection connection = CreateConnection())
            {
                using (SqlCommand Command = CreateCommand(command, connection))
                {
                    connection.Open();
                    object result = Command.ExecuteScalar();
                    return (result is DBNull) ? null : result;
                }
            }
        }

        public DataTable GetDataTable(Command command)
        {
            using (SqlConnection connection = CreateConnection())
            {
                using (SqlCommand Command = CreateCommand(command, connection))
                {
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
                    {
                        dataAdapter.SelectCommand = Command;
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        return dataTable;
                    }
                }
            }
        }

        public T ExecuteReaderSingle<T>(Command command, Func<IDataReader, T> selector)
        {
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            using (SqlConnection connection = CreateConnection())
            {
                using (SqlCommand Command = CreateCommand(command, connection))
                {
                    connection.Open();
                    using (SqlDataReader sqlDataReader = Command.ExecuteReader())
                    {
                        sqlDataReader.Read();
                        return selector(sqlDataReader);
                    }
                }
            }
        }

        public IEnumerable<T> ExecuteReader<T>(Command command, Func<IDataReader, T> selector)
        {
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            using (SqlConnection connection = CreateConnection())
            {
                using (SqlCommand Command = CreateCommand(command, connection))
                {
                    connection.Open();
                    using (SqlDataReader sqlDataReader = Command.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            yield return selector(sqlDataReader);
                        }

                    }
                }
            }
        }

        private SqlCommand CreateCommand(Command command, SqlConnection connection)
        {
            SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = command.Query;

            if (command.IsStoredProcedure)
                sqlCommand.CommandType = CommandType.StoredProcedure;

            foreach (KeyValuePair<string, object> kvp in command.Parameters)
            {
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = kvp.Key;

                if (!(kvp.Value is ToolParameter))
                {
                    // general case
                    parameter.Value = kvp.Value;
                }
                else
                {
                    // case with Parameter object given , with DbType concept...
                    ToolParameter kvpparam = (ToolParameter)kvp.Value;
                    parameter.Value = kvpparam.Value;
                    parameter.SqlDbType = kvpparam.Type;

                }
                sqlCommand.Parameters.Add(parameter);
            }
            return sqlCommand;
        }

        private SqlConnection CreateConnection()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _ConnectionString;
            return connection;
        }
    }
}

