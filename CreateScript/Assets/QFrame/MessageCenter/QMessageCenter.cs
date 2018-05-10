using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace QFrame
{
    public partial class QMessageCenter<T>
    {
        private object sender;
        private static readonly ArgumentException ConversionType = new ArgumentException("不能转换类型");
        private Dictionary<T, QEvent> dic = new Dictionary<T, QEvent>();

        public object GetSender(){
            return sender;
        }

        private bool AddEvent(T signal, Delegate callback)
        {
            QEvent e;
            if (dic.ContainsKey(signal))
            {
                e = dic[signal];
            }
            else
            {
                e = new QEvent();
                dic.Add(signal, e);
            }
            if (e == null) return true;
            e.Add(callback);
            return false;
        }

        private bool RemoveEvent(T signal, Delegate callback)
        {
            if (!dic.ContainsKey(signal)) return true;
            var e = dic[signal];
            e.Remove(callback);
            return false;
        }

        public void OnEvent<R>(object sender, T signal, Action<R> callback) where R : class
        {
            if (dic.ContainsKey(signal))
            {
                var item = dic[signal];
                var handle = item.Handle;
                if (handle != null)
                {
                    var action = handle as R;
                    if (action == null)
                    {
                        throw ConversionType;
                    }
                    try
                    {
                        this.sender = sender;
                        callback(action);
                    }
                    catch (Exception exception)
                    {
                        throw new ArgumentException(exception.Message);
                    }
                }
            }
        }
    }

    public partial class QMessageCenter<T>
    {
        public bool AddEventListener(T signal, Action callback)
        {
            return AddEvent(signal, callback);
        }

        public bool AddEventListener<R>(T signal, Action<R> callback)
        {
            return AddEvent(signal, callback);
        }
        public bool AddEventListener<R, Q>(T signal, Action<R, Q> callback)
        {
            return AddEvent(signal, callback);
        }
        public bool AddEventListener<R, Q, W>(T signal, Action<R, Q, W> callback)
        {
            return AddEvent(signal, callback);
        }
        public bool AddEventListener<R, Q, W, E>(T signal, Action<R, Q, W, E> callback)
        {
            return AddEvent(signal, callback);
        }

        public bool RemoveEventListener(T signal, Action callback)
        {
            return RemoveEvent(signal, callback);
        }
        public bool RemoveEventListener<R>(T signal, Action<R> callback)
        {
            return RemoveEvent(signal, callback);
        }
        public bool RemoveEventListener<R, Q>(T signal, Action<R, Q> callback)
        {
            return RemoveEvent(signal, callback);
        }
        public bool RemoveEventListener<R, Q, W>(T signal, Action<R, Q, W> callback)
        {
            return RemoveEvent(signal, callback);
        }
        public bool RemoveEventListener<R, Q, W, E>(T signal, Action<R, Q, W, E> callback)
        {
            return RemoveEvent(signal, callback);
        }

        public void TirggerEvent(object sender, T signal)
        {
            OnEvent<Action>(sender, signal, value =>
            {
                value();
            });
        }

        public void TirggerEvent<R>(object sender, T signal, R arg)
        {
            OnEvent<Action<R>>(sender, signal, value =>
            {
                value(arg);
            });
        }
        public void TirggerEvent<R, Q>(object sender, T signal, R arg, Q arg1)
        {
            OnEvent<Action<R, Q>>(sender, signal, value =>
            {
                value(arg, arg1);
            });
        }
        public void TirggerEvent<R, Q, W>(object sender, T signal, R arg, Q arg1, W arg2)
        {
            OnEvent<Action<R, Q, W>>(sender, signal, value =>
            {
                value(arg, arg1, arg2);
            });
        }
        public void TirggerEvent<R, Q, W, E>(object sender, T signal, R arg, Q arg1, W arg2, E arg3)
        {
            OnEvent<Action<R, Q, W, E>>(sender, signal, value =>
            {
                value(arg, arg1, arg2, arg3);
            });
        }


    }
}