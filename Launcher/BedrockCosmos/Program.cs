using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BedrockCosmos
{
    internal static class Program
    {
        private const string AppName = "BedrockCosmos_139af231-3f91-4712-87a0-86f7d0fcfef5";
        private static Mutex mutex = null;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Check if an instance of the Mutex is already open
            bool createdNew;
            mutex = new Mutex(true, AppName, out createdNew);

            if (!createdNew)
            {
                MessageBox.Show("Another instance of the Bedrock Cosmos is already running. Please close the current instance before opening a new one.",
                    "Application Already Open", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());

            mutex.ReleaseMutex();
        }
    }
}