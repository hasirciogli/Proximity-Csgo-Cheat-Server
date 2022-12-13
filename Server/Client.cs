using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RogsoftwareServer.packet.handlers;
using RogsoftwareServer.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class Client
{
    public ConsoleLogger scLogger = new ConsoleLogger();

    public Socket soket;
    public byte[] data = new byte[8192];
    public readonly int clientID;
    public bool forceCloseThisClient = false;

    public Client(Socket skt)
    {
        this.clientID = Server.connectedClients.Count + 1;
        Globals.addNewUser(this.clientID, "nall", "Bay kemal");

        this.runClient(skt);
    }

    public void runClient(Socket skt)
    {
        this.soket = skt;
        if (Globals.loggerConfig.isDebugMode)
            Globals.LoggerG.Log("Client instance created");

        soket.BeginReceive(data, 0, data.Length,SocketFlags.None, new AsyncCallback(newDataComed), null);
    }

    private void newDataComed(IAsyncResult ar)
    {
        SocketError socketError;
        int len = soket.EndReceive(ar, out socketError);

        if (len > 0 && socketError == 0)
        {
            {
                if (Globals.loggerConfig.isDebugMode)
                {
                    string pData = Encoding.UTF8.GetString(this.data);
                    CheatPacketHandler cph = new CheatPacketHandler(this, pData);

                    if (!cph.Handle())
                        this.fuckOffThisClient();

                    //Globals.LoggerG.Log("Data received -> " + pData + " ;(END)"); Globals.LoggerG.Log("");
                }
            }
            soket.BeginReceive(data, 0, data.Length, SocketFlags.None, new AsyncCallback(newDataComed), null);
        }
        else
        {
            this.fuckOffThisClient();
            // TODO: Disconnect
        }
    }

    void fuckOffThisClient()
    {
        // TODO: Disconnect plepwqepwqlepqweplasdqw;
        this.soket.Close();

        Globals.removeUser(this.clientID);

        Server.connectedClients.Remove(this);
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
