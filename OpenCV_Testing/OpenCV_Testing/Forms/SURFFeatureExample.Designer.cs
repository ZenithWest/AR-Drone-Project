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
			this.imageBox3 = new Emgu.CV.UI.ImageBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.textBoxModelImage = new System.Windows.Forms.TextBox();
			this.LabelModel = new System.Windows.Forms.Label();
			this.buttonBrowseModel = new System.Windows.Forms.Button();
			this.buttonBrowseObserve = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxObservedImage = new System.Windows.Forms.TextBox();
			this.numericUniquenessThreshold = new System.Windows.Forms.NumericUpDown();
			this.labelUniquenessThreshold = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.numericHessianThreshhold = new System.Windows.Forms.NumericUpDown();
			this.checkBoxExtendedFlag = new System.Windows.Forms.CheckBox();
			this.numericnOctaves = new System.Windows.Forms.NumericUpDown();
			this.numericnOctavesLayers = new System.Windows.Forms.NumericUpDown();
			this.buttonDrawMatches = new System.Windows.Forms.Button();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			((System.ComponentModel.ISupportInitialize)(this.imageBox3)).BeginInit();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUniquenessThreshold)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericHessianThreshhold)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericnOctaves)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericnOctavesLayers)).BeginInit();
			this.SuspendLayout();
			// 
			// imageBox3
			// 
			this.imageBox3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imageBox3.Location = new System.Drawing.Point(212, 0);
			this.imageBox3.Name = "imageBox3";
			this.imageBox3.Size = new System.Drawing.Size(962, 497);
			this.imageBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.imageBox3.TabIndex = 4;
			this.imageBox3.TabStop = false;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.buttonDrawMatches);
			this.panel1.Controls.Add(this.numericnOctavesLayers);
			this.panel1.Controls.Add(this.numericnOctaves);
			this.panel1.Controls.Add(this.checkBoxExtendedFlag);
			this.panel1.Controls.Add(this.numericHessianThreshhold);
			this.panel1.Controls.Add(this.label5);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.labelUniquenessThreshold);
			this.panel1.Controls.Add(this.numericUniquenessThreshold);
			this.panel1.Controls.Add(this.buttonBrowseObserve);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.textBoxObservedImage);
			this.panel1.Controls.Add(this.buttonBrowseModel);
			this.panel1.Controls.Add(this.LabelModel);
			this.panel1.Controls.Add(this.textBoxModelImage);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(212, 497);
			this.panel1.TabIndex = 5;
			// 
			// textBoxModelImage
			// 
			this.textBoxModelImage.Location = new System.Drawing.Point(12, 38);
			this.textBoxModelImage.Name = "textBoxModelImage";
			this.textBoxModelImage.Size = new System.Drawing.Size(185, 20);
			this.textBoxModelImage.TabIndex = 0;
			// 
			// LabelModel
			// 
			this.LabelModel.AutoSize = true;
			this.LabelModel.Location = new System.Drawing.Point(28, 19);
			this.LabelModel.Name = "LabelModel";
			this.LabelModel.Size = new System.Drawing.Size(68, 13);
			this.LabelModel.TabIndex = 1;
			this.LabelModel.Text = "Model Image";
			// 
			// buttonBrowseModel
			// 
			this.buttonBrowseModel.Location = new System.Drawing.Point(122, 9);
			this.buttonBrowseModel.Name = "buttonBrowseModel";
			this.buttonBrowseModel.Size = new System.Drawing.Size(75, 23);
			this.buttonBrowseModel.TabIndex = 2;
			this.buttonBrowseModel.Text = "browse";
			this.buttonBrowseModel.UseVisualStyleBackColor = true;
			this.buttonBrowseModel.Click += new System.EventHandler(this.buttonBrowseModel_Click);
			// 
			// buttonBrowseObserve
			// 
			this.buttonBrowseObserve.Location = new System.Drawing.Point(122, 90);
			this.buttonBrowseObserve.Name = "buttonBrowseObserve";
			this.buttonBrowseObserve.Size = new System.Drawing.Size(75, 23);
			this.buttonBrowseObserve.TabIndex = 5;
			this.buttonBrowseObserve.Text = "browse";
			this.buttonBrowseObserve.UseVisualStyleBackColor = true;
			this.buttonBrowseObserve.Click += new System.EventHandler(this.buttonBrowseObserve_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(28, 100);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(85, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Observed Image";
			// 
			// textBoxObservedImage
			// 
			this.textBoxObservedImage.Location = new System.Drawing.Point(12, 119);
			this.textBoxObservedImage.Name = "textBoxObservedImage";
			this.textBoxObservedImage.Size = new System.Drawing.Size(185, 20);
			this.textBoxObservedImage.TabIndex = 3;
			// 
			// numericUniquenessThreshold
			// 
			this.numericUniquenessThreshold.DecimalPlaces = 3;
			this.numericUniquenessThreshold.Increment = new decimal(new int[] {
				1,
				0,
				0,
				131072});
			this.numericUniquenessThreshold.Location = new System.Drawing.Point(15, 180);
			this.numericUniquenessThreshold.Maximum = new decimal(new int[] {
				1,
				0,
				0,
				0});
			this.numericUniquenessThreshold.Name = "numericUniquenessThreshold";
			this.numericUniquenessThreshold.Size = new System.Drawing.Size(70, 20);
			this.numericUniquenessThreshold.TabIndex = 6;
			this.numericUniquenessThreshold.Value = new decimal(new int[] {
				8,
				0,
				0,
				65536});
			// 
			// labelUniquenessThreshold
			// 
			this.labelUniquenessThreshold.AutoSize = true;
			this.labelUniquenessThreshold.Location = new System.Drawing.Point(12, 164);
			this.labelUniquenessThreshold.Name = "labelUniquenessThreshold";
			this.labelUniquenessThreshold.Size = new System.Drawing.Size(113, 13);
			this.labelUniquenessThreshold.TabIndex = 7;
			this.labelUniquenessThreshold.Text = "Uniqueness Threshold";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(15, 219);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(101, 13);
			this.label2.TabIndex = 8;
			this.label2.Text = "Hessian Threshhold";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(15, 315);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(53, 13);
			this.label4.TabIndex = 10;
			this.label4.Text = "nOctaves";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(13, 370);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(84, 13);
			this.label5.TabIndex = 11;
			this.label5.Text = "nOctavesLayers";
			// 
			// numericHessianThreshhold
			// 
			this.numericHessianThreshhold.DecimalPlaces = 1;
			this.numericHessianThreshhold.Increment = new decimal(new int[] {
				50,
				0,
				0,
				0});
			this.numericHessianThreshhold.Location = new System.Drawing.Point(18, 235);
			this.numericHessianThreshhold.Maximum = new decimal(new int[] {
				100000,
				0,
				0,
				0});
			this.numericHessianThreshhold.Name = "numericHessianThreshhold";
			this.numericHessianThreshhold.Size = new System.Drawing.Size(70, 20);
			this.numericHessianThreshhold.TabIndex = 12;
			this.numericHessianThreshhold.Value = new decimal(new int[] {
				500,
				0,
				0,
				0});
			// 
			// checkBoxExtendedFlag
			// 
			this.checkBoxExtendedFlag.AutoSize = true;
			this.checkBoxExtendedFlag.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.checkBoxExtendedFlag.Location = new System.Drawing.Point(18, 270);
			this.checkBoxExtendedFlag.Name = "checkBoxExtendedFlag";
			this.checkBoxExtendedFlag.Size = new System.Drawing.Size(79, 31);
			this.checkBoxExtendedFlag.TabIndex = 13;
			this.checkBoxExtendedFlag.Text = "Extended Flag";
			this.checkBoxExtendedFlag.UseVisualStyleBackColor = true;
			// 
			// numericnOctaves
			// 
			this.numericnOctaves.Location = new System.Drawing.Point(18, 331);
			this.numericnOctaves.Name = "numericnOctaves";
			this.numericnOctaves.Size = new System.Drawing.Size(70, 20);
			this.numericnOctaves.TabIndex = 14;
			this.numericnOctaves.Value = new decimal(new int[] {
				3,
				0,
				0,
				0});
			// 
			// numericnOctavesLayers
			// 
			this.numericnOctavesLayers.Location = new System.Drawing.Point(18, 386);
			this.numericnOctavesLayers.Name = "numericnOctavesLayers";
			this.numericnOctavesLayers.Size = new System.Drawing.Size(70, 20);
			this.numericnOctavesLayers.TabIndex = 15;
			this.numericnOctavesLayers.Value = new decimal(new int[] {
				4,
				0,
				0,
				0});
			// 
			// buttonDrawMatches
			// 
			this.buttonDrawMatches.Location = new System.Drawing.Point(15, 423);
			this.buttonDrawMatches.Name = "buttonDrawMatches";
			this.buttonDrawMatches.Size = new System.Drawing.Size(75, 62);
			this.buttonDrawMatches.TabIndex = 16;
			this.buttonDrawMatches.Text = "Draw Matches";
			this.buttonDrawMatches.UseVisualStyleBackColor = true;
			this.buttonDrawMatches.Click += new System.EventHandler(this.buttonDrawMatches_Click);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// SURFFeatureExample
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1174, 497);
			this.Controls.Add(this.imageBox3);
			this.Controls.Add(this.panel1);
			this.Name = "SURFFeatureExample";
			this.Text = "SURFFeatureExample";
			((System.ComponentModel.ISupportInitialize)(this.imageBox3)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUniquenessThreshold)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericHessianThreshhold)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericnOctaves)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericnOctavesLayers)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private Emgu.CV.UI.ImageBox imageBox3;
		private System.Windows.Forms.Panel panel1;
		public System.Windows.Forms.Button buttonBrowseObserve;
		public System.Windows.Forms.Label label1;
		public System.Windows.Forms.TextBox textBoxObservedImage;
		public System.Windows.Forms.Button buttonBrowseModel;
		public System.Windows.Forms.Label LabelModel;
		public System.Windows.Forms.TextBox textBoxModelImage;
		public System.Windows.Forms.Label labelUniquenessThreshold;
		public System.Windows.Forms.NumericUpDown numericUniquenessThreshold;
		public System.Windows.Forms.NumericUpDown numericnOctavesLayers;
		public System.Windows.Forms.NumericUpDown numericnOctaves;
		public System.Windows.Forms.CheckBox checkBoxExtendedFlag;
		public System.Windows.Forms.NumericUpDown numericHessianThreshhold;
		public System.Windows.Forms.Label label5;
		public System.Windows.Forms.Label label4;
		public System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button buttonDrawMatches;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
	}
}