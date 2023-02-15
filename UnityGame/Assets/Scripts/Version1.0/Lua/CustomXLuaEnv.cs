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
    /// ��װdostring 
    /// </summary>
    /// <param name="code">lua���룺��require������</param>
    /// <returns></returns>
    public object[] DoString(string code)
    {
        return _luaEnv.DoString(code);
    }

    //�Զ������������������ϵͳ���ü�����ִ�У����Զ�����������ص��ļ��󣬺����ļ������򲻻�ִ��
    //��lua����ִ��require() ����ʱ���Զ���������᳢�Ի���ļ�������
    /// <summary>
    /// //�Զ��������  
    /// </summary>
    /// <param name="filepath">������lua�ļ���·��</param>
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
    /// �ͷ���Դ  �������õ�������Ϊ��
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