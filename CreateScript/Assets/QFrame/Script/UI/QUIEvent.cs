using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class QUIEvent
{
    protected List<EventTrigger.Entry> trigger;
    protected Dictionary<EventTriggerType, EventTrigger.Entry> dic = new Dictionary<EventTriggerType, EventTrigger.Entry>();
    public QUIEvent(Transform transform)
    {
        trigger = new List<EventTrigger.Entry>();
        QGlobalFun.GetComponent<EventTrigger>(transform.gameObject).triggers = trigger;
    }

    public void AddEvent(EventTriggerType type, UnityAction<BaseEventData> callback)
    {
        EventTrigger.Entry entry;
        if (dic.ContainsKey(type))
        {
            entry = dic[type];
        }
        else
        {
            entry = new EventTrigger.Entry();
            entry.eventID = type;
            entry.callback = new EventTrigger.TriggerEvent();
            dic.Add(type, entry);
            trigger.Add(entry);
        }
        entry.callback.AddListener(callback);
    }

    public void RemoveEvent(EventTriggerType type, UnityAction<BaseEventData> callback)
    {
        if(dic.ContainsKey(type))
        {
            dic[type].callback.RemoveListener(callback);
        }
    }

    public void RemoveAllEvent(EventTriggerType type){
        if(dic.ContainsKey(type))
        {
            dic[type].callback.RemoveAllListeners();
        }
    }

}
