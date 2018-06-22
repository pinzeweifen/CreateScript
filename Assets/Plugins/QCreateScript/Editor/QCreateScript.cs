using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace CreateScript
{
    public class QCreateScript : EditorWindow
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
            if (QConfigure.selectTransform != null)
                manager.Init();
        }

        [InitializeOnLoadMethod]
        private static void App()
        {
            Selection.selectionChanged += () =>
            {
                if (window != null)
                {
                    window.manager.Clear();
                    window.Repaint();
                }
            };
        }

        private void OnGUI()
        {
            if (QConfigure.selectTransform != Selection.activeTransform)
            {
                if (QConfigure.selectTransform != null)
                {
                    manager.Clear();
                }
                QConfigure.selectTransform = Selection.activeTransform;
                if (QConfigure.selectTransform != null)
                {
                    manager.Init();
                }
                if(Selection.gameObjects.Length==1){
                    QConfigure.selectTransform = Selection.gameObjects[0].transform;
                    if(QConfigure.selectTransform!=null){
                        manager.Init();
                    }
                }
            }


            Test();
            EditorGUILayout.BeginHorizontal();
            {
                if (QConfigure.version == 2)
                {
                    DrawLeftListView();
                }
                DrawLeft();
                DrawRight();
            }
            EditorGUILayout.EndHorizontal();

            if (!EditorApplication.isCompiling)
            {
                if (QConfigure.IsCompiling())
                {
                    manager.Init();
                }
            }
        }

        private void Test()
        {/*
            if(GUILayout.Button("Test")){
                manager.GetBindingInfoToJson();
            }*/
        }

        private void DrawCreatePath()
        {
            QConfigure.referencePath = EditorPrefs.GetString("文件生成路径", QConfigure.referencedefaultPath);
            QConfigure.referencePath = EditorGUILayout.TextField("生成路径：", QConfigure.referencePath);
            EditorPrefs.SetString("文件生成路径", QConfigure.referencePath);
        }

        Vector2 leftViewPos;
        private void DrawLeftListView()
        {
            /*EditorGUILayout.BeginVertical(GUILayout.MaxWidth(200));
            {
                EditorGUILayout.LabelField("已生成界面", "");
                leftViewPos = EditorGUILayout.BeginScrollView(leftViewPos);
                {
                }
                EditorGUILayout.EndScrollView();
            }
            EditorGUILayout.EndVertical();*/
        }

        private void DrawLeft()
        {
            EditorGUILayout.BeginVertical();
            {
                DrawCreatePath();
                EditorGUILayout.Space();
                QConfigure.Version = EditorGUILayout.Popup("版本", QConfigure.Version, QConfigure.versionStr);
                EditorGUILayout.Space();
                EditorGUILayout.BeginHorizontal();
                {
                    if (QConfigure.version == 1)
                    {
                        QConfigure.isCreateModel = EditorPrefs.GetBool("是否生成Model", true);
                        QConfigure.isCreateModel = EditorGUILayout.ToggleLeft("是否生成Model", QConfigure.isCreateModel, GUILayout.MaxWidth(100));
                        EditorPrefs.SetBool("是否生成Model", QConfigure.isCreateModel);
                    }

                    QConfigure.isCreateController = EditorPrefs.GetBool("是否生成Controller", true);
                    QConfigure.isCreateController = EditorGUILayout.ToggleLeft("是否生成Controller", QConfigure.isCreateController, GUILayout.MaxWidth(120));
                    EditorPrefs.SetBool("是否生成Controller", QConfigure.isCreateController);

                }
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space();
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

                //EditorGUILayout.BeginVertical();
                {
                    pos = EditorGUILayout.BeginScrollView(pos, GUILayout.Width(position.width * 0.5f));
                    {
                        if (QConfigure.selectTransform != null)
                        {
                            var str = manager.ToString();
                            var array = QGlobalFun.GetStringList(str);
                            EditorGUILayout.BeginVertical();
                            {
                                foreach (var item in array)
                                {
                                    GUILayout.Label(item);
                                }
                            }
                            EditorGUILayout.EndVertical();

                        }
                    }
                    EditorGUILayout.EndScrollView();
                }
                //EditorGUILayout.EndVertical();
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
                if (QConfigure.selectTransform != null)
                {
                    DrawRow(QConfigure.selectTransform);
                }
            }
            EditorGUILayout.EndScrollView();
        }

        private void DrawRow(Transform tr, int depth = 0)
        {
            foreach (Transform t in tr)
            {
                if (manager[t].state.Update(t, depth) && t.childCount > 0)
                {
                    DrawRow(t, depth + 1);
                }
            }
        }
    }
}