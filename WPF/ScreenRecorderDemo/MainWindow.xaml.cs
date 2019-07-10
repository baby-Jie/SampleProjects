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

namespace ScreenRecorderDemo
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

        private void InitialBtn_OnClick(object sender, RoutedEventArgs e)
        {
            ScnLibHelper.CommonInitialRecord();
        }

        private void StartRecordBtn_OnClick(object sender, RoutedEventArgs e)
        {
            ScnLibHelper.StartRecord();
        }

        private void PasueRecordBtn_OnClick(object sender, RoutedEventArgs e)
        {
            ScnLibHelper.PauseRecord();
        }

        private void StopRecordBtn_OnClick(object sender, RoutedEventArgs e)
        {
            ScnLibHelper.StopRecord();
        }

        private void RecordAudioBtn_OnClick(object sender, RoutedEventArgs e)
        {
            ScnLibHelper.RecordAudio();
        }
    }
}
