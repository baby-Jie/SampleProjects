using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace WebSocketSamples.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region Constructors

        public MainViewModel()
        {
        }

        #endregion Constructors	

        #region Fields

        private WebSocketServer _serverWebSocket;

        #endregion Fields	

        #region Properties

        #region MVVMProperty ServerUrl

        private string _serverUrl;

        public string ServerUrl
        {
            get { return _serverUrl; }
            set { Set(ref _serverUrl, value); }
        }

        #endregion	MVVMProperty ServerUrl


        #endregion Properties	

        #region Commands

        #region OpenServerCmd

        #region CommandProperty OpenServerCmd

        private RelayCommand _OpenServerCmd;

        public RelayCommand OpenServerCmd
        {
            get
            {
                return _OpenServerCmd ?? new RelayCommand(() =>
                {
                    // 打开服务器
                    

                });
            }
        }

        #endregion


        #endregion OpenServerCmd	

        #endregion Commands	

        #region Methods

        /// <summary>
        /// 打开服务器
        /// </summary>
        private void OpenServer()
        {
            if (string.IsNullOrWhiteSpace(ServerUrl))
            {
                return;
            }

            try
            {
                if (_serverWebSocket != null)
                {
                    _serverWebSocket.Stop();

                    _serverWebSocket = null;
                }
            }
            catch (Exception e)
            {
            }

            _serverWebSocket = new WebSocketServer(ServerUrl);
            _serverWebSocket.AddWebSocketService<Laputa>("/");
            _serverWebSocket.Start();
        }


        #endregion Methods	

        #region EventHandler

        #region Custom EventHandlers

        #region ServerWebSocket Open

        private void ServerWebSocketOnOnOpen(object sender, EventArgs e)
        {

        }

        #endregion ServerWebSocket Open	

        #region ServerWebSocket Error

        private void ServerWebSocketOnOnError(object sender, ErrorEventArgs e)
        {

        }

        #endregion ServerWebSocket Error	

        #region WebSocket Message

        private void ServerWebSocketOnOnMessage(object sender, MessageEventArgs e)
        {

        }

        #endregion WebSocket Message	

        #endregion Custom EventHandlers	

        #endregion EventHandler	
    }

    public class Laputa : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            var msg = e.Data == "BALUS"
                ? "I've been balused already..."
                : "I'm not available now.";

            Send(msg);
        }
    }
}