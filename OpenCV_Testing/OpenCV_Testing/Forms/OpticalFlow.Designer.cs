namespace OpenCV_Testing
{
	partial class OpticalFlow
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
			this.components = new System.ComponentModel.Container();
			this.imageBoxOpticalFlow = new Emgu.CV.UI.ImageBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.buttonInitializeTracking = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.imageBoxOpticalFlow2 = new Emgu.CV.UI.ImageBox();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.imageBoxOpticalFlow)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.imageBoxOpticalFlow2)).BeginInit();
			this.SuspendLayout();
			// 
			// imageBoxOpticalFlow
			// 
			this.imageBoxOpticalFlow.Location = new System.Drawing.Point(2, 3);
			this.imageBoxOpticalFlow.Name = "imageBoxOpticalFlow";
			this.imageBoxOpticalFlow.Size = new System.Drawing.Size(638, 425);
			this.imageBoxOpticalFlow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.imageBoxOpticalFlow.TabIndex = 3;
			this.imageBoxOpticalFlow.TabStop = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(646, 89);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "label1";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(646, 74);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(86, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "Feature Tracked";
			// 
			// buttonInitializeTracking
			// 
			this.buttonInitializeTracking.Location = new System.Drawing.Point(649, 12);
			this.buttonInitializeTracking.Name = "buttonInitializeTracking";
			this.buttonInitializeTracking.Size = new System.Drawing.Size(114, 46);
			this.buttonInitializeTracking.TabIndex = 6;
			this.buttonInitializeTracking.Text = "Initialize Tracking";
			this.buttonInitializeTracking.UseVisualStyleBackColor = true;
			this.buttonInitializeTracking.Click += new System.EventHandler(this.buttonInitializeTracking_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(646, 132);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(129, 13);
			this.label3.TabIndex = 7;
			this.label3.Text = "Direction Estimated using ";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(645, 159);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(13, 20);
			this.label4.TabIndex = 8;
			this.label4.Text = " ";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(646, 146);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(68, 13);
			this.label5.TabIndex = 9;
			this.label5.Text = "Optical Flow:";
			// 
			// imageBoxOpticalFlow2
			// 
			this.imageBoxOpticalFlow2.Location = new System.Drawing.Point(2, 427);
			this.imageBoxOpticalFlow2.Name = "imageBoxOpticalFlow2";
			this.imageBoxOpticalFlow2.Size = new System.Drawing.Size(638, 425);
			this.imageBoxOpticalFlow2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.imageBoxOpticalFlow2.TabIndex = 10;
			this.imageBoxOpticalFlow2.TabStop = false;
			// 
			// richTextBox1
			// 
			this.richTextBox1.Location = new System.Drawing.Point(646, 86);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(270, 483);
			this.richTextBox1.TabIndex = 11;
			this.richTextBox1.Text = "";
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Checked = true;
			this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox1.Location = new System.Drawing.Point(803, 28);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(58, 17);
			this.checkBox1.TabIndex = 12;
			this.checkBox1.Text = "Detect";
			this.checkBox1.UseVisualStyleBackColor = true;
			this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// checkBox2
			// 
			this.checkBox2.AutoSize = true;
			this.checkBox2.Checked = true;
			this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox2.Location = new System.Drawing.Point(769, 63);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(90, 17);
			this.checkBox2.TabIndex = 13;
			this.checkBox2.Text = "Draw Vectors";
			this.checkBox2.UseVisualStyleBackColor = true;
			this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
			// 
			// OpticalFlow
			// 
			this.AcceptButton = this.buttonInitializeTracking;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(928, 606);
			this.Controls.Add(this.checkBox2);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.richTextBox1);
			this.Controls.Add(this.imageBoxOpticalFlow2);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.buttonInitializeTracking);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.imageBoxOpticalFlow);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Name = "OpticalFlow";
			this.Text = "EmguCV  Tracking Head Movement";
			((System.ComponentModel.ISupportInitialize)(this.imageBoxOpticalFlow)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.imageBoxOpticalFlow2)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Emgu.CV.UI.ImageBox imageBoxOpticalFlow;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button buttonInitializeTracking;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private Emgu.CV.UI.ImageBox imageBoxOpticalFlow2;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.CheckBox checkBox2;
	}
}
