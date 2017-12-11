using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SimpleTCP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hawkchat.Server.stats;
using Hawkchat.Server.utils;

namespace Hawkchat.Server
{
    class Server
    {

        private static BackgroundWorker serverWorker;

        private static string[] INTRO_ASCII_ART = new[] {" _    _                _     _____ _           _   ",
@"| |  | |              | |   / ____| |         | |           ",
@"| |__| | __ ___      _| | _| |    | |__   __ _| |_          ",
@"|  __  |/ _` \ \ /\ / / |/ / |    | '_ \ / _` | __|         ",
@"| |  | | (_| |\ V  V /|   <| |____| | | | (_| | |_          ",
@"|_|  |_|\__,_| \_/\_/ |_|\_\\_____|_| |_|\__,_|\__|         "


                                                    };


        static void Main(string[] args) => new Server().Start().GetAwaiter().GetResult();

        private async Task Start()
        {

            foreach(string line in INTRO_ASCII_ART)
            {

                Util.CyanWriteLine(line);

            }

            Console.WriteLine();

            Util.CyanWriteLine("[INIT] Starting server thread...");

            serverWorker = new BackgroundWorker();

            serverWorker.DoWork += ServerWorker_DoWork;

            serverWorker.WorkerSupportsCancellation = true;
            serverWorker.WorkerReportsProgress = false;

            serverWorker.RunWorkerAsync();

            await Task.Delay(-1);

        }

        public static SimpleTcpServer server;

        //todo: add pool of servers?

        private void ServerWorker_DoWork(object sender, DoWorkEventArgs e)
        {

            server = new SimpleTcpServer().Start(3289);

            Util.CyanWriteLine("[INIT] Server listening on port 3289.");

            server.ClientConnected += Server_ClientConnected;
            server.ClientDisconnected += Server_ClientDisconnected;

            server.DataReceived += Server_DataReceived;

        }

        private void Server_DataReceived(object sender, SimpleTCP.Message e)
        {

            string properJson = Util.RemoveEndSpecialCharacter(e.MessageString);

            StatsTracker.IncreaseBytesReceived(e.Data.Length);

            Util.CyanWriteLine($"Received message from {e.TcpClient.Client.RemoteEndPoint}: {properJson}");

            JObject json = JObject.Parse(properJson);

            string command = json["command"].ToString();

            Parser.ParseCommand(command, e, json);

        }

        private void Server_ClientDisconnected(object sender, System.Net.Sockets.TcpClient e)
        {
            
            Util.RemoveUserOnline(Util.ReturnClientModel(e));
            
        }

        private void Server_ClientConnected(object sender, System.Net.Sockets.TcpClient e)
        {
            
        }
    }
}
