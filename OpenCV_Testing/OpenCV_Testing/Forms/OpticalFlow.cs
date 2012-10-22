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

namespace OpenCV_Testing
{
	public partial class OpticalFlow : Form
	{
		public OpticalFlow()
		{
			InitializeComponent();
			System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.High;
		}

		private Capture _capture;
		private HaarCascade _faces;
		private MCvAvgComp[][] faceDetected;

		public Image<Bgr, Byte> frame { get; set; }
		public Image<Gray, Byte> grayFrame { get; set; }
		public Image<Bgr, Byte> nextFrame { get; set; }
		public Image<Gray, Byte> nextGrayFrame { get; set; }
		public Image<Bgr, Byte> opticalFlowFrame { get; set; }
		public Image<Gray, Byte> opticalFlowGrayFrame { get; set; }
		public Image<Bgr, Byte> faceImage { get; set; }
		public Image<Bgr, Byte> faceNextImage { get; set; }
		public Image<Gray, Byte> faceGrayImage { get; set; }
		public Image<Gray, Byte> faceNextGrayImage { get; set; }
		public Image<Gray, Single> velx { get; set; }
		public Image<Gray, Single> vely { get; set; }
		public PointF[][] vectorField { get; set; }
		public int vectorFieldX { get; set; }
		public int vectorFieldY { get; set; }
		public Image<Gray, Byte> flow { get; set; }

		public PointF[][] ActualFeature;
		public PointF[] NextFeature;
		public Byte[] Status;
		public float[] TrackError;

		public Rectangle trackingArea;
		public PointF[] hull, nextHull;
		public PointF referenceCentroid, nextCentroid;
		public float sumVectorFieldX;
		public float sumVectorFieldY;
		public bool dense = false;
		public Random rand;

		private void buttonInitializeTracking_Click(object sender, EventArgs e)
		{
			rand = new Random();
			//_capture = new Capture("LucaHead.wmv");
			//_capture = new Capture(5);
			if (!dense)
			{
				_capture = new Capture(5);
				Thread.Sleep(1000);
			}
				else
				_capture = new Capture("C:\\Users\\Zenith\\SkyDrive\\2012 FALL\\CSCE 483 Computer System Design\\ARDroneOut.avi");

			InitializeFaceTracking();
			Application.Idle += new EventHandler(Application_Idle);
		}

		private void InitializeFaceTracking()
		{
			//_faces = new HaarCascade("../../haarcascade_frontalface_alt_tree.xml");
			_faces = new HaarCascade("C:\\OpenCV\\OpenCV\\data\\haarcascades\\haarcascade_frontalface_alt.xml");
			frame = _capture.QueryFrame();
			//We convert it to grayscale
			grayFrame = frame.Convert<Gray, Byte>();
			if (dense)
			{
				trackingArea = new Rectangle(0, 0, frame.Width, frame.Height);
				faceDetected = grayFrame.DetectHaarCascade(_faces, 1.1, 1, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(20, 20));
				faceGrayImage = new Image<Gray, Byte>(trackingArea.Width, trackingArea.Height);
			}
			else
			{
				// We detect a face using haar cascade classifiers, we'll work only on face area
				faceDetected = grayFrame.DetectHaarCascade(_faces, 1.1, 1, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(20, 20));
				if (faceDetected[0].Length == 1)
				{
					trackingArea = new Rectangle(faceDetected[0][0].rect.X, faceDetected[0][0].rect.Y, faceDetected[0][0].rect.Width, faceDetected[0][0].rect.Height);

					// Here we enlarge or restrict the search features area on a smaller or larger face area
					float scalingAreaFactor = 1.6f;
					int trackingAreaWidth = (int)(faceDetected[0][0].rect.Width * scalingAreaFactor);
					int trackingAreaHeight = (int)(faceDetected[0][0].rect.Height * scalingAreaFactor);
					int leftXTrackingCoord = faceDetected[0][0].rect.X - (int)(((faceDetected[0][0].rect.X + trackingAreaWidth) - (faceDetected[0][0].rect.X + faceDetected[0][0].rect.Width)) / 2);
					int leftYTrackingCoord = faceDetected[0][0].rect.Y - (int)(((faceDetected[0][0].rect.Y + trackingAreaHeight) - (faceDetected[0][0].rect.Y + faceDetected[0][0].rect.Height)) / 2);
					trackingArea = new Rectangle(leftXTrackingCoord, leftYTrackingCoord, trackingAreaWidth, trackingAreaHeight);

					// Allocating proper working images
					faceImage = new Image<Bgr, Byte>(trackingArea.Width, trackingArea.Height);
					faceGrayImage = new Image<Gray, Byte>(trackingArea.Width, trackingArea.Height);
					frame.ROI = trackingArea;
					frame.Copy(faceImage, null);
					frame.ROI = Rectangle.Empty;
					faceGrayImage = faceImage.Convert<Gray, Byte>();

					// Detecting good features that will be tracked in following frames
					ActualFeature = faceGrayImage.GoodFeaturesToTrack(400, 0.5d, 5d, 5);
					faceGrayImage.FindCornerSubPix(ActualFeature, new Size(5, 5), new Size(-1, -1), new MCvTermCriteria(25, 1.5d));

					// Features computed on a different coordinate system are shifted to their original location
					for (int i = 0; i < ActualFeature[0].Length; i++)
					{
						ActualFeature[0][i].X += trackingArea.X;
						ActualFeature[0][i].Y += trackingArea.Y;
					}

					// Computing convex hull                
					using (MemStorage storage = new MemStorage())
						hull = PointCollection.ConvexHull(ActualFeature[0], storage, Emgu.CV.CvEnum.ORIENTATION.CV_CLOCKWISE).ToArray();

					referenceCentroid = FindCentroid(hull);
				}
			}
		}

