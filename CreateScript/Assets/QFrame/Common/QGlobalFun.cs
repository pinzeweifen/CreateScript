﻿using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

public static class QGlobalFun
{
    private static readonly char[] replaceChars = new char[] { ' ', '(', ')' };

    public static string GetString(string value)
    {
        StringBuilder sb = new StringBuilder(value);
        foreach (var error in replaceChars)
        {
            sb.Replace(error, '_');
        }
        return sb.ToString();
    }

    public static T GetComponent<T>(GameObject go)where T:Component
    {
        T com = go.GetComponent<T>();
        if (com == null) com = go.AddComponent<T>();
        return com;
    }

    public static string GetFirstUpper(string value)
    {
        return char.ToUpper(value[0]) + value.Substring(1);
    }

    public static string[] GetGetComponentsName(Transform t)
    {
        var coms = t.GetComponents<Component>();
        if (coms == null) return null;
        List<string> types = new List<string>();

        for (int i = 0; i < coms.Length; i++)
        {
            if (coms[i] == null) continue;
            types.Add(coms[i].GetType().Name);
        }
        return types.ToArray();
    }

    public static string GetGameObjectPath(Transform target, Transform parent)
    {
        return GetFindPath(target, parent).Remove(0,1);
    }
    
    public static Assembly GetAssembly()
    {
        Assembly[] AssbyCustmList = System.AppDomain.CurrentDomain.GetAssemblies();
        for (int i = 0; i < AssbyCustmList.Length; i++)
        {
            string assbyName = AssbyCustmList[i].GetName().Name;
            if (assbyName == "Assembly-CSharp")
            {
                return AssbyCustmList[i];
            }
        }
        return null;
    }

    private static string GetFindPath(Transform target, Transform parent)
    {
        if (target == parent)
            return string.Empty;
        return GetFindPath(target.parent, parent) + "/" + target.name;
    }
}