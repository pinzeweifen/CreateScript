using UnityEngine;
using UnityEngine.UI;

public class QInputField : QBaseUI
{
    private QText text;
    private QText placeholder;
    public InputField target;
    public QText Text{get{return text??(text=new QText(target.textComponent));}}
    public QText Placeholder{get{return placeholder??(placeholder=new QText(target.placeholder.GetComponent<Text>()));}}
    public QInputField(InputField value) : base(value.transform)
    {
        target = value;
    }

}