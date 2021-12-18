﻿using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace DallasMicrofController
{
    [Serializable]
    public enum DallasResolution : byte
    {
        bit9 = 9,
        bit10 = 10,
        bit11 = 11,
    }
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class DallasSettings
    {
        public const string Path = "DallasController\\DallasSettings";
        public DallasSettings(string cOMPorts, DallasResolution resolution)
        {
            COMPorts = cOMPorts;
            Resolution = resolution;
        }

        public string COMPorts
        {
            get;
            set;
        }
        public DallasResolution Resolution
        {
            get;
            set;
        }

        public void SetCOM(string port)
        {
            COMPorts = port;
        }
        public void SetResolution(DallasResolution Resolutions)
        {
            Resolution = Resolutions;
        }

        public void Save()
        {
            Settings.Save<DallasSettings>(new DallasSettings[] { this }, Path);
        }
        public static DallasSettings Load()
        {
           return Settings.Load<DallasSettings>(Path)[0];
        }
    }
    public class Dallas
    {
        SerialPort _serialPort = new SerialPort();

        public struct Termometr
        {
            public bool ParasitePowers
            {
                get;
                internal set;
            }
            public string Address
            {
                get;
                internal set;
            }
            public float Temperature
            {
                get;
                internal set;
            }
            public bool IsError
            {
                get;
                internal set;
            }
            public DallasResolution CurrentResolution
            {
                get;
                internal set;
            }
        }

        public Dallas()
        {
            _serialPort.BaudRate = 115200;
            _serialPort.ReadTimeout = 5000;
            _serialPort.WriteTimeout = 5000;

            Termometrs = new Termometr[byte.MaxValue];
        }

        public DallasSettings Setting
        {
            get;
            set;
        }


        public byte DevicesOfLines
        {
            get;
            private set;
        }
        
        public Termometr[] Termometrs
        {
            get;
            private set;
        }

        public bool IsConnect
        {
            get
            {
                return _serialPort.IsOpen;
            }
        }


        public void Load()
        {
            if (Settings.IsFile(DallasSettings.Path))
            {
                Setting = DallasSettings.Load();
            }
            else
            {
                Setting = new DallasSettings("COM1", DallasResolution.bit9);
                Setting.Save();
            }
            //CurrentResolution = Setting.Resolution;
        }

        public void WriteCOM(string text)
        {
            if (IsConnect)
                _serialPort.WriteLine(text);
        }

        public void SendReadTemperature()
        {
            WriteCOM("pt;");
        }
        public void SendResolution()
        {
            WriteCOM("sr"+(byte)Setting.Resolution);
            System.Threading.Thread.Sleep(500);
            SendReadInformation();
        }
        public void SendReadInformation()
        {
            WriteCOM("pi;");
        }
        public void SendReadPing()
        {
            WriteCOM("ping;");
        }

        public void Read()
        {
            if (!_serialPort.IsOpen) return;
            string reads = "";
            try
            {
                reads = _serialPort.ReadTo(";").Replace("\r", "").Trim();
            }
            catch (Exception ex) { return; }
            if (reads.Contains("Requesting temperatures..."))
            {
                var deviceid = byte.Parse(reads.Split('\n')[1].Split(' ')[1]);
                Termometrs[deviceid].Temperature = float.Parse(reads.Split('\n')[1].Split(':')[1].Trim().Replace('.', ','));
            }
            else if (reads.Contains("Informations:"))
            {
                DevicesOfLines = byte.Parse(reads.Split('\n')[1].Split(' ')[1].Trim());
                var list = Termometrs.ToList();
                list.RemoveRange(DevicesOfLines, list.Count - DevicesOfLines);
                Termometrs = list.ToArray();

                var deviceid = byte.Parse(reads.Split('\n')[2].Split(' ')[4].Trim(':'));
                Termometrs[deviceid].ParasitePowers = reads.Split('\n')[2].Split(':')[1].Trim() == "ON";

                if (reads.Contains("Unable to find address for Device")) Termometrs[deviceid].IsError = true;
                else
                {
                    Termometrs[deviceid].Address = reads.Split('\n')[3].Split(':')[1].Trim();
                    Termometrs[deviceid].CurrentResolution = (DallasResolution)byte.Parse(reads.Split('\n')[4].Split(':')[1].Trim());
                }
            }
        }

        public void Connect()
        {
            if (!_serialPort.IsOpen)
            {
                _serialPort.PortName = Setting.COMPorts;
                _serialPort.Open();
                SendReadInformation();
                Read();
            }
        }
        public void Close()
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.Close();
            }
        }
    }
}
