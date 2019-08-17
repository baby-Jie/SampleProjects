using System;
using GalaSoft.MvvmLight.Messaging;

namespace AdvancedInkcanvas.CommonTools
{
    public static class AppUtil
    {

        #region MVVMLight

        /// <summary>
        /// 发送事件消息和数据 泛型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="objectToken"></param>
        public static void SendMessage<T>(T message, string strToken)
        {
            Messenger.Default.Send(message, strToken);
        }

        /// <summary>
        /// 注册接收事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="recipient"></param>
        /// <param name="token"></param>
        /// <param name="handler"></param>
        public static void RegisterEvents<T>(object recipient, string token, Action<T> handler)
        {
            Messenger.Default.Register(recipient, token, handler);
        }

        /// <summary>
        /// 注销事件
        /// </summary>
        /// <param name="recipient"></param>
        public static void UnRegisterEvents(object recipient)
        {
            Messenger.Default.Unregister(recipient);
        }

        #endregion MVVMLight
    }
}
