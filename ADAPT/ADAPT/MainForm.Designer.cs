namespace ADAPT
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
			this.labelCamera = new System.Windows.Forms.Label();
			this.buttonConnect = new System.Windows.Forms.Button();
			this.buttonShutdown = new System.Windows.Forms.Button();
			this.groupBoxStatus = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.labelStatusEmergency = new System.Windows.Forms.Label();
			this.labelStatusFrameRate = new System.Windows.Forms.Label();
			this.labelStatusAltitude = new System.Windows.Forms.Label();
			this.labelStatusBatteryInfo = new System.Windows.Forms.Label();
			this.labelStatusHovering = new System.Windows.Forms.Label();
			this.labelStatusConnected = new System.Windows.Forms.Label();
			this.labelStatusFlying = new System.Windows.Forms.Label();
			this.labelStatusCameraShown = new System.Windows.Forms.Label();
			this.labelStatusAltitudeInfo = new System.Windows.Forms.Label();
			this.labelStatusBattery = new System.Windows.Forms.Label();
			this.labelStatusFrameRateInfo = new System.Windows.Forms.Label();
			this.labelStatusCameraShownInfo = new System.Windows.Forms.Label();
			this.labelStatusConnectedInfo = new System.Windows.Forms.Label();
			this.labelStatusEmergencyInfo = new System.Windows.Forms.Label();
			this.labelStatusFlyingInfo = new System.Windows.Forms.Label();
			this.labelStatusHoveringInfo = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.labelStatusGazInfo = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.labelStatusYawInfo = new System.Windows.Forms.Label();
			this.labelStatusRoll = new System.Windows.Forms.Label();
			this.labelStatusRollInfo = new System.Windows.Forms.Label();
			this.labelStatusPitch = new System.Windows.Forms.Label();
			this.labelStatusPitchInfo = new System.Windows.Forms.Label();
			this.textBoxOutput = new System.Windows.Forms.TextBox();
			this.buttonCommandEmergency = new System.Windows.Forms.Button();
			this.buttonCommandHover = new System.Windows.Forms.Button();
			this.buttonCommandTakeoff = new System.Windows.Forms.Button();
			this.buttonCommandFlatTrim = new System.Windows.Forms.Button();
			this.pictureBoxVideo = new System.Windows.Forms.PictureBox();
			this.buttonCommandChangeCamera = new System.Windows.Forms.Button();
			this.groupBoxInput = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.groupBoxVideoSnapshots = new System.Windows.Forms.GroupBox();
			this.labelVideoStatus = new System.Windows.Forms.Label();
			this.buttonSnapshot = new System.Windows.Forms.Button();
			this.buttonVideoEnd = new System.Windows.Forms.Button();
			this.checkBoxVideoCompress = new System.Windows.Forms.CheckBox();
			this.buttonVideoStart = new System.Windows.Forms.Button();
			this.buttonShowConfig = new System.Windows.Forms.Button();
			this.buttonInputSettings = new System.Windows.Forms.Button();
			this.buttonGeneralSettings = new System.Windows.Forms.Button();
			this.labelCurrentBooleanInput = new System.Windows.Forms.Label();
			this.groupBoxStatus.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxVideo)).BeginInit();
			this.groupBoxInput.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.groupBoxVideoSnapshots.SuspendLayout();
			this.SuspendLayout();
			// 
			// labelCamera
			// 
			this.labelCamera.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelCamera.ForeColor = System.Drawing.Color.Goldenrod;
			this.labelCamera.Location = new System.Drawing.Point(93, 13);
			this.labelCamera.Name = "labelCamera";
			this.labelCamera.Size = new System.Drawing.Size(478, 24);
			this.labelCamera.TabIndex = 58;
			this.labelCamera.Text = "No picture";
			this.labelCamera.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// buttonConnect
			// 
			this.buttonConnect.Location = new System.Drawing.Point(12, 12);
			this.buttonConnect.Name = "buttonConnect";
			this.buttonConnect.Size = new System.Drawing.Size(75, 24);
			this.buttonConnect.TabIndex = 56;
			this.buttonConnect.Text = "Startup";
			this.buttonConnect.UseVisualStyleBackColor = true;
			this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
			// 
			// buttonShutdown
			// 
			this.buttonShutdown.Location = new System.Drawing.Point(577, 12);
			this.buttonShutdown.Name = "buttonShutdown";
			this.buttonShutdown.Size = new System.Drawing.Size(75, 24);
			this.buttonShutdown.TabIndex = 57;
			this.buttonShutdown.Text = "Shutdown";
			this.buttonShutdown.UseVisualStyleBackColor = true;
			this.buttonShutdown.Click += new System.EventHandler(this.buttonShutdown_Click);
			// 
			// groupBoxStatus
			// 
			this.groupBoxStatus.Controls.Add(this.tableLayoutPanel1);
			this.groupBoxStatus.Location = new System.Drawing.Point(658, 124);
			this.groupBoxStatus.Name = "groupBoxStatus";
			this.groupBoxStatus.Size = new System.Drawing.Size(184, 195);
			this.groupBoxStatus.TabIndex = 55;
			this.groupBoxStatus.TabStop = false;
			this.groupBoxStatus.Text = "Status";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.4382F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.5618F));
			this.tableLayoutPanel1.Controls.Add(this.labelStatusEmergency, 1, 6);
			this.tableLayoutPanel1.Controls.Add(this.labelStatusFrameRate, 1, 7);
			this.tableLayoutPanel1.Controls.Add(this.labelStatusAltitude, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.labelStatusBatteryInfo, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.labelStatusHovering, 1, 5);
			this.tableLayoutPanel1.Controls.Add(this.labelStatusConnected, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.labelStatusFlying, 1, 4);
			this.tableLayoutPanel1.Controls.Add(this.labelStatusCameraShown, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.labelStatusAltitudeInfo, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.labelStatusBattery, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.labelStatusFrameRateInfo, 0, 7);
			this.tableLayoutPanel1.Controls.Add(this.labelStatusCameraShownInfo, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.labelStatusConnectedInfo, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.labelStatusEmergencyInfo, 0, 6);
			this.tableLayoutPanel1.Controls.Add(this.labelStatusFlyingInfo, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.labelStatusHoveringInfo, 0, 5);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 8;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(178, 176);
			this.tableLayoutPanel1.TabIndex = 61;
			// 
			// labelStatusEmergency
			// 
			this.labelStatusEmergency.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.labelStatusEmergency.AutoSize = true;
			this.labelStatusEmergency.Location = new System.Drawing.Point(90, 136);
			this.labelStatusEmergency.Name = "labelStatusEmergency";
			this.labelStatusEmergency.Size = new System.Drawing.Size(32, 13);
			this.labelStatusEmergency.TabIndex = 42;
			this.labelStatusEmergency.Text = "False";
			// 
			// labelStatusFrameRate
			// 
			this.labelStatusFrameRate.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.labelStatusFrameRate.AutoSize = true;
			this.labelStatusFrameRate.Location = new System.Drawing.Point(90, 158);
			this.labelStatusFrameRate.Name = "labelStatusFrameRate";
			this.labelStatusFrameRate.Size = new System.Drawing.Size(51, 13);
			this.labelStatusFrameRate.TabIndex = 46;
			this.labelStatusFrameRate.Text = "No Video";
			// 
			// labelStatusAltitude
			// 
			this.labelStatusAltitude.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.labelStatusAltitude.AutoSize = true;
			this.labelStatusAltitude.Location = new System.Drawing.Point(90, 48);
			this.labelStatusAltitude.Name = "labelStatusAltitude";
			this.labelStatusAltitude.Size = new System.Drawing.Size(27, 13);
			this.labelStatusAltitude.TabIndex = 50;
			this.labelStatusAltitude.Text = "N/A";
			// 
			// labelStatusBatteryInfo
			// 
			this.labelStatusBatteryInfo.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.labelStatusBatteryInfo.AutoSize = true;
			this.labelStatusBatteryInfo.Location = new System.Drawing.Point(3, 4);
			this.labelStatusBatteryInfo.Name = "labelStatusBatteryInfo";
			this.labelStatusBatteryInfo.Size = new System.Drawing.Size(71, 13);
			this.labelStatusBatteryInfo.TabIndex = 0;
			this.labelStatusBatteryInfo.Text = "Battery status";
			// 
			// labelStatusHovering
			// 
			this.labelStatusHovering.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.labelStatusHovering.AutoSize = true;
			this.labelStatusHovering.Location = new System.Drawing.Point(90, 114);
			this.labelStatusHovering.Name = "labelStatusHovering";
			this.labelStatusHovering.Size = new System.Drawing.Size(32, 13);
			this.labelStatusHovering.TabIndex = 38;
			this.labelStatusHovering.Text = "False";
			// 
			// labelStatusConnected
			// 
			this.labelStatusConnected.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.labelStatusConnected.AutoSize = true;
			this.labelStatusConnected.Location = new System.Drawing.Point(90, 70);
			this.labelStatusConnected.Name = "labelStatusConnected";
			this.labelStatusConnected.Size = new System.Drawing.Size(32, 13);
			this.labelStatusConnected.TabIndex = 40;
			this.labelStatusConnected.Text = "False";
			// 
			// labelStatusFlying
			// 
			this.labelStatusFlying.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.labelStatusFlying.AutoSize = true;
			this.labelStatusFlying.Location = new System.Drawing.Point(90, 92);
			this.labelStatusFlying.Name = "labelStatusFlying";
			this.labelStatusFlying.Size = new System.Drawing.Size(32, 13);
			this.labelStatusFlying.TabIndex = 37;
			this.labelStatusFlying.Text = "False";
			// 
			// labelStatusCameraShown
			// 
			this.labelStatusCameraShown.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.labelStatusCameraShown.AutoSize = true;
			this.labelStatusCameraShown.Location = new System.Drawing.Point(90, 26);
			this.labelStatusCameraShown.Name = "labelStatusCameraShown";
			this.labelStatusCameraShown.Size = new System.Drawing.Size(27, 13);
			this.labelStatusCameraShown.TabIndex = 48;
			this.labelStatusCameraShown.Text = "N/A";
			// 
			// labelStatusAltitudeInfo
			// 
			this.labelStatusAltitudeInfo.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.labelStatusAltitudeInfo.AutoSize = true;
			this.labelStatusAltitudeInfo.Location = new System.Drawing.Point(3, 48);
			this.labelStatusAltitudeInfo.Name = "labelStatusAltitudeInfo";
			this.labelStatusAltitudeInfo.Size = new System.Drawing.Size(42, 13);
			this.labelStatusAltitudeInfo.TabIndex = 49;
			this.labelStatusAltitudeInfo.Text = "Altitude";
			// 
			// labelStatusBattery
			// 
			this.labelStatusBattery.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.labelStatusBattery.AutoSize = true;
			this.labelStatusBattery.Location = new System.Drawing.Point(90, 4);
			this.labelStatusBattery.Name = "labelStatusBattery";
			this.labelStatusBattery.Size = new System.Drawing.Size(27, 13);
			this.labelStatusBattery.TabIndex = 34;
			this.labelStatusBattery.Text = "N/A";
			// 
			// labelStatusFrameRateInfo
			// 
			this.labelStatusFrameRateInfo.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.labelStatusFrameRateInfo.AutoSize = true;
			this.labelStatusFrameRateInfo.Location = new System.Drawing.Point(3, 158);
			this.labelStatusFrameRateInfo.Name = "labelStatusFrameRateInfo";
			this.labelStatusFrameRateInfo.Size = new System.Drawing.Size(62, 13);
			this.labelStatusFrameRateInfo.TabIndex = 45;
			this.labelStatusFrameRateInfo.Text = "Frame Rate";
			// 
			// labelStatusCameraShownInfo
			// 
			this.labelStatusCameraShownInfo.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.labelStatusCameraShownInfo.AutoSize = true;
			this.labelStatusCameraShownInfo.Location = new System.Drawing.Point(3, 26);
			this.labelStatusCameraShownInfo.Name = "labelStatusCameraShownInfo";
			this.labelStatusCameraShownInfo.Size = new System.Drawing.Size(77, 13);
			this.labelStatusCameraShownInfo.TabIndex = 47;
			this.labelStatusCameraShownInfo.Text = "Camera shown";
			// 
			// labelStatusConnectedInfo
			// 
			this.labelStatusConnectedInfo.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.labelStatusConnectedInfo.AutoSize = true;
			this.labelStatusConnectedInfo.Location = new System.Drawing.Point(3, 70);
			this.labelStatusConnectedInfo.Name = "labelStatusConnectedInfo";
			this.labelStatusConnectedInfo.Size = new System.Drawing.Size(59, 13);
			this.labelStatusConnectedInfo.TabIndex = 39;
			this.labelStatusConnectedInfo.Text = "Connected";
			// 
			// labelStatusEmergencyInfo
			// 
			this.labelStatusEmergencyInfo.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.labelStatusEmergencyInfo.AutoSize = true;
			this.labelStatusEmergencyInfo.Location = new System.Drawing.Point(3, 136);
			this.labelStatusEmergencyInfo.Name = "labelStatusEmergencyInfo";
			this.labelStatusEmergencyInfo.Size = new System.Drawing.Size(60, 13);
			this.labelStatusEmergencyInfo.TabIndex = 41;
			this.labelStatusEmergencyInfo.Text = "Emergency";
			// 
			// labelStatusFlyingInfo
			// 
			this.labelStatusFlyingInfo.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.labelStatusFlyingInfo.AutoSize = true;
			this.labelStatusFlyingInfo.Location = new System.Drawing.Point(3, 92);
			this.labelStatusFlyingInfo.Name = "labelStatusFlyingInfo";
			this.labelStatusFlyingInfo.Size = new System.Drawing.Size(34, 13);
			this.labelStatusFlyingInfo.TabIndex = 3;
			this.labelStatusFlyingInfo.Text = "Flying";
			// 
			// labelStatusHoveringInfo
			// 
			this.labelStatusHoveringInfo.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.labelStatusHoveringInfo.AutoSize = true;
			this.labelStatusHoveringInfo.Location = new System.Drawing.Point(3, 114);
			this.labelStatusHoveringInfo.Name = "labelStatusHoveringInfo";
			this.labelStatusHoveringInfo.Size = new System.Drawing.Size(50, 13);
			this.labelStatusHoveringInfo.TabIndex = 4;
			this.labelStatusHoveringInfo.Text = "Hovering";
			// 
			// label3
			// 
			this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(90, 68);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(40, 13);
			this.label3.TabIndex = 52;
			this.label3.Text = "+0.000";
			// 
			// labelStatusGazInfo
			// 
			this.labelStatusGazInfo.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.labelStatusGazInfo.AutoSize = true;
			this.labelStatusGazInfo.Location = new System.Drawing.Point(3, 68);
			this.labelStatusGazInfo.Name = "labelStatusGazInfo";
			this.labelStatusGazInfo.Size = new System.Drawing.Size(26, 13);
			this.labelStatusGazInfo.TabIndex = 51;
			this.labelStatusGazInfo.Text = "Gaz";
			// 
			// label1
			// 
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(90, 46);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(40, 13);
			this.label1.TabIndex = 50;
			this.label1.Text = "+0.000";
			// 
			// labelStatusYawInfo
			// 
			this.labelStatusYawInfo.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.labelStatusYawInfo.AutoSize = true;
			this.labelStatusYawInfo.Location = new System.Drawing.Point(3, 46);
			this.labelStatusYawInfo.Name = "labelStatusYawInfo";
			this.labelStatusYawInfo.Size = new System.Drawing.Size(28, 13);
			this.labelStatusYawInfo.TabIndex = 49;
			this.labelStatusYawInfo.Text = "Yaw";
			// 
			// labelStatusRoll
			// 
			this.labelStatusRoll.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.labelStatusRoll.AutoSize = true;
			this.labelStatusRoll.Location = new System.Drawing.Point(90, 25);
			this.labelStatusRoll.Name = "labelStatusRoll";
			this.labelStatusRoll.Size = new System.Drawing.Size(40, 13);
			this.labelStatusRoll.TabIndex = 48;
			this.labelStatusRoll.Text = "+0.000";
			// 
			// labelStatusRollInfo
			// 
			this.labelStatusRollInfo.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.labelStatusRollInfo.AutoSize = true;
			this.labelStatusRollInfo.Location = new System.Drawing.Point(3, 25);
			this.labelStatusRollInfo.Name = "labelStatusRollInfo";
			this.labelStatusRollInfo.Size = new System.Drawing.Size(25, 13);
			this.labelStatusRollInfo.TabIndex = 47;
			this.labelStatusRollInfo.Text = "Roll";
			// 
			// labelStatusPitch
			// 
			this.labelStatusPitch.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.labelStatusPitch.AutoSize = true;
			this.labelStatusPitch.Location = new System.Drawing.Point(90, 4);
			this.labelStatusPitch.Name = "labelStatusPitch";
			this.labelStatusPitch.Size = new System.Drawing.Size(40, 13);
			this.labelStatusPitch.TabIndex = 46;
			this.labelStatusPitch.Text = "+0.000";
			// 
			// labelStatusPitchInfo
			// 
			this.labelStatusPitchInfo.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.labelStatusPitchInfo.AutoSize = true;
			this.labelStatusPitchInfo.Location = new System.Drawing.Point(3, 4);
			this.labelStatusPitchInfo.Name = "labelStatusPitchInfo";
			this.labelStatusPitchInfo.Size = new System.Drawing.Size(31, 13);
			this.labelStatusPitchInfo.TabIndex = 45;
			this.labelStatusPitchInfo.Text = "Pitch";
			// 
			// textBoxOutput
			// 
			this.textBoxOutput.BackColor = System.Drawing.SystemColors.MenuText;
			this.textBoxOutput.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBoxOutput.ForeColor = System.Drawing.Color.Yellow;
			this.textBoxOutput.Location = new System.Drawing.Point(12, 526);
			this.textBoxOutput.Multiline = true;
			this.textBoxOutput.Name = "textBoxOutput";
			this.textBoxOutput.ReadOnly = true;
			this.textBoxOutput.Size = new System.Drawing.Size(640, 187);
			this.textBoxOutput.TabIndex = 49;
			// 
			// buttonCommandEmergency
			// 
			this.buttonCommandEmergency.Location = new System.Drawing.Point(658, 621);
			this.buttonCommandEmergency.Name = "buttonCommandEmergency";
			this.buttonCommandEmergency.Size = new System.Drawing.Size(184, 23);
			this.buttonCommandEmergency.TabIndex = 50;
			this.buttonCommandEmergency.Text = "Emergency";
			this.buttonCommandEmergency.UseVisualStyleBackColor = true;
			this.buttonCommandEmergency.Click += new System.EventHandler(this.buttonCommandEmergency_Click);
			// 
			// buttonCommandHover
			// 
			this.buttonCommandHover.Location = new System.Drawing.Point(658, 581);
			this.buttonCommandHover.Name = "buttonCommandHover";
			this.buttonCommandHover.Size = new System.Drawing.Size(184, 23);
			this.buttonCommandHover.TabIndex = 54;
			this.buttonCommandHover.Text = "Hover";
			this.buttonCommandHover.UseVisualStyleBackColor = true;
			this.buttonCommandHover.Click += new System.EventHandler(this.buttonCommandHover_Click);
			// 
			// buttonCommandTakeoff
			// 
			this.buttonCommandTakeoff.Location = new System.Drawing.Point(658, 556);
			this.buttonCommandTakeoff.Name = "buttonCommandTakeoff";
			this.buttonCommandTakeoff.Size = new System.Drawing.Size(184, 23);
			this.buttonCommandTakeoff.TabIndex = 52;
			this.buttonCommandTakeoff.Text = "Take off";
			this.buttonCommandTakeoff.UseVisualStyleBackColor = true;
			this.buttonCommandTakeoff.Click += new System.EventHandler(this.buttonCommandTakeoff_Click);
			// 
			// buttonCommandFlatTrim
			// 
			this.buttonCommandFlatTrim.Location = new System.Drawing.Point(658, 650);
			this.buttonCommandFlatTrim.Name = "buttonCommandFlatTrim";
			this.buttonCommandFlatTrim.Size = new System.Drawing.Size(184, 23);
			this.buttonCommandFlatTrim.TabIndex = 53;
			this.buttonCommandFlatTrim.Text = "Flat trim";
			this.buttonCommandFlatTrim.UseVisualStyleBackColor = true;
			this.buttonCommandFlatTrim.Click += new System.EventHandler(this.buttonCommandFlatTrim_Click);
			// 
			// pictureBoxVideo
			// 
			this.pictureBoxVideo.Location = new System.Drawing.Point(12, 40);
			this.pictureBoxVideo.Name = "pictureBoxVideo";
			this.pictureBoxVideo.Size = new System.Drawing.Size(640, 480);
			this.pictureBoxVideo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBoxVideo.TabIndex = 48;
			this.pictureBoxVideo.TabStop = false;
			// 
			// buttonCommandChangeCamera
			// 
			this.buttonCommandChangeCamera.Location = new System.Drawing.Point(658, 690);
			this.buttonCommandChangeCamera.Name = "buttonCommandChangeCamera";
			this.buttonCommandChangeCamera.Size = new System.Drawing.Size(184, 23);
			this.buttonCommandChangeCamera.TabIndex = 59;
			this.buttonCommandChangeCamera.Text = "Change camera";
			this.buttonCommandChangeCamera.UseVisualStyleBackColor = true;
			this.buttonCommandChangeCamera.Click += new System.EventHandler(this.buttonCommandChangeCamera_Click);
			// 
			// groupBoxInput
			// 
			this.groupBoxInput.Controls.Add(this.tableLayoutPanel2);
			this.groupBoxInput.Location = new System.Drawing.Point(658, 12);
			this.groupBoxInput.Name = "groupBoxInput";
			this.groupBoxInput.Size = new System.Drawing.Size(184, 106);
			this.groupBoxInput.TabIndex = 60;
			this.groupBoxInput.TabStop = false;
			this.groupBoxInput.Text = "Input";
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 2;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.4382F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.5618F));
			this.tableLayoutPanel2.Controls.Add(this.label3, 1, 3);
			this.tableLayoutPanel2.Controls.Add(this.labelStatusPitchInfo, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.label1, 1, 2);
			this.tableLayoutPanel2.Controls.Add(this.labelStatusPitch, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.labelStatusRoll, 1, 1);
			this.tableLayoutPanel2.Controls.Add(this.labelStatusGazInfo, 0, 3);
			this.tableLayoutPanel2.Controls.Add(this.labelStatusRollInfo, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.labelStatusYawInfo, 0, 2);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 4;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(178, 87);
			this.tableLayoutPanel2.TabIndex = 61;
			// 
			// groupBoxVideoSnapshots
			// 
			this.groupBoxVideoSnapshots.Controls.Add(this.labelVideoStatus);
			this.groupBoxVideoSnapshots.Controls.Add(this.buttonSnapshot);
			this.groupBoxVideoSnapshots.Controls.Add(this.buttonVideoEnd);
			this.groupBoxVideoSnapshots.Controls.Add(this.checkBoxVideoCompress);
			this.groupBoxVideoSnapshots.Controls.Add(this.buttonVideoStart);
			this.groupBoxVideoSnapshots.Location = new System.Drawing.Point(658, 325);
			this.groupBoxVideoSnapshots.Name = "groupBoxVideoSnapshots";
			this.groupBoxVideoSnapshots.Size = new System.Drawing.Size(184, 118);
			this.groupBoxVideoSnapshots.TabIndex = 61;
			this.groupBoxVideoSnapshots.TabStop = false;
			this.groupBoxVideoSnapshots.Text = "Video && Snapshots";
			// 
			// labelVideoStatus
			// 
			this.labelVideoStatus.AutoSize = true;
			this.labelVideoStatus.Location = new System.Drawing.Point(6, 98);
			this.labelVideoStatus.Name = "labelVideoStatus";
			this.labelVideoStatus.Size = new System.Drawing.Size(41, 13);
			this.labelVideoStatus.TabIndex = 66;
			this.labelVideoStatus.Text = "Idling...";
			// 
			// buttonSnapshot
			// 
			this.buttonSnapshot.Location = new System.Drawing.Point(9, 19);
			this.buttonSnapshot.Name = "buttonSnapshot";
			this.buttonSnapshot.Size = new System.Drawing.Size(169, 23);
			this.buttonSnapshot.TabIndex = 62;
			this.buttonSnapshot.Text = "Take a Snapshot";
			this.buttonSnapshot.UseVisualStyleBackColor = true;
			this.buttonSnapshot.Click += new System.EventHandler(this.buttonSnapshot_Click);
			// 
			// buttonVideoEnd
			// 
			this.buttonVideoEnd.Location = new System.Drawing.Point(103, 71);
			this.buttonVideoEnd.Name = "buttonVideoEnd";
			this.buttonVideoEnd.Size = new System.Drawing.Size(75, 23);
			this.buttonVideoEnd.TabIndex = 65;
			this.buttonVideoEnd.Text = "End Video";
			this.buttonVideoEnd.UseVisualStyleBackColor = true;
			this.buttonVideoEnd.Click += new System.EventHandler(this.buttonVideoEnd_Click);
			// 
			// checkBoxVideoCompress
			// 
			this.checkBoxVideoCompress.AutoSize = true;
			this.checkBoxVideoCompress.Location = new System.Drawing.Point(6, 48);
			this.checkBoxVideoCompress.Name = "checkBoxVideoCompress";
			this.checkBoxVideoCompress.Size = new System.Drawing.Size(101, 17);
			this.checkBoxVideoCompress.TabIndex = 63;
			this.checkBoxVideoCompress.Text = "Compress video";
			this.checkBoxVideoCompress.UseVisualStyleBackColor = true;
			// 
			// buttonVideoStart
			// 
			this.buttonVideoStart.Location = new System.Drawing.Point(6, 71);
			this.buttonVideoStart.Name = "buttonVideoStart";
			this.buttonVideoStart.Size = new System.Drawing.Size(75, 23);
			this.buttonVideoStart.TabIndex = 64;
			this.buttonVideoStart.Text = "Start Video";
			this.buttonVideoStart.UseVisualStyleBackColor = true;
			this.buttonVideoStart.Click += new System.EventHandler(this.buttonVideoStart_Click);
			// 
			// buttonShowConfig
			// 
			this.buttonShowConfig.Location = new System.Drawing.Point(658, 497);
			this.buttonShowConfig.Name = "buttonShowConfig";
			this.buttonShowConfig.Size = new System.Drawing.Size(184, 23);
			this.buttonShowConfig.TabIndex = 62;
			this.buttonShowConfig.Text = "Show Drone config";
			this.buttonShowConfig.UseVisualStyleBackColor = true;
			this.buttonShowConfig.Click += new System.EventHandler(this.buttonShowConfig_Click);
			// 
			// buttonInputSettings
			// 
			this.buttonInputSettings.Location = new System.Drawing.Point(658, 449);
			this.buttonInputSettings.Name = "buttonInputSettings";
			this.buttonInputSettings.Size = new System.Drawing.Size(184, 23);
			this.buttonInputSettings.TabIndex = 63;
			this.buttonInputSettings.Text = "Input settings";
			this.buttonInputSettings.UseVisualStyleBackColor = true;
			this.buttonInputSettings.Click += new System.EventHandler(this.buttonInputSettings_Click);
			// 
			// buttonGeneralSettings
			// 
			this.buttonGeneralSettings.Location = new System.Drawing.Point(658, 473);
			this.buttonGeneralSettings.Name = "buttonGeneralSettings";
			this.buttonGeneralSettings.Size = new System.Drawing.Size(184, 23);
			this.buttonGeneralSettings.TabIndex = 64;
			this.buttonGeneralSettings.Text = "General settings";
			this.buttonGeneralSettings.UseVisualStyleBackColor = true;
			this.buttonGeneralSettings.Click += new System.EventHandler(this.buttonGeneralSettings_Click);
			// 
			// labelCurrentBooleanInput
			// 
			this.labelCurrentBooleanInput.AutoSize = true;
			this.labelCurrentBooleanInput.Location = new System.Drawing.Point(711, 529);
			this.labelCurrentBooleanInput.Name = "labelCurrentBooleanInput";
			this.labelCurrentBooleanInput.Size = new System.Drawing.Size(54, 13);
			this.labelCurrentBooleanInput.TabIndex = 65;
			this.labelCurrentBooleanInput.Text = "No button";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(848, 730);
			this.Controls.Add(this.labelCurrentBooleanInput);
			this.Controls.Add(this.buttonGeneralSettings);
			this.Controls.Add(this.buttonInputSettings);
			this.Controls.Add(this.buttonShowConfig);
			this.Controls.Add(this.groupBoxVideoSnapshots);
			this.Controls.Add(this.groupBoxInput);
			this.Controls.Add(this.buttonCommandChangeCamera);
			this.Controls.Add(this.labelCamera);
			this.Controls.Add(this.buttonConnect);
			this.Controls.Add(this.buttonShutdown);
			this.Controls.Add(this.groupBoxStatus);
			this.Controls.Add(this.textBoxOutput);
			this.Controls.Add(this.buttonCommandEmergency);
			this.Controls.Add(this.buttonCommandHover);
			this.Controls.Add(this.buttonCommandTakeoff);
			this.Controls.Add(this.buttonCommandFlatTrim);
			this.Controls.Add(this.pictureBoxVideo);
			this.Name = "MainForm";
			this.Text = "Form1";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.groupBoxStatus.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxVideo)).EndInit();
			this.groupBoxInput.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.groupBoxVideoSnapshots.ResumeLayout(false);
			this.groupBoxVideoSnapshots.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labelCamera;
		private System.Windows.Forms.Button buttonConnect;
		private System.Windows.Forms.Button buttonShutdown;
		private System.Windows.Forms.GroupBox groupBoxStatus;
		private System.Windows.Forms.Label labelStatusRoll;
		private System.Windows.Forms.Label labelStatusRollInfo;
		private System.Windows.Forms.Label labelStatusPitch;
		private System.Windows.Forms.Label labelStatusPitchInfo;
		private System.Windows.Forms.Label labelStatusEmergency;
		private System.Windows.Forms.Label labelStatusEmergencyInfo;
		private System.Windows.Forms.Label labelStatusConnected;
		private System.Windows.Forms.Label labelStatusConnectedInfo;
		private System.Windows.Forms.Label labelStatusHovering;
		private System.Windows.Forms.Label labelStatusFlying;
		private System.Windows.Forms.Label labelStatusBattery;
		private System.Windows.Forms.Label labelStatusHoveringInfo;
		private System.Windows.Forms.Label labelStatusFlyingInfo;
		private System.Windows.Forms.Label labelStatusBatteryInfo;
		private System.Windows.Forms.TextBox textBoxOutput;
		private System.Windows.Forms.Button buttonCommandEmergency;
		private System.Windows.Forms.Button buttonCommandHover;
		private System.Windows.Forms.Button buttonCommandTakeoff;
		private System.Windows.Forms.Button buttonCommandFlatTrim;
		private System.Windows.Forms.PictureBox pictureBoxVideo;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label labelStatusGazInfo;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label labelStatusYawInfo;
		private System.Windows.Forms.Button buttonCommandChangeCamera;
		private System.Windows.Forms.GroupBox groupBoxInput;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label labelStatusFrameRate;
		private System.Windows.Forms.Label labelStatusAltitude;
		private System.Windows.Forms.Label labelStatusCameraShown;
		private System.Windows.Forms.Label labelStatusAltitudeInfo;
		private System.Windows.Forms.Label labelStatusFrameRateInfo;
		private System.Windows.Forms.Label labelStatusCameraShownInfo;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.GroupBox groupBoxVideoSnapshots;
		private System.Windows.Forms.Label labelVideoStatus;
		private System.Windows.Forms.Button buttonSnapshot;
		private System.Windows.Forms.Button buttonVideoEnd;
		private System.Windows.Forms.CheckBox checkBoxVideoCompress;
		private System.Windows.Forms.Button buttonVideoStart;
		private System.Windows.Forms.Button buttonShowConfig;
		private System.Windows.Forms.Button buttonInputSettings;
		private System.Windows.Forms.Button buttonGeneralSettings;
		private System.Windows.Forms.Label labelCurrentBooleanInput;
	}
}

