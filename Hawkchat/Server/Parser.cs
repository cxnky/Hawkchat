using Hawkchat.Server.enums;
using Hawkchat.Server.models;
using Hawkchat.Server.Models;
using Hawkchat.Server.utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SimpleTCP;
using System;
using System.Data.SQLite;

namespace Hawkchat.Server
{
    public class Parser
    {

        public static bool messagesDisabled = false, messagesEnabled = true;
        public static string REASON = "";

        public static async void ParseCommand(string command, Message message, JObject json = null)
        {

            dynamic jsonResponse = new JObject();

            switch (command)
            {

                case "DISABLEMESSAGES":
                    REASON = json["reason"].ToString();

                    jsonResponse.command = "DISABLEMESSAGING";
                    jsonResponse.reason = REASON;

                    string authorisedBy = json["authorisedby"].ToString();

                    Util.CyanWriteLine($"{authorisedBy} has disabled message for all users.");

                    messagesEnabled = false;
                    messagesDisabled = true;

                    Server.server.Broadcast(jsonResponse.ToString());


                    break;

                case "ENABLEMESSAGES":
                    jsonResponse.command = "ENABLEMESSAGING";

                    messagesDisabled = false;
                    messagesEnabled = true;

                    Server.server.Broadcast(jsonResponse.ToString());

                    string authoriser = json["authorisedby"].ToString();
                    Util.CyanWriteLine($"{authoriser} has enabled messaging for all users.");

                    break;

                case "REQUESTCHAT":

                    string accountID = json["accountid"].ToString();

                    ClientModel client = Util.ReturnClientByID(accountID);

                    jsonResponse.ip = client.IP;
                    jsonResponse.port = client.Port;

                    message.Reply(jsonResponse.ToString());

                    break;
                    
                case "AUTH":

                    SQLiteConnection connection = DBUtils.EstablishConnection();

                    string username = json["username"].ToString();
                    string password = json["password"].ToString();

                    // check to see if the user is banned
                    string query = $"SELECT * FROM users WHERE username='{username}' AND password='{password}'";

                    SQLiteDataReader reader = DBUtils.ExecuteReader(connection, query);

                    if (reader.HasRows)
                    {

                        jsonResponse.authenticated = true;

                    } else
                    {

                        jsonResponse.authenticated = false;
                        jsonResponse.reason = "CREDENTIALS";

                        DBUtils.CloseConnection(connection);

                        message.Reply(jsonResponse.ToString(Formatting.None));

                        return;

                    }

                    string UserID = "", avatarURL = "";

                    while (reader.Read())
                    {

                        UserID = reader["AccountID"].ToString();
                        avatarURL = reader["avatarurl"].ToString();

                    }

                    reader.Close();

                    string banQuery = $"SELECT * FROM bans WHERE accountid='{UserID}'";

                    SQLiteDataReader banReader = DBUtils.ExecuteReader(connection, banQuery);

                    if (banReader.HasRows)
                    {

                        // user is banned
                        jsonResponse.authenticated = false;
                        jsonResponse.reason = "BANNED";

                    }
                    else
                    {

                        jsonResponse.accountID = UserID;
                        jsonResponse.avatarURL = avatarURL;
                        if (messagesDisabled)
                        {

                            jsonResponse.information = "MESSAGINGDISABLED";
                            jsonResponse.reason = REASON;

                        } else
                        {

                            jsonResponse.information = "MESSAGINGENABLED";

                        }

                    }

                    while (banReader.Read())
                    {

                        jsonResponse.BanID = banReader["id"].ToString();
                        jsonResponse.AccountID = banReader["accountid"].ToString();
                        jsonResponse.Reason = banReader["reason"].ToString();
                        jsonResponse.expires = banReader["expires"].ToString();
                        jsonResponse.Appealable = banReader["appealable"].ToString();

                    }

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

                case "REGISTER":

                    string usrName = json["username"].ToString();
                    string pwd = json["password"].ToString();
                    string IPAddress = json["IP"].ToString();
                    string avatarURLl = json["avatarURL"].ToString();

                    long AccountID = Util.GenerateAccountID();

                    SQLiteConnection sQLiteConnection = DBUtils.EstablishConnection();

                    int rowsAffected = await DBUtils.ExecuteNonQuery(sQLiteConnection, $"INSERT INTO users (AccountID, username, password, avatarurl, lastip) VALUES ('{AccountID}', '{usrName}', '{pwd}', '{avatarURLl}', '{IPAddress}')");


                    if (rowsAffected == 1)
                    {

                        jsonResponse.success = true;
                        jsonResponse.AccountID = AccountID;
                        jsonResponse.username = usrName;
                        jsonResponse.avatarURL = avatarURLl;

                        DBUtils.CloseConnection(sQLiteConnection);

                        message.Reply(jsonResponse.ToString(Formatting.None));

                    } else
                    {

                        jsonResponse.success = false;

                        DBUtils.CloseConnection(sQLiteConnection);

                        message.Reply(jsonResponse.ToString(Formatting.None));

                    }

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

                case "REPORT":

                    UserReport report = new UserReport();

                    report.ReporterAccountID = long.Parse(json["reporterid"].ToString());
                    report.ReportedAccountID = long.Parse(json["reportedid"].ToString());
                    report.ReportID = Util.GenerateAccountID();

                    string categoryText = json["category"].ToString();

                    switch (categoryText)
                    {

                        case "RACISM":
                            report.ReportCategory = ReportCategory.RACIST;
                            break;

                        case "OFFENSIVE":
                            report.ReportCategory = ReportCategory.OFFENSIVE;
                            break;

                        case "SPAM":
                            report.ReportCategory = ReportCategory.SPAM;
                            break;

                        case "COMPROMISEDACCOUNT":
                            report.ReportCategory = ReportCategory.ACCOUNTHACKED;
                            break;

                        case "DOX":
                            report.ReportCategory = ReportCategory.PERSONALINFORMATION;
                            break;

                        default:
                            report.ReportCategory = ReportCategory.OTHER;
                            break;

                    }

                    report.Timestamp = (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                    report.ChatLogURL = Util.CreateChatLog(json, report);

                    if (await Util.CreateReport(report))
                    {

                        // report made successfully
                        jsonResponse.success = true;

                    } else
                    {
                        // failed to create report
                        jsonResponse.success = false;

                    }

                    message.Reply(jsonResponse.ToString(Formatting.None));
                    
                    break;
                    

            }

        }

    }
}
