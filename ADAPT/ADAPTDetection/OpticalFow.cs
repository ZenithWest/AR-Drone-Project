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


#region EmguCV's OpticalFlow Class


/**************************************************************************************************
 * OpticalFlow.BM Method 
 **************************************************************************************************
 * Calculates optical flow for overlapped blocks (block_size.width * block_size.height) pixels each,
 * thus the velocity fields are smaller than the original images.
 * For every block in prev the functions tries to find a similar block in curr in some neighborhood
 * of the original block or shifted by (velx(x0,y0),vely(x0,y0)) block as has been calculated
 * by previous function call (if use_previous) 
 **************************************************************************************************
 * 
 *	public static void BM(
 *		Image<Gray, byte> prev,
 *		Image<Gray, byte> curr,
 *		Size blockSize,
 *		Size shiftSize,
 *		Size maxRange,
 *		bool usePrevious,
 *		Image<Gray, float> velx,
 *		Image<Gray, float> vely
 *	)
 *
 * prev
 * Type: Emgu.CV..::..Image<(Of <(<'Gray, Byte>)>)>
 * First image
 * 
 * curr
 * Type: Emgu.CV..::..Image<(Of <(<'Gray, Byte>)>)>
 * Second image
 * 
 * blockSize
 * Type: System.Drawing..::..Size
 * Size of basic blocks that are compared.
 * 
 * shiftSize
 * Type: System.Drawing..::..Size
 * Block coordinate increments. 
 * 
 * maxRange
 * Type: System.Drawing..::..Size
 * Size of the scanned neighborhood in pixels around block.
 * 
 * usePrevious
 * Type: System..::..Boolean
 * Uses previous (input) velocity field. 
 * 
 * velx
 * Type: Emgu.CV..::..Image<(Of <(<'Gray, Single>)>)>
 * Horizontal component of the optical flow of floor((prev->width - block_size.width)/shiftSize.width) 
 * x floor((prev->height - block_size.height)/shiftSize.height) size. 
 * 
 * vely
 * Type: Emgu.CV..::..Image<(Of <(<'Gray, Single>)>)>
 * Vertical component of the optical flow of the same size velx.
 ****************************************************************************************************/
/*
OpticalFlow.Farneback Method
public static void Farneback(
	Image<Gray, byte> prev0,
	Image<Gray, byte> next0,
	Image<Gray, float> flowX,
	Image<Gray, float> flowY,
	double pyrScale,
	int levels,
	int winSize,
	int iterations,
	int polyN,
	double polySigma,
	OPTICALFLOW_FARNEBACK_FLAG flags
)

prev0
Type: Emgu.CV..::..Image<(Of <(<'Gray, Byte>)>)>
The first 8-bit single-channel input image
next0
Type: Emgu.CV..::..Image<(Of <(<'Gray, Byte>)>)>
The second input image of the same size and the same type as prevImg
flowX
Type: Emgu.CV..::..Image<(Of <(<'Gray, Single>)>)>
The computed flow image for x-velocity; will have the same size as prevImg
flowY
Type: Emgu.CV..::..Image<(Of <(<'Gray, Single>)>)>
The computed flow image for y-velocity; will have the same size as prevImg
pyrScale
Type: System..::..Double
Specifies the image scale (!1) to build the pyramids for each image. pyrScale=0.5 means the classical pyramid, where each next layer is twice smaller than the previous
levels
Type: System..::..Int32
The number of pyramid layers, including the initial image. levels=1 means that no extra layers are created and only the original images are used
winSize
Type: System..::..Int32
The averaging window size; The larger values increase the algorithm robustness to image noise and give more chances for fast motion detection, but yield more blurred motion field
iterations
Type: System..::..Int32
The number of iterations the algorithm does at each pyramid level
polyN
Type: System..::..Int32
Size of the pixel neighborhood used to find polynomial expansion in each pixel. The larger values mean that the image will be approximated with smoother surfaces, yielding more robust algorithm and more blurred motion field. Typically, poly n=5 or 7
polySigma
Type: System..::..Double
Standard deviation of the Gaussian that is used to smooth derivatives that are used as a basis for the polynomial expansion. For poly n=5 you can set poly sigma=1.1, for poly n=7 a good value would be poly sigma=1.5
flags
Type: Emgu.CV.CvEnum..::..OPTICALFLOW_FARNEBACK_FLAG
The operation flags
*/

