using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RogsoftwareServer.packet.workers
{
    public class CheatWorker
    {
        public class fromClientToServer
        {
            public bool UserAuth(Client _cl, string fullData)
            {
                try
                {
                    JObject jsonObject = JObject.Parse(fullData);

                    jsonObject = JObject.Parse(jsonObject.SelectToken("data").ToString());

                    var userHWID = jsonObject.SelectToken("hwid").ToString() ?? throw new ArgumentNullException("jsonObject.SelectToken(\"hwid\").ToString()");

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

                                PacketJsonSerializes.CheatPacketData.serverToClient.ServerLoginPacketData slpd = new PacketJsonSerializes.CheatPacketData.serverToClient.ServerLoginPacketData();

                                slpd.packet_id = (int)PacketEnums.CHEAT.ServerToClient.USER_AUTH;

                                slpd.data.isSuccess = true;
                                slpd.data.token = uToken;


                                string tjo = JsonConvert.SerializeObject(slpd);

                                _cl.sendData(tjo);

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
        }

        public class fromServerToClient
        {

        }
    }
}
