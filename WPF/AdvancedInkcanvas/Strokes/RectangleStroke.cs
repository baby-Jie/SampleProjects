using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;

namespace AdvancedInkcanvas.Strokes
{
    public class RectangleStroke:CustomStrokeBase
    {
        public RectangleStroke(StylusPointCollection stylusPoints) : base(stylusPoints)
        {
        }

        protected override void DrawCore(DrawingContext drawingContext, DrawingAttributes drawingAttributes)
        {
            drawingContext.DrawRectangle(Pen.Brush, Pen, new Rect((Point)StylusPoints[0], (Point)StylusPoints[StylusPoints.Count - 1]));
        }

        public static RectangleStroke GetRectangleStroke(StylusPointCollection stylusPoints)
        {
            var count = stylusPoints.Count;
            for (int i = 1; i <= count - 2; i++)
            {
                stylusPoints.RemoveAt(1);
            }

            return new RectangleStroke(stylusPoints);
        }
    }
}
