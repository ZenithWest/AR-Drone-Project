using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Timers;

using ARDrone.Control;
using ARDrone.Control.Commands;
using ARDrone.Control.Data;
using ARDrone.Control.Events;
using ARDrone.Input;
using ARDrone.Input.Utils;
using ARDrone.Input.InputMappings;
using ARDrone.Capture;
using System.IO;

namespace ARDrone.NavData
{
	class NavDataRecorder
	{
		XElement xNavData;
		DroneControl droneControl;
		InputManager inputManager;
		Timer navDataTimer;
		long dataPoints = 0;
		DateTime startTime;
		public bool recording = false;

		public NavDataRecorder(DroneControl droneControl, InputManager inputManager, double interval=50)
		{
			xNavData = new XElement("NavData");
			dataPoints = 0;
			navDataTimer = new Timer(interval);
			navDataTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
			
			this.droneControl = droneControl;
			this.inputManager = inputManager;
		}

		private void OnTimedEvent(object source, ElapsedEventArgs e)
		{
			addNavDataPoint();
		}

		public void startRecording()
		{
			recording = true;
			startTime = DateTime.Now;
			navDataTimer.Start();
			//navDataTimer.Enabled = true;
		}

		public void stopRecording()
		{
			recording = false;
			//navDataTimer.Enabled = false;
			navDataTimer.Stop();
		}

		public void addNavDataPoint()
		{
			InputState inputState = inputManager.lastInputState;
			DroneData droneData = droneControl.NavigationData;

			XElement navDataPoint = new XElement("Data");
			
			navDataPoint.Add(
				new XElement("Info",
					new XElement("TimeStamp", DateTime.Now.ToString()),
					new XElement("Time_ms", (DateTime.Now - startTime).TotalMilliseconds),
					new XElement("DataPoint", dataPoints++)
				)
			);

			if (inputState != null)
			{
				navDataPoint.Add(
					new XElement("InputState",
						new XElement("Roll", inputState.Roll),
						new XElement("Pitch", inputState.Pitch),
						new XElement("Yaw", inputState.Yaw),
						new XElement("Gaz", inputState.Gaz),
						new XElement("CameraSwap", inputState.CameraSwap),
						new XElement("Emergency", inputState.Emergency),
						new XElement("FlatTrim", inputState.FlatTrim),
						new XElement("Hover", inputState.Hover),
						new XElement("Land", inputState.Land),
						new XElement("TakeOff", inputState.TakeOff)
					)
				);
			}

			if (droneData != null)
			{
				navDataPoint.Add(
					new XElement("DroneData",
						new XElement("Theta", droneData.Theta),
						new XElement("Phi", droneData.Phi),
						new XElement("Psi", droneData.Psi),
						new XElement("VX", droneData.VX),
						new XElement("VY", droneData.VY),
						new XElement("VZ", droneData.VZ),
						new XElement("Altitude", droneData.Altitude),
						new XElement("BatteryLevel", droneData.batteryLevel)
					)
				);
			}

			xNavData.Add(navDataPoint);
			
		}

		public void save(string filename)
		{
			
			saveToXML(filename);
			saveToCVS(filename);
		}

		public void saveToXML(string fileName)
		{
			xNavData.Save(fileName + ".xml");
		}

		public void saveToCVS(string fileName)
		{
			string cvs = "";
			cvs += "TimeStamp,";
			cvs += "Time_ms,";
			cvs += "DataPoint,";
			cvs += "Roll,";
			cvs += "Pitch,";
			cvs += "Yaw,";
			cvs += "Gaz,";
			cvs += "CameraSwap,";
			cvs += "Emergency,";
			cvs += "FlatTrim,";
			cvs += "Hover,";
			cvs += "Land,";
			cvs += "TakeOff,";
			cvs += "Theta,";
			cvs += "Phi,";
			cvs += "Psi,";
			cvs += "VX,";
			cvs += "VY,";
			cvs += "VZ,";
			cvs += "Altitude,";
			cvs += "BatteryLevel,\n";
			foreach (XElement dataPoint in xNavData.Elements())
			{
				cvs += dataPoint.Element("Info").Element("TimeStamp").Value + ",";
				cvs += dataPoint.Element("Info").Element("Time_ms").Value + ",";
				cvs += dataPoint.Element("Info").Element("DataPoint").Value + ",";
				cvs += dataPoint.Element("InputState").Element("Roll").Value + ",";
				cvs += dataPoint.Element("InputState").Element("Pitch").Value + ",";
				cvs += dataPoint.Element("InputState").Element("Yaw").Value + ",";
				cvs += dataPoint.Element("InputState").Element("Gaz").Value + ",";
				cvs += dataPoint.Element("InputState").Element("CameraSwap").Value + ",";
				cvs += dataPoint.Element("InputState").Element("Emergency").Value + ",";
				cvs += dataPoint.Element("InputState").Element("FlatTrim").Value + ",";
				cvs += dataPoint.Element("InputState").Element("Hover").Value + ",";
				cvs += dataPoint.Element("InputState").Element("TakeOff").Value + ",";
				cvs += dataPoint.Element("DroneData").Element("Theta").Value + ",";
				cvs += dataPoint.Element("DroneData").Element("Phi").Value + ",";
				cvs += dataPoint.Element("DroneData").Element("Psi").Value + ",";
				cvs += dataPoint.Element("DroneData").Element("VX").Value + ",";
				cvs += dataPoint.Element("DroneData").Element("VY").Value + ",";
				cvs += dataPoint.Element("DroneData").Element("VZ").Value + ",";
				cvs += dataPoint.Element("DroneData").Element("Altitude").Value + ",";
				cvs += dataPoint.Element("DroneData").Element("BatteryLevel").Value + "\n";
			}

			using (StreamWriter outfile = new StreamWriter(fileName + ".csv"))
			{
				outfile.Write(cvs);
			}
			
			int abc = 0;
		}

	}
}
