using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class QEventListener : 
MonoBehaviour,
IPointerClickHandler,
IPointerDownHandler,
IPointerUpHandler,
IPointerEnterHandler,
IPointerExitHandler,
IInitializePotentialDragHandler,
IBeginDragHandler,
IEndDragHandler,
IDragHandler,
IDropHandler,
IScrollHandler,
ISelectHandler,
IUpdateSelectedHandler,
IDeselectHandler,
IMoveHandler,
ISubmitHandler,
ICancelHandler
{
    public Action<PointerEventData> onClick;
    public Action<PointerEventData> onDoubleClick;
    public Action<PointerEventData> onMouseUp;
    public Action<PointerEventData> onMouseDown;
    public Action<PointerEventData> onEnter;
    public Action<PointerEventData> onExit;
    public Action<PointerEventData> onInitializePotentialDrag;
    public Action<PointerEventData> onBeginDrag;
    public Action<PointerEventData> onDrop;
    public Action<PointerEventData> onDrag;
    public Action<PointerEventData> onEndDrag;
    public Action<PointerEventData> onScroll;
    public Action<BaseEventData> onSelect;
    public Action<BaseEventData> onUpdateSelected;
    public Action<BaseEventData> onSubmit;
    public Action<BaseEventData> onCancel;
    public Action<AxisEventData> onMove;
    public Action<BaseEventData> onDeselect;

    public static QEventListener Get(GameObject go)
    {
        QEventListener listener = go.GetComponent<QEventListener>();
        if (listener == null) listener = go.AddComponent<QEventListener>();
        return listener;
    }

    public static void SetFocus(GameObject go)
    {
        if(EventSystem.current!=null)
            EventSystem.current.SetSelectedGameObject(go);
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        SetFocus(gameObject);
        if(eventData.clickCount == 1)
        {
            if (onClick != null) onClick(eventData);
        }
        else if(eventData.clickCount == 2)
        {
            if (onDoubleClick != null) onDoubleClick(eventData);
        }
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        if (onMouseUp != null) onMouseUp(eventData);
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        SetFocus(gameObject);
        if (onMouseDown != null) onMouseDown(eventData);
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if (onEnter != null)
            onEnter(eventData);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if (onExit != null)
            onExit(eventData);
    }

    void IInitializePotentialDragHandler.OnInitializePotentialDrag(PointerEventData eventData)
    {
        SetFocus(gameObject);
        if (onInitializePotentialDrag != null)
            onInitializePotentialDrag(eventData);
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        if (onBeginDrag != null)
            onBeginDrag(eventData);
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if (onDrag != null)
            onDrag(eventData);
    }

    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        if (onDrop != null)
            onDrop(eventData);
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        SetFocus(gameObject);
        if (onEndDrag != null)
            onEndDrag(eventData);
    }

    void IScrollHandler.OnScroll(PointerEventData eventData)
    {
        if (onScroll != null)
            onScroll(eventData);
    }

    void ISelectHandler.OnSelect(BaseEventData eventData)
    {
        if (onSelect != null)
            onSelect(eventData);
    }

    void IUpdateSelectedHandler.OnUpdateSelected(BaseEventData eventData)
    {
        if (onUpdateSelected != null)
            onUpdateSelected(eventData);
    }

    void ISubmitHandler.OnSubmit(BaseEventData eventData)
    {
        if (onSubmit != null)
            onSubmit(eventData);
    }

    void ICancelHandler.OnCancel(BaseEventData eventData)
    {
        if (onCancel != null)
            onCancel(eventData);
    }

    void IMoveHandler.OnMove(AxisEventData eventData)
    {
        if (onMove != null)
            onMove(eventData);
    }

    void IDeselectHandler.OnDeselect(BaseEventData eventData)
    {
        if (onDeselect != null)
            onDeselect(eventData);
    }
}