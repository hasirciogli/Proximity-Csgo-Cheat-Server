using System;
using Newtonsoft.Json;
using PacketJsonSerializes.CheatPacketData.serverToClient;
using RogsoftwareServer.Libs.AllEnumerations;
using RogsoftwareServer.Server;

public class PromtCommands
{
    static class CommandTypes
    {
        public const string chat = "chat";
        public const string user = "user";
    }



    public bool run(string command)
    {



        Server.connectedClients.ForEach((item) =>
        {
            if (item.soket != null && item.soket.Connected && item.CConfig.userAuthed && item.CConfig.whoIAM == isWhat.IS_CHEAT && item.CConfig.userAuthed)
            {
                PacketJsonSerializes.CheatPacketData.serverToClient.CHAT_MESSAGE_SENT cms = new PacketJsonSerializes.CheatPacketData.serverToClient.CHAT_MESSAGE_SENT();

                cms.packet_id = (int)PacketEnums.CHEAT.ServerToClient.CHAT_MESSAGE_SENT;

                cms.data.message_id = 1;
                cms.data.message_author_color = new CHAT_MESSAGE_SENT.thizCData(255, 40, 40);
                cms.data.message_content_color = new CHAT_MESSAGE_SENT.thizCData(40, 80, 160);
                cms.data.message_author = "Console";
                cms.data.message_content = command;
                cms.data.message_date = DateTime.Now.ToString("F");




                string tjo = JsonConvert.SerializeObject(cms);

                item.sendData(tjo);

            }
        });




        return false;

        if (command.Length > 4)
        {
            switch (command.Split('.')[0])
            {
                case "chat":
                     this.ChatCommand(command);
                    break;
            }
        }

        return false;
    }





    public bool ChatCommand(string command)
    {
        return false;
    }

}