using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;


namespace OpenCV_Testing
{
	public partial class Form1 : Form
	{
		private Capture cap;
		private HaarCascade haarRightEye;
		private HaarCascade haarLeftEye;
		private HaarCascade haarMouth;
		private HaarCascade haarFace;
		private HaarCascade haarNose;
		private HaarCascade haarRightEar;
		private HaarCascade haarLeftEar;
		private HaarCascade haarUpperBody;
		public Form1()
		{
			InitializeComponent();
		}
		private void timer1_Tick(object sender, EventArgs e)
		{
			checkBox2.Checked = true;
			using (Emgu.CV.Image<Bgr,byte> nextFrame = cap.QueryFrame())
			{
				checkBox2.Checked = true;
				if (nextFrame != null)
				{
					// there's only one channel (greyscale), hence the zero index
					//var faces = nextFrame.DetectHaarCascade(haar)[0];
					checkBox3.Checked = true;

					Emgu.CV.Image<Gray, byte> grayframe = nextFrame.Convert<Gray, byte>();
					var faces =
						grayframe.DetectHaarCascade(
							haarFace, 1.4, 4,
						HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
						new Size(nextFrame.Width / 8, nextFrame.Height / 8)
						)[0];
					var rightear =
						grayframe.DetectHaarCascade(
							haarRightEar, 1.4, 4,
						HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
						new Size(nextFrame.Width / 8, nextFrame.Height / 8)
						)[0];
					var leftear =
						grayframe.DetectHaarCascade(
							haarLeftEar, 1.4, 4,
						HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
						new Size(nextFrame.Width / 8, nextFrame.Height / 8)
						)[0];
					var righteye =
						grayframe.DetectHaarCascade(
							haarRightEye, 1.4, 4,
						HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
						new Size(nextFrame.Width / 8, nextFrame.Height / 8)
						)[0];
					var lefteye =
						grayframe.DetectHaarCascade(
							haarLeftEye, 1.4, 4,
						HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
						new Size(nextFrame.Width / 8, nextFrame.Height / 8)
						)[0];
					var mouth =
						grayframe.DetectHaarCascade(
							haarMouth, 1.4, 4,
						HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
						new Size(nextFrame.Width / 8, nextFrame.Height / 8)
						)[0];
					var nose =
						grayframe.DetectHaarCascade(
							haarNose, 1.4, 4,
						HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
						new Size(nextFrame.Width / 8, nextFrame.Height / 8)
						)[0];
					var upperbody =
						grayframe.DetectHaarCascade(
							haarUpperBody, 1.4, 4,
						HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
						new Size(nextFrame.Width / 8, nextFrame.Height / 8)
						)[0];

					foreach (var face in faces)
					{
						nextFrame.Draw(face.rect, new Bgr(Color.AliceBlue), 1);
					}

					foreach (var face in rightear)
					{
						nextFrame.Draw(face.rect, new Bgr(Color.Red), 1);
					}

					foreach (var face in leftear)
					{
						nextFrame.Draw(face.rect, new Bgr(Color.Blue), 1);
					}

					foreach (var face in righteye)
					{
						nextFrame.Draw(face.rect, new Bgr(Color.Green), 1);
					}

					foreach (var face in lefteye)
					{
						nextFrame.Draw(face.rect, new Bgr(Color.Yellow), 1);
					}

					foreach (var face in mouth)
					{
						nextFrame.Draw(face.rect, new Bgr(Color.Purple), 1);
					}

					foreach (var face in nose)
					{
						nextFrame.Draw(face.rect, new Bgr(Color.Orange), 1);
					}

					foreach (var face in upperbody)
					{
						nextFrame.Draw(face.rect, new Bgr(Color.GreenYellow), 1);
					}

					pictureBox1.Image = nextFrame.ToBitmap();

				}
			}
		}
		private void Form1_Load(object sender, EventArgs e)
		{
			// passing 0 gets zeroth webcam

			cap = new Capture(5);
			// adjust path to find your xml

			haarRightEye = new HaarCascade("C:\\OpenCV\\OpenCV\\data\\haarcascades\\haarcascade_mcs_righteye.xml");
			haarLeftEye = new HaarCascade("C:\\OpenCV\\OpenCV\\data\\haarcascades\\haarcascade_mcs_lefteye.xml");
			haarMouth = new HaarCascade("C:\\OpenCV\\OpenCV\\data\\haarcascades\\haarcascade_mcs_mouth.xml");
			haarNose = new HaarCascade("C:\\OpenCV\\OpenCV\\data\\haarcascades\\haarcascade_mcs_nose.xml");
			haarRightEar = new HaarCascade("C:\\OpenCV\\OpenCV\\data\\haarcascades\\haarcascade_mcs_rightear.xml");
			haarLeftEar = new HaarCascade("C:\\OpenCV\\OpenCV\\data\\haarcascades\\haarcascade_mcs_leftear.xml");
			haarUpperBody = new HaarCascade("C:\\OpenCV\\OpenCV\\data\\haarcascades\\haarcascade_upperbody.xml");
			haarFace = new HaarCascade("C:\\OpenCV\\OpenCV\\data\\haarcascades\\haarcascade_frontalface_alt.xml");
			//	 "..\\..\\..\\..\\lib\\haarcascade_frontalface_alt2.xml");
			checkBox1.Checked = true;
		}
	}
}
