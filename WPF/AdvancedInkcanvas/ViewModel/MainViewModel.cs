using System;
using System.Collections.Generic;
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
            CustomInitialization();
        }

        #endregion Constructors

        #region  Fields
        #endregion Fields

        #region Properties

        #endregion Properties

        #region Methods

        #endregion Methods

        #region EventHandlers

        #endregion EventHandlers

        #region Classified

        #region Initializations

        private void CustomInitialization()
        {
            InkMode mode = InkMode.Ink;
            _inkModeSets = AppUtil.GetEnumSets(mode);
        }

        #endregion Initializations	

        #region Ink Mode Relations

        #region MVVMProperty InkModeSets  Inkģʽ���ϣ����ʡ���Ƥ����ѡ�񡢾��εȣ�

        private List<InkMode> _inkModeSets;

        public List<InkMode> InkModeSets
        {
            get { return _inkModeSets; }
            set { Set(ref _inkModeSets, value); }
        }

        #endregion	MVVMProperty InkModeSets

        #region MVVMProperty SelectedInkMode  ѡ���Ink ģʽ

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

        #region �л�Inkģʽ

        private void SwitchInkMode(InkMode mode)
        {
            AppUtil.SendMessage(mode, AppConsts.MSG_SwitchInkMode);
        }

        #endregion �л�Inkģʽ	

        #endregion Ink Mode	Relations

        #endregion Classified	
    }
}