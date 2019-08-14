using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZDSoft;
using MessageBox = System.Windows.MessageBox;
using RecordLibHelper = ZDSoft.SDK;

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

        private void SetLogTextBtn_OnClick(object sender, RoutedEventArgs e)
        {
            //ScnLibHelper.SetLogText("我的一家");
        }

        private void ShowFontDialogBtn_OnClick(object sender, RoutedEventArgs e)
        {
            FontDialog dialog = new FontDialog();
            dialog.ShowColor = true;
            var ret = dialog.ShowDialog();

            if (ret == System.Windows.Forms.DialogResult.OK)
            {
                var color = dialog.Color;

                var font = dialog.Font;

                ScnLibHelper.SetLogText("我的一家", font, color);

            }
        }

        private void CursorRelationBtn_OnClick(object sender, RoutedEventArgs e)
        {
            RecordLibHelper.ScnLib_RecordCursor(false);
            RecordLibHelper.ScnLib_AddCursorEffects(true, true, true, true);

            //RecordLibHelper.ScnLib_SetCursorOriginalSize(true)
            //ScnLibHelper
            //bool flag = RecordLibHelper.ScnLib_IsCursorOriginalSize();

            //MessageBox.Show(flag.ToString());
        }
    }
}
