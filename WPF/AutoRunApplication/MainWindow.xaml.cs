using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using Microsoft.Win32;

namespace AutoRunApplication
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

        public static bool Create(string directory, string shortcutName, string targetPath,
                                  string description = null, string iconLocation = null)
        {
            try
            {
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                //添加引用 Com 中搜索 Windows Script Host Object Model
                string shortcutPath = Path.Combine(directory, string.Format("{0}.lnk", shortcutName));
                IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShell();
                IWshRuntimeLibrary.IWshShortcut shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(shortcutPath);//创建快捷方式对象
                shortcut.TargetPath = targetPath;//指定目标路径
                shortcut.WorkingDirectory = Path.GetDirectoryName(targetPath);//设置起始位置
                shortcut.WindowStyle = 1;//设置运行方式，默认为常规窗口
                shortcut.Description = description;//设置备注
                shortcut.IconLocation = string.IsNullOrWhiteSpace(iconLocation) ? targetPath : iconLocation;//设置图标路径
                shortcut.Save();//保存快捷方式

                return true;
            }
            catch
            { }
            return false;
        }

        private void AutoRunBtn_OnClick(object sender, RoutedEventArgs e)
        {
            AutoRunFun2();
        }

        private void CancelAutoRunBtn_OnClick(object sender, RoutedEventArgs e)
        {
            CancelAutoRunFun2();
        }

        #region Methods

        /// <summary>
        /// 通过创建快捷方式并放到菜单启动根目录下实现 开机自启动
        /// </summary>
        private void AutoRunFun1()
        {
            // 通过创建快捷方式并放到菜单启动根目录下实现 开机自启动
            var startupPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);

            var currentDirectory = Directory.GetCurrentDirectory();

            var str = Path.Combine(currentDirectory, "AutoRunApplication.exe");

            try
            {
                Create(startupPath, "AutoRunApplication", str);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void CancelAutoRunFun1()
        {
            var startupPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);

            startupPath = Path.Combine(startupPath, "AutoRunApplication.lnk");

            if (File.Exists(startupPath))
            {
                File.Delete(startupPath);
            }
        }

        /// <summary>
        /// 通过 注册表开机启动项
        /// </summary>
        private void AutoRunFun2()
        {
            string executePath = Path.Combine(Directory.GetCurrentDirectory(), "AutoRunApplication.exe");
            RegistryKey rKey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
            rKey.SetValue("autorunapp", @"""" + executePath + @"""");
        }

        private void CancelAutoRunFun2()
        {
            RegistryKey rKey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
            rKey.DeleteValue("autorunapp");
        }

        #endregion Methods	

    }
}