/*
 * OpticalFlow..::..HS Method
 * Computes flow for every pixel of the first input image using Horn & Schunck algorithm
 * public static void HS(
	Image<Gray, byte> prev,
	Image<Gray, byte> curr,
	bool usePrevious,
	Image<Gray, float> velx,
	Image<Gray, float> vely,
	double lambda,
	MCvTermCriteria criteria
)
prev
Type: Emgu.CV..::..Image<(Of <(<'Gray, Byte>)>)>
First image, 8-bit, single-channel
curr
Type: Emgu.CV..::..Image<(Of <(<'Gray, Byte>)>)>
Second image, 8-bit, single-channel
usePrevious
Type: System..::..Boolean
Uses previous (input) velocity field
velx
Type: Emgu.CV..::..Image<(Of <(<'Gray, Single>)>)>
Horizontal component of the optical flow of the same size as input images, 32-bit floating-point, single-channel
vely
Type: Emgu.CV..::..Image<(Of <(<'Gray, Single>)>)>
Vertical component of the optical flow of the same size as input images, 32-bit floating-point, single-channel
lambda
Type: System..::..Double
Lagrangian multiplier
criteria
Type: Emgu.CV.Structure..::..MCvTermCriteria
Criteria of termination of velocity computing
 * */

/*
 * OpticalFlow..::..LK Method 
 * public static void LK(
	Image<Gray, byte> prev,
	Image<Gray, byte> curr,
	Size winSize,
	Image<Gray, float> velx,
	Image<Gray, float> vely
)
prev
Type: Emgu.CV..::..Image<(Of <(<'Gray, Byte>)>)>
First image
curr
Type: Emgu.CV..::..Image<(Of <(<'Gray, Byte>)>)>
Second image
winSize
Type: System.Drawing..::..Size
Size of the averaging window used for grouping pixels
velx
Type: Emgu.CV..::..Image<(Of <(<'Gray, Single>)>)>
Horizontal component of the optical flow of the same size as input images
vely
Type: Emgu.CV..::..Image<(Of <(<'Gray, Single>)>)>
Vertical component of the optical flow of the same size as input images
 * */

/*
 * OpticalFlow..::..PyrLK 
 * Calculates optical flow for a sparse feature set using iterative Lucas-Kanade method in pyramids 
 * public static void PyrLK(
	Image<Gray, byte> prev,
	Image<Gray, byte> curr,
	PointF[] prevFeatures,
	Size winSize,
	int level,
	MCvTermCriteria criteria,
	out PointF[] currFeatures,
	out byte[] status,
	out float[] trackError
)
prev
Type: Emgu.CV..::..Image<(Of <(<'Gray, Byte>)>)>
First frame, at time t
curr
Type: Emgu.CV..::..Image<(Of <(<'Gray, Byte>)>)>
Second frame, at time t + dt 
prevFeatures
Type: array<System.Drawing..::..PointF>[]()[][]
Array of points for which the flow needs to be found
winSize
Type: System.Drawing..::..Size
Size of the search window of each pyramid level
level
Type: System..::..Int32
Maximal pyramid level number. If 0 , pyramids are not used (single level), if 1 , two levels are used, etc
criteria
Type: Emgu.CV.Structure..::..MCvTermCriteria
Specifies when the iteration process of finding the flow for each point on each pyramid level should be stopped
currFeatures
Type: array<System.Drawing..::..PointF>[]()[][]%
Array of 2D points containing calculated new positions of input features in the second image
status
Type: array<System..::..Byte>[]()[][]%
Array. Every element of the array is set to 1 if the flow for the corresponding feature has been found, 0 otherwise
trackError
Type: array<System..::..Single>[]()[][]%
Array of double numbers containing difference between patches around the original and moved points
 * */

