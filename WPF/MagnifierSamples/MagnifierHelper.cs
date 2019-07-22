using System;
using System.Runtime.InteropServices;
using Gma.System.MouseKeyHook;
using Rectangle = System.Drawing.Rectangle;


namespace MagnifierSamples
{
    public static class MagnifierHelper
    {
        #region Native

        const int SM_XVIRTUALSCREEN = 76;
        const int SM_YVIRTUALSCREEN = 77;
        const int SM_CXVIRTUALSCREEN = 78;
        const int SM_CYVIRTUALSCREEN = 79;

        const int SM_CXSCREEN = 0;
        const int SM_CYSCREEN = 1;

        [DllImport("Magnification.dll")]
        public static extern bool MagSetFullscreenTransform(float level, int xOffset, int yOffsety);

        [DllImport("Magnification.dll")]
        public static extern bool MagGetFullscreenTransform(out float level, out int xOffset, out int yOffsety);

        [DllImport("Magnification.dll")]
        public static extern bool MagInitialize();

        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(int index);

        #endregion Native

        const int GAP = 100;

        static float _magnificationFactor = 2f;

        private static IKeyboardMouseEvents m_GlobalHook;

        public static float MagnificationFactor
        {
            get { return _magnificationFactor; }
            set
            {
                if (value < 1)
                {
                    value = 1;
                }

                if (value > 1)
                {
                    IsZoomed = true;
                }
                else
                {
                    IsZoomed = false;
                }
                _magnificationFactor = value;
            }
        }

        private static Rectangle _boundary;

        private static int _virtualLeft = 0;

        private static int _virtualTop = 0;

        private static int _virtualOffsetX = 0;

        private static int _virtualOffsetY = 0;

        private static bool _magInitialized = false;

        public static bool MagInitialized
        {
            get { return _magInitialized; }
            private set
            {
                _magInitialized = value;
            }
        }

        #region FullProperty IsZoomed

        private static bool _isZoomed;

        public static bool IsZoomed
        {
            get { return _isZoomed; }
            set { _isZoomed = value; }
        }

        #endregion	FullProperty IsZoomed

        static MagnifierHelper()
        {
            int left = GetSystemMetrics(SM_XVIRTUALSCREEN);
            int top = GetSystemMetrics(SM_YVIRTUALSCREEN);

            int width = GetSystemMetrics(SM_CXVIRTUALSCREEN);
            int height = GetSystemMetrics(SM_CYVIRTUALSCREEN);

            _virtualLeft = left;
            _virtualTop = top;

            var offsetX = left - left / _magnificationFactor;

            var offsetY = top - top / _magnificationFactor;

            _virtualOffsetX = (int)offsetX;
            _virtualOffsetY = (int)offsetY;

            _boundary = new Rectangle(left, top, width, height);

            MagInitialized = MagInitialize();

            Subscribe();
        }

        private static void SetZoom(int offsetX, int offsetY)
        {
            if (_magnificationFactor < 1.0)
            {
                return;
            }

            App.Current.Dispatcher.BeginInvoke((Action)(() =>
            {
                MagSetFullscreenTransform(_magnificationFactor, offsetX, offsetY);
            }));
        }

        // 此方法必须使用ui线程执行
        public static void ZoomIn(float factor)
        {
            MagnificationFactor = factor;

            int xDlg = (int)((float)GetSystemMetrics(
            SM_CXSCREEN) * (1.0 - (1.0 / _magnificationFactor)) / 2.0);
            int yDlg = (int)((float)GetSystemMetrics(
                SM_CYSCREEN) * (1.0 - (1.0 / _magnificationFactor)) / 2.0);

            //SetZoom(_virtualOffsetX, _virtualOffsetY);
            SetZoom(xDlg, yDlg);
        }

        /// <summary>
        /// 取消放大
        /// </summary>
        public static void ZoomOut()
        {
            QuitMagnify();
        }

        private static void QuitMagnify()
        {
            MagnificationFactor = 1;

            SetZoom(0, 0);
        }

        private static void Subscribe()
        {
            m_GlobalHook = Hook.GlobalEvents();

            m_GlobalHook.MouseMove += M_GlobalHook_MouseMove;
        }

        private static Rectangle GetVisibleRegion()
        {
            Rectangle rect = new Rectangle();

            float level;
            int xOffset;
            int yOffset;
            bool ret1 = MagGetFullscreenTransform(out level, out xOffset, out yOffset);

            var transformWidth = (int)(_boundary.Width / level);
            var transformHeight = (int)(_boundary.Height / level);

            rect.X = xOffset - _virtualOffsetX;
            rect.Y = yOffset - _virtualOffsetY;
            rect.Width = transformWidth;
            rect.Height = transformHeight;

            return rect;
        }

        private static Rectangle GetEdgeRegion()
        {
            var visibleRegion = GetVisibleRegion();

            visibleRegion.X += GAP;

            visibleRegion.Y += GAP;

            visibleRegion.Width -= 2 * GAP;

            visibleRegion.Height -= 2 * GAP;

            return visibleRegion;
        }

        private static void M_GlobalHook_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //return;
            if (!IsZoomed)
            {
                return;
            }

            var visibleRegion = GetVisibleRegion();

            var edgeRegion = GetEdgeRegion();

            float level;
            int xOffset;
            int yOffset;
            bool ret1 = MagGetFullscreenTransform(out level, out xOffset, out yOffset);

            if (level <= 1)
            {
                return;
            }

            var point = e.Location;

            point.X -= _virtualLeft;
            point.Y -= _virtualTop;


            // 左侧
            if (point.X < edgeRegion.X)
            {
                if (xOffset >= _boundary.Left)
                {
                    xOffset -= 5;

                    if (xOffset < _virtualOffsetX)
                    {
                        xOffset = _virtualOffsetX;
                    }
                }
            }

            // 上侧
            if (point.Y < edgeRegion.Y)
            {
                if (yOffset >= _virtualOffsetY)
                {
                    yOffset -= 5;

                    if (yOffset < _virtualOffsetY)
                    {
                        yOffset = _virtualOffsetY;
                    }
                }
            }

            // 右侧
            if (point.X > edgeRegion.Right)
            {
                xOffset += 5;
            }

            // 下侧
            if (point.Y > edgeRegion.Bottom)
            {
                yOffset += 5;
            }

            var maxX = (int)(_boundary.Width - _boundary.Width / level) + _virtualOffsetX;
            var maxY = (int)(_boundary.Height - _boundary.Height / level) + _virtualOffsetY;

            if (xOffset > maxX)
            {
                xOffset = maxX;
            }

            if (yOffset > maxY)
            {
                yOffset = maxY;
            }

            SetZoom(xOffset, yOffset);
        }

        public static void Dispose()
        {
            m_GlobalHook.MouseMove -= M_GlobalHook_MouseMove;
            m_GlobalHook.Dispose();
        }

        //private static void SetInputStream()
        //{
        //    float level;
        //    int xOffset;
        //    int yOffset;
        //    bool ret1 = MagGetFullscreenTransform(out level, out xOffset, out yOffset);

        //    if ()
        //}
    }
}
