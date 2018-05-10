using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using QFrame;

public static class QMessage
{
    private static QMessageCenter<QMsgType> messageCenter = new QMessageCenter<QMsgType>();

    public static object GetSender(){
        return messageCenter.GetSender();
    }

    public static void AddEventListener(QMsgType signal, Action callback)
    {
        messageCenter.AddEventListener( signal, callback);
    }
    public static void AddEventListener<T>(QMsgType signal, Action<T> callback)
    {
        messageCenter.AddEventListener( signal, callback);
    }
    public static void AddEventListener<T, Q>(QMsgType signal, Action<T, Q> callback)
    {
        messageCenter.AddEventListener( signal, callback);
    }
    public static void AddEventListener<T, Q, W>(QMsgType signal, Action<T, Q, W> callback)
    {
        messageCenter.AddEventListener( signal, callback);
    }
    public static void AddEventListener<T, Q, W, E>(QMsgType signal, Action<T, Q, W, E> callback)
    {
        messageCenter.AddEventListener( signal, callback);
    }

    public static void RemoveEventListener(QMsgType signal, Action callback)
    {
        messageCenter.RemoveEventListener(signal, callback);
    }
    public static void RemoveEventListener<T>(QMsgType signal, Action<T> callback)
    {
        messageCenter.RemoveEventListener(signal, callback);
    }
    public static void RemoveEventListener<T, Q>(QMsgType signal, Action<T, Q> callback)
    {
        messageCenter.RemoveEventListener(signal, callback);
    }
    public static void RemoveEventListener<T, Q, W>(QMsgType signal, Action<T, Q, W> callback)
    {
        messageCenter.RemoveEventListener(signal, callback);
    }
    public static void RemoveEventListener<T, Q, W, E>(QMsgType signal, Action<T, Q, W, E> callback)
    {
        messageCenter.RemoveEventListener(signal, callback);
    }
    public static object TirggerEvent(this object sender,QMsgType signal){
        messageCenter.TirggerEvent(sender,signal);
        return sender;
    }
    public static object TirggerEvent<T>(this object sender,QMsgType signal,T arg){
        messageCenter.TirggerEvent(sender,signal,arg);
        return sender;
    }
    public static object TirggerEvent<T,Q>(this object sender,QMsgType signal,T arg, Q arg1){
        messageCenter.TirggerEvent(sender,signal,arg,arg1);
        return sender;
    }
    public static object TirggerEvent<T,Q,W>(this object sender,QMsgType signal,T arg, Q arg1, W arg2){
        messageCenter.TirggerEvent(sender,signal,arg,arg1,arg2);
        return sender;
    }
    public static object TirggerEvent<T,Q,W,E>(this object sender,QMsgType signal,T arg, Q arg1, W arg2, E arg3){
        messageCenter.TirggerEvent(sender,signal,arg,arg1,arg2,arg3);
        return sender;
    }
}
