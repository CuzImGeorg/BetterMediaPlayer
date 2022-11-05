using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMediaPlayer
{
    public class Config
    {

        private static DataTable cfg;
        public static void init()
        {
            cfg = getConfig();
            VOLUME_MODF = Double.Parse(getValue("volm"));
            KEY_VOLUMEUP = getValue("volup");
            KEY_VOLUMEDOWN = getValue("voldown");
            KEY_MUTE = getValue("mute");
        }



        private static string getValue(string name)
        {
            var results = from myRow in cfg.AsEnumerable()
                          where myRow.Field<string>("name") == name
                            select myRow;

            DataView view = results.AsDataView();
            view[0].Row["value"].ToString();
            

            return view[0].Row["value"].ToString();

        }
        private static DataTable getConfig()
        {
            SQLiteConnectionStringBuilder connstr = new SQLiteConnectionStringBuilder();
            connstr.DataSource = "db.db";

            SQLiteConnection conn = new SQLiteConnection(connstr.ToString());
            conn.Open();
            SQLiteCommand com = new SQLiteCommand("SELECT name, value FROM config ", conn);
            SQLiteDataReader reader = com.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            reader.Close();
            conn.Close();
            return dt;
        }


        public static Double VOLUME_MODF { get; set; }


        //Hotkeys
        public static string KEY_VOLUMEUP { get; set; }
        public static string KEY_VOLUMEDOWN { get; set; }
        public static string KEY_MUTE { get; set; }

    }
}
