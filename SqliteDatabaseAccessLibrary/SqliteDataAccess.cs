using Dapper;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;

namespace SqliteDatabaseAccessLibrary
{
    public static class SqliteDataAccess
    {
        /// <summary>
        /// Checks if connection to the database can be established.
        /// </summary>
        /// <param name="connectionString">Connection string to the database.</param>
        /// <returns>True if connection can be established; otherwise, false.</returns>
        public static bool HasConnection(string connectionString)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    connection.Close();

                    return true;
                }
                catch (SQLiteException)
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
            using (var connection = new SQLiteConnection(connectionString))
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
            using (var connection = new SQLiteConnection(connectionString))
            {
                return await connection.QueryAsync<TOut>(query, data);
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
            using (var connection = new SQLiteConnection(connectionString))
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
            using (var connection = new SQLiteConnection(connectionString))
            {
                return await connection.ExecuteAsync(query, data);
            }
        }
    }
}