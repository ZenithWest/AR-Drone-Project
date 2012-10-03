using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using System.IO;
using System.Windows;

using Emgu.CV;
using Emgu.CV.Structure;

using ARDrone.Control;
using ARDrone.Control.Commands;
using ARDrone.Control.Data;
using ARDrone.Control.Events;
using ARDrone.Input;
using ARDrone.Input.Utils;
using ARDrone.Input.InputMappings;
using ARDrone.Capture;

namespace ADAPT
{
	//class Utility
	//{
	//   public static BitmapImage CreateBitmapImageFromImage(Image image)
	//   {
	//      MemoryStream memoryStream = new MemoryStream();
	//      image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
	//      BitmapImage bitmapImage = new System.Windows.Media.Imaging.BitmapImage();

	//      bitmapImage.BeginInit();
	//      bitmapImage.StreamSource = new MemoryStream(memoryStream.ToArray());
	//      bitmapImage.EndInit();

	//      return bitmapImage;
	//   }

	//   public static IntPtr GetWindowHandle(Window window)
	//   {
	//      System.Windows.Interop.WindowInteropHelper helper = new System.Windows.Interop.WindowInteropHelper(window);
	//      return helper.Handle;
	//   }
	//}

	public partial class MainForm : Form
	{
		private readonly TimeSpan booleanInputTimeout = new TimeSpan(0, 0, 0, 0, 250);

		private delegate void OutputEventHandler(String output);


		//private DispatcherTimer timerStatusUpdate;
		//private DispatcherTimer timerVideoUpdate;
		//private DispatcherTimer timerHudStatusUpdate;

		private VideoRecorder videoRecorder;
		private SnapshotRecorder snapshotRecorder;

		private InputManager inputManager = null;
		private DroneControl droneControl = null;
		private DroneConfig currentDroneConfig; 


		int frameCountSinceLastCapture = 0;
		DateTime lastFrameRateCaptureTime;
		int averageFrameRate = 0;

		private Dictionary<String, DateTime> booleanInputFadeout;
		String snapshotFilePath = string.Empty;
		int snapshotFileCount = 0;

		public MainForm()
		{
			InitializeComponent();
			InitializeInputManager();

			InitializeDroneControl();
		}

		public void Dispose()
		{
			if (inputManager != null)
				inputManager.Dispose();
			if (videoRecorder != null)
				videoRecorder.Dispose();
		}

		private void InitializeDroneControl()
		{
			currentDroneConfig = new DroneConfig();
			currentDroneConfig.Load();

			InitializeDroneControl(currentDroneConfig);


			//DroneConfig droneConfig = new DroneConfig();
			//droneConfig.FirmwareVersion = SupportedFirmwareVersion.Firmware_164_Or_Above;
			//droneConfig.DefaultCameraMode = DroneCameraMode.BottomCamera;

			//droneControl = new DroneControl(droneConfig);
			//droneControl.Error += droneControl_Error_Async;
			//droneControl.ConnectionStateChanged += droneControl_ConnectionStateChanged_Async;
		}

		private void InitializeDroneControl(DroneConfig droneConfig)
		{
			droneControl = new DroneControl(droneConfig);
		}
		
		private void InitializeOtherComponents()
		{
			InitializeDroneControlEventHandlers();

			InitializeTimers();
			//InitializeInputManager();

			//InitializeRecorders();
		}

		private void InitializeDroneControlEventHandlers()
		{
			droneControl.Error += droneControl_Error_Async;
			droneControl.ConnectionStateChanged += droneControl_ConnectionStateChanged_Async;
		}

		private void InitializeTimers()
		{
			//timerStatusUpdate = new DispatcherTimer();
			//timerStatusUpdate.Interval = new TimeSpan(0, 0, 1);
			//timerStatusUpdate.Tick += new EventHandler(timerStatusUpdate_Tick);

			//timerHudStatusUpdate = new DispatcherTimer();
			//timerHudStatusUpdate.Interval = new TimeSpan(0, 0, 0, 0, 50);
			//timerHudStatusUpdate.Tick += new EventHandler(timerHudStatusUpdate_Tick);

			//timerVideoUpdate = new DispatcherTimer();
			//timerVideoUpdate.Interval = new TimeSpan(0, 0, 0, 0, 50);
			//timerVideoUpdate.Tick += new EventHandler(timerVideoUpdate_Tick);
		}

