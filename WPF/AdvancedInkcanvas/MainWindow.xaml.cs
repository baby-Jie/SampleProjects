using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AdvancedInkcanvas.CommonTools;
using AdvancedInkcanvas.Models;

namespace AdvancedInkcanvas
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructors

        public MainWindow()
        {
            InitializeComponent();

            CustomInitialize();
        }

        #endregion Constructors

        #region  Fields
        #endregion Fields

        #region Properties
        #endregion Properties

        #region Methods
        #endregion Methods

        #region EventHandlers

        #region UI Releations

        /// <summary>
        /// 清除所有
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ClearAllBtn_OnClick(object sender, RoutedEventArgs e)
        {
            CustomInkCanvas.Strokes.Clear();
        }

        #endregion UI Releations

        #endregion EventHandlers

        #region Classfied

        #region Initializations and Disopose 

        private void CustomInitialize()
        {
            RegisterMessage();
        }

        #endregion Initializations	

        #region 注册消息

        private void RegisterMessage()
        {
            AppUtil.RegisterEvents<InkMode>(this, AppConsts.MSG_SwitchInkMode, SwitchInkMode);
        }

        public void SwitchInkMode(InkMode mode)
        {
            CustomInkCanvas.SwitchInkMode(mode);
        }

        #endregion 注册消息	

        #endregion

        private void TestSelectBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var field = AppUtil.GetField(CustomInkCanvas, typeof(InkCanvas), "_feedbackAdorner",
                BindingFlags.NonPublic | BindingFlags.Instance);

            AppUtil.SetField(field, field.GetType(), "_adornerBorderPen", BindingFlags.NonPublic | BindingFlags.Instance, new Pen(Brushes.Red, 3d));

            //var field = AppUtil.GetField(CustomInkCanvas, typeof(InkCanvas), "_selectionAdorner",
            //    BindingFlags.NonPublic | BindingFlags.Instance);

            //AppUtil.SetField(field, field.GetType(), "_adornerFillBrush", BindingFlags.NonPublic | BindingFlags.Instance, Brushes.Green);

            var brush = AppUtil.GetPrivateField(field, "_adornerBorderPen");
        }
    }
}
