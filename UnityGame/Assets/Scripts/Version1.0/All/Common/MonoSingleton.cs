using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// ½Å±¾µ¥ÀýÀà
/// </summary>
/// <typeparam name="T"></typeparam>
public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    instance = new GameObject("Singleton of" + typeof(T)).AddComponent<T>();
                }
                else
                {
                    instance.Init();
                }
            }
            return instance;
        }
    }


    public virtual void Init() { }
   

    private void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            Init();
        }
    }

}
