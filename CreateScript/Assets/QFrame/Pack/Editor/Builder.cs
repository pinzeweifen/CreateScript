using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

public class SaveBuildPath:EditorWindow
{
    private static string savePath = Application.dataPath+"configure.txt";
    public static string path="Assets";
    [MenuItem("Assets/Build/BuildPath")]
    static void Init(){
        GetWindow<SaveBuildPath>().Show();
    }

    [InitializeOnLoadMethod]
    static void AppInit(){
        if(QFileOperation.IsExists(savePath)){
            path = QFileOperation.ReadText(savePath);
        }
    }  

    void OnGUI()
    {
        EditorGUILayout.Separator();
        path = EditorGUILayout.TextField("打包路径：",path);
        EditorGUILayout.Space();
        if(GUILayout.Button("更新打包路径")){
             QFileOperation.WriteText(savePath,path);
        }
    }
}

public class Builder : Editor
{
    [MenuItem("Assets/Build/ChangedName")]
    static void BuildChangedName(){
        var selects = Selection.GetFiltered<Object>(SelectionMode.DeepAssets);
        string path;
        foreach(var item in selects)
        {
            path = AssetDatabase.GetAssetPath(item);
            var asset = AssetImporter.GetAtPath(path);
            Debug.Log(asset.GetType());
            asset.assetBundleName = item.name;
            asset.assetBundleVariant = "asset";
            asset.SaveAndReimport();
        }
        AssetDatabase.Refresh();
    }

    [MenuItem("Assets/Build/SelectBuild/window")]
    static void SelectBuild_StandaloneWindows()
    {
        SelectBuild(BuildTarget.StandaloneWindows);
    }
    [MenuItem("Assets/Build/SelectBuild/window64")]
    static void SelectBuild_StandaloneWindows64()
    {
        SelectBuild(BuildTarget.StandaloneWindows64);
    }
    [MenuItem("Assets/Build/SelectBuild/mac64]")]
    static void SelectBuild_StandaloneOSXIntel64()
    {
        SelectBuild(BuildTarget.StandaloneOSXIntel64);
    }
    [MenuItem("Assets/Build/SelectBuild/mac")]
    static void SelectBuild_StandaloneOSXIntel()
    {
        SelectBuild(BuildTarget.StandaloneOSXIntel);
    }
    [MenuItem("Assets/Build/SelectBuild/Android")]
    static void SelectBuild_Android()
    {
        SelectBuild(BuildTarget.Android);
    }
    [MenuItem("Assets/Build/SelectBuild/iOS")]
    static void SelectBuild_iOS()
    {
        SelectBuild(BuildTarget.iOS);
    }

    

    [MenuItem("Assets/Build/BuildAll/window")]
    static void BuildAll_StandaloneWindows()
    {
        BuildAll(BuildTarget.StandaloneWindows);
    }
    [MenuItem("Assets/Build/BuildAll/window64")]
    static void BuildAll_StandaloneWindows64()
    {
        BuildAll(BuildTarget.StandaloneWindows64);
    }
    [MenuItem("Assets/Build/BuildAll/mac64")]
    static void BuildAll_StandaloneOSXIntel64()
    {
        BuildAll(BuildTarget.StandaloneOSXIntel64);
    }
    [MenuItem("Assets/Build/BuildAll/mac")]
    static void BuildAll_StandaloneOSXIntel()
    {
        BuildAll(BuildTarget.StandaloneOSXIntel);
    }
    [MenuItem("Assets/Build/BuildAll/Android")]
    static void BuildAll_Android()
    {
        BuildAll(BuildTarget.Android);
    }
    [MenuItem("Assets/Build/BuildAll/iOS")]
    static void BuildAll_iOS()
    {
        BuildAll(BuildTarget.iOS);
    }

    static void SelectBuild(BuildTarget target)
    {
        var selects = Selection.GetFiltered<Object>(SelectionMode.DeepAssets);
        int count = selects.Length;
        string[] stringList = new string[count];
        
        for(int i=0;i<count;i++)
        {
             stringList[i]= AssetDatabase.GetAssetPath(selects[i]);
        }

        AssetBundleBuild[] buildMap = new AssetBundleBuild[1];
        buildMap[0].assetNames = stringList;
        buildMap[0].assetBundleName = selects[0].name;

        BuildPipeline.BuildAssetBundles(SaveBuildPath.path, buildMap,
            BuildAssetBundleOptions.ChunkBasedCompression, target);

        AssetDatabase.Refresh();
    }

    static void BuildAll(BuildTarget target)
    {
        BuildPipeline.BuildAssetBundles(SaveBuildPath.path,
        BuildAssetBundleOptions.ChunkBasedCompression, target);
        AssetDatabase.Refresh();
    }
}