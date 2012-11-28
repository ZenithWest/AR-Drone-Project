using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using Emgu.CV.UI;
using Emgu.CV.VideoSurveillance;
using System.Threading.Tasks;
using OpenCV_Testing;


namespace Detection
{
	class Camshift
	{
		public Point currentCentroid;
		public Image<Gray, Double> Result;
		public Image<Bgr, Byte> Target;
		public Image<Bgr, Byte> currentImage;
		public Point[] Max_Loc;
		public Double[] Max_Size;
		public Point[] Min_Loc;
		public Double[] Min_Size;
		public Rectangle search;

		private Image<Hsv, Byte> hsv;
		private Image<Hsv, Byte> hue;
		private Image<Gray, Byte> mask;
		private Image<Hsv, Byte> hist;
		private Image<Hsv, Byte> backproject;
		private Capture capture;
		private CancellationTokenSource cts;
		private Rectangle nextSearch;


		public Camshift()
		{
			capture = new Capture(0);
			Task.Factory.StartNew(() => initializeThread());
		}

		private void initializeThread()
		{
			cts = new CancellationTokenSource();
			currentImage = capture.QueryFrame();

			SetTarget();

			mainThread();
		}

		public void SetTarget()
		{
			currentCentroid = new Point(currentImage.Width / 2, currentImage.Height / 2);
			Target = currentImage.Copy(new Rectangle(currentCentroid, new Size(100, 100)));
		}

		private void mainThread()
		{
			while (true)
			{
				currentImage = capture.QueryFrame();
				Detect_object(currentImage.Convert<Gray, Byte>(), Target.Convert<Gray, Byte>());
			}
		}

		private void camshiftRun(Image<Gray, Byte> capture, Image<Gray, Byte> target)
		{
			capture._EqualizeHist();
			hsv = new Image<Hsv, byte>(capture.Width, capture.Height);
			hsv = capture.Convert<Hsv, Byte>();
			hsv._EqualizeHist();

			hue = new Image<Hsv, Byte>(capture.Width, capture.Height);
			mask = new Image<Gray, Byte>(capture.Width, capture.Height);
			backproject = new Image<Hsv, Byte>(capture.Width, capture.Height);

			Emgu.CV.CvInvoke.cvCalcBackProject(new IntPtr[1] { hsv }, backproject, hist);

			search = nextSearch;
			MCvConnectedComp comp;
			MCvBox2D box;
			int results = Emgu.CV.CvInvoke.cvCamShift((IntPtr) backproject, search, new MCvTermCriteria(25, 1.5d), out comp, out box);
		}

		// FFT Method
		private bool Detect_object(Image<Gray, Byte> Area_Image, Image<Gray, Byte> image_object)
		{
			bool success = false;

			//Work out padding array size
			Point dftSize = new Point(Area_Image.Width + (image_object.Width * 2), Area_Image.Height + (image_object.Height * 2));
			//Pad the Array with zeros
			using (Image<Gray, Byte> pad_array = new Image<Gray, Byte>(dftSize.X, dftSize.Y))
			{
				//copy centre
				pad_array.ROI = new Rectangle(image_object.Width, image_object.Height, Area_Image.Width, Area_Image.Height);
				CvInvoke.cvCopy(Area_Image, pad_array, IntPtr.Zero);

				pad_array.ROI = (new Rectangle(0, 0, dftSize.X, dftSize.Y));

				//Match Template
				using (Image<Gray, float> result_Matrix = pad_array.MatchTemplate(image_object, TM_TYPE.CV_TM_CCOEFF_NORMED))
				{
					// Point[] MAX_Loc, Min_Loc;
					// double[] min, max;
					//Limit ROI to look for Match

					result_Matrix.ROI = new Rectangle(image_object.Width, image_object.Height, Area_Image.Width - image_object.Width, Area_Image.Height - image_object.Height);

					result_Matrix.MinMax(out Min_Size, out Max_Size, out Min_Loc, out Max_Loc);

					currentCentroid = new Point((Max_Loc[0].X), (Max_Loc[0].Y));
					success = true;
					Result = result_Matrix.Convert<Gray, Double>();

				}
			}
			return success;
		}

	}
}
