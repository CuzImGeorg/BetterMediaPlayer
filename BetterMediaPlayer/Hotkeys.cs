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
            Hotkeylist.Add(new Hotkey(0, (Keys)  int.Parse(Config.KEY_VOLUMEUP[..Config.KEY_VOLUMEUP.IndexOf(";")], System.Globalization.NumberStyles.HexNumber), "volup", Program.actions.VoulumeUp, int.Parse(Config.KEY_VOLUMEUP[Config.KEY_VOLUMEUP.IndexOf(";")+1].ToString()) == 1, int.Parse(Config.KEY_VOLUMEUP[Config.KEY_VOLUMEUP.IndexOf(";") + 2].ToString()) == 1, int.Parse(Config.KEY_VOLUMEUP[Config.KEY_VOLUMEUP.IndexOf(";") + 3].ToString()) == 1));
            Hotkeylist.Add(new Hotkey(0, (Keys)  int.Parse(Config.KEY_VOLUMEDOWN[..Config.KEY_VOLUMEDOWN.IndexOf(";")], System.Globalization.NumberStyles.HexNumber), "volup", Program.actions.VoulumeDown, int.Parse(Config.KEY_VOLUMEDOWN[Config.KEY_VOLUMEDOWN.IndexOf(";") + 1].ToString()) == 1, int.Parse(Config.KEY_VOLUMEDOWN[Config.KEY_VOLUMEDOWN.IndexOf(";") + 2].ToString()) == 1, int.Parse(Config.KEY_VOLUMEDOWN[Config.KEY_VOLUMEDOWN.IndexOf(";") + 3].ToString()) == 1));
            Hotkeylist.Add(new Hotkey(0, (Keys)  int.Parse(Config.KEY_MUTE[..Config.KEY_MUTE.IndexOf(";")], System.Globalization.NumberStyles.HexNumber), "volup", Program.actions.SwichMute, int.Parse(Config.KEY_MUTE[Config.KEY_MUTE.IndexOf(";") + 1].ToString()) == 1, int.Parse(Config.KEY_MUTE[Config.KEY_MUTE.IndexOf(";") + 2].ToString()) == 1, int.Parse(Config.KEY_MUTE[Config.KEY_MUTE.IndexOf(";") + 3].ToString()) == 1));

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
