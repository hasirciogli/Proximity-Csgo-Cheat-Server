using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public class ConsoleLogger
{
    public void Log(string text)
    {
        RichTextBox KRTB = Application.OpenForms["MainForm"].Controls["LogBox"] as RichTextBox;
        KRTB.AppendText(text);
        KRTB.AppendText("\n");
    }
}