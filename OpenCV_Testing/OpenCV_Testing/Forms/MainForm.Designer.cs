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
			this.SuspendLayout();
			// 
			// buttonSURF
			// 
			this.buttonSURF.Location = new System.Drawing.Point(155, 63);
			this.buttonSURF.Name = "buttonSURF";
			this.buttonSURF.Size = new System.Drawing.Size(75, 39);
			this.buttonSURF.TabIndex = 0;
			this.buttonSURF.Text = "SURF";
			this.buttonSURF.UseVisualStyleBackColor = true;
			this.buttonSURF.Click += new System.EventHandler(this.buttonSURF_Click);
			// 
			// buttonFace
			// 
			this.buttonFace.Location = new System.Drawing.Point(58, 63);
			this.buttonFace.Name = "buttonFace";
			this.buttonFace.Size = new System.Drawing.Size(75, 39);
			this.buttonFace.TabIndex = 1;
			this.buttonFace.Text = "Face Detection";
			this.buttonFace.UseVisualStyleBackColor = true;
			this.buttonFace.Click += new System.EventHandler(this.buttonFace_Click);
			// 
			// buttonOpticalFlow
			// 
			this.buttonOpticalFlow.Location = new System.Drawing.Point(105, 112);
			this.buttonOpticalFlow.Name = "buttonOpticalFlow";
			this.buttonOpticalFlow.Size = new System.Drawing.Size(75, 39);
			this.buttonOpticalFlow.TabIndex = 2;
			this.buttonOpticalFlow.Text = "Optical Flow";
			this.buttonOpticalFlow.UseVisualStyleBackColor = true;
			this.buttonOpticalFlow.Click += new System.EventHandler(this.buttonOpticalFlow_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 262);
			this.Controls.Add(this.buttonOpticalFlow);
			this.Controls.Add(this.buttonFace);
			this.Controls.Add(this.buttonSURF);
			this.Name = "MainForm";
			this.Text = "Main";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button buttonSURF;
		private System.Windows.Forms.Button buttonFace;
		private System.Windows.Forms.Button buttonOpticalFlow;
	}
}