/*
 * OpticalFlow..::..PyrLK 
 * Calculates optical flow for a sparse feature set using iterative Lucas-Kanade method in pyramids 
 * public static void PyrLK(
	Image<Gray, byte> prev,
	Image<Gray, byte> curr,
	Image<Gray, byte> prevPyrBuffer,
	Image<Gray, byte> currPyrBuffer,
	PointF[] prevFeatures,
	Size winSize,
	int level,
	MCvTermCriteria criteria,
	LKFLOW_TYPE flags,
	out PointF[] currFeatures,
	out byte[] status,
	out float[] trackError
)

prev
Type: Emgu.CV..::..Image<(Of <(<'Gray, Byte>)>)>
First frame, at time t
curr
Type: Emgu.CV..::..Image<(Of <(<'Gray, Byte>)>)>
Second frame, at time t + dt 
prevPyrBuffer
Type: Emgu.CV..::..Image<(Of <(<'Gray, Byte>)>)>
Buffer for the pyramid for the first frame. If it is not NULL, the buffer must have a sufficient size to store the pyramid from level 1 to level #level ; the total size of (image_width+8)*image_height/3 bytes is sufficient
currPyrBuffer
Type: Emgu.CV..::..Image<(Of <(<'Gray, Byte>)>)>
Similar to prev_pyr, used for the second frame
prevFeatures
Type: array<System.Drawing..::..PointF>[]()[][]
Array of points for which the flow needs to be found
winSize
Type: System.Drawing..::..Size
Size of the search window of each pyramid level
level
Type: System..::..Int32
Maximal pyramid level number. If 0 , pyramids are not used (single level), if 1 , two levels are used, etc
criteria
Type: Emgu.CV.Structure..::..MCvTermCriteria
Specifies when the iteration process of finding the flow for each point on each pyramid level should be stopped
flags
Type: Emgu.CV.CvEnum..::..LKFLOW_TYPE
Flags
currFeatures
Type: array<System.Drawing..::..PointF>[]()[][]%
Array of 2D points containing calculated new positions of input features in the second image
status
Type: array<System..::..Byte>[]()[][]%
Array. Every element of the array is set to 1 if the flow for the corresponding feature has been found, 0 otherwise
trackError
Type: array<System..::..Single>[]()[][]%
Array of double numbers containing difference between patches around the original and moved points
 * */

#endregion


namespace ADAPTDetection
{



	class OpticalFlow
	{

		public Image<Bgr, Byte> _prevFrame { get; set; }
		public Image<Gray, Byte> _prevGrayFrame { get; set; }
		public Image<Bgr, Byte> _currentFrame { get; set; }
		public Image<Gray, Byte> _currentGrayFrame { get; set; }
		public Image<Bgr, Byte> _opticalFlowFrame { get; set; }
		public Image<Bgr, Byte> _prevOpticalFlowFrame { get; set; }
		public Image<Bgr, Byte> _prevTrackedImage { get; set; }
		public Image<Gray, Byte> _prevTrackedGrayImage { get; set; }
		public Image<Bgr, Byte> _currentTrackedImage { get; set; }
		public Image<Gray, Byte> _currentTrackedGrayImage { get; set; }

		private Capture _capture;
		private HaarCascade _faces;
		private MCvAvgComp[][] faceDetected;
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

		public Rectangle _trackingArea;
		public PointF[] hull, nextHull;
		public PointF referenceCentroid, currentCentroid;
		public PointF differenceCentroid;
		public float sumVectorFieldX;
		public float sumVectorFieldY;
		public Random rand;

		public bool reinitialized;
		public Rectangle _initialTrackingArea;
		public PointF[][] InitialFeature;
		public PointF initialCentroid;

		public Task detectFaceTask = null;
		CancellationTokenSource cts;
		CancellationToken cancelToken;
		bool _stop = false;
		bool detected;
		public PointF NonPrunnedCentroid;

		public bool DRAW = true;

		public OpticalFlow()
		{
			_faces = new HaarCascade("C:\\OpenCV\\OpenCV\\data\\haarcascades\\haarcascade_frontalface_alt.xml");
			rand = new Random();
			_capture = new Capture(5);
			//_capture = new Capture("C:\\Users\\Zenith\\SkyDrive\\2012 FALL\\CSCE 483 Computer System Design\\ARDroneOut.avi");
			Task.Factory.StartNew(() => initializeThread());
		}

