using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;

public class CustomXLuaEnv
{
    private static CustomXLuaEnv instance;
    public static  CustomXLuaEnv Instance 
    { 
    get
        {
            if (instance == null)
            {
                instance=new CustomXLuaEnv();
            }
            return instance;
        }
    }

    private LuaEnv _luaEnv;
    private CustomXLuaEnv()
    {
        _luaEnv = new LuaEnv();
        _luaEnv.AddLoader(CustomLoader);
    }

    /// <summary>
    /// 封装dostring 
    /// </summary>
    /// <param name="code">lua代码：（require（））</param>
    /// <returns></returns>
    public object[] DoString(string code)
    {
        return _luaEnv.DoString(code);
    }

    //自定义加载器，会优先于系统内置加载器执行，当自定义加载器加载到文件后，后续的加载器则不会执行
    //当lua代码执行require() 函数时，自定义加载器会尝试获得文件的内容
    /// <summary>
    /// //自定义加载器  
    /// </summary>
    /// <param name="filepath">被加载lua文件的路径</param>
    /// <returns></returns>
    private byte[] CustomLoader(ref string filepath)
    {
        string path = Application.dataPath;  
        path = path = path.Substring(0, path.Length - 7) + "Scripts/Version1.0/Lua/Login/" + filepath + ".lua";  
        if (File.Exists(path))
            return File.ReadAllBytes(path);
        else return null;
    }
    //E:\UnityProject\UnityGame\UnityGame\Assets\Scripts\Version1.0\Lua\Login
    /// <summary>
    /// 释放资源  并且设置单例对象为空
    /// </summary>
    public void DisposeEnv()
    {
        _luaEnv.Dispose();
        instance = null;
    }

    public LuaTable Global
    {
        get
        {
            return _luaEnv.Global;
        }
    }
}