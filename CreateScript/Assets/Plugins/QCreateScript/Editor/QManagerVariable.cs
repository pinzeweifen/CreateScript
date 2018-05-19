using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;
using System.IO;
using System.Reflection;

namespace CreateScript
{
    public class QManagerVariable
    {
        public bool isFind = false;

        private Dictionary<Transform, QVariableModel> dic = new Dictionary<Transform, QVariableModel>();

        public QVariableModel this[Transform t]
        {
            get
            {
                if (!dic.ContainsKey(t))
                {
                    dic.Add(t, new QVariableModel(t));
                }
                return dic[t];
            }
        }

        StringBuilder variable = new StringBuilder();
        StringBuilder controllerEvent = new StringBuilder();
        StringBuilder attributeVariable = new StringBuilder();
        StringBuilder attribute = new StringBuilder();
        StringBuilder find = new StringBuilder();
        StringBuilder newAttribute = new StringBuilder();
        StringBuilder register = new StringBuilder();
        StringBuilder function = new StringBuilder();
        public string GetBuildUICode()
        {
            newAttribute.Length =
            attributeVariable.Length =
            function.Length =
            register.Length =
            variable.Length =
            controllerEvent.Length =
            attribute.Length =
            find.Length = 0;

            foreach (var value in dic.Values)
            {
                if (!value.state.isVariable) continue;
                variable.AppendFormat(QConfigure.variableFormat, value.type, value.name);

                if (isFind)
                    //find.AppendFormat("\t\t{0,-25} = transform.Find(\"{1}\").GetComponent<{2}>();\n", value.name, value.path, value.type);
                    find.AppendFormat(QConfigure.findFormat, value.name, value.path, value.type);

                if (value.state.isAttribute)
                {
                    if (value.isUI)
                    {
                        /*attributeVariable.AppendFormat("\tprivate Q{0} q{1};\n", value.type, value.name);
                        attribute.AppendFormat("\tpublic Q{0} Q{1}{{get{{return q{1};}}}}\n", value.type, value.name);
                        newAttribute.AppendFormat("\t\tq{0,-49} = new Q{1}({0});\n", value.name, value.type);*/
                        attributeVariable.AppendFormat(QConfigure.attributeVariableFormat, value.type, value.name);
                        attribute.AppendFormat(QConfigure.attributeFormat, value.type, value.name);
                        newAttribute.AppendFormat(QConfigure.newAttributeFormat, value.name, value.type);
                    }
                    else
                    {
                        //attribute.AppendFormat("\tpublic {0} Q{1}{{get{{return {1};}}}}\n", value.type, value.name);
                        attribute.AppendFormat(QConfigure.attribute2Format, value.type, value.name);
                    }
                }

                if (value.variableEvent != string.Empty && value.state.isEvent)
                {
                    /*register.AppendFormat("\t\t{0}.{1}.AddListener( {2} );\n", value.name, value.variableEvent, value.eventName);
                    controllerEvent.AppendFormat("\tpublic Action{0,-20} {1};\n", value.type == "Button" ? string.Empty : string.Format("<{0}>", value.eventType), value.attributeName);
                    function.AppendFormat("\tprivate void {0}({1})\n\t{{\n\t\tif({2}!=null){2}({3});\n\t}}\n", value.eventName,
                        value.eventType + (value.eventType != string.Empty ? " value" : ""), value.attributeName,
                        value.type == "Button" ? string.Empty : "value");*/
                    register.AppendFormat(QConfigure.registerFormat, value.name, value.variableEvent, value.eventName);
                    controllerEvent.AppendFormat(QConfigure.controllerEventFormat, value.IsButton() ? string.Empty : string.Format("<{0}>", value.eventType), value.attributeName);
                    function.AppendFormat(QConfigure.functionFormat, value.eventName,
                        value.eventType + (!value.eventType.IsLengthZero() ? " value" : string.Empty), value.attributeName,
                        value.IsButton() ? string.Empty : "value");
                    //Debug.Log(value.IsButton());
                }
            }

            var tmp = string.Format(QConfigure.uiClassCode,
                variable, attributeVariable, controllerEvent, attribute, find, newAttribute, register, function);
            return string.Format(QConfigure.uiCode, QGlobalFun.GetString(Selection.activeTransform.name), tmp);
        }

