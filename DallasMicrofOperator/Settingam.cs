using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace DallasMicrofOperator
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class Settingam
    {
        public const string Path = "DallasController\\OperatorSettings";

        public Settingam()
        {
            RemoteServers = new string[0];
            TermometrID = new string[0];
            Alarms = new Alarm[0];
            Red = 50;
            Yellow = 30;
            IsLog = false;
        }

        public string[] RemoteServers
        {
            get;
            set;
        }
        public string[] TermometrID
        {
            get;
            set;
        }
        public Alarm[] Alarms
        {
            get;
            set;
        }

        public int Red
        {
            get;
            set;
        }
        public int Yellow
        {
            get;
            set;
        }
        public bool IsLog
        {
            get;
            set;
        }

        public void Save()
        {
            ST.Save<Settingam>(new Settingam[] { this }, Path);
        }
        public static Settingam Load()
        {
            if (ST.IsFile(Path))
            {
                return ST.Load<Settingam>(Path)[0];
            }
            return new Settingam();
        }
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class Alarm
    {
        public int IDDM;
        public int Minimum;
        public int Maximum;
        public bool Enable;
        public string File;

        public Alarm()
        {
        }

        public Alarm(int iDDM, int minimum, int maximum, bool enable, string file)
        {
            IDDM = iDDM;
            Minimum = minimum;
            Maximum = maximum;
            Enable = enable;
            File = file;
        }
    }
}
