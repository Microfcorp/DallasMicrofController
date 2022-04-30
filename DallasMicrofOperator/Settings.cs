using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DallasMicrofOperator
{
    public partial class Settings : Form
    {
        Settingam set = Settingam.Load();
        Thread th;
        public Settings()
        {
            InitializeComponent();
        }

        void UpdateServers()
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox5.Items.Clear();
            foreach (var item in set.RemoteServers)
            {
                var n = Network.GetServerName(item);
                comboBox1.Items.Add(n == "" ? item : n);
                comboBox2.Items.Add(n == "" ? item : n);
                comboBox5.Items.Add(n == "" ? item : n);
                comboBox8.Items.Add(n == "" ? item : n);
                label6.Visible = n == "";
            }
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
                comboBox2.SelectedIndex = 0;
                comboBox8.SelectedIndex = 0;
            }
        }

        void UpdateAlarms()
        {
            comboBox3.Items.Clear();
            for (int i = 0; i < set.Alarms.Length; i++)
            {
                comboBox3.Items.Add(i);
            }
            if (comboBox3.Items.Count > 0)
                comboBox3.SelectedIndex = 0;
        }
        void UpdateColor()
        {
            numericUpDown3.Value = set.Red;
            numericUpDown4.Value = set.Yellow;

            checkBox2.Checked = set.IsLog;
        }

        void UpdateVisibler()
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var c = set.RemoteServers.ToList();
            c.Add("localhost");
            set.RemoteServers = c.ToArray();
            var a = set.TermometrID.ToList();
            a.Add("0");
            set.TermometrID = a.ToArray();
            var o = set.SensorSettings.ToList();
            o.Add(new SensorSettings(c.Count-1));
            set.SensorSettings = o.ToArray();
            set.Save();
            UpdateServers();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var c = set.RemoteServers.ToList();
            c.RemoveAt(comboBox1.SelectedIndex);
            set.RemoteServers = c.ToArray();
            var a = set.TermometrID.ToList();
            a.RemoveAt(comboBox1.SelectedIndex);
            set.TermometrID = a.ToArray();
            set.Save();
            UpdateServers();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            set.RemoteServers[comboBox1.SelectedIndex] = comboBox7.Text;
            set.Save();
            UpdateServers();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox7.Text = set.RemoteServers[comboBox1.SelectedIndex];

            comboBox6.Items.Clear();
            for (int i = 0; i < Network.GetServerNemberTermometrs(set.RemoteServers[comboBox1.SelectedIndex]); i++)
            {
                comboBox6.Items.Add(i);
            }
            if (comboBox6.Items.Count > int.Parse(set.TermometrID[comboBox1.SelectedIndex]))
                comboBox6.SelectAtIndex(int.Parse(set.TermometrID[comboBox1.SelectedIndex]));
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox2.Text = Network.GetServerName(set.RemoteServers[comboBox2.SelectedIndex]);
            if (textBox2.Text != "")
            {
                var nn = Network.GetServerInfo(set.RemoteServers[comboBox2.SelectedIndex]);
                comboBox4.SelectAtIndex(byte.Parse(nn.Split('\n').GetOfIndex(7, "9")) - 9);
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            Network.SetServerResol(set.RemoteServers[comboBox2.SelectedIndex], (comboBox4.SelectedIndex + 9).ToString(), set.TermometrID[comboBox2.SelectedIndex]);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Network.SetServerName(set.RemoteServers[comboBox2.SelectedIndex], textBox2.Text);
            UpdateServers();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var rm = Network.GetServerInfo(set.RemoteServers[comboBox2.SelectedIndex]).Split('\n');
            if (rm == null)
            {
                MessageBox.Show("Неизвестная ошибка");
                return;
            }
            string tmp = "Информация о термометре: " + Environment.NewLine;
            tmp += "Соединение с платой: " + (rm[0] == "True" ? "установленно" : "потеряно") + Environment.NewLine;
            tmp += "Серийный номер контроллера: " + rm[1] + Environment.NewLine;
            tmp += "Дата производства контроллера: " + rm[2] + Environment.NewLine;
            if (rm[0] == "True")
            {
                tmp += "Количество датчиков на линии: " + rm[3] + Environment.NewLine;
                for (byte i = 0, u = 4; i < byte.Parse(rm[3]); i++, u += 4)
                {
                    tmp += "-------------------" + Environment.NewLine;
                    tmp += "Опрашиваемый датчик: " + (i + 1) + Environment.NewLine;
                    tmp += "Ошибки датчика: " + (rm[u] == "True" ? "есть" : "нет") + Environment.NewLine;
                    if (rm[u] != "True")
                    {
                        tmp += "Фантомное питание: " + (rm[u + 1] == "True" ? "включено" : "выключено") + Environment.NewLine;
                        tmp += "Серийный номер датчика: " + rm[u + 2] + Environment.NewLine;
                        tmp += "Установленное разрешение: " + rm[u + 3] + " бит" + Environment.NewLine;
                    }
                    tmp += "-------------------" + Environment.NewLine;
                }
            }
            MessageBox.Show(tmp);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var c = set.Alarms.ToList();
            c.Add(new Alarm(0, -10, 5, false, ""));
            set.Alarms = c.ToArray();
            set.Save();
            UpdateAlarms();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var c = set.Alarms.ToList();
            c.RemoveAt(comboBox3.SelectedIndex);
            set.Alarms = c.ToArray();
            set.Save();
            UpdateAlarms();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            var al = set.Alarms[comboBox3.SelectedIndex];
            comboBox5.SelectedIndex = al.IDDM;
            numericUpDown1.Value = al.Minimum;
            numericUpDown2.Value = al.Maximum;
            checkBox1.Checked = al.Enable;
            textBox3.Text = al.File;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var al = set.Alarms[comboBox3.SelectedIndex];
            al.IDDM = comboBox5.SelectedIndex;
            al.Minimum = (int)numericUpDown1.Value;
            al.Maximum = (int)numericUpDown2.Value;
            al.Enable = checkBox1.Checked;
            al.File = textBox3.Text;
            set.Alarms[comboBox3.SelectedIndex] = al;
            set.Save();
            UpdateAlarms();
        }

        private void textBox3_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog opg = new OpenFileDialog();
            opg.Filter = "WAV File|*.wav";
            if (opg.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = opg.FileName;
            }
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            set.Red = (int)numericUpDown3.Value;
            set.Save();
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            set.Yellow = (int)numericUpDown4.Value;
            set.Save();
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            set.TermometrID[comboBox1.SelectedIndex] = comboBox6.Text;
            set.Save();
            UpdateServers();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            set.IsLog = checkBox2.Checked;
            set.Save();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            UpdateServers();
            UpdateAlarms();
            UpdateColor();
            UpdateVisibler();

            th = new Thread(() =>
            {
                Network.SearchServers((o, a) =>
                {
                    Invoke(new Action(() =>
                    {
                        foreach (var p in (o as string).Split('\n'))
                            if (!comboBox7.Items.Contains(p))
                                comboBox7.Items.Add(p);
                    }));
                });
            });
            //System.Timers.Timer t = new System.Timers.Timer();
            //t.Elapsed += (r, y) => { t.Stop(); th.Abort(); };
            //t.Interval = 2000;
            //t.Start();
            Disposed += (q, w) => th.Abort();
            th.Start();
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            th.Abort();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var c = set.SensorSettings.Where(tmp => tmp.IDDM == comboBox8.SelectedIndex).FirstOrDefault();
            if (c == default(SensorSettings)) return;
            int ind = set.SensorSettings.ToList().IndexOf(c);
            c.TimeUpdate = (uint)numericUpDown5.Value;
            c.UseAutoResize = checkBox3.Checked;
            c.UseSystemFont = checkBox4.Checked;
            c.BackgroungColor = selectColor;
            c.KFontSize = numericUpDown6.Value;
            set.SensorSettings[ind] = c;
            set.Save();
        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            var c = set.SensorSettings.Where(tmp => tmp.IDDM == comboBox8.SelectedIndex).FirstOrDefault();
            if (c == default(SensorSettings)) return;
            numericUpDown5.Value = c.TimeUpdate;
            checkBox3.Checked = c.UseAutoResize;
            checkBox4.Checked = c.UseSystemFont;
            selectColor = c.BackgroungColor;
            numericUpDown6.Value = c.KFontSize;
            panel1.CreateGraphics().Clear(selectColor);
        }

        Color selectColor;

        private void button11_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if(cd.ShowDialog() == DialogResult.OK)
            {
                panel1.CreateGraphics().Clear(cd.Color);
                selectColor = cd.Color;
            }
        }
    }
}
