using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public static class Globals
{
    public static ConsoleLogger LoggerG = new ConsoleLogger();

    public static void addNewUser(int ID, string username, string stemID)
    {
        ListView usersListView = Application.OpenForms["MainForm"].Controls["usersListView"] as ListView;


        ListViewItem lvi = new ListViewItem();
        lvi.Text = ID.ToString();

        lvi.SubItems.Add(username);
        lvi.SubItems.Add(stemID);

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

    public struct logCFG
    {
        public bool isDebugMode;
    }

    public static logCFG loggerConfig;
}
