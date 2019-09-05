using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using AdvancedInkcanvas.CommonTools;

namespace AdvancedInkcanvas.Renders
{
    public class SelectDynamicRenderer:DynamicRendererBase
    {
        public SelectDynamicRenderer()
        {
            this.Pen = AppConfig.DefaultSelectPen;
        }

        protected override void OnDraw(DrawingContext drawingContext, StylusPointCollection stylusPoints, Geometry geometry, Brush fillBrush)
        {
            var owner = this.Element;
            var visual = GetContainerVisual();
            visual?.Children.Clear();
            if (owner is InkCanvas inkCanvas)
            {
                // 如果有选中的Stroke 那么不显示
                if (inkCanvas.GetSelectedStrokes().Any())
                {
                    return;
                }

                // 没有选中的 画矩形选择框
                drawingContext.DrawRectangle(Pen.Brush, Pen, new Rect(_downPoint, (Point)stylusPoints[stylusPoints.Count - 1]));
            }
        }
    }
}
