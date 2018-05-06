using UnityEngine;
using System.IO;
using System.Text;

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
    public static void WriteText(string filePath, string data, FileMode fileMode=FileMode.OpenOrCreate)
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

    public static Sprite ReadSprite(string filePath)
    {

        var data = File.ReadAllBytes(filePath);
        Texture2D t = new Texture2D(2, 2, TextureFormat.PVRTC_RGB2, false);
        t.LoadImage(data);
        return Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0.5f, 0.5f));
    }
}
