using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input.StylusPlugIns;
using System.Windows.Media;
using AdvancedInkcanvas.CommonTools;

namespace AdvancedInkcanvas.Renders
{
    public class DynamicRendererBase : DynamicRenderer
    {
        public DynamicRendererBase()
        {
            this.Pen = AppConfig.DefaultPen;
        }

        protected Point _prevPoint;

        protected Point _downPoint;

        public Pen Pen { get; set; }

        protected override void OnStylusDown(RawStylusInput rawStylusInput)
        {
            var stylusPoints = rawStylusInput.GetStylusPoints();

            _downPoint = (Point)stylusPoints[0];
            _prevPoint = new Point(double.NegativeInfinity, double.NegativeInfinity);
            base.OnStylusDown(rawStylusInput);
        }

        /// <summary>
        /// 获取ContainerVisual
        /// </summary>
        /// <returns></returns>
        protected virtual ContainerVisual GetContainerVisual()
        {
            try
            {
                var strokeInfoList = AppUtil.GetField(this, typeof(DynamicRenderer), "_strokeInfoList", BindingFlags.NonPublic | BindingFlags.Instance);

                var stroleInfo = AppUtil.InvokeMethod(strokeInfoList, "get_Item", new Object[] { 0 });

                var strokeCV = AppUtil.GetProperty(stroleInfo, "StrokeCV");

                if (strokeCV is ContainerVisual visual)
                {
                    return visual;
                }
            }
            catch (Exception e)
            {
            }
            return null;
        }
    }
}
