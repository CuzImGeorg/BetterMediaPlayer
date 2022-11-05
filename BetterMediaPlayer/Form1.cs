using System.Windows.Forms.Design;
using Windows.Networking.Sockets;

namespace BetterMediaPlayer
{
    public partial class MediaPlayer : Form
    {
        public MediaPlayer()
        {
            InitializeComponent();
            UpdateVolume();
            this.label1.Location = new Point(34, (int)(125 - trackBar1.Value * 1.25) + 15);

        }


        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label1.Text = trackBar1.Value.ToString();
            label1.Location = new Point(34,  (int) (125 - trackBar1.Value * 1.25) + 15 );
            Program.actions.SetVolume(trackBar1.Value);
        }

        private int _tmpvol;
        private void bmute_Click(object sender, EventArgs e)
        {
            if(!Program.actions.ISMute())
            {
                _tmpvol = trackBar1.Value;
                Program.actions.SwichMute();
                trackBar1.Value = 0;
                label1.Text = trackBar1.Value.ToString();
                label1.Location = new Point(34, (int)(125 - trackBar1.Value * 1.25) + 15);
            }else
            {
                trackBar1.Value = _tmpvol;
                Program.actions.SwichMute();
                label1.Text = trackBar1.Value.ToString();
                label1.Location = new Point(34, (int)(125 - trackBar1.Value * 1.25) + 15);
            }


        }

        public void UpdateVolume()
        {
            Thread t = new Thread(() =>
            {
                Thread.Sleep(500);
                while(true)
                {
                    while(Program.actions.ISMute())
                    {

                    }
                    
                    this.Invoke(() => {
                        trackBar1.Value = Program.actions.GetCurrentVolume();
                        label1.Text = trackBar1.Value.ToString();
                        label1.Location = new Point(34, (int)(125 - trackBar1.Value * 1.25) + 15);


                    });
                    Thread.Sleep(100);

                }


            });
            t.Start();
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x84:
                    base.WndProc(ref m);
                    if ((int)m.Result == 0x1)
                        m.Result = (IntPtr)0x2;
                    return;
            }

            base.WndProc(ref m);
        }
    }
}