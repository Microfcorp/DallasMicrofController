using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DallasMicrofOperator
{
    public class LogFile
    {
        public string Path;

        public LogFile()
        {
        }

        public LogFile(string path)
        {
            Path = path;
        }
    
        public void AddLog(params string[] data)
        {
            try
            {
                var t = DateTime.Now.ToString() + ";" + string.Join(";", data);
                File.AppendAllText(Path, t + Environment.NewLine);
            }
            catch { }
        }
    }
}
