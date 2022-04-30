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

        bool IsDefaultFonts = false;

        public Sensor()
        {
            InitializeComponent();
            try
            {
                AddFont("7 Segment.ttf");
                AddFont("7LED.ttf");
            }
            catch { IsDefaultFonts = true; }
        }
        Color BackColors = Color.Black;
        bool AutoResize;
        decimal kF;
        public Sensor(uint Selected) : this()
        {
            id = Selected;

            try
            {
                var c = Settingam.Load().SensorSettings.Where(tmp => tmp.IDDM == Selected).FirstOrDefault();
                if (c != default(SensorSettings))
                {
                    timer1.Interval = (int)c.TimeUpdate * 1000;
                    if (c.UseSystemFont) IsDefaultFonts = true; //наоборот незя
                    BackColors = c.BackgroungColor;
                    AutoResize = c.UseAutoResize;
                    kF = c.KFontSize;
                    if (!AutoResize)
                    {
                        Label lb = new Label();
                        lb.Name = "ShefTTT";
                        lb.AutoSize = true;
                        lb.Location = new Point(0, 0);
                        lb.BackColor = BackColors;
                        lb.ForeColor = Color.Aqua;
                        lb.TextAlign = ContentAlignment.MiddleCenter;
                        lb.Dock = DockStyle.Fill;
                        lb.Anchor = AnchorStyles.Top & AnchorStyles.Right & AnchorStyles.Bottom & AnchorStyles.Left;
                        lb.Font = new Font(IsDefaultFonts ? FontFamily.GenericMonospace : _privateFontCollection.Families.GetOfIndex(0, FontFamily.GenericMonospace), 32f);
                        panel1.Controls.Add(lb);
                        //lb.XCentre();
                        //lb.YCentre();
                    }
                }
            }
            catch { }

            timer1.Start();
        }

        public void Reload()
        {
            set = Settingam.Load();
            label4.Visible = set.Alarms.Where(tmp => tmp.Enable && tmp.IDDM == id).Any();
        }

        void DrawNumbers(Graphics g, string number)
        {
            float FS1 = ((Width > 400 ? Width : 400) / number.Length-1) / 1.3f;
            float FS2 = ((Height > 400 ? Height : 400) / number.Length-1) / 1f;
            float FS = Math.Max(FS1, FS2) * (float)kF;
            Brush br = Brushes.Green;
            if (temper != "" && temper != "----" && float.Parse(temper) >= set.Red)
                br = Brushes.Red;
            else if (temper != "" && temper != "----" && float.Parse(temper) >= set.Yellow)
                br = Brushes.Yellow;
            else if (temper != "" && temper != "----" && float.Parse(temper) < 0)
                br = Brushes.MediumBlue;
            else if (temper == "" || temper == "----")
                br = Brushes.Blue;
            g.Clear(BackColors);

            if (AutoResize)
            {
                g.DrawString(number, new Font(IsDefaultFonts ? FontFamily.GenericMonospace : _privateFontCollection.Families.GetOfIndex(0, FontFamily.GenericMonospace), FS, FontStyle.Regular), br, panel1.Width / 2f - (number.Length * FS) / 2.8f, panel1.ClientSize.Height / 5f);
                //g.DrawString("< " + set.Alarms.Min(tmp => tmp.Minimum).ToString() + " " + "> " + set.Alarms.Max(tmp => tmp.Maximum).ToString(), new Font(_privateFontCollection.Families[1], FS, FontStyle.Regular), Brushes.Gray, new PointF(panel1.Width / 2f - (number.Length * FS)/2.8f, panel1.Height - FS - 10f));
            }
            else
            { 
                var lb = panel1.Controls.Find("ShefTTT", false)[0] as Label;
                lb.Font = new Font(IsDefaultFonts ? FontFamily.GenericMonospace : _privateFontCollection.Families.GetOfIndex(0, FontFamily.GenericMonospace), FS);                
                lb.ForeColor = new Pen(br).Color;
                lb.Text = number;
                lb.XCentre();
                lb.YCentre();
            }
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
            if(set.IsLog) data.AddLog("Temperature:", set.RemoteServers[id], temper);
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
