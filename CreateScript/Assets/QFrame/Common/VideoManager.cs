using UnityEngine.Video;
using System.Collections.Generic;
using System;

public class VideoManager : QSingleton<VideoManager>
{
    private static readonly int notPlayer = 0;
    private int currentId = notPlayer;
    private VideoPlayer currentPlayer;
    private Dictionary<int, VideoPlayer> dic = new Dictionary<int, VideoPlayer>();

    public Action onCurrentChanged;
    public static Action<int,string> onTotalTime;
    public static Action<int,long,string> onCurrentTime;
    public static Action<int> onStop;
    public static Action<int> onPause;

    /// <summary>
    /// 添加播放设备
    /// </summary>
    /// <param name="video"></param>
    public static void Add(VideoPlayer video)
    {
        var instance = getInstance();
        instance.AddVideo(video);
        video.sendFrameReadyEvents = true;
        video.started += instance.OnStart;
        video.frameReady += instance.OnFrameReady;
    }

    /// <summary>
    /// 移除播放设备
    /// </summary>
    /// <param name="id"></param>
    public static void Remove(int id)
    {
        var instance = getInstance();
        var video = instance.dic[id];
        video.sendFrameReadyEvents = false;
        video.started -= instance.OnStart;
        video.frameReady -= instance.OnFrameReady;
        instance.RemoveVideo(id);
    }

    /// <summary>
    /// 播放
    /// </summary>
    /// <param name="id"></param>
    public static void Play(int id)
    {
        getInstance().PlayVideo(id);
    }

    /// <summary>
    /// 暂停
    /// </summary>
    public static void Pause()
    {
        getInstance().PauseVideo();
    }

    /// <summary>
    /// 停止
    /// </summary>
    public static void Stop()
    {
        getInstance().StopVideo();
    }

    /// <summary>
    /// 当前播放设备
    /// </summary>
    /// <returns></returns>
    public static VideoPlayer CurrentPlayer()
    {
        return getInstance().currentPlayer;
    }

    /// <summary>
    /// 是否在播放状态
    /// </summary>
    /// <param name="id">播放设备的ID</param>
    /// <returns></returns>
    public static bool IsPlaying(int id)
    {
        var instance = getInstance();
        if(instance.currentId != id)return false;
        return instance.currentPlayer.isPlaying;
    }

    /// <summary>
    /// 设置当前进度
    /// </summary>
    /// <param name="value"></param>
    public static void SetFrame(long value){
        getInstance().currentPlayer.frame = value;
    }

    /// <summary>
    /// 总播放大小
    /// </summary>
    /// <returns></returns>
    public static ulong FrameCount(){
        return getInstance().currentPlayer.frameCount;
    }

    private void AddVideo(VideoPlayer video)
    {
        dic.Add(video.GetInstanceID(), video);
    }

    private void RemoveVideo(int id)
    {
        dic.Remove(id);
    }

    private void PlayVideo(int id)
    {
        var player = dic[id];
        if (currentId == id)//如果是同一个设备
        {
            if (!player.isPlaying)
            {
                player.Play();
                currentPlayer = player;
            }
        }
        else//如果是不同设备 或 没有设备在播放
        {
            if (currentId!=notPlayer && currentId != id)
            {
                currentPlayer.Stop();
                if(onStop!=null)onStop(currentId);
            }
            currentId = id;
            currentPlayer = player;
            player.Play();
            if (onCurrentChanged != null) onCurrentChanged();
        }
        QDebug.Log(string.Format("[播放 url] {0}",player.url));
    }

    private void PauseVideo()
    {
        if (currentPlayer.isPlaying){
            currentPlayer.Pause();
            if(onPause!=null)onPause(currentId);
        }
    }

    private void StopVideo()
    {
        if(currentId == notPlayer)return;
        if (currentPlayer.isPlaying)
            currentPlayer.Stop();
        if (onStop != null) onStop(currentId);
        currentId = notPlayer;
        currentPlayer = null;
    }

    bool isHoursZero = false;
    private void OnStart(VideoPlayer video)
    {
        if (onTotalTime == null) return;
        var span = new TimeSpan(0, 0, 0, (int)(video.frameCount / video.frameRate));
        if (span.Hours == 0)
        {
            isHoursZero = true;
            onTotalTime(currentId,string.Format("{0:D2}:{1:D2}", span.Minutes, span.Seconds));
        }
        else
        {
            onTotalTime(currentId,string.Format("{0:D2}:{1:D2}:{2:D2}", span.Hours, span.Minutes, span.Seconds));
        }
    }

    private void OnFrameReady(VideoPlayer video, long value)
    {
        if(onCurrentTime == null)return;
        var span = new TimeSpan(0, 0, 0, (int)video.time);
        if (isHoursZero)
        {
            onCurrentTime(currentId,value, string.Format("{0:D2}:{1:D2}", span.Minutes, span.Seconds));
        }
        else
        {
            onCurrentTime(currentId,value, string.Format("{0:D2}:{1:D2}:{2:D2}", span.Hours, span.Minutes, span.Seconds));
        }
    }
}
