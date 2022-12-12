using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace RogsoftwareServer.Server
{
    public static class Server
    {
        public static List<Client> connectedClients = new List<Client>();
        public static Socket ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        public static void runServer()
        {
            ServerSocket.Bind(new IPEndPoint(IPAddress.Any, 6655));
            ServerSocket.Listen(0);
            ServerSocket.BeginAccept(new AsyncCallback(newConnection), null);

            if (Globals.loggerConfig.isDebugMode)
                Globals.LoggerG.Log("Server Listen Started on -> " + IPAddress.Any + ":" + 6655);
        }

        public static void newConnection(IAsyncResult ar)
        {
            Socket ts = ServerSocket.EndAccept(ar);
            
            if (Globals.loggerConfig.isDebugMode)
                Globals.LoggerG.Log("New connection from -> " + ts.RemoteEndPoint);

            connectedClients.Add(new Client(ts));  // Create Client ınstande and push the connectedClients list

            ServerSocket.BeginAccept(new AsyncCallback(newConnection), null);
        }
    }
}
