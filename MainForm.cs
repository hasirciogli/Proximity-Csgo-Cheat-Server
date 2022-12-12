using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using RogsoftwareServer.Server;

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
            ConsoleLogger logger = new ConsoleLogger();
            logger.Log("qwe");
        }

        private void kryptonCheckedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void serverStartButton_Click(object sender, EventArgs e)
        {
            Server.Server.runServer();
        }

        private void commandSendButton_Click_1(object sender, EventArgs e)
        {
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
