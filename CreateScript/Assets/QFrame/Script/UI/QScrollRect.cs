using UnityEngine;
using UnityEngine.UI;

public class QScrollRect : QBaseUI
{
    public ScrollRect target;
    public QScrollRect(ScrollRect value) : base(value.transform)
    {
        target = value;
    }

}