		private void InitializeInputManager()
		{

			inputManager = new ARDrone.Input.InputManager(this.Handle);
			AddInputListeners();

			//inputManager = new ARDrone.Input.InputManager(ARDrone.Utility.GetWindowHandle(this));
			inputManager.SwitchInputMode(ARDrone.Input.InputManager.InputMode.ControlInput);

			inputManager.NewInputState += inputManager_NewInputState;
			inputManager.NewInputDevice += inputManager_NewInputDevice;
			inputManager.InputDeviceLost += inputManager_InputDeviceLost;

			booleanInputFadeout = new Dictionary<String, DateTime>();
		}

		private void InitializeRecorders()
		{
			videoRecorder = new VideoRecorder();
			snapshotRecorder = new SnapshotRecorder();

			videoRecorder.CompressionComplete += new EventHandler(videoRecorder_CompressionComplete);
			videoRecorder.CompressionError += new System.IO.ErrorEventHandler(videoRecorder_CompressionError);
		}

		public void Init()
		{
			timerStatusUpdate.Start();

			UpdateStatus();
			UpdateInteractiveElements();
		}

		public void DisposeControl()
		{
			inputManager.Dispose();
		}
		
		private void AddInputListeners()
		{
			inputManager.NewInputState += new NewInputStateHandler(inputManager_NewInputState);
		}

		private void RemoveInputListeners()
		{
			inputManager.NewInputState -= new NewInputStateHandler(inputManager_NewInputState);
		}

		private void Connect()
		{
			if (droneControl.IsConnected) { return; }

			droneControl.ConnectToDroneNetworkAndDrone();
			UpdateUISync("Connecting to the drone");
		}

		private void Disconnect()
		{
			if (!droneControl.IsConnected) { return; }

			timerVideoUpdate.Stop();

			droneControl.Disconnect();
			UpdateUISync("Disconnecting from the drone");
		}

		private void ChangeCamera()
		{
			Command switchCameraCommand = new SwitchCameraCommand(DroneCameraMode.NextMode);

			if (!droneControl.IsCommandPossible(switchCameraCommand) || videoRecorder.IsVideoCaptureRunning)
				return;

			droneControl.SendCommand(switchCameraCommand);
			UpdateUIAsync("Changing camera");
		}

		private void Takeoff()
		{
			Command takeOffCommand = new FlightModeCommand(DroneFlightMode.TakeOff);

			if (!droneControl.IsCommandPossible(takeOffCommand))
				return;

			droneControl.SendCommand(takeOffCommand);
			UpdateUIAsync("Taking off");
		}

		private void Land()
		{
			Command landCommand = new FlightModeCommand(DroneFlightMode.Land);

			if (!droneControl.IsCommandPossible(landCommand))
				return;

			droneControl.SendCommand(landCommand);
			UpdateUIAsync("Landing");
		}

		private void Emergency()
		{
			Command emergencyCommand = new FlightModeCommand(DroneFlightMode.Emergency);

			if (!droneControl.IsCommandPossible(emergencyCommand))
				return;

			droneControl.SendCommand(emergencyCommand);
			UpdateUIAsync("Sending emergency signal");
		}

		private void FlatTrim()
		{
			Command resetCommand = new FlightModeCommand(DroneFlightMode.Reset);
			Command flatTrimCommand = new FlatTrimCommand();

			if (!droneControl.IsCommandPossible(resetCommand) || !droneControl.IsCommandPossible(flatTrimCommand))
				return;

			droneControl.SendCommand(resetCommand);
			droneControl.SendCommand(flatTrimCommand);
			UpdateUIAsync("Sending flat trim");
		}

