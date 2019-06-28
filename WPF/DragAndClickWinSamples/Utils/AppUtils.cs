using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DragAndClickWinSamples.Utils
{
    public class AppUtils
    {
        /// <summary>
        /// 判断两个点是否满足拖动的条件
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        public static bool IsAbleToDrag(Point point1, Point point2)
        {
            var flag = false;
            var offsetX = Math.Abs(point1.X - point2.X);
            var offsetY = Math.Abs(point1.Y - point2.Y);

            if (offsetX > SystemParameters.MinimumHorizontalDragDistance ||
                offsetY > SystemParameters.MinimumVerticalDragDistance)
            {
                flag = true;
            }

            return flag;
        }
    }
}
