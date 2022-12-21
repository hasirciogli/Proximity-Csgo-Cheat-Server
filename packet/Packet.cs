using RogsoftwareServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RogsoftwareServer.packet
{

    class CPacketSender
    {
        public void sendDataTo(Client _client, string data)
        {
            _client.sendData(data);
        }

        public void sendDataAll(string data)
        {
            foreach (var item in Server.Server.connectedClients)
            {
                if (item.soket.Connected)
                    item.sendData(data);
            }
        }

        public void sendDataToOthers(Client exclude, string data)
        {
            foreach (var item in Server.Server.connectedClients)
            {
                if (item.soket.Connected && item != exclude)
                    item.sendData(data);
            }
        }
    }
}
