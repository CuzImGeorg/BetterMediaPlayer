using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMediaPlayer
{
    public class Hotkeys
    {
        public List<Hotkey> Hotkeylist { get; private set; } = new();

        public Hotkeys()
        {
            //TODO set usefull descs 
            Hotkeylist.Add(new Hotkey(0, (Keys)  int.Parse(Config.KEY_VOLUMEUP[..Config.KEY_VOLUMEUP.IndexOf(";")], System.Globalization.NumberStyles.HexNumber), "volup", Program.actions.VoulumeUp, int.Parse(Config.KEY_VOLUMEUP[Config.KEY_VOLUMEUP.IndexOf(";")+1].ToString()) == 1, int.Parse(Config.KEY_VOLUMEUP[Config.KEY_VOLUMEUP.IndexOf(";") + 2].ToString()) == 1, int.Parse(Config.KEY_VOLUMEUP[Config.KEY_VOLUMEUP.IndexOf(";") + 3].ToString()) == 1));
            Hotkeylist.Add(new Hotkey(0, (Keys)  int.Parse(Config.KEY_VOLUMEDOWN[..Config.KEY_VOLUMEDOWN.IndexOf(";")], System.Globalization.NumberStyles.HexNumber), "volup", Program.actions.VoulumeDown, int.Parse(Config.KEY_VOLUMEDOWN[Config.KEY_VOLUMEDOWN.IndexOf(";") + 1].ToString()) == 1, int.Parse(Config.KEY_VOLUMEDOWN[Config.KEY_VOLUMEDOWN.IndexOf(";") + 2].ToString()) == 1, int.Parse(Config.KEY_VOLUMEDOWN[Config.KEY_VOLUMEDOWN.IndexOf(";") + 3].ToString()) == 1));
            Hotkeylist.Add(new Hotkey(0, (Keys)  int.Parse(Config.KEY_MUTE[..Config.KEY_MUTE.IndexOf(";")], System.Globalization.NumberStyles.HexNumber), "volup", Program.actions.SwichMute, int.Parse(Config.KEY_MUTE[Config.KEY_MUTE.IndexOf(";") + 1].ToString()) == 1, int.Parse(Config.KEY_MUTE[Config.KEY_MUTE.IndexOf(";") + 2].ToString()) == 1, int.Parse(Config.KEY_MUTE[Config.KEY_MUTE.IndexOf(";") + 3].ToString()) == 1));

            Hotkeylist.Add(new Hotkey(0, (Keys)  int.Parse(Config.KEY_PREV[..Config.KEY_PREV.IndexOf(";")], System.Globalization.NumberStyles.HexNumber), "volup", Program.actions.Previous, int.Parse(Config.KEY_PREV[Config.KEY_PREV.IndexOf(";") + 1].ToString()) == 1, int.Parse(Config.KEY_PREV[Config.KEY_PREV.IndexOf(";") + 2].ToString()) == 1, int.Parse(Config.KEY_PREV[Config.KEY_PREV.IndexOf(";") + 3].ToString()) == 1));
            Hotkeylist.Add(new Hotkey(0, (Keys)  int.Parse(Config.KEY_NEXT[..Config.KEY_NEXT.IndexOf(";")], System.Globalization.NumberStyles.HexNumber), "volup", Program.actions.Next, int.Parse(Config.KEY_NEXT[Config.KEY_NEXT.IndexOf(";") + 1].ToString()) == 1, int.Parse(Config.KEY_NEXT[Config.KEY_NEXT.IndexOf(";") + 2].ToString()) == 1, int.Parse(Config.KEY_NEXT[Config.KEY_NEXT.IndexOf(";") + 3].ToString()) == 1));
            Hotkeylist.Add(new Hotkey(0, (Keys)  int.Parse(Config.KEY_PLAY[..Config.KEY_PLAY.IndexOf(";")], System.Globalization.NumberStyles.HexNumber), "volup", Program.actions.SwichPlay, int.Parse(Config.KEY_PLAY[Config.KEY_PLAY.IndexOf(";") + 1].ToString()) == 1, int.Parse(Config.KEY_PLAY[Config.KEY_PLAY.IndexOf(";") + 2].ToString()) == 1, int.Parse(Config.KEY_PLAY[Config.KEY_PLAY.IndexOf(";") + 3].ToString()) == 1));
            
           Hotkeylist.Add(new Hotkey(0, (Keys)  int.Parse(Config.KEY_SWITCHSESSION[..Config.KEY_SWITCHSESSION.IndexOf(";")], System.Globalization.NumberStyles.HexNumber), "volup", Program.actions.NextSession, int.Parse(Config.KEY_SWITCHSESSION[Config.KEY_SWITCHSESSION.IndexOf(";") + 1].ToString()) == 1, int.Parse(Config.KEY_SWITCHSESSION[Config.KEY_SWITCHSESSION.IndexOf(";") + 2].ToString()) == 1, int.Parse(Config.KEY_SWITCHSESSION[Config.KEY_SWITCHSESSION.IndexOf(";") + 3].ToString()) == 1));
            CheckHotkeyPressed();

        }


        public void CheckHotkeyPressed()
        {
            Thread t = new Thread(() =>
            {
                while (true)
                {
                    foreach (Hotkey hotkey in Hotkeylist)
                    {
                        if (InputListener.IsKeyDown(hotkey.key) 
                        && (((Control.ModifierKeys & Keys.Alt) == Keys.Alt) == hotkey.alt || !hotkey.alt) 
                        && (((Control.ModifierKeys & Keys.Shift) == Keys.Shift) == hotkey.shift || !hotkey.shift)
                        && (((Control.ModifierKeys & Keys.Control) == Keys.Control) == hotkey.ctrl || !hotkey.ctrl))
                        {
                            hotkey.action();
                        }

                    }
                    Thread.Sleep(100);

                }
            });
            t.Start();
        }

    }
}
