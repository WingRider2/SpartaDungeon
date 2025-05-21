using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<T>() as T;
                if (instance == null)
                {
                    setupInstance();
                }
                
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

    private void Awake()
    {
        RemoveDuplicates();
    }
    private void RemoveDuplicates()
    {
        if(instance == null)
        {
            instance = FindAnyObjectByType<T>() as T;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

    }
    static void setupInstance()
    {
        GameObject gameObj = new GameObject();
        gameObj.name = typeof(T).Name;
        instance = gameObj.AddComponent<T>();
    }
}
