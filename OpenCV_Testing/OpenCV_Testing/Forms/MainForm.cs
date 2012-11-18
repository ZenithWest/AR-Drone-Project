using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OpenCV_Testing
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		private void buttonSURF_Click(object sender, EventArgs e)
		{

			(new SURFFeatureExample()).Show();
		}

		private void buttonFace_Click(object sender, EventArgs e)
		{
			(new FaceDetection()).Show();
		}

		private void buttonOpticalFlow_Click(object sender, EventArgs e)
		{
			(new OpticalFlow()).Show();
		}
	}
}
