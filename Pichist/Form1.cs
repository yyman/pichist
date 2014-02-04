using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        Bitmap src, dst;
        Color cs, cc;
        int rr, gg, bb, h, s, l, max;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            DialogResult result = openDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                //fn = openDialog.SafeFileName;
                src = new Bitmap(openDialog.FileName);

                pictureBox1.Image = src;

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int[] hueCounter = new int[360];

            for (int z = 0; z < 360; z++) hueCounter[z] = 0; 

            for (int y = 0; y < src.Height; y++)
            {
                for (int x = 0; x < src.Width; x++)
                {
                    cs = src.GetPixel(x, y);
                    RGBHSL.RGBHSL.RGBToHSL(cs.R, cs.G, cs.B, out h, out s, out l);
                    hueCounter[h] = hueCounter[h] + 1;
                }
            }
            max = hueCounter.Max();
            dst = new Bitmap(360, 254);

            for (int hc = 0; hc < 360; hc++)
            {
                Graphics g = Graphics.FromImage(dst);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                RGBHSL.RGBHSL.HSLToRGB(hc, 255, 127, out rr, out gg, out bb);
                Pen p = new Pen(Color.FromArgb(rr, gg, bb), 1);
                g.DrawLine(p, new Point(hc, 254), new Point(hc, (int)(254 - 254 * (hueCounter[hc] / (double)max))));
            }

            pictureBox2.Image = dst;
        }
    }
}
