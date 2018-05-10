using UnityEngine;

public class QDebug
{
    public static RunModel environment = RunModel.Debug;

    public static void Log(object message)
    {
        if (environment != RunModel.Debug) return;
        Debug.Log(message);
    }

    public static void LogWarning(object message)
    {
        if (environment != RunModel.Debug) return;
        Debug.LogWarning(message);
    }

    public static void LogError(object message)
    {
        if (environment != RunModel.Debug) return;
        Debug.LogError(message);
    }

	public static void LogException(string message)
    {
        if (environment != RunModel.Debug) return;
		Debug.LogException(new System.Exception(message));
    }
}

//使用环境
public enum RunModel
{
    //调试
    Debug,
    //真机测试
    Machine
}