		#region Threads
		void initializeThread()
		{
			cts = new CancellationTokenSource();
			detected = false;
			ActualFeature = new PointF[1][];
			_currentFrame = _capture.QueryFrame();
			currentCentroid = new PointF(_currentFrame.Width / 2, _currentFrame.Height / 2);
			while (_currentFrame == null)
			{
				_currentFrame = _capture.QueryFrame();
			}
			_currentGrayFrame = _currentFrame.Convert<Gray, Byte>();
			addFrame(_currentFrame);

			calculateFeaturesToTrack(_currentFrame, new Rectangle(new Point(_currentFrame.Width / 2 - 40, _currentFrame.Height / 2 - 40), new Size(40, 40)));
			copyInitial();

			detectFaceTask = Task.Factory.StartNew(() => detectFace(_currentFrame));
			detectFaceTask.Wait();

			if (faceDetected[0].Length == 1)
			{
				calculateFeaturesToTrack(_currentFrame, new Rectangle(faceDetected[0][0].rect.X, faceDetected[0][0].rect.Y, faceDetected[0][0].rect.Width, faceDetected[0][0].rect.Height));
			}
			if (reinitialized)
			{
				copyInitial();
			}


			reinitialized = false;
			Task.Factory.StartNew(() => mainThread());
		}
		public bool test = true;
		public long timeA = DateTime.Now.Ticks;
		void mainThread()
		{
			timeA = DateTime.Now.Ticks;
			while (!_stop)
			{
				addFrame(_capture.QueryFrame());

				if (NextFeature != null && NextFeature.Length < 10)
				{
					calculateFeaturesToTrack(_currentFrame, new Rectangle(new Point(_currentFrame.Width / 2 - 100, _currentFrame.Height / 2 - 100), new Size(200, 200)), 1.0f, 4);
					copyInitial();
				}

				if (test)
				{
					if (detectFaceTask.IsCompleted)
					{
						if (faceDetected[0].Length == 1)
						{
							if (TimeSpan.FromTicks(DateTime.Now.Ticks - timeA).TotalMilliseconds < 500)
							{
								calculateFeaturesToTrack(_currentFrame, new Rectangle(faceDetected[0][0].rect.X, faceDetected[0][0].rect.Y, faceDetected[0][0].rect.Width, faceDetected[0][0].rect.Height));
								if (reinitialized)
									detected = true;
							}
						}
						timeA = DateTime.Now.Ticks;
						detectFaceTask = Task.Factory.StartNew(() => detectFace(_currentFrame));
					}
					try
					{
						if (detectFaceTask.Wait(30))
						{
							if (faceDetected[0].Length == 1)
							{
								if (TimeSpan.FromTicks(DateTime.Now.Ticks - timeA).TotalMilliseconds < 500)
								{
									calculateFeaturesToTrack(_currentFrame, new Rectangle(faceDetected[0][0].rect.X, faceDetected[0][0].rect.Y, faceDetected[0][0].rect.Width, faceDetected[0][0].rect.Height));
									if (reinitialized)
										detected = true;
								}
							}
							timeA = DateTime.Now.Ticks;
						}
					} catch (Exception)
					{

					}
				}

				ComputeFlow();
			}
		}

		public void stop()
		{
			_stop = true;
		}
		#endregion

		#region Image Processing
		public void addFrame(Image<Bgr, Byte> frame)
		{
			_prevFrame = _currentFrame.Copy();
			_prevGrayFrame = _currentGrayFrame.Copy();
			_currentFrame = frame.Copy();
			_currentGrayFrame = frame.Convert<Gray, Byte>();
			_prevOpticalFlowFrame = _opticalFlowFrame;
			_opticalFlowFrame = frame.Copy();
		}

		public void createTrackedImageFromCurrentFrame(Rectangle trackingArea)
		{
			_prevTrackedImage = _currentTrackedImage;
			_prevTrackedGrayImage = _currentTrackedGrayImage;
			_currentTrackedImage = _currentFrame.Copy(roi: trackingArea);
			_currentTrackedGrayImage = _currentTrackedImage.Convert<Gray, Byte>();
		}

