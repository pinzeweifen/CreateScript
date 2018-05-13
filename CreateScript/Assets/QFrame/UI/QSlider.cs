using UnityEngine;
using UnityEngine.UI;

public class QSlider : QBaseUI
{
    private QImage background;
    private QImage fill;
    private QImage handle;
    public Slider target;
    public QImage QBackground{get{return background ??(background = new QImage(transform.Find("Background").GetComponentInChildren<Image>()));}}
    public QImage QFill{get{return fill ??(fill = new QImage(target.fillRect.GetComponentInChildren<Image>()));}}
    public QImage QHandle{get{return handle ??(handle = new QImage(target.handleRect.GetComponentInChildren<Image>()));}}
    
    public QSlider(Slider value) : base(value.transform)
    {
        target = value;
    }

}