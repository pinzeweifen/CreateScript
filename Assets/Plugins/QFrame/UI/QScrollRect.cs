using UnityEngine;
using UnityEngine.UI;

public class QScrollRect : QBaseUI
{
    private RectTransform content;
    private QImage viewport;
    private QScrollbar horizontal;
    private QScrollbar vertical;
    public ScrollRect target;

    public RectTransform QContent{get{return content??(content=target.content);}}
    public QScrollbar QHorizontal{get{return horizontal ??(horizontal = new QScrollbar(target.horizontalScrollbar));}}
    public QScrollbar QVertical{get{return vertical ??(vertical = new QScrollbar(target.verticalScrollbar));}}
    public QImage QViewport{get{return viewport ??(viewport = new QImage(target.viewport.GetComponentInChildren<Image>()));}}
    public QScrollRect(ScrollRect value) : base(value.transform)
    {
        target = value;
        
    }

}