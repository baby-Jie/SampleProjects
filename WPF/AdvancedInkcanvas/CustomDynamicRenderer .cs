using System;
using System.Reflection;
using System.Windows.Media;
using System.Windows;
using System.Windows.Input.StylusPlugIns;
using System.Windows.Input;

namespace AdvancedInkcanvas
{
    // A StylusPlugin that renders ink with a linear gradient brush effect.
    class CustomDynamicRenderer : DynamicRenderer
    {
        [ThreadStatic]
        static private Brush brush = null;

        [ThreadStatic]
        static private Pen pen = null;

        private Point prevPoint;

        private Point _downPoint;

        private bool _isFirst = true;


        protected override void OnStylusDown(RawStylusInput rawStylusInput)
        {

            var stylusPoints = rawStylusInput.GetStylusPoints();

            _downPoint = (Point)stylusPoints[0];
            prevPoint = new Point(double.NegativeInfinity, double.NegativeInfinity);
            base.OnStylusDown(rawStylusInput);
        }

        protected override void OnDraw(DrawingContext drawingContext,
                                       StylusPointCollection stylusPoints,
                                       Geometry geometry, Brush fillBrush)
        {
            var type = typeof(DynamicRenderer);
            var fieldInfo = type.GetField("_strokeInfoList", BindingFlags.NonPublic | BindingFlags.Instance);
            
            var strokeInfoList = fieldInfo.GetValue(this);
            var strokeInfoListType = strokeInfoList.GetType();
            var getItemMethodInfo = strokeInfoListType.GetMethod("get_Item");
            var si = getItemMethodInfo.Invoke(strokeInfoList, new Object[] {0});

            var siType = si.GetType();

            var strokeCVPropertyInfo = siType.GetProperty("StrokeCV");

            var strokeCV = strokeCVPropertyInfo.GetValue(si);


            if (strokeCV is ContainerVisual visual)
            {
                visual.Children.Clear();
            }
           
            var element = this.Element;


            if (brush == null)
            {
                brush = new LinearGradientBrush(Colors.Red, Colors.Blue, 20d);
            }

            if (pen == null)
            {
                pen = new Pen(brush, 2d);
            }


            drawingContext.DrawRectangle(brush, pen, new Rect(_downPoint, (Point)stylusPoints[stylusPoints.Count - 1]));
        }
    }

    public class Person
    {
        public string this[int index]
        {
            get { return "111"; }
        }
    }
}
