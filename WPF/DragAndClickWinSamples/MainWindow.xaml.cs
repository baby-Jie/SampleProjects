using System.Windows;
using System.Windows.Input;
using DragAndClickWinSamples.Utils;

namespace DragAndClickWinSamples
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private Point _downPoint;

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("hello");
        }

        private void TestGrid_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var uiElement = sender as UIElement;

            _downPoint = e.GetPosition(uiElement);
        }

        private void TestGrid_OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var uiElement = sender as UIElement;
                var mouseMovePoint = e.GetPosition(uiElement);

                if (AppUtils.IsAbleToDrag(mouseMovePoint, _downPoint))
                {
                    this.DragMove();
                }
            }
        }

        private void TestGrid_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            var uiElement = sender as UIElement;

            var mouseMovePoint = e.GetPosition(uiElement);

            if (AppUtils.IsAbleToDrag(mouseMovePoint, _downPoint))
            {
                e.Handled = true;

                var element = Mouse.Captured;
                element?.ReleaseMouseCapture();
            }
        }
    }
}
