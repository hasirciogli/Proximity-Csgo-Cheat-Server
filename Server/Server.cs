using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Net.WebSockets;

namespace RogsoftwareServer.Server
{
    public static class Server
    {
        public static List<Client> connectedClients = new List<Client>();
        public static Socket ServerSocket;

        public static void runServer()
        {
            ServerSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            ServerSocket.Bind(new IPEndPoint(IPAddress.Any, 6655));
            ServerSocket.Listen(-1);
            ServerSocket.BeginAccept(new AsyncCallback(newConnection), null);

            Globals.LoggerG.Log("Server Listen Started on -> " + IPAddress.Any + ":" + 6655);
        }

        public static void newConnection(IAsyncResult ar)
        {
            try
            {
                SocketError err;
                Socket ts = ServerSocket.EndAccept(ar);

                if (ts == null || !ts.Connected)
                    return;

                connectedClients.Add(new Client(ts));  // Create Client ınstande and push the connectedClients list

                ts = null;

                ServerSocket.BeginAccept(new AsyncCallback(newConnection), null);
            }
            catch (Exception e)
            {
                return;
            }
        }
    }
}