		void Application_Idle(object sender, EventArgs e)
		{
			nextFrame = _capture.QueryFrame();
			faceNextGrayImage = new Image<Gray, byte>(trackingArea.Width, trackingArea.Height);
			if (dense)
			{
				if (nextFrame != null)
				{
					nextGrayFrame = nextFrame.Convert<Gray, Byte>();

					nextGrayFrame.ROI = trackingArea;
					nextGrayFrame.Copy(faceNextGrayImage, null);
					nextGrayFrame.ROI = Rectangle.Empty;

					opticalFlowFrame = new Image<Bgr, Byte>(frame.Width, frame.Height);
					opticalFlowGrayFrame = new Image<Gray, Byte>(frame.Width, frame.Height);
					//opticalFlowFrame = nextFrame.Copy();

					ComputeDenseOpticalFlow();
					ComputeMotionFromDenseOpticalFlow();


					//opticalFlowFrame.Draw(new CircleF(referenceCentroid, 1.0f), new Bgr(Color.Goldenrod), 2);
					//opticalFlowFrame.Draw(new CircleF(nextCentroid, 1.0f), new Bgr(Color.Red), 2);

					//imageBoxOpticalFlow.Image = opticalFlowFrame.Flip(FLIP.HORIZONTAL);
					imageBoxOpticalFlow.Image = nextFrame.Copy().Flip(FLIP.HORIZONTAL);
					imageBoxOpticalFlow2.Image = opticalFlowFrame.Flip(FLIP.HORIZONTAL);

					//Updating actual frames and features with the new ones
					frame = nextFrame;
					grayFrame = nextGrayFrame;
					faceGrayImage = faceNextGrayImage;
					faceImage = faceNextImage;
				}
			}
			else
			{
				if (nextFrame != null && faceDetected[0].Length == 1)
				{
					nextGrayFrame = nextFrame.Convert<Gray, Byte>();

					opticalFlowFrame = new Image<Bgr, Byte>(frame.Width, frame.Height);
					opticalFlowGrayFrame = new Image<Gray, Byte>(frame.Width, frame.Height);
					opticalFlowFrame = nextFrame.Copy();
					ComputeSparseOpticalFlow();
					ComputeMotionFromSparseOpticalFlow();

					opticalFlowFrame.Draw(new CircleF(referenceCentroid, 1.0f), new Bgr(Color.Goldenrod), 2);
					opticalFlowFrame.Draw(new CircleF(nextCentroid, 1.0f), new Bgr(Color.Red), 2);

					imageBoxOpticalFlow.Image = opticalFlowFrame.Flip(FLIP.HORIZONTAL);

					//imageBoxOpticalFlow.Image = opticalFlowFrame;
					if (ActualFeature[0] != null)
						label1.Text = ActualFeature[0].Length.ToString();
					else
						label1.Text = ActualFeature[0].Length.ToString() + ".";

					//Updating actual frames and features with the new ones
					frame = nextFrame;
					grayFrame = nextGrayFrame;
					faceGrayImage = faceNextGrayImage;
					faceImage = faceNextImage;
					ActualFeature[0] = NextFeature;
				}
			}


		}

