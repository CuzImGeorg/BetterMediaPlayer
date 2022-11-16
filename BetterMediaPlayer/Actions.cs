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


        private bool cd1 = false;
        private bool cd2 = false;


        public async void SwichPlay()
        {
            if(!cd1)
            {
                await gsmtcsm.GetSessions()[Program.mediaPlayer.sid].TryTogglePlayPauseAsync();
                cd1 = true;
                #pragma warning disable CS4014 
                Task.Run(()=>
                {


                   Thread.Sleep(250);
                    cd1 = false;
                });
                #pragma warning restore CS4014 
            }

            

        }




        public async void SwichPlay(int id)
        {
            if (!cd2)
            {
                await gsmtcsm.GetSessions()[id].TryTogglePlayPauseAsync();
                cd2 = true;
                #pragma warning disable CS4014
                Task.Run(() =>
                {


                    Thread.Sleep(250);
                    cd2 = false;
                });
                #pragma warning restore CS4014
            }
        }

        private bool cd3 = false;
        private bool cd4 = false;


        public async void Previous()
        {
            if (!cd3)
            {
                await gsmtcsm.GetSessions()[Program.mediaPlayer.sid].TrySkipPreviousAsync();
                cd3 = true;
                #pragma warning disable CS4014
                Task.Run(() =>
                {


                    Thread.Sleep(250);
                    cd3 = false;
                });
                #pragma warning restore CS4014
            }
        }



        public async void Next()
        {
            if (!cd4)
            {
                await gsmtcsm.GetSessions()[Program.mediaPlayer.sid].TrySkipNextAsync();
                cd4 = true;
                #pragma warning disable CS4014
                Task.Run(() =>
                {


                    Thread.Sleep(250);
                    cd4 = false;
                });
                #pragma warning restore CS4014
            }
        }

        public void NextSession()
        {
            Program.mediaPlayer.SwitchSession();
        }




        //[DllImport("user32.dll")]
        //static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

        public IReadOnlyList<GlobalSystemMediaTransportControlsSession> GetAllSessions() => gsmtcsm.GetSessions();

        public async Task<GlobalSystemMediaTransportControlsSessionManager> GetSystemMediaTransportControlsSessionManager() =>   await GlobalSystemMediaTransportControlsSessionManager.RequestAsync();

        public async Task<GlobalSystemMediaTransportControlsSessionMediaProperties> GetMediaProperties(GlobalSystemMediaTransportControlsSession session) =>  await session.TryGetMediaPropertiesAsync();

    }
}
