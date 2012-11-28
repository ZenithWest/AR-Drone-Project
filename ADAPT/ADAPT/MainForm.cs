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
using ARDrone.NavData;

namespace ARDrone.UI
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
		#region Class Members
		private readonly TimeSpan booleanInputTimeout = new TimeSpan(0, 0, 0, 0, 250);

		private delegate void OutputEventHandler(String output);

		private System.Windows.Forms.Timer timerStatusUpdate;
		private System.Windows.Forms.Timer timerVideoUpdate;
		//private System.Windows.Forms.Timer timerHudStatusUpdate;

		private VideoRecorder videoRecorder;
		private SnapshotRecorder snapshotRecorder;

		private InputManager inputManager = null;

		private DroneControl droneControl = null;
		private DroneConfig currentDroneConfig;

		private bool droneControlAuto = false;

		int frameCountSinceLastCapture = 0;
		DateTime lastFrameRateCaptureTime;
		int averageFrameRate = 0;

		private Dictionary<String, DateTime> booleanInputFadeout;
		String snapshotFilePath = string.Empty;
		int snapshotFileCount = 0;

		NavDataRecorder navDataRecorder;

		Detection.OpticalFlow opticalFlow;
		#endregion

		#region MainForm
		public MainForm()
		  {
			opticalFlow = new Detection.OpticalFlow();
			InitializeDroneControl();

			InitializeComponent();
			
			InitializeTimers();
			InitializeInputManager();
			InitializeRecorders();

			navDataRecorder = new NavDataRecorder(droneControl, inputManager, (double)navDataInterval.Value);
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			Init();
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			Dispose();
			Disconnect();
		}

		public void Dispose()
		{
			if (inputManager != null)
				inputManager.Dispose();
			if (videoRecorder != null)
				videoRecorder.Dispose();
		}
		#endregion
		
		#region Initialize
		public void Init()
		{
			timerStatusUpdate.Start();

			UpdateStatus();
			UpdateInteractiveElements();
		} 

		private void InitializeDroneControl()
		{
			currentDroneConfig = new DroneConfig();
			currentDroneConfig.Load();

			InitializeDroneControl(currentDroneConfig);
			InitializeDroneControlEventHandlers();
		}

		private void InitializeDroneControl(DroneConfig droneConfig)
		{
			droneControl = new DroneControl(droneConfig);
		}

		private void InitializeDroneControlEventHandlers()
		{
			droneControl.Error += droneControl_Error_Async;
			droneControl.ConnectionStateChanged += droneControl_ConnectionStateChanged_Async;
		}

		private void InitializeTimers()
		{
			timerStatusUpdate = new System.Windows.Forms.Timer { Interval = 1000 };
			timerStatusUpdate.Tick += new EventHandler(timerStatusUpdate_Tick);

			//timerHudStatusUpdate = new DispatcherTimer();
			//timerHudStatusUpdate.Interval = 50;
			//timerHudStatusUpdate.Tick += new EventHandler(timerHudStatusUpdate_Tick);

			timerVideoUpdate = new System.Windows.Forms.Timer { Interval = 50 };
			timerVideoUpdate.Tick += new EventHandler(timerVideoUpdate_Tick);
		}

		private void InitializeInputManager()
		{

			inputManager = new ARDrone.Input.InputManager(this.Handle);
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
		#endregion
				
		#region Connection
		private void Connect()
		{
			if (droneControl.IsConnected) { return; }

			UpdateUISync("Connecting to the drone");
			droneControl.ConnectToDroneNetworkAndDrone();
		}

		private void Disconnect()
		{
			if (!droneControl.IsConnected) { return; }

			timerVideoUpdate.Stop();

			UpdateUISync("Disconnecting from the drone");
			droneControl.Disconnect();
		} 
		#endregion

		#region Commands
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

		private void TakeSnapshot()
		{
			//DroneData data = droneControl.NavigationData;
			//pictureBoxMask.Image.Save(@"D:\bla.png");

			if (snapshotFilePath == string.Empty)
			{
				snapshotFilePath = ShowFileDialog(".png", "PNG files (.png)|*.png");
				if (snapshotFilePath == null) { return; }
			}

			System.Drawing.Bitmap currentImage = (System.Drawing.Bitmap)droneControl.BitmapImage.Clone();
			snapshotRecorder.SaveSnapshot(currentImage, snapshotFilePath.Replace(".png", String.Format("_{0}.png", snapshotFileCount)));
			UpdateUISync("Saved image #" + snapshotFileCount.ToString());
			snapshotFileCount++;
		}

		private void SendDroneCommands(InputState inputState)
		{
			if (inputState.CameraSwap)
			{
				ChangeCamera();
			}

			if (inputState.TakeOff && droneControl.CanTakeoff)
			{
				Takeoff();
			}
			else if (inputState.Land && droneControl.CanLand)
			{
				Land();
			}

			if (inputState.Hover && droneControl.CanEnterHoverMode)
			{
				EnterHoverMode();
			}
			else if (inputState.Hover && droneControl.CanLeaveHoverMode)
			{
				LeaveHoverMode();
			}

			if (inputState.Emergency)
			{
				Emergency();
			}
			else if (inputState.FlatTrim)
			{
				FlatTrim();
			}
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
		#endregion

		#region Update
		private void UpdateUIAsync(String message)
		{
			this.BeginInvoke(new OutputEventHandler(UpdateUISync), message);
		}

		private void UpdateUISync(String message)
		{
			textBoxOutput.AppendText(message + "\r\n");
			//textBoxOutput.ScrollToCaret();
			UpdateInteractiveElements();
		}

		private void UpdateInteractiveElements()
		{
			inputManager.SetFlags(droneControl.IsConnected, droneControl.IsEmergency, droneControl.IsFlying, droneControl.IsHovering);

			if (!droneControl.IsConnected) { buttonConnect.Enabled = true; } else { buttonConnect.Enabled = false; }
			if (droneControl.IsConnected) { buttonShutdown.Enabled = true; } else { buttonShutdown.Enabled = false; }
            if (droneControl.IsConnected) { trackingAlgorithmsGroupBox.Enabled = true; } else { trackingAlgorithmsGroupBox.Enabled = false; }

			if (droneControl.CanTakeoff || droneControl.CanLand) { buttonCommandTakeoff.Enabled = true; } else { buttonCommandTakeoff.Enabled = false; }
			if (droneControl.CanEnterHoverMode || droneControl.CanLeaveHoverMode) { buttonCommandHover.Enabled = true; } else { buttonCommandHover.Enabled = false; }
			if (droneControl.CanCallEmergency) { buttonCommandEmergency.Enabled = true; } else { buttonCommandEmergency.Enabled = false; }
			if (droneControl.CanSendFlatTrim) { buttonCommandFlatTrim.Enabled = true; } else { buttonCommandFlatTrim.Enabled = false; }
			if (droneControl.IsCommandPossible(new SwitchCameraCommand(DroneCameraMode.NextMode)) && !videoRecorder.IsVideoCaptureRunning && !videoRecorder.IsCompressionRunning) { buttonCommandChangeCamera.Enabled = true; } else { buttonCommandChangeCamera.Enabled = false; }

			if (!droneControl.IsFlying) { buttonCommandTakeoff.Text = "Take off"; } else { buttonCommandTakeoff.Text = "Land"; }
			if (!droneControl.IsHovering) { buttonCommandHover.Text = "Start hover"; } else { buttonCommandHover.Text = "Stop hover"; }

			if (droneControl.IsConnected) { buttonSnapshot.Enabled = true; } else { buttonSnapshot.Enabled = false; }
			if (!droneControl.IsConnected || videoRecorder.IsVideoCaptureRunning || videoRecorder.IsCompressionRunning) { checkBoxVideoCompress.Enabled = false; } else { checkBoxVideoCompress.Enabled = true; }
			if (CanCaptureVideo && !videoRecorder.IsVideoCaptureRunning && !videoRecorder.IsCompressionRunning) { buttonVideoStart.Enabled = true; } else { buttonVideoStart.Enabled = false; }
			if (CanCaptureVideo && videoRecorder.IsVideoCaptureRunning && !videoRecorder.IsCompressionRunning) { buttonVideoEnd.Enabled = true; } else { buttonVideoEnd.Enabled = false; }

			if (droneControl.IsConnected && !droneControl.IsFlying) { buttonShowConfig.Enabled = true; } else { buttonShowConfig.Enabled = false; }
			if (!droneControl.IsConnected && !droneControl.IsConnecting) { buttonGeneralSettings.Enabled = true; } else { buttonGeneralSettings.Enabled = false; }
			if (!droneControl.IsConnected && !droneControl.IsConnecting) { buttonInputSettings.Enabled = true; } else { buttonInputSettings.Enabled = false; }

			if (videoRecorder.IsCompressionRunning) { labelVideoStatus.Text = "Compressing"; }
			else if (videoRecorder.IsVideoCaptureRunning) { labelVideoStatus.Text = "Recording"; }
			else { labelVideoStatus.Text = "Idling ..."; }
		}

		private void UpdateStatus()
		{
			if (!droneControl.IsConnected)
			{
				labelStatusCamera.Text = "No picture";

				labelStatusBattery.Text = "N/A";
				labelStatusAltitude.Text = "N/A";

				labelStatusPitch.Text = "+0.0000°";
				labelStatusRoll.Text = "+0.0000°";
				labelStatusYaw.Text = "+0.0000°";
				labelStatusGaz.Text = "+0.0000°";

				labelStatusFrameRate.Text = "No video";
				//imageVideo.Source = null;
			}
			else
			{
				DroneData data = droneControl.NavigationData;
				int frameRate = GetCurrentFrameRate();

				ChangeCameraStatus();

				labelStatusBattery.Text = data.BatteryLevel + "%";
				labelStatusAltitude.Text = data.Altitude.ToString();

				labelStatusPitch.Text = String.Format("{0:+0.000;-0.000;+0.000}", data.Theta);
				labelStatusRoll.Text = String.Format("{0:+0.000;-0.000;+0.000}", data.Phi);
				labelStatusYaw.Text = String.Format("{0:+0.000;-0.000;+0.000}", data.Psi);

				labelStatusVX.Text = String.Format("{0:+0.000;-0.000;+0.000}", data.VX);
				labelStatusRoll.Text = String.Format("{0:+0.000;-0.000;+0.000}", data.Phi);
				labelStatusYaw.Text = String.Format("{0:+0.000;-0.000;+0.000}", data.Psi);
				//labelStatusGaz.Text = String.Format("{0:+0.000;-0.000;+0.000}", data.);
			}

			labelStatusConnected.Text = droneControl.IsConnected.ToString();
			labelStatusFlying.Text = droneControl.IsFlying.ToString();
			labelStatusHovering.Text = droneControl.IsHovering.ToString();

			UpdateCurrentBooleanInputState();
		}

		private void ChangeCameraStatus()
		{
			if (droneControl.CurrentCameraType == DroneCameraMode.FrontCamera)
			{
				labelStatusCamera.Text = "Front camera";
				labelStatusCamera.Text = "Front";
			}
			else if (droneControl.CurrentCameraType == DroneCameraMode.PictureInPictureFront)
			{
				labelStatusCamera.Text = "Front camera (PiP)";
				labelStatusCamera.Text = "Front (PiP)";
			}
			else if (droneControl.CurrentCameraType == DroneCameraMode.BottomCamera)
			{
				labelStatusCamera.Text = "Bottom camera";
				labelStatusCamera.Text = "Bottom";
			}
			else if (droneControl.CurrentCameraType == DroneCameraMode.PictureInPictureBottom)
			{
				labelStatusCamera.Text = "Bottom camera (PiP)";
				labelStatusCamera.Text = "Bottom (PiP)";
			}
		}

		private int GetCurrentFrameRate()
		{
			int timePassed = (int)(DateTime.Now - lastFrameRateCaptureTime).TotalMilliseconds;
			int frameRate = frameCountSinceLastCapture * 1000 / timePassed;
			averageFrameRate = (averageFrameRate + frameRate) / 2;

			lastFrameRateCaptureTime = DateTime.Now;
			frameCountSinceLastCapture = 0;

			return averageFrameRate;
		}

		private void UpdateInputState(InputState inputState)
		{
			//labelStatusSpecialAction.Text = inputState.SpecialAction.ToString();
		} 

		private void UpdateDroneState(InputState inputState)
		{
			CheckForIllegalCrossThreadCalls = false;
			//DroneData data = droneControl.NavigationData;

			//labelStatusPitch.Text = String.Format("{0:+0.000;-0.000;+0.000}", inputState.Roll);
			//labelStatusRoll.Text = String.Format("{0:+0.000;-0.000;+0.000}", inputState.Pitch);
			//labelStatusYaw.Text = String.Format("{0:+0.000;-0.000;+0.000}", inputState.Yaw);
			labelStatusGaz.Text = String.Format("{0:+0.000;-0.000;+0.000}", inputState.Gaz);
			//lastValueForSpecialAction = inputState.SpecialAction;

			UpdateCurrentBooleanInputState(inputState);
		}

		private void UpdateCurrentBooleanInputState()
		{
			RemoveOldBooleanInputStates();
		}

		private void UpdateCurrentBooleanInputState(InputState inputState)
		{
			RemoveOldBooleanInputStates();
			AddNewBooleanInputStates(inputState);
		}
		
		private void UpdateVideoImage()
		{
			if (droneControl.IsConnected)
			{
					 if ((Bitmap)droneControl.BitmapImage == null) return;
				Bitmap newImage = (Bitmap)droneControl.BitmapImage.Clone();

				if (newImage != null)
					 {
						  Image<Bgr, byte> image = new Image<Bgr, byte>(newImage);
						  opticalFlow.addFrame(image);
					frameCountSinceLastCapture++;

					if (videoRecorder.IsVideoCaptureRunning)
					{
						videoRecorder.AddFrame((System.Drawing.Bitmap)newImage.Clone());
					}
						  if (opticalFlow._prevOpticalFlowFrame != null) UpdateVisualImage(opticalFlow._prevOpticalFlowFrame.Bitmap);
					//UpdateVisualImage(newImage);

					//PerformStopSignDetection(newImage);
				}
			}
		}

		private void UpdateVisualImage(Bitmap image)
		{
			if (image != null)
			{
				imageVideo.Image = image;
			}
		}
		#endregion

		#region Boolean Input States
		private void AddNewBooleanInputStates(InputState inputState)
		{
			if (inputState.TakeOff)
				AddNewBooleanInputState("TakeOff");
			if (inputState.Land)
				AddNewBooleanInputState("Land");
			if (inputState.Emergency)
				AddNewBooleanInputState("Emergency");
			if (inputState.FlatTrim)
				AddNewBooleanInputState("FlatTrim");
			if (inputState.Hover)
				AddNewBooleanInputState("Hover");
			if (inputState.CameraSwap)
				AddNewBooleanInputState("Camera");
			if (inputState.SpecialAction)
				AddNewBooleanInputState("Special");
		}

		private void AddNewBooleanInputState(String command)
		{
			DateTime expirationDate = DateTime.Now + booleanInputTimeout;
			if (booleanInputFadeout.ContainsKey(command))
				booleanInputFadeout[command] = expirationDate;
			else
				booleanInputFadeout.Add(command, expirationDate);
		}

		private void RemoveOldBooleanInputStates()
		{
			List<String> keysToDelete = new List<String>();
			foreach (KeyValuePair<String, DateTime> keyValuePair in booleanInputFadeout)
			{
				String command = keyValuePair.Key;
				DateTime expirationDate = keyValuePair.Value;

				if (expirationDate < DateTime.Now)
					keysToDelete.Add(command);
			}

			foreach (String key in keysToDelete)
				booleanInputFadeout.Remove(key);
		}

		private String GetCurrentBooleanInputStates()
		{
			String commandText = "";

			List<String> commands = new List<String>();
			foreach (KeyValuePair<String, DateTime> keyValuePair in booleanInputFadeout)
				commands.Add(keyValuePair.Key);

			for (int i = 0; i < commands.Count; i++)
			{
				commandText += commands[i];

				if (i != commands.Count - 1)
					commandText += ", ";
			}

			if (commandText == "")
				commandText = "No buttons";

			return commandText;
		}
		#endregion

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

		/*private Bitmap CopyBitmap(Bitmap image)
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
		}*/

		#region Video Capture
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

			videoRecorder.StartVideo(videoFilePath, averageFrameRate, size.Width, size.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb, 4, checkBoxVideoCompress.Checked == true ? true : false);
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

		private bool CanCaptureVideo
		{
			get
			{
				return droneControl.CanSwitchCamera;
			}
		} 
		#endregion

		#region Dialog
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

		private void OpenDroneConfigDialog()
		{
			if (!droneControl.IsConnected)
				return;

			//DroneConfigurationOutput configOutput = new DroneConfigurationOutput(droneControl.InternalDroneConfiguration);
			//configOutput.ShowDialog();
		} 
		#endregion

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
		
		#region Error Handling
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
		#endregion

		#region Event Handlers
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
		#endregion

		#region Buttons
		private void buttonConnect_Click(object sender, EventArgs e)
		{
			Connect();
		}

		private void buttonShutdown_Click(object sender, EventArgs e)
		{
			Disconnect();
		}

		private void buttonCommandChangeCamera_Click(object sender, EventArgs e)
		{
			ChangeCamera();
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
			TakeSnapshot();
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
		#endregion

		#region Timers
		private void timerStatusUpdate_Tick(object sender, EventArgs e)
		{
			UpdateStatus();
		}

		private void timerVideoUpdate_Tick(object sender, EventArgs e)
		{
			UpdateVideoImage();
			PointF[] kalman = opticalFlow.syncKalmanData();

			if ( kalman != null )
				UpdateUIAsync(string.Join(".", kalman));

			// Movement calls
			if (droneControlAuto && droneControl.IsFlying)
			{
				// 160 is half the width
				float deltaYaw = kalman[0].X/160 - 1;
				Control.Commands.FlightMoveCommand movecmd = new Control.Commands.FlightMoveCommand(0, 0, deltaYaw, 0);
				droneControl.SendCommand(movecmd);
			}
		} 
		#endregion

		private void buttonToggleDroneCtrl_Click(object sender, EventArgs e)
		{
			droneControlAuto = !droneControlAuto;
			UpdateUIAsync("Drone Autopilot: " + droneControlAuto);
		}

		private void buttonNavData_Click(object sender, EventArgs e)
		{
			if (navDataRecorder.recording == false)
			{
				navDataRecorder.startRecording();
				buttonNavData.Text = "Pause Recording";
			}
			else
			{
				navDataRecorder.stopRecording();
				buttonNavData.Text = "Start Recording";
			}
		}

		private void buttonSaveNavData_Click(object sender, EventArgs e)
		{
			navDataRecorder.save("NavData");
		}

        private void haarCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (haarCheckBox.Checked == true)
                opticalFlow.isHaarDetectionVisible = true;
            else
                opticalFlow.isHaarDetectionVisible = false;
        }

        private void allPointsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (allPointsCheckBox.Checked == true)
                opticalFlow.isAllPointsVisible = true;
            else
                opticalFlow.isAllPointsVisible = false;
        }

        private void within100PixelsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (within100PixelsCheckBox.Checked == true)
                opticalFlow.isWithin100PixelsVisible = true;
            else
                opticalFlow.isWithin100PixelsVisible = false;
        }

        private void kfPredictedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (kfPredictedCheckBox.Checked == true)
                opticalFlow.isKalmanPredictVisible = true;
            else
                opticalFlow.isKalmanPredictVisible = false;
        }

        private void kfEstimatedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (kfEstimatedCheckBox.Checked == true)
                opticalFlow.isKalmanEstimatedVisible = true;
            else
                opticalFlow.isKalmanEstimatedVisible = false;
        }

        private void trackingVectorsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (trackingVectorsCheckBox.Checked == true)
                opticalFlow.isVectorsVisible = true;
            else
                opticalFlow.isVectorsVisible = false;
        }

        private void btnCheckAll_Click(object sender, EventArgs e)
        {
            haarCheckBox.Checked = true;
            allPointsCheckBox.Checked = true;
            within100PixelsCheckBox.Checked = true;
            kfPredictedCheckBox.Checked = true;
            kfEstimatedCheckBox.Checked = true;
            trackingVectorsCheckBox.Checked = true;
        }

        private void btnUncheckAll_Click(object sender, EventArgs e)
        {
            haarCheckBox.Checked = false;
            allPointsCheckBox.Checked = false;
            within100PixelsCheckBox.Checked = false;
            kfPredictedCheckBox.Checked = false;
            kfEstimatedCheckBox.Checked = false;
            trackingVectorsCheckBox.Checked = false;
        }
	}
}