        StringBuilder assignment = new StringBuilder();
        StringBuilder declare = new StringBuilder();
        StringBuilder fun = new StringBuilder();
        public string GetControllerBuildCode()
        {
            assignment.Length =
            declare.Length =
            fun.Length = 0;
            string type = string.Empty;
            foreach (var value in dic.Values)
            {
                if (value.variableEvent != string.Empty && value.state.isEvent)
                {
                    type = value.IsButton() ? string.Empty : string.Format("{0} value", value.eventType);
                    //assignment.AppendFormat("\t\tui.{0,-50} = {1};\n", value.attributeName, value.eventName);
                    assignment.AppendFormat(QConfigure.assignmentFormat, value.attributeName, value.eventName);
                    //declare.AppendFormat("\tpartial void {0}({1});\n", value.attributeName, type);
                    declare.AppendFormat(QConfigure.declareFormat, value.attributeName, type);
                    /*fun.AppendFormat("\tprivate void {0}({1})\n\t{{\n\t\t{2}({3});\n\t}}\n",
                        value.eventName, type, value.attributeName, value.type == "Button" ? string.Empty : "value");*/
                    fun.AppendFormat(QConfigure.funFormat,value.eventName, type,
                        value.attributeName, value.IsButton() ? string.Empty : "value");
                }
            }

            return string.Format(
                QConfigure.controllerBuildCode,
                QGlobalFun.GetString(Selection.activeTransform.name),
                assignment,
                declare,
                fun);
        }

        public string GetUICode()
        {
            return string.Format(QConfigure.uiCode, QGlobalFun.GetString(Selection.activeTransform.name), QConfigure.uicodeOnAwake);
        }

        public string GetModelCode()
        {
            return string.Format(QConfigure.modelCode, QGlobalFun.GetString(Selection.activeTransform.name));
        }

        public string GetControllerCode()
        {
            return string.Format(QConfigure.controllerCode, QGlobalFun.GetString(Selection.activeTransform.name));
        }

        public override string ToString()
        {
            return GetBuildUICode();
        }

        public void Clear()
        {
            foreach (var value in dic.Values)
            {
                value.Reset();
            }
            dic.Clear();
        }

        public void TotalFold(bool isOn = true)
        {
            foreach (var value in dic.Values)
            {
                value.state.isOpen = isOn;
            }
        }

        public void TotalSelectVariable(bool isOn = true)
        {
            if (Selection.activeTransform != null)
                TotalSelect(Selection.activeTransform, isOn);
        }

        public void TotalAttribute(bool isOn = true)
        {
            foreach (var value in dic.Values)
            {
                if (!value.state.isVariable) continue;
                value.state.isAttribute = isOn;
            }
        }

        public void TotalEvent(bool isOn = true)
        {
            foreach (var value in dic.Values)
            {
                if (!value.state.isVariable || !value.state.isSelectEvent) continue;
                value.state.isEvent = isOn;
            }
        }

        private void TotalSelect(Transform tr, bool isOn)
        {
            foreach (Transform t in tr)
            {
                var tmp = dic[t];
                tmp.state.isVariable = isOn;
                if (tmp.state.isOpen && t.childCount > 0)
                {
                    TotalSelect(t, isOn);
                }
            }
        }

        public void CreateFile()
        {
            if (Selection.activeTransform == null)
            {
                EditorUtility.DisplayDialog(QConfigure.msgTitle, QConfigure.noSelect, QConfigure.ok);
                return;
            }
            if (EditorApplication.isCompiling)
            {
                EditorUtility.DisplayDialog(QConfigure.msgTitle, QConfigure.editorCompiling, QConfigure.ok);
                return;
            }
            if (QFileOperation.IsExists(QConfigure.FilePath(QConfigure.UIBuildFileName)))
            {
                EditorUtility.DisplayDialog(QConfigure.msgTitle, QConfigure.haveBeenCreated, QConfigure.ok);
                return;
            }

            QFileOperation.WriteText(QConfigure.FilePath(QConfigure.UIFileName), GetUICode());
            QFileOperation.WriteText(QConfigure.FilePath(QConfigure.UIBuildFileName), GetBuildUICode());
            QFileOperation.WriteText(QConfigure.FilePath(QConfigure.ModelFileName), GetModelCode());
            QFileOperation.WriteText(QConfigure.FilePath(QConfigure.ControllerFileName), GetControllerCode());
            QFileOperation.WriteText(QConfigure.FilePath(QConfigure.ControllerBuildFileName), GetControllerBuildCode());

            GetBindingInfo();
            AssetDatabase.Refresh();
        }

