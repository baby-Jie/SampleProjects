using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Input.StylusPlugIns;
using AdvancedInkcanvas.CommonTools;
using AdvancedInkcanvas.Models;
using AdvancedInkcanvas.Renders;
using AdvancedInkcanvas.Strokes;

namespace AdvancedInkcanvas.Controls
{
    public class CustomRenderingInkCanvas : InkCanvas
    {
        #region Constructors

        public CustomRenderingInkCanvas() : base()
        {
            _inkMode = InkMode.None;
            _originalRenderer = this.DynamicRenderer;

            CustomInitial();
        }

        #endregion Constructors

        #region  Fields

        private readonly DynamicRenderer _originalRenderer;
        private InkMode _inkMode;

        private readonly List<DynamicRenderer> _dynamicRenderers = new List<DynamicRenderer>();

        #endregion Fields

        #region Properties
        #endregion Properties

        #region Methods

        public void CustomInitial()
        {
            _dynamicRenderers.Add(_originalRenderer);
            _dynamicRenderers.Add(_originalRenderer);
            _dynamicRenderers.Add(_originalRenderer);
            _dynamicRenderers.Add(_originalRenderer);
            _dynamicRenderers.Add(_originalRenderer);
            //_dynamicRenderers.Add(new SelectDynamicRenderer());
            _dynamicRenderers.Add(_originalRenderer);
            _dynamicRenderers.Add(_originalRenderer);
            _dynamicRenderers.Add(new RectangleDynamicRenderer());
        }

        /// <summary>
        /// 切换工作模式 ink、rect、eraser等
        /// </summary>
        /// <param name="mode"></param>
        public void SwitchInkMode(InkMode mode)
        {
            string name = mode.ToString();
            this._inkMode = mode;

            // 系统的几个Mode
            if (AppConsts.InkCanvasEditingModeNames.Contains(name))
            {
                if (Enum.TryParse(name, true, out InkCanvasEditingMode editMode))
                {
                    this.EditingMode = editMode;
                }

                this.DynamicRenderer = _dynamicRenderers[(int)mode];
            }
            // 自定义的几个Mode 比如Rect、Circle
            else
            {
                this.EditingMode = InkCanvasEditingMode.Ink;

                if (mode == InkMode.Rectangle)
                {
                    this.DynamicRenderer = new RectangleDynamicRenderer();
                }
            }
        }

        #endregion Methods

        #region EventHandlers

        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            //var point = e.GetPosition(this);
            //if (this.EditingMode == InkCanvasEditingMode.None)
            //{
            //    var strokes = this.Strokes.Where(stroke => stroke is RectangleStroke && stroke.GetGeometry().Bounds.Contains(point));

            //    if (strokes.Any())
            //    {
            //        StrokeCollection collection = new StrokeCollection(strokes);
            //        this.Select(collection, null);
            //    }
            //}
            base.OnPreviewMouseLeftButtonDown(e);
        }

        protected override void OnStrokeCollected(InkCanvasStrokeCollectedEventArgs e)
        {
            switch (_inkMode)
            {
                case InkMode.None:
                case InkMode.Ink:
                case InkMode.GestureOnly:
                case InkMode.InkAndGesture:
                case InkMode.Select:
                case InkMode.EraseByPoint:
                case InkMode.EraseByStroke:
                    base.OnStrokeCollected(e);
                    break;
                case InkMode.Rectangle:
                    // Remove the original stroke and add a custom stroke.
                    this.Strokes.Remove(e.Stroke);
                    Stroke stroke = RectangleStroke.GetRectangleStroke(e.Stroke.StylusPoints);
                    this.Strokes.Add(stroke);

                    // Pass the custom stroke to base class' OnStrokeCollected method.
                    InkCanvasStrokeCollectedEventArgs args =
                        new InkCanvasStrokeCollectedEventArgs(stroke);
                    base.OnStrokeCollected(args);
                    break;
                default:
                    break;
            }
        }

        protected override void OnSelectionChanged(EventArgs e)
        {
            Console.WriteLine("OnSelectionChanged");
            var selects = this.GetSelectedStrokes();
            base.OnSelectionChanged(e);
        }

        protected override void OnSelectionMoved(EventArgs e)
        {
            Console.WriteLine("OnSelectionMoved");
            base.OnSelectionMoved(e);
        }

        protected override void OnSelectionChanging(InkCanvasSelectionChangingEventArgs e)
        {
            Console.WriteLine("OnSelectionChanging");
            base.OnSelectionChanging(e);
        }

        protected override void OnSelectionMoving(InkCanvasSelectionEditingEventArgs e)
        {
            Console.WriteLine("OnSelectionMoving");
            base.OnSelectionMoving(e);
        }

        protected override void OnSelectionResized(EventArgs e)
        {
            Console.WriteLine("OnSelectionResized");
            base.OnSelectionResized(e);
        }

        protected override void OnSelectionResizing(InkCanvasSelectionEditingEventArgs e)
        {
            Console.WriteLine("OnSelectionResizing");
            base.OnSelectionResizing(e);
        }

        #endregion EventHandlers
    }
}
