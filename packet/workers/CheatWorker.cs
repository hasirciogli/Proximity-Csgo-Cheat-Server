using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Bcpg.OpenPgp;
using PacketJsonSerializes.CheatPacketData.serverToClient;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace RogsoftwareServer.packet.workers
{
    public class CheatWorker
    {
        public struct c_userConfig_t
        {
            public string name;
            public string cdate;
            public string udate;
            public string data;
            public string owner;
            public int owner_id;
            public int id;
            public bool isSubscribed;
        }
        public c_userConfig_t[] getUserConfigs(Client _cl, int uid = -1)
        {
            using (var baglan = new MySqlConnection(Globals.databaseConfig.connectorString))
            {
                using (var cmd = new MySqlCommand("SELECT * FROM configs", baglan))
                {
                    //cmd.Parameters.AddWithValue("@ownerID", uid >= 0 ? uid : this.userID);
                    cmd.Connection.Open();
                    MySqlDataReader mdr = cmd.ExecuteReader();

                    c_userConfig_t[] uConfigs = { };

                    while (mdr.Read())
                    {
                        // ReSharper disable once ReplaceWithSingleAssignment.False

                        if (Convert.ToInt32(mdr["owner_id"]) != _cl.CConfig.userID)
                            continue;

                        if (!string.IsNullOrEmpty(mdr["token"].ToString()))
                        {
                            c_userConfig_t temp_ucfg = new c_userConfig_t();
                            temp_ucfg.id = Convert.ToInt32(mdr["id"]);
                            temp_ucfg.owner_id = Convert.ToInt32(mdr["owner_id"]);
                            temp_ucfg.name = mdr["name"].ToString();
                            temp_ucfg.data = mdr["data"].ToString();
                            temp_ucfg.udate = mdr["updated_at"].ToString();
                            temp_ucfg.cdate = mdr["created_at"].ToString();
                            temp_ucfg.isSubscribed = Convert.ToBoolean(mdr["is_subscribed"]);
                            uConfigs.Append(temp_ucfg);
                        }
                    }

                    return uConfigs;
                }
            }

            return null;
        }
        public c_userConfig_t getUserConfig(int cfgID, Client _cl)
        {
            c_userConfig_t[] cfgs = this.getUserConfigs(_cl);
            c_userConfig_t rCfg = new c_userConfig_t();
            
            rCfg.id = -1;

            foreach (var item in cfgs)
            {
                if (item.id == cfgID)
                    return rCfg;
            }

            return rCfg;
        }

        public c_userConfig_t createNewConfig(int uid)
        {
            using (var baglan = new MySqlConnection(Globals.databaseConfig.connectorString))
            {
                using (var cmd = new MySqlCommand("INSERT INTO configs (owner_id, name, data, is_subscribed) VALUES ('"+uid+"', 'New Config', '{}', '1')", baglan))
                {
                    //cmd.Parameters.AddWithValue("@ownerID", uid >= 0 ? uid : this.userID);
                    cmd.Connection.Open();
                    MySqlDataReader mdr = cmd.ExecuteReader();

                    c_userConfig_t uConfig = new c_userConfig_t();
                    uConfig.data = "";
                    uConfig.cdate = DateTime.Now.ToString();
                    uConfig.id = (int)cmd.LastInsertedId;
                    uConfig.isSubscribed = false;
                    uConfig.owner_id = uid;
                    uConfig.name = "New Config";

                    return uConfig;
                }
            }
        }

        public class fromClientToServer
        {
            public bool UpdateUserFromToken(Client _cl, string token)
            {
                using (var baglan = new MySqlConnection(Globals.databaseConfig.connectorString))
                {
                    using (var cmd = new MySqlCommand("SELECT * FROM users WHERE token=@token", baglan))
                    {
                        cmd.Parameters.AddWithValue("@token", token);
                        cmd.Connection.Open();
                        MySqlDataReader mdr = cmd.ExecuteReader();

                        StringBuilder sb = new StringBuilder();

                        string uToken = "";

                        bool zc = false;

                        while (mdr.Read())
                        {
                            zc = true;
                            _cl.CConfig.userToken = mdr["token"].ToString();
                            _cl.CConfig.userID = Convert.ToInt32(mdr["id"]);
                            _cl.CConfig.userAuthed = true;
                            _cl.CConfig.username = mdr["username"].ToString();
                        }

                        if (zc)
                        {
                            _cl.CConfig.userAuthed = true;
                            return true;
                        }
                        else
                        {
                            _cl.CConfig.userAuthed = false;
                            return false;
                        }

                    }
                }
            }
            public bool UserAuth(Client _cl, byte[] fullData)
            {
                if (_cl.CConfig.userAuthed)
                {
                    UserAuth slpd = new UserAuth();

                    slpd.packet_id = (int)PacketEnums.CHEAT.ServerToClient.USER_AUTH_RESPONSE;

                    slpd.data.isSuccess = true;
                    slpd.data.token = _cl.CConfig.userToken;
                    slpd.data.username = _cl.CConfig.username;
                    slpd.data.subs_till = "infinite...";


                    string tjo = JsonConvert.SerializeObject(slpd);

                    _cl.sendData(tjo);
                    return true;
                }

                try
                {
                    string workerString = Encoding.UTF8.GetString(fullData);

                    JObject jsonObject = JObject.Parse(workerString);

                    jsonObject = JObject.Parse(jsonObject.SelectToken("data").ToString());

                    var userHWID = jsonObject.SelectToken("hwid").ToString();

                    var __steamName = jsonObject.SelectToken("steam_name").ToString();
                    var __steamId = jsonObject.SelectToken("steam_id").ToString();


                    using (var baglan = new MySqlConnection(Globals.databaseConfig.connectorString))
                    {
                        using (var cmd = new MySqlCommand("SELECT * FROM users WHERE hwid=@clienthwid", baglan))
                        {
                            cmd.Parameters.AddWithValue("@clienthwid", userHWID);
                            cmd.Connection.Open();
                            MySqlDataReader mdr = cmd.ExecuteReader();

                            string uToken = "";
                            string uUsername = "";
                            int uID = -1;

                            while (mdr.Read())
                            {
                                if (!string.IsNullOrEmpty(mdr["token"].ToString()))
                                {
                                    uToken = mdr["token"].ToString();
                                }

                                if (!string.IsNullOrEmpty(mdr["username"].ToString()))
                                {
                                    uUsername = mdr["username"].ToString();
                                }

                                uID = Convert.ToInt32(mdr["id"]);
                            }

                            if (uToken != "" && uID != -1)
                            {
                                // TODO: Token grabbed so you need to send okPacket :)

                                _cl.CConfig.userAuthed = true;
                                _cl.CConfig.userID = uID;
                                _cl.CConfig.userToken = uToken;
                                _cl.CConfig.username = uUsername;
                                _cl.CConfig.steamName = __steamName;
                                _cl.CConfig.steamID = __steamId;


                                PacketJsonSerializes.CheatPacketData.serverToClient.UserAuth slpd = new PacketJsonSerializes.CheatPacketData.serverToClient.UserAuth();

                                slpd.packet_id = (int)PacketEnums.CHEAT.ServerToClient.USER_AUTH_RESPONSE;

                                slpd.data.isSuccess = true;
                                slpd.data.token = uToken;
                                slpd.data.username = uUsername;
                                slpd.data.subs_till = "Beta Feature...";

                                string tjo = JsonConvert.SerializeObject(slpd);

                                _cl.sendData(tjo);

                                return true;
                            }
                            else
                            {
                                PacketJsonSerializes.CheatPacketData.serverToClient.UserAuth slpd = new PacketJsonSerializes.CheatPacketData.serverToClient.UserAuth();

                                slpd.packet_id = (int)PacketEnums.CHEAT.ServerToClient.USER_AUTH_RESPONSE;

                                slpd.data.isSuccess = false;
                                slpd.data.token = "Not work man we are sorry :(, but good have a day! hahaha";
                                slpd.data.username = "sorry";
                                slpd.data.subs_till = "sorry";

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
                    if (!_cl.CConfig.userAuthed || _cl.CConfig.username == "")
                    {
                        new CheatWorker.fromServerToClient().SendNeedAuth(_cl);
                        return true;
                    }

                    string workerString = Encoding.UTF8.GetString(fullData);

                    JObject jsonObject = JObject.Parse(workerString);

                    jsonObject = JObject.Parse(jsonObject.SelectToken("data").ToString());

                    string author = _cl.CConfig.username;
                    string content = (string)jsonObject.SelectToken("message_content").ToString();

                    PacketJsonSerializes.CheatPacketData.serverToClient.CHAT_MESSAGE_SENT cms = new PacketJsonSerializes.CheatPacketData.serverToClient.CHAT_MESSAGE_SENT();

                    cms.packet_id = (int)PacketEnums.CHEAT.ServerToClient.CHAT_MESSAGE_SENT;

                    cms.data.message_id = 1;
                    cms.data.message_author_color = author == "admin" ? new CHAT_MESSAGE_SENT.thizCData(255, 50, 50) : new CHAT_MESSAGE_SENT.thizCData(20, 100, 20);
                    cms.data.message_content_color = author == "admin" ? new CHAT_MESSAGE_SENT.thizCData(100, 100, 200) : new CHAT_MESSAGE_SENT.thizCData(200, 200, 200);
                    cms.data.message_author = author;
                    cms.data.message_content = content;
                    cms.data.message_date = DateTime.Now.ToString("F");




                    string tjo = JsonConvert.SerializeObject(cms);

                    Server.Server.connectedClients.ForEach((item) =>
                    {
                        if (item.soket != null)
                            if (item.soket.Connected)
                                item.sendData(tjo);
                    });


                    return true;
                }
                catch (Exception e)
                {

                    return false;
                }

                return false;
            }
            public bool ConfigCreate(Client _cl, byte[] fullData)
            {
                try
                {

                    c_userConfig_t cConfig = new CheatWorker().createNewConfig(_cl.CConfig.userID);
                    
                    PacketJsonSerializes.CheatPacketData.serverToClient.CONFIG_CREATE cc = new PacketJsonSerializes.CheatPacketData.serverToClient.CONFIG_CREATE();

                    cc.data.config_author = cConfig.owner_id;
                    cc.data.config_date = cConfig.cdate;
                    cc.data.config_id = cConfig.id;
                    cc.data.config_name = cConfig.name;

                    string tjo = JsonConvert.SerializeObject(cc);

                    if (_cl.soket != null)
                        if (_cl.soket.Connected)
                            _cl.sendData(tjo);

                    return true;
                }
                catch (Exception e)
                {

                    return false;
                }

                return false;
            }
            
            
            public bool ConfigRefresh(Client _cl, byte[] fullData)
            {
                try
                {

                    c_userConfig_t cConfig = new CheatWorker().createNewConfig(_cl.CConfig.userID);

                    CONFIG_REFRESH cR = new CONFIG_REFRESH();


                    foreach (var item in new CheatWorker().getUserConfigs(_cl, _cl.CConfig.userID))
                    {
                        CONFIG_REFRESH.thisZData crtd = new CONFIG_REFRESH.thisZData();
                        crtd.config_author = cConfig.owner_id;
                        crtd.config_date = cConfig.cdate;
                        crtd.config_id = cConfig.id;
                        crtd.config_name = cConfig.name;

                        cR.data.Append(crtd);
                    }

                    string tjo = JsonConvert.SerializeObject(cR);

                    MessageBox.Show(tjo);

                    return true;


                    if (_cl.soket != null)
                        if (_cl.soket.Connected)
                            _cl.sendData(tjo);

                    return true;
                }
                catch (Exception e)
                {

                    return false;
                }

                return false;
            }
        }

        public class fromServerToClient
        {
            public void SendNeedAuth(Client _cl)
            {
                if (_cl.soket == null || !_cl.soket.Connected)
                    return;

                PacketJsonSerializes.CheatPacketData.serverToClient.NEED_AUTH na = new PacketJsonSerializes.CheatPacketData.serverToClient.NEED_AUTH();

                na.packet_id = (int)PacketEnums.CHEAT.ServerToClient.NEED_USER_AUTH;


                string tjo = JsonConvert.SerializeObject(na);

                _cl.sendData(tjo);
            }
        }
    }







    public class LoaderWorker
    {
        public class fromClientToServer
        {
            
        }
    }
}