        public void Update()
        {
            if (Selection.activeGameObject == null) return;
            if (EditorApplication.isCompiling)
            {
                EditorUtility.DisplayDialog(QConfigure.msgTitle, QConfigure.editorCompiling, QConfigure.ok);
                return;
            }
            var fileName = QConfigure.FilePath(QConfigure.UIBuildFileName);
            if (!QFileOperation.IsExists(fileName))
            {
                EditorUtility.DisplayDialog(QConfigure.msgTitle, QConfigure.notCreate, QConfigure.ok);
                return;
            }
            QFileOperation.WriteText(QConfigure.FilePath(QConfigure.UIBuildFileName), GetBuildUICode(), FileMode.Create);
            QFileOperation.WriteText(QConfigure.FilePath(QConfigure.ControllerBuildFileName), GetControllerBuildCode(), FileMode.Create);

            GetBindingInfo();
            AssetDatabase.Refresh();
        }

        public void Copy()
        {
            if (Selection.activeGameObject == null) return;
            GUIUtility.systemCopyBuffer = GetBuildUICode();
            EditorUtility.DisplayDialog(QConfigure.msgTitle, QConfigure.copy, QConfigure.ok);
        }

        public void MountScript()
        {
            if (Selection.activeGameObject == null) return;

            if (EditorApplication.isCompiling)
            {
                EditorUtility.DisplayDialog(QConfigure.msgTitle, QConfigure.editorCompiling, QConfigure.ok);
                return;
            }

            var name = QConfigure.UIName;
            var scriptType = QGlobalFun.GetAssembly().GetType(name);
            if (scriptType == null)
            {
                EditorUtility.DisplayDialog(QConfigure.msgTitle, QConfigure.notCreate, QConfigure.ok);
                return;
            }
            var root = Selection.activeGameObject;
            var target = root.GetComponent(scriptType);
            if (target == null)
            {
                target = root.AddComponent(scriptType);
            }
        }

        public void BindingUI()
        {
            if (Selection.activeGameObject == null) return;
            if (EditorApplication.isCompiling)
            {
                EditorUtility.DisplayDialog(QConfigure.msgTitle, QConfigure.editorCompiling, QConfigure.ok);
                return;
            }
            var so = AssetDatabase.LoadAssetAtPath<QScriptInfo>(QConfigure.AssetPath);

            var infos = so.GetFieldInfos(QConfigure.UIName);
            if (infos == null)
            {
                EditorUtility.DisplayDialog(QConfigure.msgTitle, QConfigure.plugCreate, QConfigure.ok);
            }
            else
            {
                var assembly = QGlobalFun.GetAssembly();
                var type = assembly.GetType(QConfigure.UIName);
                
                if(type == null)
                {
                    EditorUtility.DisplayDialog(QConfigure.msgTitle, QConfigure.notCreate, QConfigure.ok);
                    return;
                }

                var root = Selection.activeTransform;
                var target = root.GetComponent(type);

                foreach (var info in infos)
                {
                    if (string.IsNullOrEmpty(info.name)) continue;
                    type.InvokeMember(info.name,
                                    BindingFlags.SetField |
                                    BindingFlags.Instance |
                                    BindingFlags.NonPublic,
                                    null, target, new object[] { root.Find(info.path).GetComponent(info.type) }, null, null, null);
                }
            }
        }


        private void GetBindingInfo()
        {
            QScriptInfo so;
            if (QFileOperation.IsExists(QConfigure.AssetPath))
            {
                so = AssetDatabase.LoadAssetAtPath<QScriptInfo>(QConfigure.AssetPath);
            }
            else
            {
                so = ScriptableObject.CreateInstance<QScriptInfo>();
            }
            
            List<string> k = new List<string>(dic.Count);
            List<string> t = new List<string>(dic.Count);
            List<string> p = new List<string>(dic.Count);

            foreach (var key in dic.Keys)
            {
                var target = dic[key];
                if (target.state.isVariable)
                {
                    k.Add(target.name);
                    t.Add(target.type.ToString());
                    p.Add(QGlobalFun.GetGameObjectPath(key, Selection.activeTransform));
                }
            }

            int count = k.Count;
            var infos = new QScriptInfo.FieldInfo[count];
            for (int i = 0; i < count; i++)
            {
                infos[i] = new QScriptInfo.FieldInfo();
                infos[i].name = k[i];
                infos[i].type = t[i];
                infos[i].path = p[i];
            }

            so.SetClassInfo(QConfigure.UIName, infos);

            if (QFileOperation.IsExists(QConfigure.AssetPath))
            {
                AssetDatabase.SaveAssets();
            }
            else
            {
                AssetDatabase.CreateAsset(so, QConfigure.AssetPath);
            }
        }

    }
}