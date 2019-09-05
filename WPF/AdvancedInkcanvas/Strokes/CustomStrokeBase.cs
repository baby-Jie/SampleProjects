using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using AdvancedInkcanvas.CommonTools;

namespace AdvancedInkcanvas.Strokes
{
    public class CustomStrokeBase: Stroke
    {
        #region Constructors

        public CustomStrokeBase(StylusPointCollection stylusPoints) : this(stylusPoints, AppConfig.DefaultPen)
        {
        }

        public CustomStrokeBase(StylusPointCollection stylusPoints, Pen pen) : base(stylusPoints)
        {
            this.Pen = pen;
        }

        #endregion Constructors

        #region Properties

        public Pen Pen { get; set; }

        #endregion Properties

        public virtual bool Contain(Point p)
        {
            bool flag = false;

            try
            {
                flag = this.GetGeometry().Bounds.Contains(p);
            }
            catch (Exception e)
            {
            }

            return flag;
        }
    }
}
