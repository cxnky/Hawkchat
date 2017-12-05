using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hawkchat.Server.utils
{
    public class DBUtils
    {

        public static SQLiteConnection EstablishConnection()
        {

            SQLiteConnection connection = new SQLiteConnection("Data Source=db/hawkchat.sqlite;Version=3");
            connection.Open();

            return connection;

        }

        public static async Task<int> ExecuteNonQuery(SQLiteConnection connection, string query)
        {

            SQLiteCommand command = new SQLiteCommand(query, connection);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected;

        }

        public static SQLiteDataReader ExecuteReader(SQLiteConnection connection, string query)
        {

            SQLiteCommand command = new SQLiteCommand(query, connection);

            return command.ExecuteReader();

        }

        public static void CloseConnection(SQLiteConnection connection)
        {

            connection.Close();

        }

    }
}
