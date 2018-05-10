using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems
    ;

public class QImage : QBaseUI
{
    public Image target;
    
    public Sprite QValue
    {
        get { return target.sprite; }
        set { target.sprite = value; }
    }

    public QImage(Image value):base(value.rectTransform)
    {
        target = value;
    }


}