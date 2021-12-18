using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DallasMicrofOperator
{
    public class OrientationSensor
    {
        public static OrientationType GetOrientationType(SizeF DisplaySize)
        {
            //1280x1024 = 1,25 = 5:4
            //1920x1080 = 1,7 = 16:9
            //1024×768 = 1,3 = 4:3
            if ((DisplaySize.Width / DisplaySize.Height) >= 1.6) return OrientationType.Horizontal;
            else if ((DisplaySize.Width / DisplaySize.Height) <= 1.5) return OrientationType.Vertical;
            else return OrientationType.Error;
        }

        public static OrientationType CurentOrientation
        {
            get
            {
                return GetOrientationType(Screen.PrimaryScreen.Bounds.Size);
            }
        }
    }
}