		public Image<Gray, Byte> copyTrackedImage(Image<Bgr, Byte> frame, Rectangle trackingArea)
		{
			return frame.Copy(roi: trackingArea).Convert<Gray, Byte>();
		}

		void copyInitial()
		{
			_trackingArea = _initialTrackingArea;
			ActualFeature[0] = new PointF[InitialFeature[0].Length];
			Array.Copy(InitialFeature[0], ActualFeature[0], InitialFeature[0].Length);
			referenceCentroid = initialCentroid;
			createTrackedImageFromCurrentFrame(_trackingArea);
		}

		Rectangle scaleTrackingArea(Rectangle trackingArea, float scalingAreaFactor = 0.6f)
		{
			int trackingAreaWidth = (int)(trackingArea.Width * scalingAreaFactor);
			int trackingAreaHeight = (int)(trackingArea.Height * scalingAreaFactor);
			int leftXTrackingCoord = trackingArea.X - (int)(((trackingArea.X + trackingAreaWidth) - (trackingArea.X + trackingArea.Width)) / 2);
			int leftYTrackingCoord = trackingArea.Y - (int)(((trackingArea.Y + trackingAreaHeight) - (trackingArea.Y + trackingArea.Height)) / 2);
			return new Rectangle(leftXTrackingCoord, leftYTrackingCoord, trackingAreaWidth, trackingAreaHeight);
		}
		#endregion


		public bool detectFace(Image<Bgr, Byte> frame)
		{
			//faceDetected = frame.Convert<Gray, Byte>().DetectHaarCascade(_faces, 1.1, 3, 0, new Size(40, 40));
			faceDetected = frame.Convert<Gray, Byte>().DetectHaarCascade(_faces, 1.1, 3, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(40, 40));
			if (faceDetected[0].Length == 1)
			{
				return true;
			} else
			{
				return false;
			}
		}

		void calculateFeaturesToTrack(Image<Bgr, Byte> frame, Rectangle trackingArea, float scalingAreaFactor = 0.6f, int spacing = 1)
		{

			_initialTrackingArea = scaleTrackingArea(trackingArea, scalingAreaFactor);

			Image<Gray, byte> trackedImage = copyTrackedImage(frame, trackingArea);

			// Detecting good features that will be tracked in following frames
			//InitialFeature = trackedImage.GoodFeaturesToTrack(4000, 0.5d, 1d, 2500);
			//trackedImage.FindCornerSubPix(InitialFeature, new Size(5, 5), new Size(-1, -1), new MCvTermCriteria(25, 1.5d));

			// Features computed on a different coordinate system are shifted to their original location
			//for (int i = 0; i < InitialFeature[0].Length; i++)
			//{
			//	InitialFeature[0][i].X += _initialTrackingArea.X;
			//	InitialFeature[0][i].Y += _initialTrackingArea.Y;
			//}

			if (InitialFeature == null || InitialFeature.Length == 0)
			{
				InitialFeature = new PointF[1][];
			}

			if (InitialFeature[0] == null || InitialFeature[0].Length != _initialTrackingArea.Width * _initialTrackingArea.Height / spacing)
			{
				InitialFeature[0] = new PointF[_initialTrackingArea.Width * _initialTrackingArea.Height / spacing];
			}


			for (int y = 0; y < _initialTrackingArea.Width; y += spacing)
				for (int x = 0; x < _initialTrackingArea.Height; x += spacing)
				{
					InitialFeature[0][_initialTrackingArea.Height * y / spacing + x / spacing].X = x + _initialTrackingArea.X;
					InitialFeature[0][_initialTrackingArea.Height * y / spacing + x / spacing].Y = y + _initialTrackingArea.Y;
				}



			if (InitialFeature[0].Length > 2)
			{
				initialCentroid = FindCentroidByAverage(InitialFeature[0]);

				//initialCentroid = FindCentroid(hull);
				reinitialized = true;
			}

		}

