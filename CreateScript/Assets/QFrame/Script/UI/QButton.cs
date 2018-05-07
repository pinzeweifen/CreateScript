using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QButton : QBaseUI 
{
    private QText text;
    public Button target;
    public QText Text{get{return text??(text = new QText( target.GetComponentInChildren<Text>()));}}
    public QButton(Button value) : base(value.transform)
    {
        target = value;
        
    }

}