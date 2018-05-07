using UnityEngine;
using UnityEngine.UI;

public class QToggle : QBaseUI
{
    private QImage background;
    private QImage checkmark;
    private QText label;
    public Toggle target;
    public QText Label{get{return label ??(label = new QText(target.GetComponentInChildren<Text>()));}}
    public QImage Background{get{return background ??(background = new QImage(target.targetGraphic.GetComponentInChildren<Image>()));}}
    public QImage Checkmark{get{return checkmark ??(checkmark = new QImage(target.graphic.GetComponentInChildren<Image>()));}}
    
    public QToggle(Toggle value) : base(value.transform)
    {
        target = value;
    }

}