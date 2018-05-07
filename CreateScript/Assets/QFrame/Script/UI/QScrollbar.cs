using UnityEngine;
using UnityEngine.UI;

public class QScrollbar : QBaseUI
{
    private QImage handle;
    public Scrollbar target;
    public QImage Handle{get{return handle??(handle=new QImage(target.targetGraphic.GetComponent<Image>()));}}
    public QScrollbar(Scrollbar value) : base(value.transform)
    {
        target = value;
    }

}