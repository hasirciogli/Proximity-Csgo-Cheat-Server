using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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


        Thread tred = new Thread(new ThreadStart(sendSNC));
        tred.Start();
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
                    JObject test = JObject.Parse(pData);

                    object data = JsonConvert.DeserializeObject(pData);

                    Globals.LoggerG.Log(test.SelectToken("sfun2").ToString());
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

    public void sendSNC()
    {
        while (this.soket.Connected && !forceCloseThisClient)
        {
            SocketError sErr;
            byte[] newByte = Encoding.UTF8.GetBytes("Merhaba servere hoşgeldin :)))))");
            soket.Send(newByte, 0, newByte.Length, SocketFlags.None, out sErr);

            if(sErr != 0)
            {
                this.fuckOffThisClient();
            }

            Thread.Sleep(1000);

            if (forceCloseThisClient)
            {
                break;
            }
        }
    }
}
