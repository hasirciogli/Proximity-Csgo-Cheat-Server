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
            FIRST_AUTH_RESPONSE,
            USER_AUTH_RESPONSE,
        }

        enum ClientToServer
        {
            FIRST_AUTH,
            USER_AUTH,
        }
    }
}