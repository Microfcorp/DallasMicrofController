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
        public Form1()
        {
            InitializeComponent();
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
                        //Thread.Sleep(1000);
                    }
                }
            });          
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

            toolStripTextBox1.Text = srv.Setting.Name;
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
                dallas.Connect();
                timer1.Start();
                timer2.Start();
                if (!isThreadRun) th.Start();
                установитьСоединениеToolStripMenuItem.Text = "Разорвать соединение";

                flowLayoutPanel1.Controls.Clear();
                for (int i = 0; i < dallas.Termometrs.Length; i++)
                {
                    flowLayoutPanel1.Controls.Add(new Temperature() { Temerature = "----" });
                }               
            }
            else
            {
                dallas.Close();
                timer1.Stop();
                timer2.Stop();
                установитьСоединениеToolStripMenuItem.Text = "Установить соединение";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Console.WriteLine(dallas.Temperature);
            //label2.Text = dallas.Termometrs[0].Temperature.ToString() + "°C";
            for (int i = 0; i < dallas.Termometrs.Length; i++)
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
            string tmp = "Информация о термометре: "+Environment.NewLine;
            tmp += "Соединение с платой: "+(dallas.IsConnect ? "установленно" : "потеряно")+Environment.NewLine;
            if (dallas.IsConnect)
            {
                tmp += "Количество датчиков на линии: " + dallas.DevicesOfLines + Environment.NewLine;
                for (byte i = 0; i < dallas.DevicesOfLines; i++)
                {
                    tmp += "-------------------" + Environment.NewLine;
                    tmp += "Опрашиваемый датчик: "+ (i+1) + Environment.NewLine;
                    tmp += "Ошибки датчика: " + (dallas.Termometrs[i].IsError ? "есть" : "нет") + Environment.NewLine;
                    tmp += "Адрес датчика: " + dallas.Termometrs[i].Address + Environment.NewLine;
                    tmp += "Установленное разрешение: " + (byte)dallas.Termometrs[i].CurrentResolution + " бит" + Environment.NewLine;
                    tmp += "-------------------" + Environment.NewLine;
                }               
            }
            MessageBox.Show(tmp);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            dallas.SendReadInformation();
        }

        private void стартToolStripMenuItem_Click(object sender, EventArgs e)
        {
            srv.Start();
        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {           
            srv.Setting.SetName(toolStripTextBox1.Text);
            Text = "DallasMicrofController - " + srv.Setting.Name;
            srv.Setting.Save();
        }
    }
}
