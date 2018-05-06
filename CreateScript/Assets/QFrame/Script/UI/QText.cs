using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QText : QBaseUI
{
    public Text target;
    public QText(Text value) : base(value.transform)
    {
        target = value;
    }
}