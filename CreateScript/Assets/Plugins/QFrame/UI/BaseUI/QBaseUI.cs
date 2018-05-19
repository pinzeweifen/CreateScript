using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class QBaseUI
{
    protected QUIEvent uIEvent;
    protected QAnimation animation;
    protected Graphic graphic;
    protected CanvasGroup canvasGroup;
    protected RectTransform transform;
    
    public QUIEvent UIEvent{get{return uIEvent??(uIEvent = new QUIEvent(transform));}}
    public QAnimation Animation{get{return animation??(animation=new QAnimation());}}
    public Graphic Graphic { get { return graphic ?? (graphic=QGlobalFun.GetComponent<Graphic>(transform.gameObject)); } }
    public RectTransform Transform { get { return transform; } }
    public CanvasGroup CanvasGroup { get { return canvasGroup ?? (canvasGroup=QGlobalFun.GetComponent<CanvasGroup>(transform.gameObject)); } }
    public float Width{get{return Transform.sizeDelta.x;}set{Transform.sizeDelta = new Vector2( value,Transform.sizeDelta.y );}}
    public float Height{get{return Transform.sizeDelta.y;}set{Transform.sizeDelta = new Vector2( Transform.sizeDelta.x, value );}}

    public float Alpha
    {
        get { return Graphic.color.a; }
        set
        {
            var color = Graphic.color;
            Graphic.color = new Color(color.r, color.g, color.b, value);
        }
    }

    public Color Color{
        get{return Graphic.color;}
        set{Graphic.color = value;}
    }

    public Vector2 Size{
        get{return Transform.sizeDelta;}
        set{Transform.sizeDelta = value;}
    }

    public QBaseUI(Transform transform)
    {
        this.transform = transform as RectTransform;
    }

}