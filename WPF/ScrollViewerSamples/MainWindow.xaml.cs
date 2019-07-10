using System;
using System.Collections.Generic;
using System.Linq;
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

namespace ScrollViewerSamples
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

        public void ScrollToUiElement(UIElement element)
        {
            UIElement containerElement = VisualTreeHelper.GetParent(element) as UIElement;
            Point relativeLocation = element.TranslatePoint(new Point(0, 0), containerElement);
            ScrollViewer1.ScrollToVerticalOffset(relativeLocation.Y);
        }

        private void ScrollViewer1_OnScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            SetSelectedItemByVerticalOffset();
        }

        private void SetSelectedItemByVerticalOffset()
        {
            var verticalOffset = ScrollViewer1.VerticalOffset;

            var items = from item in _sectionList
                        where verticalOffset >= item.Item2 && verticalOffset < item.Item3
                        select item.Item1;
            var item1 = items.FirstOrDefault();

            if (item1 != null)
            {
                if (item1.Tag is ListBoxItem listBoxItem)
                {
                    listBoxItem.IsSelected = true;
                }
            }
        }

        private double GetTopVerticalOffset(FrameworkElement element)
        {
            UIElement containerElement = VisualTreeHelper.GetParent(element) as UIElement;
            Point relativeLocation = element.TranslatePoint(new Point(0, 0), containerElement);

            return relativeLocation.Y;
        }

        private double GetBottomVerticalOffset(FrameworkElement element)
        {
            UIElement containerElement = VisualTreeHelper.GetParent(element) as UIElement;
            Point relativeLocation = element.TranslatePoint(new Point(element.ActualWidth, element.ActualHeight), containerElement);

            return relativeLocation.Y;
        }

        private List<Tuple<FrameworkElement, double, double>> _sectionList = new List<Tuple<FrameworkElement, double, double>>();

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            var topOffset1 = GetTopVerticalOffset(Rect1);
            var bottomOffset1 = GetBottomVerticalOffset(Rect1);
            Rect1.Tag = ListBoxItem1;
            ListBoxItem1.Tag = Rect1;

            _sectionList.Add(new Tuple<FrameworkElement, double, double>(Rect1, topOffset1, bottomOffset1));

            var topOffset2 = GetTopVerticalOffset(Rect2);
            var bottomOffset2 = GetBottomVerticalOffset(Rect2);
            Rect2.Tag = ListBoxItem2;
            ListBoxItem2.Tag = Rect2;

            _sectionList.Add(new Tuple<FrameworkElement, double, double>(Rect2, topOffset2, bottomOffset2));

            var topOffset3 = GetTopVerticalOffset(Rect3);
            var bottomOffset3 = GetBottomVerticalOffset(Rect3);
            Rect3.Tag = ListBoxItem3;
            ListBoxItem3.Tag = Rect3;

            _sectionList.Add(new Tuple<FrameworkElement, double, double>(Rect3, topOffset3, bottomOffset3));

            var topOffset4 = GetTopVerticalOffset(Rect4);
            var bottomOffset4 = GetBottomVerticalOffset(Rect4);
            Rect4.Tag = ListBoxItem4;
            ListBoxItem4.Tag = Rect4;

            _sectionList.Add(new Tuple<FrameworkElement, double, double>(Rect4, topOffset4, bottomOffset4));

            SetSelectedItemByVerticalOffset();
        }

        private void NavigationListBox_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = ItemsControl.ContainerFromElement(NavigationListBox, e.OriginalSource as DependencyObject);

            if (item is ListBoxItem listBoxItem)
            {
                if (listBoxItem.IsSelected == false)
                {
                    var target = listBoxItem.Tag as UIElement;
                    if (target != null)
                    {
                        ScrollToUiElement(target);
                    }
                }
            }
        }
    }
}
