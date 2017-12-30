using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Console = Colorful.Console;
using System.Text;
using Hawkchat.Server.Models;
using System.Net.Sockets;
using Hawkchat.Server.models;
using System.Data.SQLite;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;
using System.Net;

namespace Hawkchat.Server.utils
{
    public class Util
    {

        public static Color CYAN = Color.Cyan;

        public static List<ClientModel> loggedinUsers = new List<ClientModel>();
        public static List<EstablishedChat> establishedChats = new List<EstablishedChat>();

        public static void CyanWriteLine(string text)
        {

            Console.WriteLine(text, CYAN);

        }

        public static ClientModel ReturnClientByID(string ID)
        {

            Int32 id = Int32.Parse(ID);

            return loggedinUsers.FirstOrDefault(u => u.UserID == id);

        }

        public static string CreateChatLog(JObject json, UserReport report)
        {

            var messages = new List<Tuple<string, long, string>>();

            var jsonArray = json["messages"];

            JArray jArray = JArray.Parse(jsonArray.ToString());

            string sender = "", message = "";
            long timestamp = 0;

            foreach (var item in jArray.Children())
            {

                var itemProperties = item.Children<JProperty>();

                sender = itemProperties.FirstOrDefault(x => x.Name == "Sender").Value.ToString();
                timestamp = long.Parse(itemProperties.FirstOrDefault(x => x.Name == "Timestamp").Value.ToString());
                message = itemProperties.FirstOrDefault(x => x.Name == "Message").Value.ToString();

                messages.Add(new Tuple<string, long, string>(sender, timestamp, message));

            }

            // Create a background thread to upload the chat logs
            BackgroundWorker uploadChatLogs = new BackgroundWorker();

            uploadChatLogs.WorkerSupportsCancellation = false;
            uploadChatLogs.WorkerReportsProgress = false;
            uploadChatLogs.DoWork += UploadChatLogs_DoWork;

            uploadChatLogs.RunWorkerAsync(new object[] { messages, report });

            return "test";

        }

        private static void UploadChatLogs_DoWork(object sender, DoWorkEventArgs e)
        {

            object[] parameters = e.Argument as object[];

            List<Tuple<string, long, string>> messages = parameters[0] as List<Tuple<string, long, string>>;
            UserReport report = parameters[1] as UserReport;

            if (!Directory.Exists("chatlogs"))
                Directory.CreateDirectory("chatlogs");

           string date = DateTime.UtcNow.ToString("dd.MM.yyy-HH.mm.ss");
       
            FileStream fCreate = new FileStream($"chatlogs/{date}.report", FileMode.Create);
            StreamWriter writer = new StreamWriter(fCreate);

            messages = messages.OrderByDescending(t => t.Item2).ToList();

            // format:
            // value 1 is sender
            // value 2 is timestamp
            // value 3 is message
            
            writer.WriteLine($"Report ID: {report.ReportID}");
            writer.WriteLine($"Reporter ID: {report.ReporterAccountID}");
            writer.WriteLine($"Reported ID: {report.ReportedAccountID}");
            writer.WriteLine($"Category: {report.ReportCategory}");
            writer.WriteLine($"Timestamp: {report.Timestamp}");
            writer.WriteLine("Chat log: ");

            foreach(Tuple<string, long, string> message in messages)
            {

                writer.WriteLine($"[{message.Item2}] <{message.Item1}>: {message.Item3}");

            }

            writer.Close();
            fCreate.Close();
            writer.Dispose();
            fCreate.Dispose();

            // Now upload the file to the website and notify everyone on Discord.
            using (WebClient client = new WebClient())
            {

                byte[] response = client.UploadFile("http://blackhawksoftware.net/software/hawkchat/reports/submit_user_report.php", "POST", $"chatlogs/{date}.report");

                string responseString = Encoding.UTF8.GetString(response);

            }

        }

        public static async Task<bool> CreateReport(UserReport report)
        {

            SQLiteConnection connection = DBUtils.EstablishConnection();

            string timeStamp = report.Timestamp.ToString();

            string query = $"INSERT INTO reports (id, reporter, reported, category, logurl, timestamp) VALUES ('{report.ReportID}', '{report.ReporterAccountID}', '{report.ReportedAccountID}', '{report.ReportCategory.ToString()}', '{report.ChatLogURL}', '{timeStamp}')";

            int rowsAffected = await DBUtils.ExecuteNonQuery(connection, query);

            DBUtils.CloseConnection(connection);

            return rowsAffected == 1;

        }

        public static long GenerateAccountID()
        {

            Random rand = new Random();

            return rand.Next(10000000, 99999999);

        }

        public static string RemoveEndSpecialCharacter(string data)
        {

            byte[] stringAsBytes = Encoding.ASCII.GetBytes(data);

            byte[] tmpArray = new byte[stringAsBytes.Length - 1];

            Array.Copy(stringAsBytes, tmpArray, stringAsBytes.Length - 1);

            return Encoding.Default.GetString(tmpArray);

        }

        public static void AddUserOnline(TcpClient tcpClient, string username, string ip, Int32 port, long userID = 0)
        {

            ClientModel client = new ClientModel();

            if (userID != 0)
            {

                client.UserID = userID;

            }

            client.IP = ip;
            client.Username = username;
            client.Port = port;
            client.TCPClient = tcpClient;

            loggedinUsers.Add(client);

        }

        public static ClientModel ReturnClientModel(TcpClient client)
        {

            return loggedinUsers.FirstOrDefault(u => u.TCPClient == client);

        }

        public static void RemoveUserOnline(ClientModel client)
        {

            loggedinUsers.Remove(client);

        }

    }
}
