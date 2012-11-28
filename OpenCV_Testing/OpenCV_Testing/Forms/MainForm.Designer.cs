namespace OpenCV_Testing
{
	partial class MainForm
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
			this.buttonSURF = new System.Windows.Forms.Button();
			this.buttonFace = new System.Windows.Forms.Button();
			this.buttonOpticalFlow = new System.Windows.Forms.Button();
			this.buttonCamshift = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// buttonSURF
			// 
			this.buttonSURF.Location = new System.Drawing.Point(207, 78);
			this.buttonSURF.Margin = new System.Windows.Forms.Padding(4);
			this.buttonSURF.Name = "buttonSURF";
			this.buttonSURF.Size = new System.Drawing.Size(100, 48);
			this.buttonSURF.TabIndex = 0;
			this.buttonSURF.Text = "SURF";
			this.buttonSURF.UseVisualStyleBackColor = true;
			this.buttonSURF.Click += new System.EventHandler(this.buttonSURF_Click);
			// 
			// buttonFace
			// 
			this.buttonFace.Location = new System.Drawing.Point(77, 78);
			this.buttonFace.Margin = new System.Windows.Forms.Padding(4);
			this.buttonFace.Name = "buttonFace";
			this.buttonFace.Size = new System.Drawing.Size(100, 48);
			this.buttonFace.TabIndex = 1;
			this.buttonFace.Text = "Face Detection";
			this.buttonFace.UseVisualStyleBackColor = true;
			this.buttonFace.Click += new System.EventHandler(this.buttonFace_Click);
			// 
			// buttonOpticalFlow
			// 
			this.buttonOpticalFlow.Location = new System.Drawing.Point(140, 138);
			this.buttonOpticalFlow.Margin = new System.Windows.Forms.Padding(4);
			this.buttonOpticalFlow.Name = "buttonOpticalFlow";
			this.buttonOpticalFlow.Size = new System.Drawing.Size(100, 48);
			this.buttonOpticalFlow.TabIndex = 2;
			this.buttonOpticalFlow.Text = "Optical Flow";
			this.buttonOpticalFlow.UseVisualStyleBackColor = true;
			this.buttonOpticalFlow.Click += new System.EventHandler(this.buttonOpticalFlow_Click);
			// 
			// buttonCamshift
			// 
			this.buttonCamshift.Location = new System.Drawing.Point(140, 211);
			this.buttonCamshift.Name = "buttonCamshift";
			this.buttonCamshift.Size = new System.Drawing.Size(100, 44);
			this.buttonCamshift.TabIndex = 3;
			this.buttonCamshift.Text = "Camshift";
			this.buttonCamshift.UseVisualStyleBackColor = true;
			this.buttonCamshift.Click += new System.EventHandler(this.buttonCamshift_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(379, 322);
			this.Controls.Add(this.buttonCamshift);
			this.Controls.Add(this.buttonOpticalFlow);
			this.Controls.Add(this.buttonFace);
			this.Controls.Add(this.buttonSURF);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "MainForm";
			this.Text = "Main";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button buttonSURF;
		private System.Windows.Forms.Button buttonFace;
		private System.Windows.Forms.Button buttonOpticalFlow;
		private System.Windows.Forms.Button buttonCamshift;
	}
}