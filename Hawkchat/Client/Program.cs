using SharpRaven;
using SharpRaven.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hawkchat.Client
{
    static class Program
    {

        public static RavenClient ravenClient = new RavenClient("https://13862b3c5f0f40e79f66bec89b8ddb53:ab0db147c39d4880b99631e2d8cb29e1@sentry.io/256029");
       
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            
            Application.Run(new LoginWindow());

        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {

            MessageBox.Show("An unhandled error has been caught. This has been reported to our developers automatically. Hawkchat will now close.", "Uncaught Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            ravenClient.Capture(new SentryEvent(e.ExceptionObject as Exception));

            Environment.Exit(0);

        }
    }
}
