using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QRawImage : QBaseUI
{
    public RawImage target;
    
    public Texture Value
    {
        get { return target.texture; }
        set { target.texture = value; }
    }

    public QRawImage(RawImage value):base(value.transform)
    {
        target = value;
    }
}