		private void EnterHoverMode()
		{
			Command enterHoverModeCommand = new HoverModeCommand(DroneHoverMode.Hover);

			if (!droneControl.IsCommandPossible(enterHoverModeCommand))
				return;

			droneControl.SendCommand(enterHoverModeCommand);
			UpdateUIAsync("Entering hover mode");
		}

		private void LeaveHoverMode()
		{
			Command leaveHoverModeCommand = new HoverModeCommand(DroneHoverMode.StopHovering);

			if (!droneControl.IsCommandPossible(leaveHoverModeCommand))
				return;

			droneControl.SendCommand(leaveHoverModeCommand);
			UpdateUIAsync("Leaving hover mode");
		}

		private void Navigate(float roll, float pitch, float yaw, float gaz)
		{
			FlightMoveCommand flightMoveCommand = new FlightMoveCommand(roll, pitch, yaw, gaz);

			if (droneControl.IsCommandPossible(flightMoveCommand))
				droneControl.SendCommand(flightMoveCommand);
		}

		private void TakeScreenshot()
		{
			//DroneData data = droneControl.NavigationData;
			//pictureBoxMask.Image.Save(@"D:\bla.png");

			//if (snapshotFilePath == string.Empty)
			//{
			//   snapshotFilePath = ShowFileDialog(".png", "PNG files (.png)|*.png");
			//   if (snapshotFilePath == null) { return; }
			//}

			//System.Drawing.Bitmap currentImage = (System.Drawing.Bitmap)droneControl.BitmapImage.Clone();
			//snapshotRecorder.SaveSnapshot(currentImage, snapshotFilePath.Replace(".png", "_" + snapshotFileCount.ToString() + ".png"));
			//UpdateUISync("Saved image #" + snapshotFileCount.ToString());
			//snapshotFileCount++;
		}

		private void UpdateUIAsync(String message)
		{
			this.BeginInvoke(new OutputEventHandler(UpdateUISync), message);
		}

		private void UpdateUISync(String message)
		{
			textBoxOutput.AppendText(message + "\r\n");

			UpdateInteractiveElements();
		}

		private void UpdateInteractiveElements()
		{
			inputManager.SetFlags(droneControl.IsConnected, droneControl.IsEmergency, droneControl.IsFlying, droneControl.IsHovering);
			
			if (!droneControl.IsConnected) { buttonConnect.Enabled = true; } else { buttonConnect.Enabled = false; }
			if (droneControl.IsConnected) { buttonShutdown.Enabled = true; } else { buttonShutdown.Enabled = false; }

			if (droneControl.CanTakeoff || droneControl.CanLand) { buttonCommandTakeoff.Enabled = true; } else { buttonCommandTakeoff.Enabled = false; }
			if (droneControl.CanEnterHoverMode || droneControl.CanLeaveHoverMode) { buttonCommandHover.Enabled = true; } else { buttonCommandHover.Enabled = false; }
			if (droneControl.CanCallEmergency) { buttonCommandEmergency.Enabled = true; } else { buttonCommandEmergency.Enabled = false; }
			if (droneControl.CanSendFlatTrim) { buttonCommandFlatTrim.Enabled = true; } else { buttonCommandFlatTrim.Enabled = false; }

			if (!droneControl.IsFlying) { buttonCommandTakeoff.Text = "Take off"; } else { buttonCommandTakeoff.Text = "Land"; }
			if (!droneControl.IsHovering) { buttonCommandHover.Text = "Start hover"; } else { buttonCommandHover.Text = "Stop hover"; }
		}

		  private void UpdateStatus()
		  {
				if (!droneControl.IsConnected)
				{
					 labelCamera.Text = "No picture";

					 labelStatusPitch.Text = "+0.0000°";
					 labelStatusRoll.Text = "+0.0000°";
				}
				else
				{
					 DroneData data = droneControl.NavigationData;

					 labelCamera.Text = "Bottom camera";
				    labelStatusPitch.Text = String.Format("{0:+0.000;-0.000;+0.000}", data.Theta);
				    labelStatusRoll.Text = String.Format("{0:+0.000;-0.000;+0.000}", data.Phi);
				    labelStatusBattery.Text = data.BatteryLevel + "%";
				}

			   labelStatusConnected.Text = droneControl.IsConnected.ToString();
			   labelStatusFlying.Text = droneControl.IsFlying.ToString();
			   labelStatusHovering.Text = droneControl.IsHovering.ToString();
		  }

