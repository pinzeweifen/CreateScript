using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEngine.Events;

public abstract class QBaseUI
{
    protected Graphic graphic;
    protected List< EventTrigger.Entry> trigger;
    protected CanvasGroup canvasGroup;
    protected RectTransform transform;
    protected IAnimation currentAnimation;
    protected Dictionary<string, IAnimation> animationDic = new Dictionary<string, IAnimation>();
    
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

    protected List<EventTrigger.Entry> Triggers {
        get {
            if(trigger == null)
            {
                trigger = new List<EventTrigger.Entry>();
                QGlobalFun.GetComponent<EventTrigger>(transform.gameObject).triggers = trigger;
            }
            return trigger;
        }
    }

    public QBaseUI(Transform transform)
    {
        this.transform = transform as RectTransform;
    }

    public void AddAnimatin(string key, IAnimation animation)
    {
        if (!animationDic.ContainsKey(key))
        {
            animationDic.Add(key, animation);
            return;
        }
        animationDic[key] = animation;
    }

    public void SetCurrentAnimation(string key)
    {
        if (!animationDic.ContainsKey(key))
        {
            currentAnimation = null;
            return;
        }
        currentAnimation = animationDic[key];
    }

    public void Play()
    {
        if (currentAnimation != null)
            currentAnimation.Play();
    }

    public void Pause()
    {
        if (currentAnimation != null)
            currentAnimation.Pause();
    }

    public void Stop()
    {
        if (currentAnimation != null)
            currentAnimation.Stop();
    }
    
    public void AddEvent(EventTriggerType type, UnityAction<BaseEventData> callback)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = type;
        entry.callback = new EventTrigger.TriggerEvent();
        entry.callback.AddListener(callback);
        Triggers.Add(entry);
    }
}