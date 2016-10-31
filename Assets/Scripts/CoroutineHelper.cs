using System;
using System.Collections;
using UnityEngine;

public class CoroutineHelper : MonoBehaviour
{
    //TODO: Implement function to clean up old, but still running coroutines

    /// <summary>
    /// Call the given callback delayed.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="timeInSecToDelayCall">The time in sec to delay call.</param>
    /// <param name="onDelayOverCallback">The on delay over callback.</param>
    /// <returns></returns>
    public Coroutine CallDelayed(MonoBehaviour source, float timeInSecToDelayCall, Action onDelayOverCallback)
    {
        return source.StartCoroutine(CallDelayedCoroutine(timeInSecToDelayCall, onDelayOverCallback));
    }

    /// <summary>
    /// Call the given callback delayed.
    /// </summary>
    /// <param name="timeInSecToDelayCall">The time in sec to delay call.</param>
    /// <param name="onDelayOverCallback">The on delay over callback.</param>
    /// <returns></returns>
    private IEnumerator CallDelayedCoroutine(float timeInSecToDelayCall, Action onDelayOverCallback)
    {
        yield return new WaitForSeconds(timeInSecToDelayCall);

        if (onDelayOverCallback != null)
        {
            onDelayOverCallback();
        }
    }
}
