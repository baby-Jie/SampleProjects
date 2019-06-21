using System;
using System.Windows.Media;
using System.Windows;
using System.Windows.Input.StylusPlugIns;
using System.Windows.Input;
using System.Windows.Ink;

namespace AdvancedInkcanvas
{
    // A class for rendering custom strokes
    class CustomStroke : Stroke
    {
        Brush brush;
        Pen   pen;

        public CustomStroke(StylusPointCollection stylusPoints)
            : base(stylusPoints)
        {
            // Create the Brush and Pen used for drawing.
            brush = new LinearGradientBrush(Colors.Red, Colors.Blue, 20d);
            pen = new Pen(brush, 2d);
        }

        protected override void DrawCore(DrawingContext drawingContext,
                                         DrawingAttributes drawingAttributes)
        {

            drawingContext.DrawRectangle(brush, pen, new Rect((Point)StylusPoints[0], (Point)StylusPoints[StylusPoints.Count-1]));

            return;
            // Allocate memory to store the previous point to draw from.
            Point prevPoint = new Point(double.NegativeInfinity,
                double.NegativeInfinity);

            // Draw linear gradient ellipses between 
            // all the StylusPoints in the Stroke.
            for (int i = 0; i < this.StylusPoints.Count; i++)
            {
                Point pt = (Point)this.StylusPoints[i];
                Vector v = Point.Subtract(prevPoint, pt);

                // Only draw if we are at least 4 units away 
                // from the end of the last ellipse. Otherwise, 
                // we're just redrawing and wasting cycles.
                if (v.Length > 4)
                {
                    // Set the thickness of the stroke 
                    // based on how hard the user pressed.
                    double radius = this.StylusPoints[i].PressureFactor * 10d;
                    drawingContext.DrawEllipse(brush, pen, pt, radius, 
                        radius);
                    prevPoint = pt;
                }
            }
        }
    }
}
