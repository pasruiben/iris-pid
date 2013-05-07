using System;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using IrisPIDLib.Util;
using IrisPIDLib;

namespace IrisPID
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Iris());
        }
    }
}
