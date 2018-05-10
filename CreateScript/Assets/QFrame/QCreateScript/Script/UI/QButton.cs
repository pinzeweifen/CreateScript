using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QButton : QBaseUI 
{
    private QText label;
    public Button target;
    public QText QLabel{get{return label??(label = new QText( target.GetComponentInChildren<Text>()));}}
    public QButton(Button value) : base(value.transform)
    {
        target = value;
    }

}