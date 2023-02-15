using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvasSingleton : MonoBehaviour
{
    public static MainCanvasSingleton Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {

            Destroy(this);  
        }

    }


    void Update()
    {

    }
}
