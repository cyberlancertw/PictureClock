using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClockExercise3
{
    public partial class Form1 : Form
    {
        Image imgSec, imgMin, imgHr;
        int nowSec, nowMin, nowHr, nowMilliSec;
        float thetaSec, thetaMin, thetaHr;
        


        public Form1()
        {

            InitializeComponent();
            
            try
            {
                imgSec = Image.FromFile(@"C:\Users\user\source\repos\ClockExercise3\ClockExercise3\clocksec.png");
                imgMin = Image.FromFile(@"C:\Users\user\source\repos\ClockExercise3\ClockExercise3\clockmin.png");
                imgHr = Image.FromFile(@"C:\Users\user\source\repos\ClockExercise3\ClockExercise3\clockhr.png");
            }
            catch (Exception)
            {
                label1.Text = "file fail";
            }

            timer1.Start();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            nowSec = System.DateTime.Now.Second;
            nowMin = System.DateTime.Now.Minute;
            nowHr = System.DateTime.Now.Hour;
            nowMilliSec = System.DateTime.Now.Millisecond;
            thetaSec = (nowSec + nowMilliSec / 1000f) * 6 - 90;
            thetaMin = (nowMin + nowSec / 60f) * 6 - 90;
            thetaHr = (nowHr % 12 + nowMin / 60f) * 30 - 90;
            
            label1.Text = $"{nowHr}時{nowMin}分{nowSec}秒";
            label1.Location = new Point(this.Width/2 - label1.Size.Width/2 , this.Height - 80);
            this.Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            
            Graphics gfx = e.Graphics;

            //gfx.DrawLine(new Pen(Color.Blue, 1), 390, 0, 390, 780);
            //gfx.DrawLine(new Pen(Color.Blue, 1), 0, 390, 780, 390);
            
            gfx.TranslateTransform(390, 390);
            gfx.RotateTransform(thetaHr);
            //gfx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            gfx.TranslateTransform(-18, -18);
            gfx.DrawImage(imgHr, 0, 0);

            gfx.TranslateTransform(18, 18);
            gfx.RotateTransform(-thetaHr + thetaMin);
            gfx.TranslateTransform(-18, -18);
            gfx.DrawImage(imgMin, 0, 0);

            gfx.TranslateTransform(18, 18);
            gfx.RotateTransform(-thetaMin + thetaSec);
            gfx.TranslateTransform(-18, -18);
            gfx.DrawImage(imgSec, 0, 0);
            



            //gfx.Dispose();   有這個又 form 有開 doublebuffered 會需要exception
            
        }


    }
}
