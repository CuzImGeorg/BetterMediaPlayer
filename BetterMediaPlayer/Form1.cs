using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms.Design;
using Windows.Networking.Sockets;

namespace BetterMediaPlayer
{
    public partial class MediaPlayer : Form
    {

        public int sid { get; private set; } = 0;

        public MediaPlayer()
        {
     
            InitializeComponent();
            UpdateVolume();
            this.label1.Location = new Point(34, (int)(125 - trackBar1.Value * 1.25) + 15);
            this.Location = new Point(-508, 20);
            this.Location = new Point(Screen.FromControl(this).Bounds.Width - 508, 0);
            //TODO set location config 
            




            fillDics();
            richTextBox1.ForeColor = Color.Red;
            loadSeesions();
            updateSessions();
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
                    
                    this.Invoke((Delegate)(() => {
                        trackBar1.Value = Program.actions.GetCurrentVolume();
                        label1.Text = trackBar1.Value.ToString();
                        label1.Location = new Point(34, (int)(125 - trackBar1.Value * 1.25) + 15);
                        

                    }));
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

        public void updateSessions()
        {

            Thread t = new Thread(() =>
            {
                Thread.Sleep(1000);

                while (true)
                {
                    this.Invoke(() =>
                    {
                        loadSeesions();
                    });
                    Thread.Sleep(400);
                }
            });
            t.Start();
        }

        public  async void loadSeesions()
        {
  

            var sessions = Program.actions.GetAllSessions();
            try
            {
                if (sessions.Count >= 1)
                {
                    var info = await Program.actions.GetMediaProperties(sessions[0]);
                    richTextBox1.Visible = true;
                    button1.Visible = true;
                    button4.Visible = true;
                    button7.Visible = true;

                    richTextBox1.Text = info.Title + " - " + info.Artist;

                }
                else
                {
                    richTextBox1.Visible = false;
                    button1.Visible = false;
                    button4.Visible = false;
                    button7.Visible = false;
                }
                if (sessions.Count >= 2)
                {
                    var info = await Program.actions.GetMediaProperties(sessions[1]);
                    richTextBox2.Visible = true;
                    richTextBox2.Text = info.Title + " - " + info.Artist;
                    button2.Visible = true;
                    button5.Visible = true;
                    button8.Visible = true;

                }
                else
                {
                    richTextBox2.Visible = false;
                    button2.Visible = false;
                    button5.Visible = false;
                    button8.Visible = false;

                }
                if (sessions.Count >= 3)
                {
                    var info = await Program.actions.GetMediaProperties(sessions[2]);
                    richTextBox3.Visible = true;
                    button3.Visible = true;
                    button6.Visible = true;
                    button9.Visible = true;
                    richTextBox3.Text = info.Title + " - " + info.Artist;
                }
                else
                {
                    richTextBox3.Visible = false;
                    button3.Visible = false;
                    button6.Visible = false;
                    button9.Visible = false;
                }
            } catch(Exception)
            {

            }
            if(sid >= sessions.Count)
            {
                SwitchSession();
                if (sid >= sessions.Count)
                {
                    SwitchSession();
                }
            }
         }

        private Dictionary<RichTextBox, int> DirectoryRTBSID = new();
        private Dictionary<Button, int> DiectoryBTSID = new();


        private void fillDics()
        {
            DirectoryRTBSID.Add(richTextBox1,0);
            DirectoryRTBSID.Add(richTextBox2,1);
            DirectoryRTBSID.Add(richTextBox3,2);
            DiectoryBTSID.Add(button1,0);
            DiectoryBTSID.Add(button4,0);
            DiectoryBTSID.Add(button7,0);
            DiectoryBTSID.Add(button2,1);
            DiectoryBTSID.Add(button5,1);
            DiectoryBTSID.Add(button8,1);
            DiectoryBTSID.Add(button3,2);
            DiectoryBTSID.Add(button6,2);
            DiectoryBTSID.Add(button9,2);


        }

        public void SwitchSession()
        {
            if(sid +1 < Program.actions.GetAllSessions().Count)
            {
                sid++;
            }else
            {
                sid = 0;
            }

            this.Invoke(()=>
            {
                foreach (RichTextBox r in DirectoryRTBSID.Keys)
                {
                    r.ForeColor = Color.White;
                }
                foreach (var v in DirectoryRTBSID)
                {
                    if (v.Value == sid)
                    {
                        v.Key.ForeColor = Color.Red;
                    }

                }

            });

        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            new Thread(new ThreadStart(() =>
            {
                Program.actions.Next();
            })).Start();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Thread(new ThreadStart(() =>
            {
                Program.actions.Next();
            })).Start();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Thread(new ThreadStart(() =>
            {
                Program.actions.Next();
            })).Start();

        }


        private void button4_Click(object sender, EventArgs e)
        {
            new Thread(new ThreadStart(() =>
            {
                Program.actions.Previous();
            })).Start();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            new Thread(new ThreadStart(() =>
            {
                Program.actions.Previous();
            })).Start();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            new Thread(new ThreadStart(() =>
            {
                Program.actions.Previous();
            })).Start();

        }
        private void button7_Click(object sender, EventArgs e)
        {
            new Thread(new ThreadStart(() =>
            {
                Program.actions.SwichPlay(0);
            })).Start();
        }


        private void button8_Click(object sender, EventArgs e)
        {
            new Thread(new ThreadStart(() =>
            {
                Program.actions.SwichPlay(1);
            })).Start();

        }



        private void button9_Click(object sender, EventArgs e)
        {
            new Thread(new ThreadStart(() =>
            {
                Program.actions.SwichPlay(2);
            })).Start();

        }

        private void button10_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}