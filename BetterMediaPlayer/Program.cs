using Microsoft.VisualBasic.Devices;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Media.Control;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace BetterMediaPlayer
{
    public static class Program
    {
        #pragma warning disable CS8618 
        public static Actions actions { get; private set; }
        public static Hotkeys hotkeys { get; private set; }

        public static MediaPlayer mediaPlayer { get; private set; }


        #pragma warning restore CS8618

        [STAThread]
        private static void Main()
        {
            Init();

        }

        private static async void Init()
        {

            Config.init();
            actions = new();
            hotkeys = new Hotkeys();
            foreach (var s in actions.GetAllSessions())
            {
                var mediaProperties = await actions.GetMediaProperties(s);
                Debug.WriteLine("{0} - {1}", mediaProperties.Artist, mediaProperties.Title);
            }

            ApplicationConfiguration.Initialize();
            Application.Run(mediaPlayer = new MediaPlayer());


        }
     

    }
}