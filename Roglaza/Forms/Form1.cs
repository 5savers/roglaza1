using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Video.VFW;
using AForge.Video.FFMPEG;
using System.IO;


namespace Roglaza.Forms    

{
    public partial class FormCamera : Form
    {
        public FormCamera(bool Test=false)
        {
            InitializeComponent();          

        }
        int tick = 0;
        VideoCaptureDevice videosource;

        private void Form1_Load(object sender, EventArgs e)
        {
            FilterInfoCollection videosorces = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            if (videosorces != null)
            {
                videosource = new VideoCaptureDevice(videosorces[0].MonikerString);

                try
                {
                    if (videosource.VideoCapabilities.Length > 0)
                    {
                        string highestresolution = "0;0";

                        for (int i = 0; i < videosource.VideoCapabilities.Length; i++)
                        {
                            if (videosource.VideoCapabilities[i].FrameSize.Width > Convert.ToInt32(highestresolution.Split(';')[0]))
                            {
                                highestresolution = videosource.VideoCapabilities[i].FrameSize.Width.ToString() + ";" + i.ToString();
                            }
                        }

                        videosource.VideoResolution = videosource.VideoCapabilities[Convert.ToInt32(highestresolution.Split(';')[1])];
                    }
                }
                catch { }

                videosource.NewFrame += new AForge.Video.NewFrameEventHandler(videoSource_NewFrame);
                videosource.Start();
            }

        }

        void videoSource_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            pictureBox1.BackgroundImage = (Bitmap)eventArgs.Frame.Clone();
           
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (videosource != null && videosource.IsRunning)
            {
                videosource.SignalToStop();
                videosource = null;
            }
        }

        private void _Save()
        {
            string path = "";
            try
            {
                path=System.IO.File.ReadAllText("campath").Trim();
                
            }
            catch { }


            if (path.Length > 0)
            {
                string filename = path;
                FileStream fstream = new FileStream(filename, FileMode.Create);
                // MemoryStream ms = new MemoryStream();
                pictureBox1.BackgroundImage.Save(fstream, System.Drawing.Imaging.ImageFormat.Jpeg);
                fstream.Close();

            }
        }
        

     

        private void timer1_Tick(object sender, EventArgs e)
        {
            tick++;
            _Save();
            if (tick == 10)
            {
                timer1.Stop();
                videosource.Stop();
                this.Close();
                Application.Exit();
        
            }
        }

      
    }
}

