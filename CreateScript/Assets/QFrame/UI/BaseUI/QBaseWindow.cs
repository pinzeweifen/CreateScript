using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class QBaseWindow : MonoBehaviour 
{
    public Button[] returnButtons;

	private void Awake()
	{
        foreach(var button in returnButtons)
        {
            button.onClick.AddListener(HideWindow);
        }
	}

	public void ShowWindow()
    {

    }

    public void HideWindow()
    {
        QWindowNavigation.HideWindow();
    }
}
/*
 * 
/*
 * 按下按钮->申请切换界面->执行事件 ->成功切换界面
 * 
 */