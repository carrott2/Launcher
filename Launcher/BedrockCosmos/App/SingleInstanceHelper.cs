using BedrockCosmos.App;
using System;
using System.IO;
using System.IO.Pipes;
using System.Threading;
using System.Windows.Forms;

namespace BedrockCosmos
{
    internal static class SingleInstanceHelper
    {
        private const string PipeName = "BedrockCosmos_Pipe_139af231-3f91-4712-87a0-86f7d0fcfef5";

        // Called by 1st instance of the launcher
        public static void StartListening(MainForm mainForm)
        {
            var thread = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        using (var server = new NamedPipeServerStream(PipeName, PipeDirection.In))
                        {
                            server.WaitForConnection();

                            using (var reader = new StreamReader(server))
                            {
                                var line = reader.ReadLine();
                                if (!string.IsNullOrEmpty(line))
                                {
                                    var args = line.Split('\n');

                                    // Marshal back to the UI thread
                                    mainForm.Invoke((Action)(() =>
                                    {
                                        mainForm.HandleIncomingArgs(args);
                                        mainForm.BringToFront();
                                        mainForm.WindowState = FormWindowState.Normal;
                                        mainForm.Activate();
                                    }));
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Pipe broke (app is shutting down), exits the loop
                        CosmosConsole.WriteLine("Pipe listener error: " + ex.Message);
                        break;
                    }
                }
            });

            thread.IsBackground = true; // Kills thread automatically when the app exits
            thread.Start();
        }

        // Called by additional instances of the launcher
        public static void SendArgsToRunningInstance(string[] args)
        {
            try
            {
                using (var client = new NamedPipeClientStream(".", PipeName, PipeDirection.Out))
                {
                    client.Connect(3000); // 3 second timeout

                    using (var writer = new StreamWriter(client))
                    {
                        writer.WriteLine(string.Join("\n", args));
                        writer.Flush();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not communicate with the running instance.\n\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}