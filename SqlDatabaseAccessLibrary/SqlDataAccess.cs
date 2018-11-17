using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

namespace SqlDatabaseAccessLibrary
{
    public static class SqlDataAccess
    {
        /// <summary>
        /// Checks if connection to the database can be established.
        /// </summary>
        /// <param name="connectionString">Connection string to the database.</param>
        /// <returns>True if connection can be established; otherwise, false.</returns>
        public static bool HasConnection(string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    connection.Close();

                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Execute an asynchronous query command to the database.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionString">Connection string to the database.</param>
        /// <param name="query">Command to execute.</param>
        /// <returns>Collection of results with object type specified.</returns>
        public static async Task<IEnumerable<T>> ExecuteQueryAsync<T>(string connectionString, string query)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                return await connection.QueryAsync<T>(query);
            }
        }

        /// <summary>
        /// Execute an asynchronous parameterized query command to the database.
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="connectionString">Connection string to the database.</param>
        /// <param name="query">Command to execute.</param>
        /// <param name="data">Object containing the input data.</param>
        /// <returns>Collection of results with object type specified.</returns>
        public static async Task<IEnumerable<TOut>> ExecuteQueryAsync<TIn, TOut>(string connectionString, string query, TIn data)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                return await connection.QueryAsync<TOut>(query, data);
            }
        }

        /// <summary>
        /// Executes an asynchronous parameterized stored procedure command to the database.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionString">Connection string to the database.</param>
        /// <param name="storedProcedure">Name of stored procedure to execute.</param>
        /// <param name="param">Parameterized query.</param>
        /// <returns>Collection of results with object type specified.</returns>
        public static async Task<IEnumerable<T>> ExecuteQueryAsync<T>(string connectionString, string storedProcedure, object param)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                return await connection.QueryAsync<T>(storedProcedure, param, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Executes an asynchronous table-valued parameter stored procedure command to the database.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionString">Connection string to the database.</param>
        /// <param name="storedProcedure">Name of stored procedure to execute.</param>
        /// <param name="parameters">DataTable containing the parameters for the command.</param>
        /// <returns>Collection of results with object type specified.</returns>
        public static async Task<IEnumerable<T>> ExecuteQueryAsync<T>(string connectionString, string storedProcedure, DataTable parameters)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                return await connection.QueryAsync<T>(storedProcedure,
                    new { @Values = parameters.AsTableValuedParameter() }, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Executes an asynchronous non query command to the database.
        /// </summary>
        /// <param name="connectionString">Connection string to the database.</param>
        /// <param name="query">Command to execute.</param>
        /// <returns>Number of rows affected.</returns>
        public static async Task<int> ExecuteNonQueryAsync(string connectionString, string query)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                return await connection.ExecuteAsync(query);
            }
        }

        /// <summary>
        /// Executes an asynchronous non query parameterized command to the database.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionString">Connection string to the database.</param>
        /// <param name="query">Command to execute.</param>
        /// <param name="data">Object containing the input data.</param>
        /// <returns>Number of rows affected.</returns>
        public static async Task<int> ExecuteNonQueryAsync<T>(string connectionString, string query, T data)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                return await connection.ExecuteAsync(query, data);
            }
        }

        /// <summary>
        /// Executes an asynchronous non query parameterized stored procedure command to the database.
        /// </summary>
        /// <param name="connectionString">Connection string to the database.</param>
        /// <param name="storedProcedure">Name of stored procedure to execute.</param>
        /// <param name="param">Parameterized query.</param>
        /// <returns>Number of rows affected.</returns>
        public static async Task<int> ExecuteNonQueryAsync(string connectionString, string storedProcedure, object param)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                return await connection.ExecuteAsync(storedProcedure, param, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Executes an asynchronous non query table-valued parameter stored procedure command to the database.
        /// </summary>
        /// <param name="connectionString">Connection string to the database.</param>
        /// <param name="storedProcedure">Name of stored procedure to execute.</param>
        /// <param name="parameters">DataTable containing the parameters for the command.</param>
        /// <returns>Number of rows affected.</returns>
        public static async Task<int> ExecuteNonQueryAsync(string connectionString, string storedProcedure, DataTable parameters)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                return await connection.ExecuteAsync(storedProcedure,
                    new { @Values = parameters.AsTableValuedParameter() }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