		void ComputeFlow()
		{
			bool sparse = true;

			if (reinitialized)
			{
				if (detected)
				{
					copyInitial();
				}

				reinitialized = false;
				detected = false;
			}

			if (_currentFrame != null)
			{
				if (sparse)
				{
					ComputeSparseOpticalFlow();
					ComputeMotionFromSparseOpticalFlow();
					createTrackedImageFromCurrentFrame(_trackingArea);
					//ComputeDenseOpticalFlow();
					if (DRAW) _opticalFlowFrame.Draw(_trackingArea, new Bgr(Color.Red), 1);
					_opticalFlowFrame.Draw(new CircleF(referenceCentroid, 1.0f), new Bgr(Color.Goldenrod), 4);
					_opticalFlowFrame.Draw(new CircleF(NonPrunnedCentroid, 1.0f), new Bgr(Color.Cyan), 4);
					_opticalFlowFrame.Draw(new CircleF(currentCentroid, 1.0f), new Bgr(Color.Red), 4);
					ActualFeature[0] = NextFeature;
				} else
				{
					createTrackedImageFromCurrentFrame(_trackingArea);
					ComputeDenseOpticalFlow();
					ComputeMotionFromDenseOpticalFlow();
				}
			}
		}

		void ComputeDenseOpticalFlow()
		{

			if (_currentTrackedGrayImage.Size.Width == _prevTrackedGrayImage.Size.Width)
			{
				velx = new Image<Gray, float>(_prevTrackedGrayImage.Size);
				vely = new Image<Gray, float>(_currentTrackedGrayImage.Size);
				int a = 2;
				Emgu.CV.OpticalFlow.HS(_prevTrackedGrayImage, _currentTrackedGrayImage, true, velx, vely, 0.1d, new MCvTermCriteria(100));
				CalculateCentroidOfDense();
			}

		}

