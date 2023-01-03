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
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.BouncyCastle.Bcpg.Sig;
using RogsoftwareServer.packet.workers;
using PacketJsonSerializes.CheatPacketData.serverToClient;
using RogsoftwareServer.Libs.AllEnumerations;
using System.Xml.Linq;
using RogsoftwareServer.Libs;
using Timer = System.Threading.Timer;

public class Client
{
    public Socket soket;
    public byte[] clientDComet = new byte[8192];
    public readonly int clientID;
    public bool forceCloseThisClient = false;
    public Timer threadTimer;
    public ClientConfig CConfig = new ClientConfig();

    public bool NeedAuthSented = false;

    public List<string> sendBuffers = new List<string>();
    public Client(Socket skt)
    {
        this.CConfig.PoxiAuthStatus = false;
        this.clientID = Server.connectedClients.Count; 

        this.runClient(skt);
        threadTimer = new Timer(sokSendTimer, null, 0, 200);
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
                string clientPacketWhoFind2 = Encoding.UTF8.GetString(this.clientDComet);

                Globals.LoggerG.Log("recv -> "+ clientPacketWhoFind2);
                
                if (!this.CConfig.PoxiAuthStatus)
                {
                    ProxiAuth pa = new ProxiAuth();


                    try
                    {
                        if (!pa.AuthProxiClient(clientPacketWhoFind2, this))
                            disconnect();
                        else
                        {
                            this.CConfig.PoxiAuthStatus = true;

                            if (!NeedAuthSented)
                            {
                                new CheatWorker.fromServerToClient().SendNeedAuth(this);
                                NeedAuthSented = true;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        this.disconnect();
                        return;
                    }

                }
                else
                {
                    ////Decrypt encoded data from client
                    //this.clientDComet = Encoding.UTF8.GetBytes(xorCrypter.openSSL.cfun().crypt(Encoding.UTF8.GetString(this.clientDComet)));

                    ////Remove uncatagorized characters..
                    //this.clientDComet = Encoding.UTF8.GetBytes(new StringVerifier().verifyText(Encoding.UTF8.GetString(this.clientDComet), len));

                    //Create Packet Handler class and wait other checks
                    PacketHandler cph = new PacketHandler(this, this.clientDComet);

                    // Get decrypted and catagorized text data from socket byte...
                    string clientPacketWhoFind = Encoding.UTF8.GetString(this.clientDComet);
                    Console.WriteLine(clientPacketWhoFind);

                    JObject jObj = new JObject();

                    try
                    {
                        jObj = JObject.Parse(clientPacketWhoFind);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        this.disconnect();
                        return;
                    }


                    string who_i_am = jObj.SelectToken("who_i_am").ToString();

                    if (who_i_am == "cheat")
                    {
                        this.CConfig.whoIAM = isWhat.IS_CHEAT;
                        if (!cph.HandleCheat())
                            this.disconnect();
                    }
                    else if (who_i_am == "loader")
                    {
                        this.CConfig.whoIAM = isWhat.IS_LOADER;
                        if (!cph.HandleLoader())
                            this.disconnect();
                    }
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
            Globals.removeUser(this.clientID);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return;
        }
    }

    public void sendData(byte[] data)
    {
        this.soket.Send(data, 0, data.Length, SocketFlags.None);
    }

    public void sokSendTimer(Object o)
    {
        if (sendBuffers.Count > 0)
        {
            Globals.LoggerG.Log("Sent -> " + sendBuffers[0]);
            this.sendData(sendBuffers[0]);
            this.sendBuffers.RemoveAt(0);
        }
    }

    public void sendData(string _data)
    {
        //_data = xorCrypter.openSSL.cfun().crypt(_data);
        //Globals.LoggerG.Log("SDATA = " + _data);

        byte[] data = Encoding.UTF8.GetBytes(_data);

        try
        {
            this.soket.Send(data, 0, data.Length, SocketFlags.None);
        }
        catch (Exception e)
        {
            this.disconnect();
            return;
        }
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