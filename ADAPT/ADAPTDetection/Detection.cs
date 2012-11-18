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

using ARDrone.Control;

namespace ARDrone.Detection
{
	class Detection
	{
		private DroneControl droneControl = null;

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

		public Detection(DroneControl droneControl)
		{
			this.droneControl = droneControl;

			Task.Factory.StartNew(() => initializeThread());
		}

		void initializeThread()
		{

		}
	}
}

