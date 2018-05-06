using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public partial class QConfigure
{
    public static string referencePath = "Script/UI";

    public static string msgTitle = "温馨提示";
    public static string assetPath = "Assets/QFrame/QCreateScript/Text/info.asset";

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
        "using UnityEngine;\n"+
        "using UnityEngine.UI;\n"+
        "using System;\n\n"+
        "public partial class {0}_UI :MonoBehaviour\n{{\n{1}\n}}";

    public static readonly string uiClassCode = 
        "{0}\n{1}\n{2}\n{3}\n"+
        "\tpartial void OnAwake();\n"+
        "\tprivate void Awake()\n\t{{\n"+
        "\t\tOnAwake();\n\n"+
        "{4}\n{5}\n{6}\n"+
        "\t}}\n"+
        "{7}";

    public static readonly string modelCode =
        "\n\npublic class {0}_Model\n{{\n\tpublic {0}_Model()\n\t{{\n\n\t}}\n}}";

    public static readonly string controllerCode =
        "using UnityEngine;\n" +
        "using UnityEngine.EventSystems;\n\n" +
        "\n\npublic partial class {0}_Controller\n{{\n"+
        "\tpartial void OnAwake()\n\t{{\n\n\t}}\n}}";

    public static readonly string controllerBuildCode =
        "using UnityEngine;\n\n" +
        "\n\npublic partial class {0}_Controller\n{{\n" +
        "\tprivate {0}_UI ui;\n" +
        "\tprivate {0}_Model model = new {0}_Model();\n\n" +
        "\tpublic {0}_Controller({0}_UI ui)\n\t{{\n" +
        "\t\tthis.ui = ui;\n" +
        "\t\tOnAwake();\n" +
        "{1}" +
        "\t}}\n" +
        "\tpartial void OnAwake();\n" +
        "{2}\n{3}\n"+
        "}}";

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
        return string.Format("{1}_{0}",suffix, QGlobalFun.GetString(Selection.activeTransform.name));
    }
    
    private static string GetFileName(string suffix)
    {
        return string.Format("{0}/{1}_{0}",suffix, QGlobalFun.GetString(Selection.activeTransform.name));
    }
}
