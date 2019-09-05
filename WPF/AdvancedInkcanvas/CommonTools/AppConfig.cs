using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AdvancedInkcanvas.CommonTools
{
    public class AppConfig
    {
        public static readonly Brush DefaultPenBrush = Brushes.Aqua;  // 默认的画笔颜色

        public static readonly double DefaultPenThickness = 2d; // 默认的画笔粗细

        public static readonly Pen DefaultPen = new Pen(DefaultPenBrush, DefaultPenThickness);  // 默认的画笔

        public static readonly Pen DefaultSelectPen = new Pen(new SolidColorBrush(Color.FromArgb(80, 80, 80, 80)), 2d);

    }
}
