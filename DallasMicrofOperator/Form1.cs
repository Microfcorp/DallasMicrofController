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
    public partial class Form1 : Form
    {
        Settingam set = Settingam.Load();
        public Form1()
        {
            InitializeComponent();
        }

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var t = new Settings();
            t.FormClosing += (a, b) =>
            {
                foreach (var item in flowLayoutPanel1.Controls)
                {
                    if (item is Sensor s)
                        s.Reload();
                }
                //LoadCnc();
            };
            t.Show();
        }

        void LoadCnc()
        {
            flowLayoutPanel1.Controls.Clear();
            for (int i = 0; i < set.RemoteServers.Length; i++)
            {
                var s = new Sensor((uint)i);
                flowLayoutPanel1.Controls.Add(s);
            }
            flowLayoutPanel1.ResizeF();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadCnc();
        }

        private void flowLayoutPanel1_Resize(object sender, EventArgs e)
        {
            flowLayoutPanel1.ResizeF();
        }
    }
}
