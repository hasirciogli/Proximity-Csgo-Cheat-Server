using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public static class Globals
{
    public static ConsoleLogger LoggerG = new ConsoleLogger();

    public static void addNewUser(int ID, string userID, string username, string steamId, string steamName)
    {
        ListView usersListView = Application.OpenForms["MainForm"].Controls["usersListView"] as ListView;


        ListViewItem lvi = new ListViewItem();
        lvi.Text = ID.ToString();

        lvi.SubItems.Add(userID);
        lvi.SubItems.Add(username);
        lvi.SubItems.Add(steamId);
        lvi.SubItems.Add(steamName);

        usersListView.Items.Add(lvi);
    }

    public static void removeUser(int ID)
    {
        ListView usersListView = Application.OpenForms["MainForm"].Controls["usersListView"] as ListView;

        foreach(ListViewItem item in usersListView.Items) {
            if (item.Text == ID.ToString())
            {
                item.Remove();
            }
        }
    }








    public static logCFG loggerConfig;
    public struct logCFG
    {
        public bool isDebugMode;
    }
    public static class databaseConfig
    {
        public static string connectorString = $"SERVER=78.189.149.69;DATABASE=cheat;UID=ttk;PASSWORD=11223344;";
    }
}
