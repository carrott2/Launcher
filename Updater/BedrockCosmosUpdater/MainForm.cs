using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace BedrockCosmosUpdater
{
    public partial class MainForm : Form
    {
        private readonly AsyncFileDownload asyncDownload;

        string programPath = AppDomain.CurrentDomain.BaseDirectory;
        string downloadPath = AppDomain.CurrentDomain.BaseDirectory + @"Misc\";
        string updateDelayPath = AppDomain.CurrentDomain.BaseDirectory + @"Misc\UpdateDelayTest.txt";
        string updaterFilePath = AppDomain.CurrentDomain.BaseDirectory + AppDomain.CurrentDomain.FriendlyName;
        bool successfulUpdate = false;

        public MainForm()
        {
            InitializeComponent();
            asyncDownload = new AsyncFileDownload();

            if (!Directory.Exists(downloadPath))
                Directory.CreateDirectory(downloadPath);

            Startup();
        }

        // Start of window movement
        bool drag = false;
        Point start_point = new Point(0, 0);

        private void DragPanel_MouseDown(object sender, MouseEventArgs e)
        {
            drag = true;
            start_point = new Point(e.X, e.Y);
        }

        private void DragPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag)
            {
                Point p = PointToScreen(e.Location);
                this.Location = new Point(p.X - start_point.X, p.Y - start_point.Y);
            }
        }

        private void DragPanel_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;
        }
        // End of window movement

        private void Startup()
        {
            if (!File.Exists(updateDelayPath))
                File.WriteAllText(updateDelayPath, "0");

            string appTest = File.ReadAllText(updateDelayPath); // Helps fix app still opened error.

            if (appTest == "0")
            {
                File.WriteAllText(updateDelayPath, "1");
                FiveSecOpenDelay.Start();
            }
            else
            {
                try
                {
                    File.Delete(updateDelayPath);
                }
                catch
                {

                }

                StartUpdate();
            }
        }

        private void FiveSecOpenDelay_Tick(object sender, EventArgs e)
        {
            StatusLabel.Text = "Opening...";

            if (File.Exists(updaterFilePath))
            {
                Process.Start(updaterFilePath);
                Close();
            }
            else
            {
                try
                {
                    File.Delete(updateDelayPath);
                }
                catch
                {

                }

                StatusLabel.Text = "An error has occurred. Please try opening Bedrock Cosmos and restarting the update.";
                CloseButton.Visible = true;
            }
        }

        private async void StartUpdate()
        {
            StatusLabel.Text = "Downloading update file...";

            try
            {
                await asyncDownload.DownloadFileAsync("https://raw.githubusercontent.com/Bedrock-Cosmos/Launcher/main/LauncherFiles/BedrockCosmos.exe", downloadPath + @"BedrockCosmos.exe");
                MoveNewVersion();
            }
            catch
            {
                StatusLabel.Text = "An error has occurred. Please connect to the Internet and restart the update.";
                CloseButton.Visible = true;
            }
        }

        private void MoveNewVersion()
        {
            StatusLabel.Text = "Applying update...";

            if (File.Exists(programPath + @"BedrockCosmos.exe"))
                File.Delete(programPath + @"BedrockCosmos.exe");

            File.Move(downloadPath + @"BedrockCosmos.exe", programPath + @"BedrockCosmos.exe");

            FinishInstallation();
        }

        private void FinishInstallation()
        {
            successfulUpdate = true;
            StatusLabel.Text = "Successfully updated Bedrock Cosmos!";
            CloseButton.Visible = true;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            if (successfulUpdate)
            {
                try
                {
                    Process.Start(programPath + @"BedrockCosmos.exe");
                }
                catch
                {

                }
            }

            Close();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            asyncDownload.Dispose();
        }
    }
}
