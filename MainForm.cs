using System;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using RogsoftwareServer.Libs;

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
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void CommandSendButton_Click(object sender, EventArgs e)
        {

        }

        private void serverStartButton_Click(object sender, EventArgs e)
        {
            Server.Server.runServer();
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
    }
}
