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
	public partial class OpticalFlow : Form
	{
		Detection.OpticalFlow opticalFlow;

		public OpticalFlow()
		{
			InitializeComponent();
			System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.High;
		}

		private void buttonInitializeTracking_Click(object sender, EventArgs e)
		{
			opticalFlow = new Detection.OpticalFlow();

			Task t = Task.Factory.StartNew(() => drawImage());
		}

		void drawImage()
		{
			CheckForIllegalCrossThreadCalls = false;
			while (this.Visible)
			{
				if (opticalFlow._prevOpticalFlowFrame != null && !imageBoxOpticalFlow.IsDisposed)
				{
					try
					{
						imageBoxOpticalFlow.Image = opticalFlow._prevOpticalFlowFrame;

						label1.Text = opticalFlow.sumVectorFieldX.ToString();
						label2.Text = opticalFlow.sumVectorFieldY.ToString();
					} catch (Exception)
					{

					}

					string Text = "";
					//Text = Text + string.Format("Both:\n {0}\n\n", opticalFlow.both);


					Text = Text + string.Format("Centroid:\n {0}\n\n", opticalFlow.currentCentroid);
					if (opticalFlow.NextFeature != null)
					Text = Text + string.Format("Features:\n {0}\n\n", opticalFlow.NextFeature.Length);

					richTextBox1.Text = Text;


				}
			}
			opticalFlow.stop();

		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			opticalFlow.test = checkBox1.Checked;
		}

		private void checkBox2_CheckedChanged(object sender, EventArgs e)
		{
			opticalFlow.DRAW = checkBox2.Checked;
		}

	}
}
