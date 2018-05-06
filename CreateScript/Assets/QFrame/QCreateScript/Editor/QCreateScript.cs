using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class QCreateScript :EditorWindow
{
    public static QCreateScript window;

    private QManagerVariable manager = new QManagerVariable();

    [MenuItem("Tools/CreateUIScript")]
    private static void ShowWindow()
    {
        window = GetWindow<QCreateScript>();
        window.Show();
    }

    private void Awake()
    {
        manager = new QManagerVariable();
    }

    [InitializeOnLoadMethod]
    private static void App()
    {
        Selection.selectionChanged += () => {
            if (window != null)
            {
                window.manager.Clear();
                window.Repaint();
            }
        };
    }

    private void OnGUI()
    {
        Test();
        EditorGUILayout.BeginHorizontal();
        {
            DrawLeft();
            DrawRight();
        }
        EditorGUILayout.EndHorizontal();
    }

    private void Test()
    {
        //if (GUILayout.Button("测试"))
        {
        }
    }

    private void DrawLeft()
    {
        EditorGUILayout.BeginVertical();
        {
            QConfigure.referencePath = EditorGUILayout.TextField("生成路径：",QConfigure.referencePath);
            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("生成脚本")) manager.CreateFile();
                if (GUILayout.Button("更新脚本")) manager.Update();
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("复制代码")) manager.Copy();
                if (GUILayout.Button("挂载脚本")) manager.MountScript();
                if (GUILayout.Button("绑定UI")) manager.BindingUI();
            }
            EditorGUILayout.EndHorizontal();

            DrawTable();
        }
        EditorGUILayout.EndVertical();
    }

    private Vector2 pos;
    private void DrawRight()
    {
        EditorGUILayout.BeginVertical();
        {
            var rect = EditorGUILayout.GetControlRect();
            rect.height = 35;
            GUI.Box(rect, "代码预览", "GroupBox");
            GUILayout.Space(20);

            EditorGUILayout.BeginVertical();
            {
                pos = EditorGUILayout.BeginScrollView(pos, GUILayout.Width(position.width * 0.5f));
                {
                    if (Selection.activeTransform != null)
                        GUILayout.Label(manager.ToString());
                }
                EditorGUILayout.EndScrollView();
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndVertical();
    }

    private Vector2 tablePos;
    private void DrawTable()
    {
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("全选变量")) manager.TotalSelectVariable();
            if (GUILayout.Button("全选属性器")) manager.TotalAttribute();
            if (GUILayout.Button("全选事件")) manager.TotalEvent();
            if (GUILayout.Button("全折叠")) manager.TotalFold(false);
            if (GUILayout.Button("查找赋值")) manager.isFind = true;
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("全取消变量")) manager.TotalSelectVariable(false);
            if (GUILayout.Button("全取消属性器")) manager.TotalAttribute(false);
            if (GUILayout.Button("全取消事件")) manager.TotalEvent(false);
            if (GUILayout.Button("全展开")) manager.TotalFold();
            if (GUILayout.Button("取消查找")) manager.isFind = false;
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
        tablePos = EditorGUILayout.BeginScrollView(tablePos);
        {
            if(Selection.activeTransform != null)
            {
                DrawRow(Selection.activeTransform);
            }
        }
        EditorGUILayout.EndScrollView();
    }
    
    private void DrawRow(Transform tr, int depth = 0)
    {
        foreach(Transform t in tr)
        {
            if(manager[t].state.Update(t, depth) && t.childCount > 0)
            {
                DrawRow(t, depth + 1);
            }
        }
    }
}
