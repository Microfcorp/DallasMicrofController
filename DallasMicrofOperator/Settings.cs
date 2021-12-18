using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DallasMicrofOperator
{
    public partial class Settings : Form
    {
        Settingam set = Settingam.Load();
        public Settings()
        {
            InitializeComponent();
            UpdateServers();
            UpdateAlarms();
            UpdateColor();
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
                label6.Visible = n == "";
            }
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
                comboBox2.SelectedIndex = 0;
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

        private void button2_Click(object sender, EventArgs e)
        {
            var c = set.RemoteServers.ToList();
            c.Add("localhost");
            set.RemoteServers = c.ToArray();
            var a = set.TermometrID.ToList();
            a.Add("0");
            set.TermometrID = a.ToArray();
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
            set.RemoteServers[comboBox1.SelectedIndex] = textBox1.Text;
            set.Save();
            UpdateServers();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = set.RemoteServers[comboBox1.SelectedIndex];

            comboBox6.Items.Clear();
            for (int i = 0; i < Network.GetServerNemberTermometrs(set.RemoteServers[comboBox1.SelectedIndex]); i++)
            {
                comboBox6.Items.Add(i);
            }
            comboBox6.SelectedIndex = int.Parse(set.TermometrID[comboBox1.SelectedIndex]);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox2.Text = Network.GetServerName(set.RemoteServers[comboBox2.SelectedIndex]);
            if(textBox2.Text != "")
            comboBox4.SelectedIndex = byte.Parse(Network.GetServerInfo(set.RemoteServers[comboBox2.SelectedIndex]).Split('\n')[4]) - 9;
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
            if(rm == null)
            {
                MessageBox.Show("Неизвестная ошибка");
                return;
            }
            string tmp = "Информация о термометре: " + Environment.NewLine;
            tmp += "Соединение с платой: " + (rm[0] == "True" ? "установленно" : "потеряно") + Environment.NewLine;
            if (rm[0] == "True")
            {
                tmp += "Количество датчиков на линии: " + rm[1] + Environment.NewLine;
                for (byte i = 0, u = 2; i < byte.Parse(rm[1]); i++, u += 3)
                {
                    tmp += "-------------------" + Environment.NewLine;
                    tmp += "Опрашиваемый датчик: " + (i+1) + Environment.NewLine;
                    tmp += "Ошибки датчика: " + (rm[u] == "True" ? "есть" : "нет") + Environment.NewLine;
                    tmp += "Адрес датчика: " + rm[u+1] + Environment.NewLine;
                    tmp += "Установленное разрешение: " + rm[u+2] + " бит" + Environment.NewLine;
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
            if(opg.ShowDialog() == DialogResult.OK)
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
    }
}