		void ComputeDenseOpticalFlow()
		{
			// Compute dense optical flow using Horn and Schunk algo
			velx = new Image<Gray, float>(faceGrayImage.Size);
			vely = new Image<Gray, float>(faceNextGrayImage.Size);
			Emgu.CV.OpticalFlow.HS(faceGrayImage, faceNextGrayImage, true, velx, vely, 0.1d, new MCvTermCriteria(100));

			#region Dense Optical Flow Drawing
			int winSizeX = 2;
			int winSizeY = 2;
			vectorFieldX = (int)Math.Round((double)faceGrayImage.Width / winSizeX);
			vectorFieldY = (int)Math.Round((double)faceGrayImage.Height / winSizeY);
			sumVectorFieldX = 0f;
			sumVectorFieldY = 0f;
			vectorField = new PointF[vectorFieldX][];
			for (int i = 0; i < vectorFieldX; i++)
			{
				vectorField[i] = new PointF[vectorFieldY];
				for (int j = 0; j < vectorFieldY; j++)
				{
					Gray velx_gray = velx[j * winSizeX, i * winSizeX];
					float velx_float = (float)velx_gray.Intensity;
					Gray vely_gray = vely[j * winSizeY, i * winSizeY];
					float vely_float = (float)vely_gray.Intensity;
					sumVectorFieldX += velx_float;
					sumVectorFieldY += vely_float;
					vectorField[i][j] = new PointF(velx_float, vely_float);

					Cross2DF cr = new Cross2DF(
						 new PointF((i * winSizeX) + trackingArea.X,
										(j * winSizeY) + trackingArea.Y),
										1, 1);
					//opticalFlowFrame.Draw(cr, new Bgr(Color.Red), 1);

					LineSegment2D ci = new LineSegment2D(
						 new Point((i * winSizeX) + trackingArea.X,
									  (j * winSizeY) + trackingArea.Y),
						 new Point((int)((i * winSizeX) + trackingArea.X + velx_float),
									  (int)((j * winSizeY) + trackingArea.Y + vely_float)));
					//opticalFlowFrame.Draw(ci, new Bgr(Color.Yellow), 1);
					
					opticalFlowFrame.Draw(ci, new Bgr(rand.Next(256), rand.Next(256), rand.Next(256)), 1);
					//double value = ci.Length * 10.0;
					//if (value > 255) value = 0.0;
					//opticalFlowFrame.Draw(ci, new Bgr(value, value, value), 1);
					/*if (ci.Length > 0)
					{
						double X = ci.Direction.X;
						double Y = ci.Direction.Y;
						if (X > 0 && Y > 0)
							opticalFlowFrame.Draw(ci, new Bgr(255, 255, 0), 1);
						else if (X < 0 && Y > 0)
							opticalFlowFrame.Draw(ci, new Bgr(0, 255, 255), 1);
						else if (X > 0 && Y < 0)
							opticalFlowFrame.Draw(ci, new Bgr(255, 0, 255), 1);
						else
							opticalFlowFrame.Draw(ci, new Bgr(255, 255, 255), 1);
					}
					else
					{
						opticalFlowFrame.Draw(ci, new Bgr(0, 0, 0), 1);
					}*/
					//
				}
			}
			#endregion
		}

		private void ComputeMotionFromDenseOpticalFlow()
		{
			// To be implemented
		}

		private void ComputeSparseOpticalFlow()
		{
			// Compute optical flow using pyramidal Lukas Kanade Method                
			Emgu.CV.OpticalFlow.PyrLK(grayFrame, nextGrayFrame, ActualFeature[0], new System.Drawing.Size(10, 10), 3, new MCvTermCriteria(20, 0.03d), out NextFeature, out Status, out TrackError);

			using (MemStorage storage = new MemStorage())
				nextHull = PointCollection.ConvexHull(ActualFeature[0], storage, Emgu.CV.CvEnum.ORIENTATION.CV_CLOCKWISE).ToArray();
			nextCentroid = FindCentroid(nextHull);
			for (int i = 0; i < ActualFeature[0].Length; i++)
			{
				DrawTrackedFeatures(i);
				//Uncomment this to draw optical flow vectors
				DrawFlowVectors(i);                
			}
		}

