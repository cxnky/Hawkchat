using Server.utils;
using SimpleTCP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hawkchat.Server
{
    class Server
    {

        private static BackgroundWorker serverWorker;

        static void Main(string[] args) => new Server().Start().GetAwaiter().GetResult();

        private async Task Start()
        {

            Utils.CyanWriteLine("[INIT] Starting server thread...");

            serverWorker = new BackgroundWorker();

            serverWorker.DoWork += ServerWorker_DoWork;

            serverWorker.WorkerSupportsCancellation = true;
            serverWorker.WorkerReportsProgress = false;

            serverWorker.RunWorkerAsync();

            await Task.Delay(-1);

        }

        SimpleTcpServer server;

        //todo: add pool of servers?

        private void ServerWorker_DoWork(object sender, DoWorkEventArgs e)
        {

            server = new SimpleTcpServer().Start(3289);

            Utils.CyanWriteLine("[INIT] Server listening on port 3289.");

            server.ClientConnected += Server_ClientConnected;
            server.ClientDisconnected += Server_ClientDisconnected;

            server.DataReceived += Server_DataReceived;

        }

        private void Server_DataReceived(object sender, SimpleTCP.Message e)
        {

            Console.WriteLine($"Received message from {e.TcpClient.Client.RemoteEndPoint}: {e.MessageString}");

        }

        private void Server_ClientDisconnected(object sender, System.Net.Sockets.TcpClient e)
        {

            Console.WriteLine("Client disconnected");

        }

        private void Server_ClientConnected(object sender, System.Net.Sockets.TcpClient e)
        {

            Console.WriteLine($"Client connected.");

        }
    }
}