		  private void UpdateInputState(InputState inputState)
		  {
			  labelStatusSpecialAction.Text = inputState.SpecialAction.ToString();
		  }
					

		private void SendDroneCommands(InputState inputState)
		{
			if (inputState.TakeOff && droneControl.CanTakeoff)
				Takeoff();
			else if (inputState.Land && droneControl.CanLand)
				Land();

			if (inputState.Hover && droneControl.CanEnterHoverMode)
				EnterHoverMode();
			else if (inputState.Hover && droneControl.CanLeaveHoverMode)
				LeaveHoverMode();

			if (inputState.Emergency)
				Emergency();
			else if (inputState.FlatTrim)
				FlatTrim();
			/*
			if (SpecialActionChanged(inputState.SpecialAction))
			{
				if (inputState.SpecialAction)
					FollowDrone();
				else
					EndFollowingDrone();
			}*/

			float roll = inputState.Roll / 1.0f;
			float pitch = inputState.Pitch / 1.0f;
			float yaw = inputState.Yaw / 2.0f;
			float gaz = inputState.Gaz / 2.0f;

			//if (followingDrone && course.LatestValidDirection.AdviceGiven)
			//	CorrectDroneCourse();
			//else
			Navigate(roll, pitch, yaw, gaz);
		}
		/*
		private void FollowDrone()
		{
			followingDrone = true;
		}

		private void EndFollowingDrone()
		{
			followingDrone = false;
		}

		private bool SpecialActionChanged(bool currentValueForSpecialAction)
		{
			return (lastValueForSpecialAction != currentValueForSpecialAction);
		}
		*/
		private void UpdateDroneState(InputState inputState)
		{
			//lastValueForSpecialAction = inputState.SpecialAction;
		}
		
		private void UpdateVideoImage()
		{
			if (droneControl.IsConnected)
			{
				Bitmap newImage = CopyBitmap((Bitmap)droneControl.BitmapImage);

				if (newImage != null)
				{
					//PerformStopSignDetection(newImage);
					UpdateVisualImage(newImage);
				}
			}
		}

		private Bitmap CopyBitmap(Bitmap image)
		{
			try
			{
				int width = image.Width;
				int height = image.Height;
				Rectangle rectangleToCopy = new Rectangle(0, 0, width, height);

				Bitmap newImage = new Bitmap(width, height);
				using (Graphics g = Graphics.FromImage(newImage))
				{
					g.DrawImage(image, rectangleToCopy, rectangleToCopy, GraphicsUnit.Pixel);
				}

				return newImage;
			}
			catch (Exception)
			{
				return null;
			}
		}

		private void UpdateVisualImage(Bitmap image)
		{
			if (image != null)
			{
				pictureBoxVideo.Image = image;
			}
		}



		private void StartVideoCapture()
		{
			if (!CanCaptureVideo || videoRecorder.IsVideoCaptureRunning) { return; }

			String videoFilePath = ShowFileDialog(".avi", "Video files (.avi)|*.avi");
			if (videoFilePath == null) { return; }

			System.Drawing.Size size;
			if (droneControl.CurrentCameraType == DroneCameraMode.FrontCamera)
			{
				size = droneControl.FrontCameraPictureSize;
			}
			else
			{
				size = droneControl.BottomCameraPictureSize;
			}

			//videoRecorder.StartVideo(videoFilePath, averageFrameRate, size.Width, size.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb, 4, checkBoxVideoCompress.IsChecked == true ? true : false);
			UpdateInteractiveElements();
		}

