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

public class Client
{
    public Socket soket;
    public byte[] clientDComet = new byte[8192];
    public readonly int clientID;
    public bool forceCloseThisClient = false;

    public Client(Socket skt)
    {
        if (Globals.loggerConfig.isDebugMode)
            Globals.LoggerG.Log("client instance created called");

        this.clientID = Server.connectedClients.Count + 1;
        Globals.addNewUser(this.clientID, "nall", "Bay kemal");

        this.runClient(skt);
    }

    public void runClient(Socket skt)
    {
        this.soket = skt;

        if (Globals.loggerConfig.isDebugMode)
            Globals.LoggerG.Log("runclient_called");

        SocketError RecSocketError;
        this.soket.BeginReceive(this.clientDComet, 0, this.clientDComet.Length, SocketFlags.None, out RecSocketError, newDataComed, null);
        if (RecSocketError != 0)
        {
            // TODO: Disconnect
            if (Globals.loggerConfig.isDebugMode)
                Globals.LoggerG.Log("first begin recieve socket error != 0 - called");
            this.disconnect();
            return;
        }
    }

    private void newDataComed(IAsyncResult ar)
    {
        try
        {
            if (Globals.loggerConfig.isDebugMode)
                Globals.LoggerG.Log("new data comed called");

            int len = this.soket.EndReceive(ar);
            if (len <= 0)
            {
                if (Globals.loggerConfig.isDebugMode)
                    Globals.LoggerG.Log("new data comed len is = <=0");
                // TODO: Disconnect
                this.disconnect();
                return;
            }
            else if (len > 0)
            {
                if (Globals.loggerConfig.isDebugMode)
                    Globals.LoggerG.Log("new data len is > 0");
                {
                    if (Globals.loggerConfig.isDebugMode && true)
                    {
                        CheatPacketHandler cph = new CheatPacketHandler(this, this.clientDComet);

                        if (Globals.loggerConfig.isDebugMode)
                            Globals.LoggerG.Log("new data handle begin");

                        if (!cph.Handle())
                            this.disconnect();

                        if (Globals.loggerConfig.isDebugMode)
                            Globals.LoggerG.Log("new data handle end");


                        //Globals.LoggerG.Log(pData);

                        //Globals.LoggerG.Log("Data received -> " + pData + " ;(END)"); Globals.LoggerG.Log("");
                    }
                    else
                    {
                        Globals.LoggerG.Log("BBK DATA = " + Encoding.UTF8.GetString(this.clientDComet).ToString());
                    }
                }


                this.clientDComet = new byte[8192];

                SocketError RecSocketError;
                this.soket.BeginReceive(this.clientDComet, 0, this.clientDComet.Length, SocketFlags.None, out RecSocketError, newDataComed, null);

                if (RecSocketError != 0)
                {
                    if (Globals.loggerConfig.isDebugMode)
                        Globals.LoggerG.Log("new data beginrecieve error != 0 - called");
                    // TODO: Disconnect
                    this.disconnect();

                    return;
                }
            }
            else
            {
                if (Globals.loggerConfig.isDebugMode)
                    Globals.LoggerG.Log("new data last else called");
                this.disconnect();
                // TODO: Disconnect
                return;
            }
        }
        catch (Exception e)
        {
            if (Globals.loggerConfig.isDebugMode)
                Globals.LoggerG.Log("newDataComed exception - called");

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