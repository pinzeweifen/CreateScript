using UnityEngine;
using UnityEngine.UI;

public class QScrollbar : QBaseUI
{
    public Scrollbar target;
    public QScrollbar(Scrollbar value) : base(value.transform)
    {
        target = value;
    }

}