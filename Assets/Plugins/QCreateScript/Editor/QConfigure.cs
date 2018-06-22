using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace CreateScript
{
    public partial class QConfigure
    {
        public static int versionIndex = 1;
        public static int version = 2;
        public static string[] versionStr=new string[]{"1","2"};
        public static Transform selectTransform;
        public static string referencedefaultPath = "Script/UI";
        public static string referencePath;
        public static bool isCreateModel = false;
        public static bool isCreateController = true;

        public static string plugName = "QCreateScript";
        private static string assetPath = "\\Asset\\info.asset";
        public static string infoPath = "/Info/{0}_Info.json";
        private static string plugPath;
        public static string InfoPath
        {
            get
            {
                if (string.IsNullOrEmpty(plugPath))
                {
                    switch(version){
                        case 1:
                        plugPath = QFileOperation.GetAssetsDirectoryPath(plugName);
                        plugPath = plugPath.Remove(0, plugPath.IndexOf("Assets")) + assetPath;
                        break;
                        case 2:
                        plugPath = Application.persistentDataPath+ infoPath;
                        break;
                    }
                }

                return plugPath;
            }
        }
        
        public static string msgTitle = "温馨提示";
        public static string ok = "知道了大佬";
        public static string noSelect = "欧尼酱～没有选对象呢";
        public static string haveBeenCreated = "欧尼酱～脚本已创建了，点击更新哦~";
        public static string notCreate = "奥尼酱～你还没生成脚本呢";
        public static string editorCompiling = "欧尼酱～编辑器傲娇中...";
        public static string plugCreate = "欧尼酱～没有使用插件生成对应的脚本呢";
        public static string copy = "欧尼酱～复制到剪贴板啦！";
        public static string noMountScript = "欧尼酱～还没挂载脚本呢～";


        public static string variableFormat = "\t[SerializeField] private {0,-29} {1};\n";
        public static string findFormat = "\t\t{0,-50} = transform.Find(\"{1}\").GetComponent<{2}>();\n";
        public static string attributeVariableFormat = "\tprivate Q{0,-45} q{1};\n";
        public static string attributeFormat = "\tpublic Q{0,-46} Q{1}{{get{{return q{1};}}}}\n";
        public static string newAttributeFormat = "\t\tq{0,-49} = new Q{1}({0});\n";
        public static string attribute2Format = "\tpublic {0,-47} Q{1}{{get{{return {1};}}}}\n";
        public static string registerFormat = "\t\t{0,-51}.{1}.AddListener( {2} );\n";
        public static string controllerEventFormat = "\tpublic Action{0,-41} {1};\n";
        public static string functionFormat = "\tprivate void {0}({1})\n\t{{\n\t\tif({2}!=null){2}({3});\n\t}}\n";

        public static string assignmentFormat = "\t\tui.{0,-50} = {1};\n";
        public static string declareFormat = "\tpartial void {0}({1});\n";
        public static string funFormat = "\tprivate void {0}({1})\n\t{{\n\t\t{2}({3});\n\t}}\n";

        public static string uicodeOnAwake = "\tpartial void OnAwake()\n\t{\n\n\t}";

        public static string UIFileName { get { return GetFileName("UI"); } }
        public static string UIBuildFileName { get { return GetFileName("BuildUI"); } }
        public static string ModelFileName { get { return GetFileName("Model"); } }
        public static string ControllerFileName { get { return GetFileName("Controller"); } }
        public static string ControllerBuildFileName { get { return GetFileName("BuildController"); } }

        public static string UIName { get { return GetClassName("UI"); } }
        public static string UIBuildName { get { return GetClassName("BuildUI"); } }
        public static string ModelName { get { return GetClassName("Model"); } }
        public static string ControllerName { get { return GetClassName("Controller"); } }
        public static string ControllerBuildName { get { return GetClassName("BuildController"); } }

        public static readonly string uiCode =
            "using UnityEngine;\n" +
            "using UnityEngine.UI;\n" +
            "using System;\n\n" +
            "public partial class {0}_UI :QBaseActive,IWindow\n{{\n{1}\n}}";

        public static readonly string uiCode2 =
    "using UnityEngine;\n" +
    "using UnityEngine.UI;\n" +
    "using System;\n\n" +
    "public partial class {0}_UI \n{{\n{1}\n}}";

        public static readonly string uiClassCode =
            "{0}\n{1}\n{2}\n{3}\n" +
            "\tpartial void OnAwake();\n" +
            "\tprivate void Awake()\n\t{{\n" +
            "{4}\n{5}\n{6}\n" +
            "\t\tOnAwake();\n\n" +
            "\t}}\n" +
            "{7}";

        public static readonly string modelCode =
            "\n\npublic class {0}_Model\n{{\n\tpublic {0}_Model()\n\t{{\n\n\t}}\n}}";

        public static readonly string controllerCode =
            "using UnityEngine;\n" +
            "using UnityEngine.EventSystems;\n\n" +
            "\n\npublic partial class {0}_Controller\n{{\n" +
            "\tpartial void OnAwake()\n\t{{\n\n\t}}\n}}";

        public static readonly string controllerBuildCode =
            "using UnityEngine;\n\n" +
            "\n\npublic partial class {0}_Controller:IController\n{{\n" +
            "\tprivate {0}_UI ui;\n" +
            "\tprivate {0}_Model model = new {0}_Model();\n\n" +
            "\tpublic {0}_Controller({0}_UI ui)\n\t{{\n" +
            "\t\tthis.ui = ui;\n" +
            "\t\tOnAwake();\n" +
            "{1}" +
            "\t}}\n" +
            "\tpartial void OnAwake();\n" +
            "{2}\n{3}\n" +
            "}}";

        public static readonly string controllerBuildCode2 =
    "using UnityEngine;\n\n" +
    "\n\npublic partial class {0}_Controller:IController\n{{\n" +
    "\tprivate {0}_UI ui;\n" +
    //"\tprivate {0}_Model model = new {0}_Model();\n\n" +
    "\tpublic {0}_Controller({0}_UI ui)\n\t{{\n" +
    "\t\tthis.ui = ui;\n" +
    "\t\tOnAwake();\n" +
    "{1}" +
    "\t}}\n" +
    "\tpartial void OnAwake();\n" +
    "{2}\n{3}\n" +
    "}}";


        public static int Version{
            get{return versionIndex;}
            set{
                versionIndex = value;
                version = int.Parse(versionStr[versionIndex]);
            }
        }

        public static string FilePath(string name)
        {
            var filePath = string.Format("{0}/{1}/{2}.cs", Application.dataPath, referencePath, name);
            if (!QFileOperation.IsDirctoryName(filePath, true))
            {
                EditorUtility.DisplayDialog(msgTitle, "文件夹无法创建", "OK");
                Debug.LogException(new Exception("文件夹无法创建"));
            }
            return filePath;
        }

        public static string GetClassName(string suffix)
        {
            return string.Format("{1}_{0}", suffix, QGlobalFun.GetString(Selection.activeTransform.name));
        }

        public static string GetInfoPath(){
            int id;
            var go = PrefabUtility.GetPrefabParent(selectTransform.gameObject);
            if(go!=null){
                id = go.GetInstanceID();
            }else{
                id = selectTransform.gameObject.GetInstanceID();
            }
            return string.Format(InfoPath,id);
        }

        public static void Compiling(){
            EditorPrefs.SetBool("QConfigureSelectCompiling",true);
        }

        public static bool IsCompiling(){
            var value = EditorPrefs.GetBool("QConfigureSelectCompiling",false);
            EditorPrefs.SetBool("QConfigureSelectCompiling",false);
            return value;
        }

        private static string GetFileName(string suffix)
        {
            return string.Format("{0}/{1}_{0}", suffix, QGlobalFun.GetString(Selection.activeTransform.name));
        }
    }
}