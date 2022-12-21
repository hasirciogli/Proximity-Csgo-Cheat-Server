using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RogsoftwareServer.packet.handlers;
using RogsoftwareServer.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RogsoftwareServer.packet.workers;
using PacketJsonSerializes.CheatPacketData.serverToClient;

public struct ClientConfig
{
    public int userID;
    public string username;
    public string userToken;
    public bool userAuthed;
}

public class Client
{
    public Socket soket;
    public byte[] clientDComet = new byte[8192];
    public readonly int clientID;
    public bool forceCloseThisClient = false;

    public ClientConfig CConfig = new ClientConfig();

    public Client(Socket skt)
    {

        this.clientID = Server.connectedClients.Count + 1;

        this.runClient(skt);

        new CheatWorker.fromServerToClient().SendNeedAuth(this);
    }

    public void runClient(Socket skt)
    {
        this.soket = skt;

        SocketError RecSocketError;
        this.soket.BeginReceive(this.clientDComet, 0, this.clientDComet.Length, SocketFlags.None, out RecSocketError, newDataComed, null);
        if (RecSocketError != 0)
        {
            // TODO: Disconnect
            
            this.disconnect();
            return;
        }
    }

    private void newDataComed(IAsyncResult ar)
    {
        try
        {

            int len = this.soket.EndReceive(ar);
            if (len <= 0)
            {
                // TODO: Disconnect
                this.disconnect();
                return;
            }
            else if (len > 0)
            {
                CheatPacketHandler cph = new CheatPacketHandler(this, this.clientDComet);


                string clientPacketWhoFind = Encoding.UTF8.GetString(this.clientDComet);

                JObject jObj = new JObject();

                try
                {
                    jObj = JObject.Parse(clientPacketWhoFind);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

                string who_i_am = jObj.SelectToken("who_i_am").ToString();

                if (who_i_am == "cheat")
                {
                    if (!cph.HandleCheat())
                        this.disconnect();
                }
                else if (who_i_am == "loader")
                {
                    if (!cph.HandleLoader())
                        this.disconnect();
                }


                this.clientDComet = new byte[8192];

                SocketError RecSocketError;
                this.soket.BeginReceive(this.clientDComet, 0, this.clientDComet.Length, SocketFlags.None, out RecSocketError, newDataComed, null);

                if (RecSocketError != 0)
                {
                    // TODO: Disconnect
                    this.disconnect();

                    return;
                }
            }
            else
            {
                this.disconnect();
                // TODO: Disconnect
                return;
            }
        }
        catch (Exception e)
        {

            this.disconnect();
            // TODO: Disconnect
            return;
        }
    }

    public void disconnect()
    {
        try
        {
            this.soket.Shutdown(SocketShutdown.Both);
        }
        catch (Exception e)
        {

        }

        try
        {
            this.soket.Dispose();
        }
        catch (Exception e)
        {

        }

        try
        {
            this.soket.Close();
        }
        catch (Exception e)
        {

        }

        try
        {
            this.soket = null;
        }
        catch (Exception e)
        {

        }

        try
        {
            //RogsoftwareServer.Server.Server.connectedClients.Remove(this);
            if (this.CConfig.userAuthed)
                Globals.removeUser(this.clientID);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


    public void sendData(byte[] data)
    {
        this.soket.Send(data, 0, data.Length, SocketFlags.None);
    }

    public void sendData(string _data)
    {
        byte[] data = new byte[8192];
        data = Encoding.UTF8.GetBytes(_data);

        this.soket.Send(data, 0, data.Length, SocketFlags.None);
    }
}



/*
 * 
 * Server Listen Started on -> 0.0.0.0:6655
New connection from -> 127.0.0.1:54863
Client instance created
{"data":{"message_author":"admin","message_content":"Merhaba"},"packet_id":6}c fuckofedclient line 68
New connection from -> 127.0.0.1:54873
{"data":{"message_author":"admin","message_content":"selam"},"packet_id":6}6}c fuckofedtry exception Bırakılmış bir nesneye erişilemiyor.
Nesne adı: 'System.Net.Sockets.Socket'.
{"data":{"message_author":"admin","message_content":"213"},"packet_id":6}c fuckofedSocket error !=0
c fuckofedclient line 79
*/