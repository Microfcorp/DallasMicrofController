using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace DallasMicrofController
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class ServerSetting
    {
        public const string Path = "DallasController\\ServerSettings";
        public ServerSetting(string Name)
        {
            this.Name = Name;
            IsAutoStart = false;
        }

        public string Name
        {
            get;
            set;
        }

        public bool IsAutoStart
        {
            get;
            set;
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void Save()
        {
            Settings.Save<ServerSetting>(new ServerSetting[] { this }, Path);
        }
        public static ServerSetting Load()
        {
            return Settings.Load<ServerSetting>(Path)[0];
        }
    }
}
