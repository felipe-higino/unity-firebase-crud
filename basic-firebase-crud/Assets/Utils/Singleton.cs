using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance;
    public void AwakeSingleton(T objectRef)
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = objectRef;
    }
}