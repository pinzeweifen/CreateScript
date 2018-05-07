using UnityEngine;
using UnityEngine.UI;

public class QDropdown : QBaseUI
{
    private QText captionText;
    private QImage captionImage;
    private QImage arrow;
    private QScrollRect template;
    public Dropdown target;
    public QText CaptionText{get{return captionText??(captionText=new QText(target.captionText));}}
    public QImage CaptionImage{get{return captionImage??(captionImage=new QImage(target.captionImage));}}
    public QImage Arrow{get{return arrow??(arrow=new QImage(transform.Find("Arrow").GetComponent<Image>()));}}
    public QScrollRect Template{get{return template??(template=new QScrollRect(target.template.GetComponent<ScrollRect>()));}}
    public QDropdown(Dropdown value) : base(value.transform)
    {
        target = value;
    }

}