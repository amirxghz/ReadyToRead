using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using MySqlConnector;

namespace ReadyToRead
{
    public static class Program
    {
        public static Image _fotoProfilo = Properties.Resources.Pfp;
        public static List<ClsLibro> _libri = new List<ClsLibro>();

        public static MySqlConnection conn;

        [STAThread]
        static void Main()
        {
            string connectionString = Properties.Settings.Default.dbConnString;
            conn = new MySqlConnection(connectionString);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmLogin());
        }
    }
}