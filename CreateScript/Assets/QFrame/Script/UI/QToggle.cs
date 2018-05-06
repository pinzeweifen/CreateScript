using UnityEngine;
using UnityEngine.UI;

public class QToggle : QBaseUI
{
    public Toggle target;
    public QToggle(Toggle value) : base(value.transform)
    {
        target = value;
    }

}