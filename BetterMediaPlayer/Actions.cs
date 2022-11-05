using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Control;
using AudioSwitcher.AudioApi.CoreAudio;

namespace BetterMediaPlayer
{
    public class Actions
    {
        private GlobalSystemMediaTransportControlsSessionManager gsmtcsm { get; set; }
        public  async void init()
        {
            gsmtcsm = await GetSystemMediaTransportControlsSessionManager();

        }
        CoreAudioDevice defaultPlaybackDevice = new CoreAudioController().DefaultPlaybackDevice;


        public Actions()
        {
            init();
     

        }

        public void VoulumeUp()
        {
            defaultPlaybackDevice.Volume = defaultPlaybackDevice.Volume + Config.VOLUME_MODF;
            
        }

        public void VoulumeDown()
        {
            defaultPlaybackDevice.Volume = defaultPlaybackDevice.Volume - Config.VOLUME_MODF;

        }

        public void SetVolume(int value)
        {
            defaultPlaybackDevice.Volume = value;

        }

        private bool _mutecd = false;
        public void SwichMute ()
        {
            if (!_mutecd)
            {
                defaultPlaybackDevice.Mute(!defaultPlaybackDevice.IsMuted);
                _mutecd = true;
                new Thread(new ThreadStart(()=>
                {
                    Thread.Sleep(400);
                    _mutecd = false;
                    
                })).Start();
            }

        }

        public bool ISMute()
        {
            return defaultPlaybackDevice.IsMuted;
        }

        public int GetCurrentVolume ()
        {
           return (int) defaultPlaybackDevice.Volume;
        }

        public void Pause()
        {

        }

        

        public void Next()
        {

        }




        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);


        private static async Task<GlobalSystemMediaTransportControlsSessionManager> GetSystemMediaTransportControlsSessionManager() =>
             await GlobalSystemMediaTransportControlsSessionManager.RequestAsync();

        private static async Task<GlobalSystemMediaTransportControlsSessionMediaProperties> GetMediaProperties(GlobalSystemMediaTransportControlsSession session) =>
            await session.TryGetMediaPropertiesAsync();

    }
}