		private void ComputeMotionFromSparseOpticalFlow()
		{
			float xCentroidsDifference = referenceCentroid.X - nextCentroid.X;
			float yCentroidsDifference = referenceCentroid.Y - nextCentroid.Y;

			float threshold = trackingArea.Width / 5;
			label4.Text = "center";
			if (Math.Abs(xCentroidsDifference) > Math.Abs(yCentroidsDifference))
			{
				if (xCentroidsDifference > threshold)
					label4.Text = "right";
				if (xCentroidsDifference < -threshold)
					label4.Text = "left";
			}
			if (Math.Abs(xCentroidsDifference) < Math.Abs(yCentroidsDifference))
			{
				if (yCentroidsDifference > threshold)
					label4.Text = "up";
				if (yCentroidsDifference < -threshold)
					label4.Text = "down";
			}
		}

		//Code adapted and improved from: http://blog.csharphelper.com/2010/01/04/find-a-polygons-centroid-in-c.aspx
		// refer to wikipedia for math formulas centroid of polygon http://en.wikipedia.org/wiki/Centroid        
		private PointF FindCentroid(PointF[] Hull)
		{
			// Add the first point at the end of the array.
			int num_points = Hull.Length;
			PointF[] pts = new PointF[num_points + 1];
			Hull.CopyTo(pts, 0);
			pts[num_points] = Hull[0];

			// Find the centroid.
			float X = 0;
			float Y = 0;
			float second_factor;
			for (int i = 0; i < num_points; i++)
			{
				second_factor = pts[i].X * pts[i + 1].Y - pts[i + 1].X * pts[i].Y;
				X += (pts[i].X + pts[i + 1].X) * second_factor;
				Y += (pts[i].Y + pts[i + 1].Y) * second_factor;
			}
			// Divide by 6 times the polygon's area.
			float polygon_area = Math.Abs(SignedPolygonArea(Hull));
			X /= (6 * polygon_area);
			Y /= (6 * polygon_area);

			// If the values are negative, the polygon is
			// oriented counterclockwise so reverse the signs.
			if (X < 0)
			{
				X = -X;
				Y = -Y;
			}
			return new PointF(X, Y);
		}

		private float SignedPolygonArea(PointF[] Hull)
		{
			int num_points = Hull.Length;
			// Get the areas.
			float area = 0;
			for (int i = 0; i < num_points; i++)
			{
				area +=
					 (Hull[(i + 1) % num_points].X - Hull[i].X) *
					 (Hull[(i + 1) % num_points].Y + Hull[i].Y) / 2;
			}
			// Return the result.
			return area;
		}

		private void DrawTrackedFeatures(int i)
		{
			opticalFlowFrame.Draw(new CircleF(new PointF(ActualFeature[0][i].X, ActualFeature[0][i].Y), 1f), new Bgr(Color.Blue), 1);
		}

		private void DrawFlowVectors(int i)
		{
			System.Drawing.Point p = new Point();
			System.Drawing.Point q = new Point();

			p.X = (int)ActualFeature[0][i].X;
			p.Y = (int)ActualFeature[0][i].Y;
			q.X = (int)NextFeature[i].X;
			q.Y = (int)NextFeature[i].Y;

			double angle;
			angle = Math.Atan2((double)p.Y - q.Y, (double)p.X - q.X);

			LineSegment2D line = new LineSegment2D(p, q);
			opticalFlowFrame.Draw(line, new Bgr(255, 0, 0), 1);

			p.X = (int)(q.X + 6 * Math.Cos(angle + Math.PI / 4));
			p.Y = (int)(q.Y + 6 * Math.Sin(angle + Math.PI / 4));
			opticalFlowFrame.Draw(new LineSegment2D(p, q), new Bgr(255, 0, 0), 1);
			p.X = (int)(q.X + 6 * Math.Cos(angle - Math.PI / 4));
			p.Y = (int)(q.Y + 6 * Math.Sin(angle - Math.PI / 4));
			opticalFlowFrame.Draw(new LineSegment2D(p, q), new Bgr(255, 0, 0), 1);
		}

	}
}
