using System.Windows;

namespace DragAndClickWinSamples
{
    /// <summary>
    /// DragMoveWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DragMoveWindow : Window
    {
        public DragMoveWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("hello");
        }
    }
}
