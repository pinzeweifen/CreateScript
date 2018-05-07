using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QAnimation  
{
    protected IAnimation currentAnimation;
	protected Dictionary<string, IAnimation> animationDic = new Dictionary<string, IAnimation>();

    public void AddAnimatin(string key, IAnimation animation)
    {
        if (!animationDic.ContainsKey(key))
        {
            animationDic.Add(key, animation);
            return;
        }
        animationDic[key] = animation;
    }

    public void RemoveAnimation(string key)
    {
        if(animationDic.ContainsKey(key))
        {
            if(currentAnimation == animationDic[key])
                currentAnimation = null;
            animationDic.Remove(key);
        }
    }

    public void SetCurrentAnimation(string key)
    {
        if (!animationDic.ContainsKey(key))
        {
            currentAnimation = null;
            return;
        }
        currentAnimation = animationDic[key];
    }

    public void Play()
    {
        if (currentAnimation != null)
            currentAnimation.Play();
    }

    public void Pause()
    {
        if (currentAnimation != null)
            currentAnimation.Pause();
    }

    public void Stop()
    {
        if (currentAnimation != null)
            currentAnimation.Stop();
    }
}
