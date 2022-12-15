using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Asn1.Cms;

namespace RogsoftwareServer.packet.workers
{
    public class CheatWorker
    {
        public class fromClientToServer
        {
            public bool UserAuth(Client _cl, byte[] fullData)
            {
                try
                {
                    string workerString = Encoding.UTF8.GetString(fullData);

                    JObject jsonObject = JObject.Parse(workerString);

                    jsonObject = JObject.Parse(jsonObject.SelectToken("data").ToString());

                    var userHWID = jsonObject.SelectToken("hwid").ToString();

                    //Globals.LoggerG.Log(fullData);
                    //Globals.LoggerG.Log(userHWID);
                    using (var baglan = new MySqlConnection(Globals.databaseConfig.connectorString))
                    {
                        using (var cmd = new MySqlCommand("SELECT * FROM users WHERE hwid=@clienthwid", baglan))
                        {
                            cmd.Parameters.AddWithValue("@clienthwid", userHWID);
                            cmd.Connection.Open();
                            MySqlDataReader mdr = cmd.ExecuteReader();

                            StringBuilder sb = new StringBuilder();

                            string uToken = "";

                            while (mdr.Read())
                            {
                                

                                for (int i = 0; i < mdr.FieldCount; i++)
                                    if (mdr.GetValue(i) != DBNull.Value)
                                        sb.Append(Convert.ToString(mdr.GetValue(i)) + "\n");
                                

                                if (!string.IsNullOrEmpty(mdr["token"].ToString()))
                                {
                                    uToken = mdr["token"].ToString();
                                }

                            }

                            if (uToken != "")
                            {
                                //Globals.LoggerG.Log("yes token grabbed " + uToken);
                                // TODO: Token grabbed so you need to send okPacket :)


                                return true;
                            }
                            else
                            {
                                PacketJsonSerializes.CheatPacketData.serverToClient.ServerLoginPacketData slpd = new PacketJsonSerializes.CheatPacketData.serverToClient.ServerLoginPacketData();

                                slpd.packet_id = (int)PacketEnums.CHEAT.ServerToClient.USER_AUTH;

                                slpd.data.isSuccess = false;
                                slpd.data.token = "Not work man we are sorry :(, but good have a day! hahaha";


                                string tjo = JsonConvert.SerializeObject(slpd);

                                _cl.sendData(tjo);


                                // TODO: Token isn't grabbed so you need to send okButNoPacket :)
                                return false;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }

                return false;
            }

            public bool ChatMessageSent(Client _cl, byte[] fullData)
            {
                try
                {
                    string workerString = Encoding.UTF8.GetString(fullData);

                    JObject jsonObject = JObject.Parse(workerString);

                    jsonObject = JObject.Parse(jsonObject.SelectToken("data").ToString());

                    string author   = (string)jsonObject.SelectToken("message_author").ToString();
                    string content  = (string)jsonObject.SelectToken("message_content").ToString();

                    PacketJsonSerializes.CheatPacketData.serverToClient.CHAT_MESSAGE_SENT cms = new PacketJsonSerializes.CheatPacketData.serverToClient.CHAT_MESSAGE_SENT();

                    cms.packet_id = (int)PacketEnums.CHEAT.ServerToClient.CHAT_MESSAGE_SENT;

                    cms.data.message_id = 1;
                    cms.data.message_author_color = "#e02d42";
                    cms.data.message_content_color = "#d9a33f";
                    cms.data.message_author = author;
                    cms.data.message_content = content;
                    cms.data.message_date = DateTime.Now.ToString("F");




                    string tjo = JsonConvert.SerializeObject(cms);

                    Server.Server.connectedClients.ForEach((item) =>
                    {
                        if(item.soket != null) 
                            if(item.soket.Connected)
                                item.sendData(tjo);
                    });

                    Globals.LoggerG.Log("sented dataa -> " + tjo + "ok");


                    return false;
                }
                catch (Exception e)
                {
                    if (Globals.loggerConfig.isDebugMode)
                        Globals.LoggerG.Log("new data camed data handler of inside try excetion - called");
                    return false;
                }

                return false;
            }
        }

        public class fromServerToClient
        {

        }
    }
}
