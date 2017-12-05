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
        
        static void Main(string[] args)
        {

            new Server().Start();

        }

        private void Start()
        {

            Utils.CyanWriteLine("[INIT] Starting server thread...");

            serverWorker = new BackgroundWorker();

            serverWorker.DoWork += ServerWorker_DoWork;

            serverWorker.WorkerSupportsCancellation = true;
            serverWorker.WorkerReportsProgress = false;

            serverWorker.RunWorkerAsync();

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

            MessageBox.Show(e.MessageString);

        }

        private void Server_ClientDisconnected(object sender, System.Net.Sockets.TcpClient e)
        {
            throw new NotImplementedException();
        }

        private void Server_ClientConnected(object sender, System.Net.Sockets.TcpClient e)
        {
            throw new NotImplementedException();
        }
    }
}
