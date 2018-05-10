
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class QWebRequest : MonoBehaviour
{
    private static QWebRequest instance;
    private static QWebRequest Instance
    {
        get
        {
            return instance ?? (instance = new GameObject("QWebRequest").AddComponent<QWebRequest>());
        }
    }

    #region  startDownload
    /// <summary>
    /// 下载文本
    /// </summary>
    /// <param name="url">请求的链接</param>
    /// <param name="action">发生的事件</param>
    /// <param name="method">请求的方式</param>
    /// <returns></returns>
    public static bool DownloadText(string url, RequestTextEvent action, string method = UnityWebRequest.kHttpVerbGET)
    {
        if (IsNull(url, action, action.success)) return true;
        Instance.StartCoroutine(Instance.DownloadTextIEnumerator( url,method, action));
        return false;
    }

    /// <summary>
    /// 下载数据
    /// </summary>
    /// <param name="url">请求的链接</param>
    /// <param name="action">发生的事件</param>
    /// <param name="method">请求的方式</param>
    /// <returns></returns>
    public static bool DownloadData(string url, RequestDataEvent action, string method = UnityWebRequest.kHttpVerbGET)
    {
        if (IsNull(url, action, action.success)) return true;
        Instance.StartCoroutine(Instance.DownloadDataIEnumerator(url,method,action));
        return false;
    }

    /// <summary>
    /// 下载文件[文件存放位置不能重复]
    /// </summary>
    /// <param name="url">请求的链接</param>
    /// <param name="savePath">保存的路径</param>
    /// <param name="action">发生的事件</param>
    /// <param name="method">请求的方式</param>
    /// <returns></returns>
    public static bool DownloadFile(string url, string savePath, RequestFileEvent action, string method = UnityWebRequest.kHttpVerbGET)
    {
        if(string.IsNullOrEmpty(url) || action==null)return true;
        Instance.StartCoroutine(Instance.DownloadFileIEnumerator(url,savePath,method,action));
        return false;
    }

    /// <summary>
    /// 下载图片
    /// </summary>
    /// <param name="url">请求的链接</param>
    /// <param name="action">发生的事件</param>
    /// <param name="method">请求的方式</param>
    /// <returns></returns>
    public static bool DownaloadSprite(string url, RequestSpriteEvent action, string method = UnityWebRequest.kHttpVerbGET)
    {
        if (IsNull(url, action, action.success)) return true;
        Instance.StartCoroutine(Instance.DownloadSpriteIEnumerator(url,method,action));
        return false;
    }

    /// <summary>
    /// 下载AssetBundle
    /// </summary>
    /// <param name="url">请求的链接</param>
    /// <param name="action">发生的事件</param>
    /// <param name="method">请求的方式</param>
    /// <returns></returns>
    public static bool DownaloadAssetBundle(string url, RequestAssetBundleEvent action,uint crc=0, string method = UnityWebRequest.kHttpVerbGET)
    {
        if (IsNull(url, action, action.success)) return true;
        Instance.StartCoroutine(Instance.DownloadAssetBundleIEnumerator(url,crc,method,action));
        return false;
    }

    /// <summary>
    /// 下载音频
    /// </summary>
    /// <param name="url">请求的链接</param>
    /// <param name="audioType">音频格式</param>
    /// <param name="action">发生的事件</param>
    /// <returns></returns>
    public static bool DownloadAudioClip(string url, RequestAudioClipEvent action, AudioType audioType=AudioType.AUDIOQUEUE)
    {
        if (IsNull(url, action, action.success)) return true;
        Instance.StartCoroutine(Instance.DownloadAudioClipIEnumerator(url,audioType,action));
        return false;
    }

    /// <summary>
    /// 下载视频
    /// </summary>
    /// <param name="url">请求的链接</param>
    /// <param name="action">发生的事件</param>
    /// <returns></returns>
    public static bool DownloadMovieTexture(string url, RequestMovieTextureEvent action)
    {
        if (IsNull(url, action, action.success)) return true;
        Instance.StartCoroutine(Instance.DownloadMovieTextureIEnumerator(url,action));
        return false;
    }
    #endregion

    IEnumerator DownloadTextIEnumerator(string url, string method, RequestTextEvent action)
    {
        var request = new UnityWebRequest(url, method);
        request.downloadHandler = new DownloadHandlerBuffer();
        if(action.downloadProgress!=null) StartCoroutine(DownloadProgress(request, action.downloadProgress));
        yield return request.SendWebRequest();
        Dispose(request,() =>
        {
            action.success(request.downloadHandler.text);
        }, action.error, action.error404, action.error500);
    }

    IEnumerator DownloadDataIEnumerator( string url, string method, RequestDataEvent action)
    {
        var request = new UnityWebRequest(url, method);
        request.downloadHandler = new DownloadHandlerBuffer();
        if(action.downloadProgress!=null) StartCoroutine(DownloadProgress(request, action.downloadProgress));
        yield return request.SendWebRequest();
        Dispose(request,() =>
        {
            action.success(request.downloadHandler.data);
        }, action.error, action.error404, action.error500);
    }

    IEnumerator DownloadFileIEnumerator(string url, string savePath, string method, RequestFileEvent action)
    {
        if (!File.Exists(savePath))
        {
            var request = new UnityWebRequest(url, method);
            request.downloadHandler = new DownloadHandlerFile(savePath);
            if (action.downloadProgress != null) StartCoroutine(DownloadProgress(request, action.downloadProgress));
            yield return request.SendWebRequest();
            Dispose(request, () =>
             {
                 Debug.Log("File successfully downloaded and saved to " + savePath);
                 if (action != null) action.success();
             }, action.error, action.error404, action.error500);
        }else{
            action.failedCreateFile();
        }
    }

    IEnumerator DownloadSpriteIEnumerator(string url, string method, RequestSpriteEvent action)
    {
        var request = new UnityWebRequest(url, method);
        var texDl = new DownloadHandlerTexture(true);
        request.downloadHandler = texDl;
        if(action.downloadProgress!=null) StartCoroutine(DownloadProgress(request, action.downloadProgress));

        yield return request.SendWebRequest();
        Dispose(request,() =>
        {
            Texture2D t = texDl.texture;
            action.success(Sprite.Create(t, new Rect(0, 0, t.width, t.height), Vector2.zero, 1f),texDl.data);
        }, action.error, action.error404, action.error500);
    }

    IEnumerator DownloadAssetBundleIEnumerator(string url, uint crc, string method, RequestAssetBundleEvent action)
    {
        var request = new UnityWebRequest(url, method);
        var handler = new DownloadHandlerAssetBundle(request.url, crc);
        request.downloadHandler = handler;
        if(action.downloadProgress!=null) StartCoroutine(DownloadProgress(request, action.downloadProgress));

        yield return request.SendWebRequest();
        Dispose(request,() =>
        {
            action.success(handler.assetBundle);
        }, action.error, action.error404, action.error500);
    }

    IEnumerator DownloadAudioClipIEnumerator(string url, AudioType audioType, RequestAudioClipEvent action)
    {
        var request = UnityWebRequestMultimedia.GetAudioClip(url, audioType);
        if(action.downloadProgress!=null) StartCoroutine(DownloadProgress(request, action.downloadProgress));

        yield return request.SendWebRequest();

        Dispose(request,() =>
        {
            action.success(DownloadHandlerAudioClip.GetContent(request));
        }, action.error, action.error404, action.error500);
    }


    IEnumerator DownloadMovieTextureIEnumerator(string url, RequestMovieTextureEvent action)
    {
        var request = UnityWebRequestMultimedia.GetMovieTexture(url);
        if(action.downloadProgress!=null) StartCoroutine(DownloadProgress(request, action.downloadProgress));
        yield return request.SendWebRequest();
        Dispose(request,() =>
        {
            action.success(DownloadHandlerMovieTexture.GetContent(request));
        }, action.error, action.error404, action.error500);
    }

    IEnumerator DownloadProgress(UnityWebRequest request, Action<float> action)
    {
        while (!request.isDone)
        {

            yield return null;
            action(request.downloadProgress);

        }
        request.Abort();
        Resources.UnloadUnusedAssets();
    }

    private void Dispose(UnityWebRequest request,Action ok, Action<string> error, Action error404, Action error500)
    {
        QDebug.Log(string.Format("url [ {0} ]\nerror [ {1} ]",request.url,request.error));
        if (request.isHttpError || request.isNetworkError)
        {
            if (error != null) error(request.error);
        }
        else
        {
            switch (request.responseCode)
            {
                case 200: if (ok != null) ok(); break;
                case 404: if (error404 != null) error404(); break;
                case 500: if (error500 != null) error500(); break;
            }
        }
    }

    private static bool IsNull(string url, BaseRequestEvent requestAction, object action)
    {
        if (string.IsNullOrEmpty(url) || requestAction == null || action == null)
        {
            QDebug.Log("Can't be empty");
            return true;
        }
        return false;
    }

    public class BaseRequestEvent
    {
        public Action<float> downloadProgress;
        public Action<string> error;
        public Action error404;
        public Action error500;
    }

    /// <summary>
    /// 文本请求事件
    /// </summary>
    public class RequestTextEvent : BaseRequestEvent
    {
        public Action<string> success;
    }

    /// <summary>
    /// 数据请求事件
    /// </summary>
    public class RequestDataEvent : BaseRequestEvent
    {
        public Action<byte[]> success;
    }

    /// <summary>
    /// 文件请求事件
    /// </summary>
    public class RequestFileEvent:BaseRequestEvent
    {
        public Action success;
        public Action failedCreateFile;
    }

    /// <summary>
    /// 图片请求事件
    /// </summary>
    public class RequestSpriteEvent : BaseRequestEvent
    {
        public Action<Sprite,byte[]> success;
    }

    /// <summary>
    /// 资源请求事件
    /// </summary>
    public class RequestAssetBundleEvent : BaseRequestEvent
    {
        public Action<AssetBundle> success;
    }

    /// <summary>
    /// 音频请求事件
    /// </summary>
    public class RequestAudioClipEvent : BaseRequestEvent
    {
        public Action<AudioClip> success;
    }

    /// <summary>
    /// 视频请求事件
    /// </summary>
    public class RequestMovieTextureEvent : BaseRequestEvent
    {
        public Action<MovieTexture> success;
    }


}

