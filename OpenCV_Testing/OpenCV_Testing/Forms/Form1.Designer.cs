namespace OpenCV_Testing
{
    partial class Form1
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
			this.checkBoxFace = new System.Windows.Forms.CheckBox();
			this.checkBoxUpperbody = new System.Windows.Forms.CheckBox();
			this.checkBoxLeftEar = new System.Windows.Forms.CheckBox();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.checkBoxRightEar = new System.Windows.Forms.CheckBox();
			this.checkBoxRightEye = new System.Windows.Forms.CheckBox();
			this.checkBoxLeftEye = new System.Windows.Forms.CheckBox();
			this.checkBoxMouth = new System.Windows.Forms.CheckBox();
			this.checkBoxNose = new System.Windows.Forms.CheckBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.imageBox1 = new Emgu.CV.UI.ImageBox();
			this.imageBox2 = new Emgu.CV.UI.ImageBox();
			this.imageBox3 = new Emgu.CV.UI.ImageBox();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.imageBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.imageBox2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.imageBox3)).BeginInit();
			this.SuspendLayout();
			// 
			// checkBoxFace
			// 
			this.checkBoxFace.AutoSize = true;
			this.checkBoxFace.Checked = true;
			this.checkBoxFace.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxFace.Location = new System.Drawing.Point(12, 12);
			this.checkBoxFace.Name = "checkBoxFace";
			this.checkBoxFace.Size = new System.Drawing.Size(50, 17);
			this.checkBoxFace.TabIndex = 1;
			this.checkBoxFace.Text = "Face";
			this.checkBoxFace.UseVisualStyleBackColor = true;
			// 
			// checkBoxUpperbody
			// 
			this.checkBoxUpperbody.AutoSize = true;
			this.checkBoxUpperbody.Location = new System.Drawing.Point(12, 173);
			this.checkBoxUpperbody.Name = "checkBoxUpperbody";
			this.checkBoxUpperbody.Size = new System.Drawing.Size(78, 17);
			this.checkBoxUpperbody.TabIndex = 2;
			this.checkBoxUpperbody.Text = "Upperbody";
			this.checkBoxUpperbody.UseVisualStyleBackColor = true;
			// 
			// checkBoxLeftEar
			// 
			this.checkBoxLeftEar.AutoSize = true;
			this.checkBoxLeftEar.Location = new System.Drawing.Point(12, 81);
			this.checkBoxLeftEar.Name = "checkBoxLeftEar";
			this.checkBoxLeftEar.Size = new System.Drawing.Size(63, 17);
			this.checkBoxLeftEar.TabIndex = 3;
			this.checkBoxLeftEar.Text = "Left Ear";
			this.checkBoxLeftEar.UseVisualStyleBackColor = true;
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// checkBoxRightEar
			// 
			this.checkBoxRightEar.AutoSize = true;
			this.checkBoxRightEar.Location = new System.Drawing.Point(12, 104);
			this.checkBoxRightEar.Name = "checkBoxRightEar";
			this.checkBoxRightEar.Size = new System.Drawing.Size(70, 17);
			this.checkBoxRightEar.TabIndex = 4;
			this.checkBoxRightEar.Text = "Right Ear";
			this.checkBoxRightEar.UseVisualStyleBackColor = true;
			// 
			// checkBoxRightEye
			// 
			this.checkBoxRightEye.AutoSize = true;
			this.checkBoxRightEye.Location = new System.Drawing.Point(12, 150);
			this.checkBoxRightEye.Name = "checkBoxRightEye";
			this.checkBoxRightEye.Size = new System.Drawing.Size(72, 17);
			this.checkBoxRightEye.TabIndex = 6;
			this.checkBoxRightEye.Text = "Right Eye";
			this.checkBoxRightEye.UseVisualStyleBackColor = true;
			// 
			// checkBoxLeftEye
			// 
			this.checkBoxLeftEye.AutoSize = true;
			this.checkBoxLeftEye.Checked = true;
			this.checkBoxLeftEye.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxLeftEye.Location = new System.Drawing.Point(12, 127);
			this.checkBoxLeftEye.Name = "checkBoxLeftEye";
			this.checkBoxLeftEye.Size = new System.Drawing.Size(65, 17);
			this.checkBoxLeftEye.TabIndex = 5;
			this.checkBoxLeftEye.Text = "Left Eye";
			this.checkBoxLeftEye.UseVisualStyleBackColor = true;
			// 
			// checkBoxMouth
			// 
			this.checkBoxMouth.AutoSize = true;
			this.checkBoxMouth.Location = new System.Drawing.Point(12, 58);
			this.checkBoxMouth.Name = "checkBoxMouth";
			this.checkBoxMouth.Size = new System.Drawing.Size(56, 17);
			this.checkBoxMouth.TabIndex = 7;
			this.checkBoxMouth.Text = "Mouth";
			this.checkBoxMouth.UseVisualStyleBackColor = true;
			// 
			// checkBoxNose
			// 
			this.checkBoxNose.AutoSize = true;
			this.checkBoxNose.Location = new System.Drawing.Point(12, 35);
			this.checkBoxNose.Name = "checkBoxNose";
			this.checkBoxNose.Size = new System.Drawing.Size(51, 17);
			this.checkBoxNose.TabIndex = 8;
			this.checkBoxNose.Text = "Nose";
			this.checkBoxNose.UseVisualStyleBackColor = true;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.imageBox3);
			this.panel1.Controls.Add(this.checkBoxFace);
			this.panel1.Controls.Add(this.imageBox2);
			this.panel1.Controls.Add(this.checkBoxNose);
			this.panel1.Controls.Add(this.checkBoxUpperbody);
			this.panel1.Controls.Add(this.checkBoxMouth);
			this.panel1.Controls.Add(this.checkBoxLeftEar);
			this.panel1.Controls.Add(this.checkBoxRightEye);
			this.panel1.Controls.Add(this.checkBoxRightEar);
			this.panel1.Controls.Add(this.checkBoxLeftEye);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(150, 600);
			this.panel1.TabIndex = 9;
			// 
			// imageBox1
			// 
			this.imageBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imageBox1.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
			this.imageBox1.Location = new System.Drawing.Point(150, 0);
			this.imageBox1.Name = "imageBox1";
			this.imageBox1.Size = new System.Drawing.Size(800, 600);
			this.imageBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.imageBox1.TabIndex = 2;
			this.imageBox1.TabStop = false;
			// 
			// imageBox2
			// 
			this.imageBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.imageBox2.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
			this.imageBox2.Location = new System.Drawing.Point(0, 450);
			this.imageBox2.Name = "imageBox2";
			this.imageBox2.Size = new System.Drawing.Size(150, 150);
			this.imageBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.imageBox2.TabIndex = 10;
			this.imageBox2.TabStop = false;
			// 
			// imageBox3
			// 
			this.imageBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.imageBox3.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
			this.imageBox3.Location = new System.Drawing.Point(0, 300);
			this.imageBox3.Name = "imageBox3";
			this.imageBox3.Size = new System.Drawing.Size(150, 150);
			this.imageBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.imageBox3.TabIndex = 11;
			this.imageBox3.TabStop = false;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(950, 600);
			this.Controls.Add(this.imageBox1);
			this.Controls.Add(this.panel1);
			this.DoubleBuffered = true;
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.imageBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.imageBox2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.imageBox3)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		  private System.Windows.Forms.CheckBox checkBoxFace;
		  private System.Windows.Forms.CheckBox checkBoxUpperbody;
		  private System.Windows.Forms.CheckBox checkBoxLeftEar;
		  private System.Windows.Forms.Timer timer1;
		  private System.Windows.Forms.CheckBox checkBoxRightEar;
		  private System.Windows.Forms.CheckBox checkBoxRightEye;
		  private System.Windows.Forms.CheckBox checkBoxLeftEye;
		  private System.Windows.Forms.CheckBox checkBoxMouth;
		  private System.Windows.Forms.CheckBox checkBoxNose;
		  private System.Windows.Forms.Panel panel1;
		  private Emgu.CV.UI.ImageBox imageBox3;
		  private Emgu.CV.UI.ImageBox imageBox2;
		  private Emgu.CV.UI.ImageBox imageBox1;
    }
}

