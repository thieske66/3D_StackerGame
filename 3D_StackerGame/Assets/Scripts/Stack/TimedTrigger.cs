using System;
using UnityEngine;

[Serializable]
public class TimedTrigger
{
    public float Interval = 1f;
    public Action OnElapsed;

    [SerializeField]
    private float elapsed = 0f;

    public void Elapse(float time)
    {
        elapsed += time;
        checkElapsed();
    }

    private void checkElapsed()
    {
        if (elapsed >= Interval)
        {
            OnElapsed?.Invoke();
            elapsed -= Interval;
            checkElapsed();
        }
    }
}
