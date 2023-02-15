
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XLua;


[CSharpCallLua]
public delegate void LuaLifeCycle();


[GCOptimize]
public struct LuaBootstrap
{
    public LuaLifeCycle Start;
    public LuaLifeCycle Update;
    public LuaLifeCycle OnDestroy;
}

public class MainCon : MonoBehaviour
{
    private LuaBootstrap _bootstrap;
    void Start()
    {
        
        CustomXLuaEnv.Instance.DoString("require('Bootstrap')");
        GameObject.DontDestroyOnLoad(this);
        //将lua的表导入到C#端
        _bootstrap= CustomXLuaEnv.Instance.Global.Get<LuaBootstrap>("Bootstrap");
        _bootstrap.Start();      
        

    }

   

    private void Update()
    {
        _bootstrap.Update();
    }
    private void OnDestroy()
    {
        CustomXLuaEnv.Instance.DisposeEnv();
        _bootstrap.OnDestroy();
    }
   

}
