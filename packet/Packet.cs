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
    class CPacket
    {
        public byte[] data = new byte[8192];
        public CPacket() 
        {
            if (Globals.loggerConfig.isDebugMode)
                Globals.LoggerG.Log("Packet instance created");
        }
    }

    class CPacketHandler
    {
        public CPacket _packet;

        public CPacketHandler(byte[] data) 
        {
            if (Globals.loggerConfig.isDebugMode)
                Globals.LoggerG.Log("Packet Handler instance created");
        }
    }


    class CPacketSender
    {
        public void sendDataTo(Client _client, CPacket paket)
        {
            _client.soket.Send(paket.data, 0, paket.data.Length, SocketFlags.None);
        }

        public void sendDataAll(CPacket paket)
        {
            foreach (var item in Server.Server.connectedClients)
            {
                if (item.soket.Connected)
                    item.soket.Send(paket.data, 0, paket.data.Length, SocketFlags.None);
            }
        }

        public void sendDataToOthers(CPacket paket, Client exclude)
        {
            foreach (var item in Server.Server.connectedClients)
            {
                if (item.soket.Connected && item != exclude)
                    item.soket.Send(paket.data, 0, paket.data.Length, SocketFlags.None);
            }
        }
    }
}
