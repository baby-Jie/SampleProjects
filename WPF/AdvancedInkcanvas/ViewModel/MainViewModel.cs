using System;
using System.Collections.Generic;
using System.Windows.Navigation;
using AdvancedInkcanvas.CommonTools;
using AdvancedInkcanvas.Models;
using GalaSoft.MvvmLight;

namespace AdvancedInkcanvas.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region Constructors

        public MainViewModel()
        {
            InitializeViewModel();
        }

        #endregion Constructors

        #region  Fields
        #endregion Fields

        #region Properties

        #endregion Properties

        #region Methods

        #endregion Methods

        #region EventHandlers

        #region Windows
        #endregion Windows

        #endregion EventHandlers

        #region Classified

        #region Initializations

        private void InitializeViewModel()
        {
            var modes = Enum.GetValues(typeof(InkMode));
            //_inkModeSets = new List<InkMode>(modes);
           //var names = Enum.GetNames(typeof(InkMode));
        }

        #endregion Initializations	

        #region Ink Mode Relations

        #region MVVMProperty InkModeSets  Ink模式集合（画笔、橡皮擦、选择、矩形等）

        private List<InkMode> _inkModeSets;

        public List<InkMode> InkModeSets
        {
            get { return _inkModeSets; }
            set { Set(ref _inkModeSets, value); }
        }

        #endregion	MVVMProperty InkModeSets

        #region MVVMProperty SelectedInkMode  选择的Ink 模式

        private InkMode _selectedInkMode;

        public InkMode SelectedInkMode
        {
            get { return _selectedInkMode; }
            set
            {
                Set(ref _selectedInkMode, value);
                SwitchInkMode(value);
            }
        }

        #endregion	MVVMProperty SelectedInkMode

        #region 切换Ink模式

        private void SwitchInkMode(InkMode mode)
        {
            AppUtil.SendMessage(mode, AppConsts.MSG_SwitchInkMode);
        }

        #endregion 切换Ink模式	


        #endregion Ink Mode	Relations

        #endregion Classified	
    }
}