using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IAnimation 
{
    bool Loop{get;set;}
    bool IsPlay{get;}
    bool IsPause{get;}
    Action onPlay{get;}
    Action onPause{get;}
    Action onStop{get;}
    Action onComplete{get;}
    void Play();
    void Pause();
    void Stop();
    void OnPlay();
    void OnPause();
    void OnStop();
    void OnComplete();
}