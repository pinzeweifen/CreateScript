using UnityEngine;
using UnityEngine.UI;

public class QDropdown : QBaseUI
{
    public Dropdown target;
    public QDropdown(Dropdown value) : base(value.transform)
    {
        target = value;
    }

}