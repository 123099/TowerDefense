using UnityEngine;
using System.Collections;
using System;

public class WaitForSecondsUnscaled : CustomYieldInstruction
{
    private float waitTime;

    public override bool keepWaiting
    {
        get
        {
            return Time.realtimeSinceStartup < waitTime;
        }
    }

    public WaitForSecondsUnscaled(float time)
    {
        waitTime = time + Time.realtimeSinceStartup;
    }
}
