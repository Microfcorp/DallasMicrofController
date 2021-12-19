using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DallasMicrofOperator
{
    public static class WindowManager
    {       
        /// <summary>
        /// Маштабирует пропорционально элемент управления в родительском элементе управления
        /// </summary>
        /// <param name="panel">Элемент маштабирования</param>
        /// <param name="Parent">Родительский элемент</param>
        /// <param name="SizeSET">Число всех элементов</param>
        public static void SizeOrient(this Control panel, Control Parent, int SizeSET)
        {
            if (true)
            {
                int Padding = 10;
                if (OrientationSensor.GetOrientationType(Parent.Size) == OrientationType.Vertical) panel.Size = new Size((Parent.ClientSize.Width) - Padding, (Parent.ClientSize.Height / SizeSET) - Padding);
                else if (OrientationSensor.GetOrientationType(Parent.Size) == OrientationType.Horizontal) panel.Size = new Size((Parent.ClientSize.Width / SizeSET) - Padding, (Parent.ClientSize.Height) - Padding);
            }
        }

        /// <summary>
        /// Маштабирует пропорционально все дочерние элементы управления
        /// </summary>
        /// <param name="Elements">Основная форма</param>
        public static void ResizeF(this Control Elements)
        {
            Elements.SuspendLayout();
            for (int i = 0; i < Elements.Controls.Count; i++)
            {
                Elements.Controls[i].SizeOrient(Elements, Elements.Controls.Count);
            }
            Elements.ResumeLayout();
        }

        /// <summary>
        /// Центрирует объект по Х
        /// </summary>
        /// <param name="Elements">Основная форма</param>
        public static void XCentre(this Control Elements)
        {
            Elements.SuspendLayout();
            Elements.Location = new Point(Elements.Parent.Width/2-Elements.Width/2, Elements.Location.Y);
            Elements.ResumeLayout();
        }
        /// <summary>
        /// Центрирует объект по Y
        /// </summary>
        /// <param name="Elements">Основная форма</param>
        public static void YCentre(this Control Elements)
        {
            Elements.SuspendLayout();
            Elements.Location = new Point(Elements.Location.X, Elements.Parent.Height / 2 - Elements.Height / 2);
            Elements.ResumeLayout();
        }

        /// <summary>
        /// Устанавливает прозрачный фон элементу
        /// </summary>
        /// <param name="Element">Элемент</param>
        public static void SetBackgroundTransparent(this Control Element)
        {
            GraphicsPath gr = new GraphicsPath();
            foreach (Control c in Element.Controls)
            {
                gr.AddRectangle(new Rectangle(c.Location, c.Size));
            }
            Element.Region = new Region(gr);
        }
        /// <summary>
        /// Устанавливает значение по индексу
        /// </summary>
        /// <param name="Element">Элемент</param>
        public static void SelectAtIndex(this ComboBox Element, int index)
        {
            if(index >= 0 && index < Element.Items.Count)
                Element.SelectedIndex = index;
        }
        /// <summary>
        /// Устанавливает значение по индексу
        /// </summary>
        /// <param name="Element">Элемент</param>
        public static T GetOfIndex<T>(this T[] Element, int index, T defaul = default(T))
        {
            if (index >= 0 && index < Element.Length)
                return Element[index];
            return defaul;
        }
    }
}
