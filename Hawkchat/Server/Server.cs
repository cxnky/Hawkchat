using Server.utils;
using SimpleTCP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private static void ServerWorker_DoWork(object sender, DoWorkEventArgs e)
        {



        }
    }
}
