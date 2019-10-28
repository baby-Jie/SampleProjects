using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace AdvancedInkcanvas.Renders
{
    public class RectangleDynamicRenderer: DynamicRendererBase
    {
        protected override void OnDraw(DrawingContext drawingContext, StylusPointCollection stylusPoints, Geometry geometry, Brush fillBrush)
        {
            var visual = GetContainerVisual();
            visual?.Children.Clear();
            drawingContext.DrawRectangle(Pen.Brush, Pen, new Rect(_downPoint, (Point)stylusPoints[stylusPoints.Count - 1]));
            Console.WriteLine($"downPoint:{_downPoint}, endpoint:{(Point)stylusPoints[stylusPoints.Count - 1]}");
        }
    }
}
