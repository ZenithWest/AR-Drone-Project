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


namespace Detection
{


	class OpticalFlow
	{
		public Image<Bgr, Byte> _prevFrame { get; set; }
		public Image<Gray, Byte> _prevGrayFrame { get; set; }
		public Image<Bgr, Byte> _currentFrame { get; set; }
		public Image<Gray, Byte> _currentGrayFrame { get; set; }
		public Image<Bgr, Byte> _opticalFlowFrame { get; set; }
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

		public Rectangle trackingArea;
		public PointF[] hull, nextHull;
		public PointF previousCentroid, currentCentroid;
		public PointF differenceCentroid;
		public float sumVectorFieldX;
		public float sumVectorFieldY;
		public Random rand;

		OpticalFlow()
		{

		}

		public void addFrame(Image<Bgr, Byte> frame)
		{
			_prevFrame = _currentFrame;
			_prevGrayFrame = _currentGrayFrame;
			_currentFrame = frame;
			_currentGrayFrame = frame.Convert<Gray, Byte>();
			_opticalFlowFrame = frame;
		}

		public void createTrackedImageFromCurrentFrame(Rectangle trackingArea)
		{
			_prevTrackedImage = _currentTrackedImage;
			_prevTrackedGrayImage = _currentTrackedGrayImage;
			_currentTrackedImage = _currentFrame.Copy(roi: trackingArea);
			_currentTrackedGrayImage = _currentTrackedImage.Convert<Gray, Byte>();
		}

		Rectangle scaleTrackingArea(Rectangle trackingArea, float scalingAreaFactor = 1.0f)
		{
			int trackingAreaWidth = (int)(trackingArea.Width * scalingAreaFactor);
			int trackingAreaHeight = (int)(trackingArea.Height * scalingAreaFactor);
			int leftXTrackingCoord = trackingArea.X - (int)(((trackingArea.X + trackingAreaWidth) - (trackingArea.X + trackingAreaWidth)) / 2);
			int leftYTrackingCoord = trackingArea.Y - (int)(((trackingArea.Y + trackingAreaHeight) - (trackingArea.Y + trackingArea.Height)) / 2);
			return new Rectangle(leftXTrackingCoord, leftYTrackingCoord, trackingAreaWidth, trackingAreaHeight);
		}

		void calculateFeaturesToTrack(Image<Bgr, Byte> frame, Rectangle trackingArea, float scalingAreaFactor = 1.0f)
		{

			this.trackingArea = scaleTrackingArea(trackingArea, scalingAreaFactor);

			addFrame(frame);
			createTrackedImageFromCurrentFrame(this.trackingArea);

			// Detecting good features that will be tracked in following frames
			ActualFeature = _currentTrackedGrayImage.GoodFeaturesToTrack(400, 0.5d, 5d, 5);
			_currentTrackedGrayImage.FindCornerSubPix(ActualFeature, new Size(5, 5), new Size(-1, -1), new MCvTermCriteria(25, 1.5d));

			// Features computed on a different coordinate system are shifted to their original location
			for (int i = 0; i < ActualFeature[0].Length; i++)
			{
				ActualFeature[0][i].X += this.trackingArea.X;
				ActualFeature[0][i].Y += this.trackingArea.Y;
			}

			// Computing convex hull                
			using (MemStorage storage = new MemStorage())
				hull = PointCollection.ConvexHull(ActualFeature[0], storage, Emgu.CV.CvEnum.ORIENTATION.CV_CLOCKWISE).ToArray();

			previousCentroid = FindCentroid(hull);
		}



		void Application_Idle(object sender, EventArgs e)
		{
			addFrame(_capture.QueryFrame());
			bool dense = true;
			if (dense)
			{
				if (_currentFrame != null)
				{
					createTrackedImageFromCurrentFrame(trackingArea);

					ComputeDenseOpticalFlow();
					ComputeMotionFromDenseOpticalFlow();
				}
			} else
			{
				if (_currentFrame != null)
				{
					ComputeSparseOpticalFlow();
					ComputeMotionFromSparseOpticalFlow();
					ActualFeature[0] = NextFeature;
				}
			}


		}

		void ComputeDenseOpticalFlow()
		{
			velx = new Image<Gray, float>(_prevTrackedGrayImage.Size);
			vely = new Image<Gray, float>(_currentTrackedGrayImage.Size);
			Emgu.CV.OpticalFlow.HS(_prevTrackedGrayImage, _currentTrackedGrayImage, true, velx, vely, 0.1d, new MCvTermCriteria(100));
			
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

					_opticalFlowFrame.Draw(ci, new Bgr(rand.Next(256), rand.Next(256), rand.Next(256)), 1);
				}
			}
		}

		private void ComputeMotionFromDenseOpticalFlow()
		{
			// To be implemented
		}

		private void ComputeSparseOpticalFlow()
		{            
			Emgu.CV.OpticalFlow.PyrLK(_prevGrayFrame, _currentGrayFrame, ActualFeature[0], new System.Drawing.Size(10, 10), 3, new MCvTermCriteria(20, 0.03d), out NextFeature, out Status, out TrackError);

			using (MemStorage storage = new MemStorage())
				nextHull = PointCollection.ConvexHull(ActualFeature[0], storage, Emgu.CV.CvEnum.ORIENTATION.CV_CLOCKWISE).ToArray();
			currentCentroid = FindCentroid(nextHull);
		}

		private void ComputeMotionFromSparseOpticalFlow()
		{
			differenceCentroid = new PointF(previousCentroid.X - currentCentroid.X, previousCentroid.Y - currentCentroid.Y);
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
	}
}
