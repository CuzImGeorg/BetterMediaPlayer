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


        #pragma warning restore CS8618

        [STAThread]
        public static void Main()
        {


            Config.init();
            actions = new();
            hotkeys = new Hotkeys();
            ApplicationConfiguration.Initialize();
            Application.Run(new MediaPlayer());
        }


        private static async void abc()
        {
            // ApplicationConfiguration.Initialize();
           // Application.Run(new Form1());
            var gsmtcsm = await GetSystemMediaTransportControlsSessionManager();
            foreach (var s in gsmtcsm.GetSessions())
            {
                var mediaProperties = await GetMediaProperties(s);

                Debug.WriteLine("{0} - {1}", mediaProperties.Artist, mediaProperties.Title );
                
            }
            
            Debug.WriteLine("Press any key to quit..");
        }

        private static async Task<GlobalSystemMediaTransportControlsSessionManager> GetSystemMediaTransportControlsSessionManager() =>
             await GlobalSystemMediaTransportControlsSessionManager.RequestAsync();

        private static async Task<GlobalSystemMediaTransportControlsSessionMediaProperties> GetMediaProperties(GlobalSystemMediaTransportControlsSession session) =>
            await session.TryGetMediaPropertiesAsync();

    }
}