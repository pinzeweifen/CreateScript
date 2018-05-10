using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class QEvent{
    private static readonly ArgumentException addNullException = new ArgumentException(" source和value为空");
    private static readonly ArgumentException equalException = new ArgumentException("类型不匹配");
    private static readonly ArgumentException removeException = new ArgumentException("source为空");
    private Delegate handle;
    public Delegate Handle{get{return handle;}}

    public void Add(Delegate callback)
    {
        if(handle == null && callback == null){
            throw addNullException;
        }
        if(handle!=null && callback != null && !Equals(callback)){
            throw equalException;
        }
        handle = Delegate.Combine(handle,callback);
    }
    
    public void Remove(Delegate callback){
        if(handle==null){
            throw removeException;
        }
        if(handle!=null && callback != null && !Equals(callback)){
            throw equalException;
        }
        handle = Delegate.Remove(handle,callback);
    }

    private bool Equals(Delegate callback){
        return handle.GetType() == callback.GetType();
    }
}