		void CalculateCentroidOfDense()
		{
			int winSizeX = 2;
			int winSizeY = 2;
			vectorFieldX = (int)Math.Round((double)_currentTrackedGrayImage.Width / winSizeX);
			vectorFieldY = (int)Math.Round((double)_currentTrackedGrayImage.Height / winSizeY);
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

				}
			}
		}

		void DrawDenseOpticalFlow()
		{
			int winSizeX = 2;
			int winSizeY = 2;
			vectorFieldX = (int)Math.Round((double)_currentTrackedGrayImage.Width / winSizeX);
			vectorFieldY = (int)Math.Round((double)_currentTrackedGrayImage.Height / winSizeY);
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
						 new PointF((i * winSizeX) + _trackingArea.X,
										(j * winSizeY) + _trackingArea.Y),
										1, 1);
					_opticalFlowFrame.Draw(cr, new Bgr(Color.Red), 1);

					LineSegment2D ci = new LineSegment2D(
						 new Point((i * winSizeX) + _trackingArea.X,
									  (j * winSizeY) + _trackingArea.Y),
						 new Point((int)((i * winSizeX) + _trackingArea.X + velx_float),
									  (int)((j * winSizeY) + _trackingArea.Y + vely_float)));
					_opticalFlowFrame.Draw(ci, new Bgr(Color.Yellow), 1);
				}
			}
		}

		private void ComputeMotionFromDenseOpticalFlow()
		{
		}

		private void ComputeSparseOpticalFlow()
		{
			Emgu.CV.OpticalFlow.PyrLK(_prevGrayFrame, _currentGrayFrame, ActualFeature[0], new System.Drawing.Size(10, 10), 3, new MCvTermCriteria(20, 0.03d), out NextFeature, out Status, out TrackError);

			//using (MemStorage storage = new MemStorage())
			//	nextHull = PointCollection.ConvexHull(ActualFeature[0], storage, Emgu.CV.CvEnum.ORIENTATION.CV_CLOCKWISE).ToArray();
			//currentCentroid = FindCentroid(nextHull);


			RemoveBadFeatures();

			NonPrunnedCentroid = FindCentroidByAverageWithOutPrunning(NextFeature);
			currentCentroid = FindCentroidByAverage(NextFeature);
			if (DRAW)
				for (int i = 0; i < ActualFeature[0].Length; i++)
				{
					DrawTrackedFeatures(i);
					DrawFlowVectors(i);
				}
		}

		private void RemoveBadFeatures()
		{
			int pos = 0;
			for (int i = 0; i < ActualFeature[0].Length; i++)
			{
				if (Status[i] == 0) continue;
				if (NextFeature[i].X < 1 || NextFeature[i].Y < 1) continue;
				if (NextFeature[i].X > _currentFrame.Width - 2 || NextFeature[i].Y > _currentFrame.Height - 2) continue;
				PointF diff = AbsDifference(NextFeature[i], ActualFeature[0][i]);
				if (diff.X > 50 || diff.Y > 50) continue;

				ActualFeature[0][pos] = ActualFeature[0][i];
				NextFeature[pos] = NextFeature[i];
				++pos;
			}
			PointF[] temp = new PointF[pos];

			Array.Copy(ActualFeature[0], temp, pos);
			ActualFeature[0] = temp;
			temp = new PointF[pos];
			Array.Copy(NextFeature, temp, pos);
			NextFeature = temp;
		}

		private void ComputeMotionFromSparseOpticalFlow()
		{
			differenceCentroid = new PointF(referenceCentroid.X - currentCentroid.X, referenceCentroid.Y - currentCentroid.Y);
		}

		private void DrawTrackedFeatures(int i)
		{
			_opticalFlowFrame.Draw(new CircleF(new PointF(ActualFeature[0][i].X, ActualFeature[0][i].Y), 1f), new Bgr(Color.Blue), 1);
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
			_opticalFlowFrame.Draw(line, new Bgr(255, 0, 0), 1);

			p.X = (int)(q.X + 6 * Math.Cos(angle + Math.PI / 4));
			p.Y = (int)(q.Y + 6 * Math.Sin(angle + Math.PI / 4));
			_opticalFlowFrame.Draw(new LineSegment2D(p, q), new Bgr(255, 0, 0), 1);
			p.X = (int)(q.X + 6 * Math.Cos(angle - Math.PI / 4));
			p.Y = (int)(q.Y + 6 * Math.Sin(angle - Math.PI / 4));
			_opticalFlowFrame.Draw(new LineSegment2D(p, q), new Bgr(255, 0, 0), 1);
		}


		private PointF FindCentroidByAverage(PointF[] points)
		{
			PointF centroid = currentCentroid;
			PointF current = FindCentroidByAverageWithOutPrunning(points);
			int count = 1;
			foreach (var point in points)
			{
				PointF dist = AbsDifference(current, point);
				if (dist.X + dist.Y < 200)
				{
					centroid.X += point.X;
					centroid.Y += point.Y;
					++count;
				}

			}
			centroid.X /= count;
			centroid.Y /= count;
			return centroid;
		}
		private PointF FindCentroidByAverageWithOutPrunning(PointF[] points)
		{
			PointF centroid = currentCentroid;
			int count = 1;
			foreach (var point in points)
			{
				centroid.X += point.X;
				centroid.Y += point.Y;
				++count;
			}
			centroid.X /= count;
			centroid.Y /= count;
			return centroid;
		}

		private PointF AbsDifference(PointF A, PointF B)
		{
			return new PointF(Math.Abs(A.X - B.X), Math.Abs(A.Y - B.Y));
		}

		private float ManhattanDistance(PointF A, PointF B)
		{
			return Math.Abs(A.X - B.X) + Math.Abs(A.Y - B.Y);
		}

		//Code adapted and improved from: http://blog.csharphelper.com/2010/01/04/find-a-polygons-centroid-in-c.aspx
		// refer to wikipedia for math formulas centroid of polygon http://en.wikipedia.org/wiki/Centroid        
		private PointF FindCentroid(PointF[] Feature)
		{
			PointF[] Hull;

			// Computing convex hull                
			using (MemStorage storage = new MemStorage())
				Hull = PointCollection.ConvexHull(Feature, storage, Emgu.CV.CvEnum.ORIENTATION.CV_CLOCKWISE).ToArray();

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
	}
}
