
using UnityEngine;
public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T instance = null;
    public static T Inst
    {
        get
        {
            if (instance == null)
            {
				instance =  Transform.FindObjectOfType(typeof(T)) as T;
                if (instance == null)
                {
                    instance = new GameObject("_" + typeof(T).Name).AddComponent<T>();
                    DontDestroyOnLoad(instance);
                }
                if (instance == null)
                    Debug.Log("Failed to create instance of " + typeof(T).FullName + ".");
            }
            return instance;
        }
    }

    void OnApplicationQuit() { if (instance != null) instance = null; }

    public static T CreateInstance()
    {
        if (Inst != null) Inst.OnCreate();
        return Inst;
    }

    protected virtual void OnCreate()
    {

    }
}
