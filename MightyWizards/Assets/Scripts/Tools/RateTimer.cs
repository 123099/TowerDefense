using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class RateTimer {

    [SerializeField] private float rate;

    private float lastReadyTime;
    private bool lastReadyTimeSet;

    //For serialization. Do not use.
    public RateTimer () { }

	public RateTimer(float rate, float lastReadyTime = 0)
    {
        this.rate = rate;
        SetLastReadyTime(lastReadyTime);
    }

    public bool IsReady ()
    {
        if(Time.time - lastReadyTime >= 1f / rate)
        {
            SetLastReadyTime(Time.time);
            lastReadyTimeSet = false;
            return true;
        }

        return false;
    }

    public void SetRate(float rate)
    {
        this.rate = rate;
    }

    public void SetLastReadyTime(float time)
    {
        lastReadyTime = time;
    }

    public void SetLastReadyTimeOnce(float time)
    {
        if(lastReadyTimeSet) return;

        SetLastReadyTime(time);
        lastReadyTimeSet = true;
    }
}
