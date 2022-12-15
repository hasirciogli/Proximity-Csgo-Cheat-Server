using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using System.Threading.Tasks;

namespace PacketEnums 
{
    namespace CHEAT
    {
        enum ServerToClient
        {
            FIRST_AUTH,
            USER_AUTH,
            CONFIG_CREATE,
            CONFIG_LOAD,
            CONFIG_REFRESH,
            CONFIG_REMOVE,
            CHAT_MESSAGE_SENT,
        }

        enum ClientToServer
        {
            FIRST_AUTH,
            USER_AUTH,
            CONFIG_CREATE,
            CONFIG_LOAD,
            CONFIG_REFRESH,
            CONFIG_REMOVE,
            CHAT_MESSAGE_SENT,
        }
    }
}