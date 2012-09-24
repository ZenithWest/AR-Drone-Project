using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;

using Emgu.CV.OCR;


namespace OpenCV_Testing
{
	public partial class Form1 : Form
	{
		private Capture cap;
		//private CascadeClassifier haarFace;
		private HaarCascade haarFace;
		private HaarCascade haarNose;
		private HaarCascade haarMouth;
		private HaarCascade haarLeftEar;
		private HaarCascade haarRightEar;
		private HaarCascade haarLeftEye;
		private HaarCascade haarRightEye;
		private HaarCascade haarUpperBody;
		private Tesseract _ocr;
		public Form1()
		{
			InitializeComponent();
			_ocr = new Tesseract();
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
					//Rectangle[] faces = null;
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
					Emgu.CV.CvEnum.HAAR_DETECTION_TYPE detection_type = HAAR_DETECTION_TYPE.DO_CANNY_PRUNING | HAAR_DETECTION_TYPE.FIND_BIGGEST_OBJECT;

					if (checkBoxFaceValue)
					{
						
						//faces = haarFace.DetectMultiScale(grayframe, 1.1, 8, minSize, Size.Empty);
						faces = haarFace.Detect(grayframe, 1.1, 4, detection_type, minSize, Size.Empty);
					}

					if (checkBoxMouthValue)
					{
						mouth = haarMouth.Detect(grayframe, 1.4, 4, detection_type, minSize, Size.Empty);
					}

					if (checkBoxNoseValue)
					{
						nose = haarNose.Detect(grayframe, 1.4, 4, detection_type, minSize, Size.Empty);
					}

					if (checkBoxLeftEarValue)
					{
						leftear = haarLeftEar.Detect(grayframe, 1.4, 4, detection_type, minSize, Size.Empty);
					}

					if (checkRightEarValue)
					{
						rightear = haarRightEar.Detect(grayframe, 1.4, 4, detection_type, minSize, Size.Empty);
					}

					if (checkBoxLeftEyeValue)
					{
						lefteye = haarLeftEye.Detect(grayframe, 1.2, 4, detection_type, minSize, Size.Empty);
					}

					if (checkBoxRightEyeValue)
					{
						righteye = haarRightEye.Detect(grayframe, 1.4, 4, detection_type, minSize, Size.Empty);
					}

					if (checkBoxUpperbodyValue)
					{
						upperbody = haarUpperBody.Detect(grayframe, 1.4, 4, detection_type, minSize, Size.Empty);
					}

					int thickness = 2;
					if (checkBoxFaceValue)
					{
						foreach (var face in faces)
						{
							//imageBox2.Image = nextFrame.Copy(face);
							imageBox2.Image = nextFrame.Copy(face.rect);
						}
					}

					if (checkBoxLeftEyeValue)
					{
						foreach (var face in lefteye)
						{
							imageBox3.Image = nextFrame.Copy(face.rect);
						}
					}


					if (checkBoxFaceValue)
					{
						foreach (var face in faces)
						{
							//nextFrame.Draw(face, new Bgr(Color.AliceBlue), thickness);
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



					imageBox1.Image = nextFrame;

				}
			}
		}
		private void Form1_Load(object sender, EventArgs e)
		{
			// passing 0 gets zeroth webcam

			cap = new Capture(5);
			// adjust path to find your xml

			//haarFace = new CascadeClassifier("C:\\OpenCV\\OpenCV\\data\\haarcascades\\haarcascade_frontalface_alt.xml");
			haarFace = new HaarCascade("C:\\OpenCV\\OpenCV\\data\\haarcascades\\haarcascade_frontalface_alt.xml");
			haarMouth = new HaarCascade("C:\\OpenCV\\OpenCV\\data\\haarcascades\\haarcascade_mcs_mouth.xml");
			haarNose = new HaarCascade("C:\\OpenCV\\OpenCV\\data\\haarcascades\\haarcascade_mcs_nose.xml");
			haarLeftEar = new HaarCascade("C:\\OpenCV\\OpenCV\\data\\haarcascades\\haarcascade_mcs_leftear.xml");
			haarRightEar = new HaarCascade("C:\\OpenCV\\OpenCV\\data\\haarcascades\\haarcascade_mcs_rightear.xml");
			haarLeftEye = new HaarCascade("C:\\OpenCV\\OpenCV\\data\\haarcascades\\haarcascade_lefteye_2splits.xml");
			haarRightEye = new HaarCascade("C:\\OpenCV\\OpenCV\\data\\haarcascades\\haarcascade_righteye_2splits.xml");
			haarUpperBody = new HaarCascade("C:\\OpenCV\\OpenCV\\data\\haarcascades\\haarcascade_upperbody.xml");
			//	 "..\\..\\..\\..\\lib\\haarcascade_frontalface_alt2.xml");
		}
	}
}
