using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using System.Threading.Tasks;

namespace PacketEnums 
{
    namespace LOADER
    {
        enum ServerToClient
        {
            USER_AUTH_RESPONSE,
            HWID_AUTH_REPONSE,
            CHEAT_RESPONSE
        }

        enum ClientToServer
        {
            USER_AUTH,
            HWID_AUTH,
        }
    }


    namespace CHEAT
    {
        enum ServerToClient
        {
            USER_AUTH_RESPONSE,
            NEED_USER_AUTH,
            CONFIG_CREATE_RESPONSE,
            CONFIG_LOAD_RESPONSE,
            CONFIG_REFRESH_RESPONSE,
            CONFIG_REMOVE_RESPONSE,
            CONFIG_SAVE_RESPONSE,
            CHAT_MESSAGE_SENT,
        }

        enum ClientToServer
        {
            USER_AUTH,
            CONFIG_CREATE,
            CONFIG_LOAD,
            CONFIG_REFRESH,
            CONFIG_REMOVE,
            CONFIG_SAVE,
            CHAT_MESSAGE_SENT,
        }
    }
}