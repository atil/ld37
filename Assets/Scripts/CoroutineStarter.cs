using System.Collections;
using UnityEngine;

public class CoroutineStarter : MonoBehaviour
{
    private static readonly MonoBehaviour coroutineStarter;
    public static Coroutine StartCoroutine(IEnumerator function)
    {
        return coroutineStarter.StartCoroutine(function);
    }

    public static void StopCoroutine(IEnumerator function)
    {
        if (function != null)
        {
            coroutineStarter.StopCoroutine(function);
        }
    }

    static CoroutineStarter()
    {
        coroutineStarter = new GameObject("CoroutineStarter").AddComponent<CoroutineStarter>();
        DontDestroyOnLoad(coroutineStarter.gameObject);
    }
}