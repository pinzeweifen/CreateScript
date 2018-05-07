using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEngine.Events;

public abstract class QBaseUI
{
    protected QUIEvent uIEvent;
    protected QAnimation animation;
    protected Graphic graphic;
    protected CanvasGroup canvasGroup;
    protected RectTransform transform;
    
    public QUIEvent UIEvent{get{return uIEvent??(uIEvent = new QUIEvent(transform));}}
    public QAnimation Animation{get{return animation??(animation=new QAnimation());}}
    protected Graphic Graphic { get { return graphic ?? (graphic=QGlobalFun.GetComponent<Graphic>(transform.gameObject)); } }
    public RectTransform Transform { get { return transform; } }
    public CanvasGroup CanvasGroup { get { return canvasGroup ?? (canvasGroup=QGlobalFun.GetComponent<CanvasGroup>(transform.gameObject)); } }

    public float Alpha
    {
        get { return graphic.color.a; }
        set
        {
            var color = graphic.color;
            graphic.color = new Color(color.r, color.g, color.b, value);
        }
    }

    public QBaseUI(Transform transform)
    {
        this.transform = transform as RectTransform;
    }

}