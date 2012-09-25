namespace OpenCV_Testing
{
	partial class SURFFeatureExample
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.imageBox1 = new Emgu.CV.UI.ImageBox();
			this.imageBox2 = new Emgu.CV.UI.ImageBox();
			this.imageBox3 = new Emgu.CV.UI.ImageBox();
			((System.ComponentModel.ISupportInitialize)(this.imageBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.imageBox2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.imageBox3)).BeginInit();
			this.SuspendLayout();
			// 
			// imageBox1
			// 
			this.imageBox1.Location = new System.Drawing.Point(12, 12);
			this.imageBox1.Name = "imageBox1";
			this.imageBox1.Size = new System.Drawing.Size(390, 334);
			this.imageBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.imageBox1.TabIndex = 2;
			this.imageBox1.TabStop = false;
			// 
			// imageBox2
			// 
			this.imageBox2.Location = new System.Drawing.Point(408, 12);
			this.imageBox2.Name = "imageBox2";
			this.imageBox2.Size = new System.Drawing.Size(356, 334);
			this.imageBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.imageBox2.TabIndex = 3;
			this.imageBox2.TabStop = false;
			// 
			// imageBox3
			// 
			this.imageBox3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imageBox3.Location = new System.Drawing.Point(0, 0);
			this.imageBox3.Name = "imageBox3";
			this.imageBox3.Size = new System.Drawing.Size(1174, 497);
			this.imageBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.imageBox3.TabIndex = 4;
			this.imageBox3.TabStop = false;
			// 
			// SURFFeatureExample
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1174, 497);
			this.Controls.Add(this.imageBox3);
			this.Controls.Add(this.imageBox2);
			this.Controls.Add(this.imageBox1);
			this.Name = "SURFFeatureExample";
			this.Text = "SURFFeatureExample";
			((System.ComponentModel.ISupportInitialize)(this.imageBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.imageBox2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.imageBox3)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private Emgu.CV.UI.ImageBox imageBox1;
		private Emgu.CV.UI.ImageBox imageBox2;
		private Emgu.CV.UI.ImageBox imageBox3;
	}
}