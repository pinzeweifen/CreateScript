using UnityEngine;
using UnityEngine.UI;

public class QInputField : QBaseUI
{
    private QText label;
    private QText placeholder;
    public InputField target;
    public QText QLabel{get{return label??(label=new QText(target.textComponent));}}
    public QText QPlaceholder{get{return placeholder??(placeholder=new QText(target.placeholder.GetComponent<Text>()));}}

    public string Value{
        get{return target.text;}
        set{target.text =value;}
    }

    public QInputField(InputField value) : base(value.transform)
    {
        target = value;
    }

}