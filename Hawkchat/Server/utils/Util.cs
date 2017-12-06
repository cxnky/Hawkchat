using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Console = Colorful.Console;
using System.Text;
using Hawkchat.Server.Models;
using System.Net.Sockets;

namespace Hawkchat.Server.Utils
{
    public class Util
    {

        public static Color CYAN = Color.Cyan;

        public static List<ClientModel> loggedinUsers = new List<ClientModel>();

        public static void CyanWriteLine(string text)
        {

            Console.WriteLine(text, CYAN);

        }

        public static long GenerateAccountID()
        {

            Random rand = new Random();

            return rand.Next(1000000, 9999999);

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
