using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using Emgu.CV.UI;
using Emgu.CV.VideoSurveillance;
using System.Threading.Tasks;

namespace OpenCV_Testing
{
	public partial class Camshift : Form
	{
		private bool started = false;
		private Detection.Camshift cs;
		
		public Camshift()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (started) return;
			cs = new Detection.Camshift();

			Task.Factory.StartNew(() => DrawImage());
		}

		private void DrawImage()
		{
			while (this.Visible)
			{
				CheckForIllegalCrossThreadCalls = false;
				Image<Bgr, Byte> screen = cs.currentImage;

				if (screen != null)
				{
					screen.Draw(new Rectangle(screen.Width/2 - 50, screen.Height/2 - 50, 100, 100), new Bgr(100, 10, 10), 1);
					if (cs.Max_Loc != null)
					{
						screen.Draw(new CircleF(new Point(cs.Max_Loc[0].X, cs.Max_Loc[0].Y), (float)cs.Max_Size[0]), new Bgr(0.0, 0.0, 255.0), 1);
					}
					screen.Draw(new CircleF(cs.currentCentroid, 10), new Bgr(255, 255, 255), 1);
					imageBox1.Image = screen;
				}
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			cs.SetTarget();
		}
	}
}
