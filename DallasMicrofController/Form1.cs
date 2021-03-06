using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace DallasMicrofController
{
  
    public partial class Form1 : Form
    {
        Dallas dallas = new Dallas();
        Server srv = new Server();
        Thread th;
        bool IsClose = false;
        bool isThreadRun = false;
        Parser args;
        public Form1()
        {
            InitializeComponent();           
        }
        public Form1(string[] args) : this()
        {
            this.args = new Parser(args);
            Parser.Global = this.args;

            dallas.Load();
            srv.Load();
            srv.ds = dallas;

            if (srv.Setting.Name != "") Text = "DallasMicrofController - " + srv.Setting.Name;

            th = new Thread(() =>
            {
                isThreadRun = true;
                while (!IsClose)
                {
                    if (dallas.IsConnect)
                    {
                        dallas.Read();
                        Thread.Sleep(100);
                    }
                }
            });

            dallas.ConnectError += CE;
            dallas.TermometrEdit += TE;
            dallas.Key = "ThisCntr";

            srv.StatusChange += (o, e) => 
            {
                try
                {
                    if (e.Status == ServerStatus.ServerStart)
                        Status = "Сервер запущен";
                    else if (e.Status == ServerStatus.ServerStop)
                        Status = "Сервер остановлен";
                    else if (e.Status == ServerStatus.ServerError)
                        Status = "Ошибка сервера";
                }
                catch { Console.WriteLine("StatusChange - Error"); }
            };
        }

        private string Status
        {
            get => label2.Text;
            set => Invoke(new Action(() => label2.Text = value));
        }

        void CE(object sender, EventArgs e)
        {
            dallas = new Dallas();
            dallas.Load();
            dallas.ConnectError += CE;
            dallas.TermometrEdit += TE;
            StopConnect();
        }

        void TE(object sender, EventArgs e)
        {
            Invoke(new Action(() => {
                Size = MinimumSize;
                flowLayoutPanel1.Controls.Clear();
                for (int i = 0; i < dallas.Termometrs.Length; i++)
                {
                    var y = new Temperature() { Temerature = "----" };
                    flowLayoutPanel1.Controls.Add(y);
                    if(i != 0)Size += new Size(0, y.Height);
                }                
            }));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (var item in SerialPort.GetPortNames())
            {
                var it = new ToolStripMenuItem(item, null, (o, q) =>
                {
                    var port = (o as ToolStripMenuItem);
                    foreach (ToolStripMenuItem qq in cOMПортToolStripMenuItem.DropDownItems)
                        qq.Checked = false;
                    port.Checked = true;
                    dallas.Setting.SetCOM(port.Text);
                    dallas.Setting.Save();
                });
                if (item == dallas.Setting.COMPorts)
                    it.Checked = true;
                cOMПортToolStripMenuItem.DropDownItems.Add(it);
            }

            if (dallas.Setting.Resolution == DallasResolution.bit9) битToolStripMenuItem.Checked = true;
            else if (dallas.Setting.Resolution == DallasResolution.bit10) битToolStripMenuItem1.Checked = true;
            else if (dallas.Setting.Resolution == DallasResolution.bit11) битToolStripMenuItem2.Checked = true;

            запускатьАвтоматическиToolStripMenuItem.Checked = srv.Setting.IsAutoStart;

            toolStripTextBox1.Text = srv.Setting.Name;

            toolTip1.SetToolTip(label4, Server.GetAllCurrentIP());
            label4.Text = Server.GetCurrentIP()[0];

            if (srv.Setting.IsAutoStart)
            {
                установитьСоединениеToolStripMenuItem_Click(null, null);
            }
        }

        private void SetResol(object sender, EventArgs e)
        {
            byte bit = byte.Parse((sender as ToolStripMenuItem).Text.Split(' ').FirstOrDefault());
            dallas.Setting.SetResolution((DallasResolution)bit);
            dallas.Setting.Save();
            dallas.SendResolution();
        }

        private void установитьСоединениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!dallas.IsConnect) {
                if (dallas.Connect())
                {
                    timer2.Start();
                    if (!isThreadRun) th.Start();
                    установитьСоединениеToolStripMenuItem.Text = "Разорвать соединение";
                    flowLayoutPanel1.Controls.Add(new Temperature() { Temerature = "----" });
                    timer1.Start();
                    if (srv.Setting.IsAutoStart)
                    {
                        srv.Start();
                        стартToolStripMenuItem.Text = "Стоп";
                    }
                }
                else
                    MessageBox.Show("Ошибка соединения");
            }
            else
            {
                StopConnect();
            }
        }

        void StopConnect()
        {
            if (dallas.IsConnect) dallas.Close();
            timer1.Stop();
            timer2.Stop();
            установитьСоединениеToolStripMenuItem.Text = "Установить соединение";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Console.WriteLine(dallas.Temperature);
            //label2.Text = dallas.Termometrs[0].Temperature.ToString() + "°C";
            for (int i = 0; i < Math.Min(flowLayoutPanel1.Controls.Count, dallas.Termometrs.Length); i++)
            {
                (flowLayoutPanel1.Controls[i] as Temperature).Temerature = dallas.Termometrs[i].Temperature.ToString() + "°C";
            }
            //dallas.SendReadTemperature();

            битToolStripMenuItem.Checked = dallas.Setting.Resolution == DallasResolution.bit9;
            битToolStripMenuItem1.Checked = dallas.Setting.Resolution == DallasResolution.bit10;
            битToolStripMenuItem2.Checked = dallas.Setting.Resolution == DallasResolution.bit11;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            IsClose = true;
            srv.IsClose = true;
            srv.Dispose();
            dallas.Close();
            Process.GetCurrentProcess().Close();
        }

        private void информацияОТермометреToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string tmp = "Информация о контроллере: "+Environment.NewLine;
            tmp += "Соединение с платой: "+(dallas.IsConnect ? "установленно" : "потеряно")+Environment.NewLine;
            tmp += "Серийный номер контроллера: " + dallas.SN + Environment.NewLine;
            tmp += "Дата производства контроллера: " + dallas.DateProduct + Environment.NewLine;
            if (dallas.IsConnect)
            {
                tmp += "Количество датчиков на линии: " + dallas.DevicesOfLines + Environment.NewLine;
                for (byte i = 0; i < dallas.DevicesOfLines; i++)
                {
                    tmp += "-------------------" + Environment.NewLine;
                    tmp += "Опрашиваемый датчик: "+ (i+1) + Environment.NewLine;
                    tmp += "Ошибки датчика: " + (dallas.Termometrs[i].IsError ? "есть" : "нет") + Environment.NewLine;
                    tmp += "Фантомное питание: " + (dallas.Termometrs[i].ParasitePowers ? "включено" : "выключено") + Environment.NewLine;
                    tmp += "Серийный номер датчика: " + dallas.Termometrs[i].Address + Environment.NewLine;
                    tmp += "Установленное разрешение: " + (byte)dallas.Termometrs[i].CurrentResolution + " бит" + Environment.NewLine;
                    tmp += "-------------------" + Environment.NewLine;
                }               
            }
            MessageBox.Show(tmp);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            dallas.CheckConnect_AND_Read();
            dallas.SendReadInformation();
        }

        private void стартToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!srv.IsRun)
            {
                if (dallas.IsCorectSN)
                {
                    srv.Start();
                    стартToolStripMenuItem.Text = "Стоп";
                }
                else
                {
                    MessageBox.Show("Запустить сервер можно только с верифицированным контроллером");
                }
            }
            else
            { 
                srv.Dispose();
                стартToolStripMenuItem.Text = "Старт";
            }
        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            bool h;
            srv.Setting.SetName(toolStripTextBox1.Text);
            Text = "DallasMicrofController - " + srv.Setting.Name + " - " + Parser.Global.FindParamsAndArgs("-p", out h);
            srv.Setting.Save();
        }

        private void добавитьВАвтозапускToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoStart.SetAutorunValue(true);
        }

        private void удалитьИзАвтозапускаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoStart.SetAutorunValue(false);
        }

        private void запускатьАвтоматическиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            запускатьАвтоматическиToolStripMenuItem.Checked = !запускатьАвтоматическиToolStripMenuItem.Checked;
            srv.Setting.IsAutoStart = !srv.Setting.IsAutoStart;
            srv.Setting.Save();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            ShowInTaskbar = !(this.WindowState == FormWindowState.Minimized);
            notifyIcon1.Visible = this.WindowState == FormWindowState.Minimized;
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }
    }
}
