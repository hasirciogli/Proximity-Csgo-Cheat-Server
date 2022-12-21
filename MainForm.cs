using System;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using RogsoftwareServer.packet.workers;

namespace RogsoftwareServer
{
    public partial class MainForm : Form 
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Globals.loggerConfig.isDebugMode = true;

            CheckForIllegalCrossThreadCalls = false;
            serverStopButton.Enabled = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)   
        {
            Environment.Exit(0);
        }

        private void serverStartButton_Click(object sender, EventArgs e)
        {
            Server.Server.runServer();
            serverStartButton.Enabled = false;
            serverStopButton.Enabled = true;
        }

        private void commandSendButton_Click_1(object sender, EventArgs e)
        {

            //Globals.LoggerG.Log("HOT RELOAD WORK :)");
            //return;
            
            Server.Server.connectedClients.ForEach((item) =>
            {
                byte[] b = new byte[8192];
                b = Encoding.UTF8.GetBytes(commandBox.Text);

                item.soket.Send(b, 0, b.Length,SocketFlags.None);
            });
        }

        private void LogBox_TextChanged(object sender, EventArgs e)
        {
            // set the current caret position to the end
            LogBox.SelectionStart = LogBox.Text.Length;
            // LogBox it automatically
            LogBox.ScrollToCaret();
        }

        private void serverStopButton_Click(object sender, EventArgs e)
        {
            Server.Server.ServerSocket.Dispose();

            foreach (Client item in Server.Server.connectedClients.ToArray())
            {
                try
                {
                    item.disconnect();
                }
                catch (Exception exception)
                {
                    
                }

                Globals.removeUser(item.clientID);
                Server.Server.connectedClients.Remove(item);
            }


            LogBox.Text = "";
            serverStartButton.Enabled = true;
            serverStopButton.Enabled = false;
        }

        private void connectedClientsCheckerTimer_Tick(object sender, EventArgs e)
        {
            return;
            foreach (Client item in Server.Server.connectedClients.ToArray())
            {
                if (!item.forceCloseThisClient)
                    continue;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LogBox.Text = "";
        }
    }
}
