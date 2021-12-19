using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Drawing.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Media;

namespace DallasMicrofOperator
{
    public partial class Sensor : UserControl
    {
        Settingam set = Settingam.Load();
        public uint id;
        Graphics graphics;
        string temper = "----";

        LogFile data = new LogFile("DataLog.csv");
        public Sensor()
        {
            InitializeComponent();
            AddFont("7 Segment.ttf");           
            AddFont("7LED.ttf");                      
        }
        public Sensor(uint Selected) : this()
        {
            id = Selected;
            timer1.Start();
        }

        public void Reload()
        {
            set = Settingam.Load();
            label4.Visible = set.Alarms.Where(tmp => tmp.Enable && tmp.IDDM == id).Any();
        }

        void DrawNumbers(Graphics g, string number)
        {
            float FS = ((Width > 500 ? Width : 500) / number.Length) / 2f;
            Brush br = Brushes.Green;
            if (temper != "" && temper != "----" && float.Parse(temper) >= set.Red)
                br = Brushes.Red;
            else if (temper != "" && temper != "----" && float.Parse(temper) >= set.Yellow)
                br = Brushes.Yellow;
            else if (temper != "" && temper != "----" && float.Parse(temper) < 0)
                br = Brushes.MediumBlue;
            else if (temper == "" || temper == "----")
                br = Brushes.Blue;
            g.Clear(Color.Black);
            g.DrawString(number, new Font(_privateFontCollection.Families[0], FS, FontStyle.Regular), br, panel1.Width / 2f - (number.Length * FS)/2.8f, panel1.ClientSize.Height / 5f);
            //g.DrawString("< " + set.Alarms.Min(tmp => tmp.Minimum).ToString() + " " + "> " + set.Alarms.Max(tmp => tmp.Maximum).ToString(), new Font(_privateFontCollection.Families[1], FS, FontStyle.Regular), Brushes.Gray, new PointF(panel1.Width / 2f - (number.Length * FS)/2.8f, panel1.Height - FS - 10f));
        }

        private PrivateFontCollection _privateFontCollection = new PrivateFontCollection();

        public FontFamily GetFontFamilyByName(string name)
        {
            return _privateFontCollection.Families.FirstOrDefault(x => x.Name == name);
        }

        public void AddFont(string fullFileName)
        {
            AddFont(File.ReadAllBytes(fullFileName));
        }

        public void AddFont(byte[] fontBytes)
        {
            var handle = GCHandle.Alloc(fontBytes, GCHandleType.Pinned);
            IntPtr pointer = handle.AddrOfPinnedObject();
            try
            {
                _privateFontCollection.AddMemoryFont(pointer, fontBytes.Length);
            }
            finally
            {
                handle.Free();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            GetName();
            try
            {
                new Thread(() => { 
                var temperature = Network.GetServerTemepature(set.RemoteServers[id], set.TermometrID[id]);
                if (InvokeRequired && IsHandleCreated) Invoke(new Action(() => {
                    if (temperature == "")
                    {
                        temper = "";
                        label2.Text = "Ошибка соединения";
                    }
                    else
                    {
                        temper = temperature; //+ "°C"
                        label2.Text = DateTime.Now.ToString();
                    }
                    Print();
                }));
                    CheckAlarm();
            }).Start();
            }
            catch { }
            //Print();
        }

        bool IsAlarmed;

        void CheckAlarm()
        {
            var al = set.Alarms.Where(tmp => tmp.Enable && tmp.IDDM == id && !IsAlarmed && temper != "" && (float.Parse(temper) >= tmp.Maximum || float.Parse(temper) <= tmp.Minimum)).ToArray();
            foreach (var item in al)
            {
                IsAlarmed = true;
                if (set.IsLog) data.AddLog("Alarm Detected", set.RemoteServers[id], temper, item.Minimum.ToString(), item.Maximum.ToString());
                new Thread(() => MessageBox.Show("Внимание! Сработала тревога по устройству " + Network.GetServerName(set.RemoteServers[item.IDDM]) + Environment.NewLine + "С температурой " + temper)).Start();
                SoundPlayer sp = new SoundPlayer(item.File);
                sp.PlaySync();
                IsAlarmed = false;
            }
        }

        void Print()
        {
            DrawNumbers(graphics, temper != "" && temper != "----" ? temper.Replace(',', '\'') + "°C" : "----");
            if(set.IsLog) data.AddLog("Temperature Read", set.RemoteServers[id], temper);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //graphics = e.Graphics;
            //DrawNumbers(graphics, "----");
            Print();
        }

        private void Sensor_Resize(object sender, EventArgs e)
        {
            graphics = panel1.CreateGraphics();         
            label3.XCentre();
            panel1.XCentre();
            panel1.YCentre();
            Print();
        }

        void GetName()
        {
            try
            {
                new Thread(() =>
                {
                    var Name = Network.GetServerName(set.RemoteServers[id]);
                    if (InvokeRequired && IsHandleCreated) Invoke(new Action(() =>
                    {
                        if (Name == "")
                        {
                            //DrawNumbers(graphics, "----");
                            //label2.Text = "Ошибка соединения";
                        }
                        else
                        {
                            label3.Text = Name;
                            label3.XCentre();
                        }
                    }));
                }).Start();
            }
            catch { }           
        }

        private void Sensor_Load(object sender, EventArgs e)
        {
            Reload();
        }
    }
}
