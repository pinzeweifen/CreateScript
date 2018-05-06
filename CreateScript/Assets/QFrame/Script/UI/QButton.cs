using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QButton : QBaseUI 
{
    public Button target;
    public QButton(Button value) : base(value.transform)
    {
        target = value;
    }

}