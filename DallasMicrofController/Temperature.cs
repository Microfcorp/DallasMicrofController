using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DallasMicrofController
{
    public partial class Temperature : UserControl
    {
        public Temperature()
        {
            InitializeComponent();
        }
        public string Temerature
        {
            get
            {
                return label2.Text;
            }
            set
            {
                label2.Text = value;
            }
        }
    }
}
