using UnityEngine;
using UnityEngine.UI;

public class QInputField : QBaseUI
{
    public InputField target;
    public QInputField(InputField value) : base(value.transform)
    {
        target = value;
    }

}