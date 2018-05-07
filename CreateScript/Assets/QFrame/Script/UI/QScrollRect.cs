using UnityEngine;
using UnityEngine.UI;

public class QScrollRect : QBaseUI
{
    private QImage viewport;
    private QScrollbar horizontal;
    private QScrollbar vertical;
    public ScrollRect target;

    public QScrollbar Horizontal{get{return horizontal ??(horizontal = new QScrollbar(target.horizontalScrollbar));}}
    public QScrollbar Vertical{get{return vertical ??(vertical = new QScrollbar(target.verticalScrollbar));}}
    public QImage Viewport{get{return viewport ??(viewport = new QImage(target.viewport.GetComponentInChildren<Image>()));}}
    public QScrollRect(ScrollRect value) : base(value.transform)
    {
        target = value;
    }

}