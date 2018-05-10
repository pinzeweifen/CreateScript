using UnityEngine;
using System.IO;
using System.Text;
using System.Collections.Generic;

public class QFileOperation
{
    /// <summary>
    /// 文件是否存在
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static bool IsExists(string path)
    {
        return File.Exists(path);
    }

    /// <summary>
    /// 检查文件夹是否存在
    /// </summary>
    /// <param name="path"></param>
    /// <param name="create"></param>
    /// <returns></returns>
    public static bool IsDirctoryName(string path, bool create = false)
    {
        var dir = Path.GetDirectoryName(path);
        var isExists = Directory.Exists(dir);
        if (create)
        {
            var info = Directory.CreateDirectory(dir);
            if (info != null) isExists = true;
        }
        return isExists;
    }

    /// <summary>
    /// 写入文件
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="data"></param>
    public static void WriteBytes(string filePath, byte[] data)
    {
        File.WriteAllBytes(filePath, data);
    }

    /// <summary>
    /// 写入文本
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="data"></param>
    public static void WriteText(string filePath, string data, FileMode fileMode = FileMode.OpenOrCreate)
    {
        FileStream stream = new FileStream(filePath, fileMode);
        StreamWriter sw = new StreamWriter(stream);
        sw.Write(data);
        sw.Close();
        sw.Dispose();
    }

    /// <summary>
    /// 读取文件
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static byte[] ReadBytes(string filePath)
    {
        return File.ReadAllBytes(filePath);
    }

    /// <summary>
    /// 读取文本
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static string ReadText(string filePath)
    {
        StreamReader sr = null;
        sr = File.OpenText(filePath);

        string line = sr.ReadToEnd();

        sr.Close();
        sr.Dispose();
        return line;
    }

    /// <summary>
    /// 读取纹理
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static Texture2D ReadTexture2D(string filePath)
    {
        var data = File.ReadAllBytes(filePath);
        Texture2D t = new Texture2D(2, 2, TextureFormat.PVRTC_RGB2, false);
        t.LoadImage(data);
        return t;
    }

    /// <summary>
    /// 读取精灵
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static Sprite ReadSprite(string filePath)
    {
        var t = ReadTexture2D(filePath);
        return Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0.5f, 0.5f));
    }

    public static string GetAssetsDirectoryPath(string fileName)
    {
        DirectoryInfo dir = new DirectoryInfo(Application.dataPath);
        var infos = dir.GetDirectories("*",SearchOption.AllDirectories);
        foreach (var info in infos)
        {
            if (info.FullName.Contains(fileName))
            {
                return info.FullName;
            }
        }
        return string.Empty;
    }

    public static string[] GetFilesPath(string dirName, string dirPattern = "*", string filePattern = "*")
    {
        List<string> list = new List<string>();
        DirectoryInfo dir = new DirectoryInfo(dirName);
        string filter = ".meta";
        foreach (var child in dir.GetDirectories(dirPattern, SearchOption.AllDirectories))
        {
            var childDir = new DirectoryInfo(child.FullName);
            foreach (var file in childDir.GetFiles(filePattern, SearchOption.AllDirectories))
            {
                if (file.FullName.Contains(filter)) continue;
                list.Add(file.FullName);
            }
        }
        return list.ToArray();
    }

    public static string GetFilePath(string dirName, string suffix, string fileName)
    {
        var dir = new DirectoryInfo(dirName);
        var infos = dir.GetFiles(string.Format("*.{0}", suffix),SearchOption.AllDirectories);
        foreach (var info in infos)
        {
            if (info.Name.Contains(fileName))
            {
                return info.FullName;
            }
        }
        return string.Empty;
    }
}
