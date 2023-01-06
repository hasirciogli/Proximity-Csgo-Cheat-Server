using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RogsoftwareServer.Libs.AllEnumerations
{
    public enum isWhat
    {
        IS_CHEAT,
        IS_LOADER,
        IS_REMOTE_SERVER,
        IS_ADMIN,
        IS_SERVER,
        IS_WEBSITE,
    }
    public struct ClientConfig
    {
        public int userID;
        public string username;
        public string userToken;
        public bool userAuthed;
        public isWhat whoIAM;
        public bool PoxiAuthStatus;
        public bool crvStatus;



        public string steamID;
        public string steamName;
    }
}