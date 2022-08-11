using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Singleton
{
    public static T GetInstance<T>() where T : Component
    {
        T instance;

        var obj = MonoBehaviour.FindObjectOfType (typeof(T)) as T;

        if(obj != null)
            instance = obj;
        else
        {
            GameObject instanceObject = new GameObject(typeof(T) + "Instance");
            instanceObject.hideFlags = HideFlags.HideAndDontSave;
            instance = instanceObject.AddComponent<T>();
            MonoBehaviour.DontDestroyOnLoad(instanceObject);
        }

        return instance;
    }
}   
