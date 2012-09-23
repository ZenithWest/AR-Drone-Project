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
			using (Emgu.CV.Image<Bgr,byte> nextFrame = cap.QueryFrame())
			{
				if (nextFrame != null)
				{
					// there's only one channel (greyscale), hence the zero index
					//var faces = nextFrame.DetectHaarCascade(haar)[0];
					bool checkBoxFaceValue = checkBoxFace.Checked;
					bool checkBoxMouthValue = checkBoxMouth.Checked;
					bool checkBoxNoseValue = checkBoxNose.Checked;
					bool checkBoxLeftEarValue = checkBoxLeftEar.Checked;
					bool checkRightEarValue = checkBoxRightEar.Checked;
					bool checkBoxLeftEyeValue = checkBoxLeftEye.Checked;
					bool checkBoxRightEyeValue = checkBoxRightEye.Checked;
					bool checkBoxUpperbodyValue = checkBoxUpperbody.Checked;

					Emgu.CV.Image<Gray, byte> grayframe = nextFrame.Convert<Gray, byte>();
					MCvAvgComp[] faces = null;
					MCvAvgComp[] mouth = null;
					MCvAvgComp[] nose = null;
					MCvAvgComp[] rightear = null;
					MCvAvgComp[] leftear = null;
					MCvAvgComp[] righteye = null;
					MCvAvgComp[] lefteye = null;
					MCvAvgComp[] upperbody = null;

					//Size minSize = new Size(nextFrame.Width / 8, nextFrame.Height / 8);
					Size minSize = new Size(20,20);
					Emgu.CV.CvEnum.HAAR_DETECTION_TYPE detection_type = HAAR_DETECTION_TYPE.FIND_BIGGEST_OBJECT | HAAR_DETECTION_TYPE.DO_CANNY_PRUNING;

					if (checkBoxFaceValue)
					{
						faces =
						grayframe.DetectHaarCascade(
							haarFace, 1.1, 4,
						detection_type,
						minSize
						)[0];
					}

					if (checkBoxMouthValue)
					{
						mouth =
						grayframe.DetectHaarCascade(
							haarMouth, 1.4, 3,
						detection_type,
						minSize
						)[0];
					}

					if (checkBoxNoseValue)
					{
						nose =
						grayframe.DetectHaarCascade(
							haarNose, 1.4, 4,
						detection_type,
						minSize
						)[0];
					}

					if (checkBoxLeftEarValue)
					{
						leftear =
						grayframe.DetectHaarCascade(
							haarLeftEar, 1.4, 4,
						detection_type,
						minSize
						)[0];
					}

					if (checkRightEarValue)
					{
						rightear =
						grayframe.DetectHaarCascade(
							haarRightEar, 1.3, 4,
						detection_type,
						minSize
						)[0];
					}

					if (checkBoxLeftEyeValue)
					{
						lefteye =
						grayframe.DetectHaarCascade(
							haarLeftEye, 1.3, 4,
						detection_type,
						minSize
						)[0];
					}

					if (checkBoxRightEyeValue)
					{
						righteye =
						grayframe.DetectHaarCascade(
							haarRightEye, 1.3, 4,
						detection_type,
						minSize
						)[0];
					}

					if (checkBoxUpperbodyValue)
					{
						upperbody =
						grayframe.DetectHaarCascade(
							haarUpperBody, 1.4, 4,
						detection_type,
						minSize
						)[0];
					}

					int thickness = 2;
					if (checkBoxFaceValue)
					{
						foreach (var face in faces)
						{
							pictureBox2.Image = nextFrame.Copy(face.rect).ToBitmap();
						}
					}

					if (checkBoxLeftEyeValue)
					{
						foreach (var face in lefteye)
						{
							pictureBox3.Image = nextFrame.Copy(face.rect).ToBitmap();
						}
					}


					if (checkBoxFaceValue)
					{
						foreach (var face in faces)
						{
							nextFrame.Draw(face.rect, new Bgr(Color.AliceBlue), thickness);
						}
					}

					if (checkBoxMouthValue)
					{
						foreach (var face in mouth)
						{
							nextFrame.Draw(face.rect, new Bgr(Color.Purple), thickness);
						}
					}

					if (checkBoxNoseValue)
					{
						foreach (var face in nose)
						{
							nextFrame.Draw(face.rect, new Bgr(Color.Orange), thickness);
						}
					}

					if (checkBoxLeftEarValue)
					{
						foreach (var face in leftear)
						{
							nextFrame.Draw(face.rect, new Bgr(Color.Blue), thickness);
						}
					}

					if (checkRightEarValue)
					{
						foreach (var face in rightear)
						{
							nextFrame.Draw(face.rect, new Bgr(Color.Red), thickness);
						}
					}

					if (checkBoxLeftEyeValue)
					{
						foreach (var face in lefteye)
						{
							nextFrame.Draw(face.rect, new Bgr(Color.Yellow), thickness);
						}
					}

					if (checkBoxRightEyeValue)
					{

						foreach (var face in righteye)
						{
							nextFrame.Draw(face.rect, new Bgr(Color.Green), thickness);
						}
						
					}

					if (checkBoxUpperbodyValue)
					{
						foreach (var face in upperbody)
						{
							nextFrame.Draw(face.rect, new Bgr(Color.GreenYellow), thickness);
						}
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

			haarRightEye = new HaarCascade("C:\\OpenCV\\OpenCV\\data\\haarcascades\\haarcascade_righteye_2splits.xml");
			haarLeftEye = new HaarCascade("C:\\OpenCV\\OpenCV\\data\\haarcascades\\haarcascade_lefteye_2splits.xml");
			haarMouth = new HaarCascade("C:\\OpenCV\\OpenCV\\data\\haarcascades\\haarcascade_mcs_mouth.xml");
			haarNose = new HaarCascade("C:\\OpenCV\\OpenCV\\data\\haarcascades\\haarcascade_mcs_nose.xml");
			haarRightEar = new HaarCascade("C:\\OpenCV\\OpenCV\\data\\haarcascades\\haarcascade_mcs_rightear.xml");
			haarLeftEar = new HaarCascade("C:\\OpenCV\\OpenCV\\data\\haarcascades\\haarcascade_mcs_leftear.xml");
			haarUpperBody = new HaarCascade("C:\\OpenCV\\OpenCV\\data\\haarcascades\\haarcascade_upperbody.xml");
			haarFace = new HaarCascade("C:\\OpenCV\\OpenCV\\data\\haarcascades\\haarcascade_frontalface_alt.xml");
			//	 "..\\..\\..\\..\\lib\\haarcascade_frontalface_alt2.xml");
		}
	}
}
