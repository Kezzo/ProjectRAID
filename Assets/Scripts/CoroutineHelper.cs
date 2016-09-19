using System;
using System.Collections;
using UnityEngine;

public class CoroutineHelper : MonoBehaviour
{
    //TODO: Implement function to clean up old, but still running coroutines

    /// <summary>
    /// Call the given callback delayed.
    /// </summary>
    /// <param name="timeInSecToDelayCall">The time in sec to delay call.</param>
    /// <param name="onDelayOverCallback">The on delay over callback.</param>
    public void CallDelayed(float timeInSecToDelayCall, Action onDelayOverCallback)
    {
        StartCoroutine(CallDelayedCoroutine(timeInSecToDelayCall, onDelayOverCallback));
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
