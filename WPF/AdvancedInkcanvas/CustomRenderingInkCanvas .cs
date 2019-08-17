using System.Windows.Controls;
using System.Windows.Input.StylusPlugIns;
using AdvancedInkcanvas.CommonTools;
using AdvancedInkcanvas.Models;

namespace AdvancedInkcanvas
{
    public class CustomRenderingInkCanvas : InkCanvas
    {


        #region Constructors
        #endregion Constructors

        #region  Fields

        private DynamicRenderer _originalRenderer;

        #endregion Fields

        #region Properties
        #endregion Properties

        #region Methods
        #endregion Methods

        #region EventHandlers

        #region Windows
        #endregion Windows

        #endregion EventHandlers


        CustomDynamicRenderer customRenderer = new CustomDynamicRenderer();

        public CustomRenderingInkCanvas() : base()
        {
            _originalRenderer = this.DynamicRenderer;
            //this.DynamicRenderer = customRenderer;
        }

        //protected override void OnStrokeCollected(InkCanvasStrokeCollectedEventArgs e)
        //{
        //    // Remove the original stroke and add a custom stroke.
        //    this.Strokes.Remove(e.Stroke);
        //    CustomStroke customStroke = new CustomStroke(e.Stroke.StylusPoints);
        //    this.Strokes.Add(customStroke);

        //    // Pass the custom stroke to base class' OnStrokeCollected method.
        //    InkCanvasStrokeCollectedEventArgs args =
        //        new InkCanvasStrokeCollectedEventArgs(customStroke);
        //    base.OnStrokeCollected(args);

        //}

        public void SwitchInkMode(InkMode mode)
        {
            string name = mode.ToString();

            if (AppConsts.InkCanvasEditingModeNames.Contains(name))
            {
                if (InkCanvasEditingMode.TryParse(name, true, out InkCanvasEditingMode editMode))
                {
                    this.EditingMode = editMode;
                }
            }
        }
    }
}
