using System;
using System.Collections.Generic;
using System.Reflection;
using GalaSoft.MvvmLight.Messaging;

namespace AdvancedInkcanvas.CommonTools
{
    public static class AppUtil
    {
        #region 获取枚举的集合

        public static List<T> GetEnumSets<T>(T enumVal)
        {
            var type = enumVal.GetType();

            var values = Enum.GetValues(type);

            List<T> list = new List<T>();

            foreach (var value in values)
            {
                if (value is T t)
                {
                    list.Add(t);
                }
            }

            return list;
        }

        #endregion 获取枚举的集合	

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

        #region Reflections

        #region Field Relations

        public static object GetPrivateField(object owner, string fieldStr)
        {
            return GetField(owner, fieldStr, BindingFlags.NonPublic | BindingFlags.Instance);
        }

        public static object GetField(object owner, string fieldStr, BindingFlags bindFlags)
        {
            object ret = null;
            try
            {
                var type = owner.GetType();
                ret = GetField(owner, type, fieldStr, bindFlags);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return ret;
        }

        public static object GetField(object owner, Type type, string fieldStr, BindingFlags bindFlags)
        {
            object ret = null;
            try
            {
                var fieldInfo = type.GetField(fieldStr, bindFlags);
                ret = fieldInfo?.GetValue(owner);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return ret;
        }

        public static void SetField(object owner, Type type, string fieldStr, BindingFlags bindFlags, object setVal)
        {
            try
            {
                var fieldInfo = type.GetField(fieldStr, bindFlags);
                fieldInfo?.SetValue(owner, setVal);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        #endregion Field Relations	

        #region Property Relations

        public static object GetProperty(object owner, string propertyName)
        {
            return GetProperty(owner, propertyName, BindingFlags.Instance | BindingFlags.Public);
        }

        public static object GetPrivateProperty(object owner, string propertyName)
        {
            return GetProperty(owner, propertyName, BindingFlags.Instance | BindingFlags.NonPublic);
        }

        public static object GetProperty(object owner, string propertyName, BindingFlags flags)
        {
            object ret = null;

            try
            {
                var type = owner.GetType();

                ret = GetProperty(owner, type, propertyName, flags);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return ret;
        }

        public static object GetProperty(object owner, Type type, string propertyName, BindingFlags flags)
        {
            object ret = null;

            try
            {
                var propertyInfo = type?.GetProperty(propertyName, flags);
                ret = propertyInfo?.GetValue(owner);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return ret;
        }

        #endregion Property Relations	

        #region Method Relations

        public static object InvokeMethod(object owner, string methodName)
        {
            object[] parameters = new object[] { };
            return InvokeMethod(owner, methodName, parameters);
        }

        public static object InvokeMethod(object owner, string methodName, object[] parameters)
        {
            return InvokeMethod(owner, methodName, BindingFlags.Instance | BindingFlags.Public, parameters);
        }

        public static object InvokePrivateMethod(object owner, string methodName)
        {
            object[] parameters = new object[] { };
            return InvokeMethod(owner, methodName, BindingFlags.Instance | BindingFlags.NonPublic, parameters);
        }

        public static object InvokePrivateMethod(object owner, string methodName, object[] parameters)
        {
            return InvokeMethod(owner, methodName, BindingFlags.Instance | BindingFlags.NonPublic, parameters);
        }

        public static object InvokeMethod(object owner, string methodStr, BindingFlags bindFlags, object[] parameters)
        {
            object ret = null;

            try
            {
                var type = owner.GetType();
                ret = InvokeMethod(owner, type, methodStr, bindFlags, parameters);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return ret;
        }

        public static object InvokeMethod(object owner, Type type, string methodStr, BindingFlags bindFlags,
                                          object[] parameters)
        {
            object ret = null;

            try
            {
                var methodInfo = type.GetMethod(methodStr, bindFlags);
                ret = methodInfo.Invoke(owner, parameters);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return ret;
        }

        #endregion Method Relations	

        #endregion Reflections	
    }
}
