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
            WHO_ARE_YOU_MOTHERFUCKER,
        }

        enum ClientToServer
        {
            IM_FUCKKING_LOADER,
            IM_FUCKING_CHEAT
        }
    }


    namespace CHEAT
    {
        enum ServerToClient
        {
            USER_AUTH_RESPONSE,
            NEED_USER_AUTH,
            CONFIG_CREATE,
            CONFIG_LOAD,
            CONFIG_REFRESH,
            CONFIG_REMOVE,
            CHAT_MESSAGE_SENT,
        }

        enum ClientToServer
        {
            USER_AUTH,
            CONFIG_CREATE,
            CONFIG_LOAD,
            CONFIG_REFRESH,
            CONFIG_REMOVE,
            CHAT_MESSAGE_SENT,
        }
    }
}