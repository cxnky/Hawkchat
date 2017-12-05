using Hawkchat.Server.utils;
using Hawkchat.Server.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SimpleTCP;
using System;
using System.Data.SQLite;

namespace Hawkchat.Server
{
    public class Parser
    {

        public static void ParseCommand(string command, Message message, JObject json = null)
        {

            dynamic jsonResponse = new JObject();

            switch (command)
            {

                case "AUTH":

                    SQLiteConnection connection = DBUtils.EstablishConnection();

                    string username = json["username"].ToString();
                    string password = json["password"].ToString();
                    
                    string query = $"SELECT * FROM users WHERE username='{username}' AND password='{password}'";

                    SQLiteDataReader reader = DBUtils.ExecuteReader(connection, query);

                    if (reader.HasRows)
                    {

                        jsonResponse.authenticated = true;

                    } else
                    {

                        jsonResponse.authenticated = false;

                    }

                    string UserID = "";

                    while (reader.Read())
                    {

                        UserID = reader["AccountID"].ToString();

                    }

                    reader.Close();

                    jsonResponse.accountID = UserID;

                    DBUtils.CloseConnection(connection);

                    message.Reply(jsonResponse.ToString(Formatting.None));
                    
                    int port = int.Parse(message.TcpClient.Client.RemoteEndPoint.ToString().Split(':')[1]);

                    Int64 userID;

                    if (UserID.Equals(""))
                    {

                        userID = 0;

                    } else
                    {

                        userID = int.Parse(UserID);

                    }

                    Util.AddUserOnline(message.TcpClient, json["username"].ToString(), json["IP"].ToString(), port, userID);

                    break;

                case "VERIFYUSERNAME":

                    string usernameToCheck = json["username"].ToString();

                    SQLiteConnection conn = DBUtils.EstablishConnection();

                    SQLiteDataReader dataReader = DBUtils.ExecuteReader(conn, $"SELECT * FROM users WHERE username='{usernameToCheck}'");

                    if (dataReader.HasRows)
                    {

                        jsonResponse.exists = true;

                    } else
                    {

                        jsonResponse.exists = false;

                    }

                    dataReader.Close();

                    DBUtils.CloseConnection(conn);

                    message.Reply(jsonResponse.ToString(Formatting.None));

                    break;

                case "DISCONNECT":
                    
                    Util.RemoveUserOnline(Util.ReturnClientModel(message.TcpClient));
                    
                    break;

            }

        }

    }
}