		private void EndVideoCapture()
		{
			if (!videoRecorder.IsVideoCaptureRunning)
			{
				return;
			}

			videoRecorder.EndVideo();

			UpdateInteractiveElements();
		}

		private String ShowFileDialog(String extension, String filter)
		{

			SaveFileDialog fileDialog = new SaveFileDialog();
			fileDialog.FileName = "ARDroneOut";
			fileDialog.DefaultExt = extension;
			fileDialog.Filter = filter;

			DialogResult result = fileDialog.ShowDialog();

			String fileName = null;
			if (result == DialogResult.OK)
			{
				fileName = fileDialog.FileName;
			}

			try
			{
				if (File.Exists(fileName))
				{
					File.Delete(fileName);
				}
			}
			catch (Exception)
			{
				MessageBox.Show(null, "The file could not be deleted", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				fileName = null;
			}

			return fileName;
		}

		private void OpenInputConfigDialog()
		{
			if (droneControl.IsConnected)
				return;

			inputManager.SwitchInputMode(ARDrone.Input.InputManager.InputMode.NoInput);

			//InputConfigDialog configInput = new InputConfigDialog(inputManager);
			//configInput.ShowDialog();

			//inputManager.SwitchInputMode(ARDrone.Input.InputManager.InputMode.ControlInput);
		}

		private void OpenGeneralConfigDialog()
		{
			if (droneControl.IsConnected)
				return;

			//GeneralConfigWindow configGeneral = new GeneralConfigWindow(currentDroneConfig, currentHudConfig);
			//configGeneral.ShowDialog();

			//if (configGeneral.ConfigChanged)
			//{
			//	SaveDroneAndHudConfigStates(configGeneral.DroneConfig/*, configGeneral.HudConfig*/);
			//	ReinitializeDroneControlAndHud();
			//}
		}

		private void SaveDroneAndHudConfigStates(DroneConfig droneConfig/*, HudConfig hudConfig*/)
		{
			currentDroneConfig = droneConfig;
			//currentHudConfig = hudConfig;

			droneConfig.Save();
			//hudConfig.Save();
		}

		private void ReinitializeDroneControlAndHud()
		{
			InitializeDroneControl(currentDroneConfig);
			InitializeDroneControlEventHandlers();
			//InitializeHudInterface(currentHudConfig);
		}

		private void OpenDroneConfigDialog()
		{
			if (!droneControl.IsConnected)
				return;

			//DroneConfigurationOutput configOutput = new DroneConfigurationOutput(droneControl.InternalDroneConfiguration);
			//configOutput.ShowDialog();
		}

		private void HandleConnectionStateChange(DroneConnectionStateChangedEventArgs args)
		{
			UpdateInteractiveElements();

			if (args.Connected)
			{
				timerVideoUpdate.Start();
				UpdateUISync("Connected to the drone");
			}
			else
			{
				UpdateUISync("Disconnected from the drone");
			}
		}



		private void HandleError(DroneErrorEventArgs args)
		{
			String errorText = SerializeException(args.CausingException);
			MessageBox.Show(errorText);
		}

		private String SerializeException(Exception e)
		{
			String errorMessage = e.Message;
			String exceptionTypeText = e.GetType().ToString();
			String stackTrace = e.StackTrace == null ? "No stack trace given" : e.StackTrace.ToString();

			String errorText = "An exception '" + exceptionTypeText + "' occured:\n" + errorMessage;
			errorText += "\n\nStack trace:\n" + stackTrace;

			if (e.InnerException != null)
			{
				errorText += "\n\n";
				errorText += SerializeException(e.InnerException);
			}

			return errorText;
		}

		private bool CanCaptureVideo
		{
			get
			{
				return droneControl.CanSwitchCamera;
			}
		}

		// Event handlers

		private void droneControl_Error_Async(object sender, DroneErrorEventArgs e)
		{
			this.BeginInvoke(new DroneErrorEventHandler(droneControl_Error_Sync), sender, e);
		}

		private void droneControl_Error_Sync(object sender, DroneErrorEventArgs e)
		{
			HandleError(e);
		}

		private void droneControl_ConnectionStateChanged_Async(object sender, DroneConnectionStateChangedEventArgs e)
		{
			this.BeginInvoke(new DroneConnectionStateChangedEventHandler(droneControl_ConnectionStateChanged_Sync), sender, e);
		}

		private void droneControl_ConnectionStateChanged_Sync(object sender, DroneConnectionStateChangedEventArgs e)
		{
			HandleConnectionStateChange(e);
		}

		private void inputManager_NewInputDevice(object sender, NewInputDeviceEventArgs e)
		{
			UpdateUIAsync("New input device: " + e.DeviceName);
		}

		private void inputManager_InputDeviceLost(object sender, InputDeviceLostEventArgs e)
		{
			UpdateUIAsync("Lost input device: " + e.DeviceName);
		}

		private void inputManager_NewInputState(object sender, NewInputStateEventArgs e)
		{
			SendDroneCommands(e.CurrentInputState);
			UpdateDroneState(e.CurrentInputState);

			this.BeginInvoke(new NewInputStateHandler(inputManagerSync_NewInputState), this, e);
		}

		private void inputManagerSync_NewInputState(object sender, NewInputStateEventArgs e)
		{
			UpdateInputState(e.CurrentInputState);
		}

		private void videoRecorder_CompressionComplete(object sender, EventArgs e)
		{
			//Dispatcher.BeginInvoke(new EventHandler(videoRecoderSync_CompressionComplete), this, e);
		}

		private void videoRecoderSync_CompressionComplete(object sender, EventArgs e)
		{
			MessageBox.Show(this, "Successfully compressed video!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
			UpdateInteractiveElements();
		}

		private void videoRecorder_CompressionError(object sender, ErrorEventArgs e)
		{
			//Dispatcher.BeginInvoke(new System.IO.ErrorEventHandler(videoRecoderSync_CompressionError), this, e);
		}

		private void videoRecoderSync_CompressionError(object sender, ErrorEventArgs e)
		{
			MessageBox.Show(this, e.GetException().Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Error);
			UpdateInteractiveElements();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			Init();
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			DisposeControl();
			Disconnect();
		}

		private void buttonConnect_Click(object sender, EventArgs e)
		{
			Connect();
		}

		private void buttonShutdown_Click(object sender, EventArgs e)
		{
			Disconnect();
		}

		private void buttonCommandTakeScreenshot_Click(object sender, EventArgs e)
		{
			TakeScreenshot();
		}

		private void buttonCommandTakeoff_Click(object sender, EventArgs e)
		{
			if (!droneControl.IsFlying)
			{
				Takeoff();
			}
			else
			{
				Land();
			}
		}

		private void buttonCommandHover_Click(object sender, EventArgs e)
		{
			if (!droneControl.IsHovering)
			{
				EnterHoverMode();
			}
			else
			{
				LeaveHoverMode();
			}
		}

		private void buttonCommandEmergency_Click(object sender, EventArgs e)
		{
			Emergency();
		}

		private void buttonCommandFlatTrim_Click(object sender, EventArgs e)
		{
			FlatTrim();
		}

		private void buttonSnapshot_Click(object sender, EventArgs e)
		{
			TakeScreenshot();
		}

		private void buttonVideoStart_Click(object sender, EventArgs e)
		{
			StartVideoCapture();
		}

		private void buttonVideoEnd_Click(object sender, EventArgs e)
		{
			EndVideoCapture();
		}

		private void buttonInputSettings_Click(object sender, EventArgs e)
		{
			OpenInputConfigDialog();
		}

		private void buttonGeneralSettings_Click(object sender, EventArgs e)
		{
			OpenGeneralConfigDialog();
		}

		private void buttonShowConfig_Click(object sender, EventArgs e)
		{
			OpenDroneConfigDialog();
		}

		private void timerStatusUpdate_Tick(object sender, EventArgs e)
		{
			UpdateStatus();
		}

		private void timerVideoUpdate_Tick(object sender, EventArgs e)
		{
			UpdateVideoImage();
		}
	}
}
