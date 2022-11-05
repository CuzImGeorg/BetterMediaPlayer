using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BetterMediaPlayer.Hotkey;

namespace BetterMediaPlayer
{
    public class Hotkey
    {

        public int id { get; private set; }
        public Keys key { get; set; }
        public string desc { get; set; }

        public delegate void Method();
        public Method action = null;
        public bool alt { get; set; }
        public bool shift { get; set; }
        public bool ctrl { get; set; }


        public Hotkey(int id, Keys key, string desc, Method action, bool alt, bool shift, bool ctrl)
        {
            this.id = id;
            this.key = key;
            this.desc = desc;
            this.action = action;
            this.alt = alt;
            this.shift = shift;
            this.ctrl = ctrl;
        }


    }
}
