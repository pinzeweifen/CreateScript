using UnityEngine;
using UnityEngine.UI;

public class QSlider : QBaseUI
{
    public Slider target;
    public QSlider(Slider value) : base(value.transform)
    {
        target = value;
    }

}