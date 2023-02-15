using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ABHelper
{
    [MenuItem("AB��/����")]
    public static void ExportAB()
    {
        string path = Application.dataPath;
        path = path.Substring(0, path.Length - 6) + "AssetBundle";

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);


        BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
        Debug.Log("�����ɹ�");
    }

   private static  AssetBundle asset;
    [MenuItem("����/����AB���ļ�")]
    public static  void FUNC()
    {
        //���ز���1��ͨ��AB���ļ�·��������AB���ļ�
        string path = Application.dataPath;
        path =path. Substring(0, Application.dataPath.Length - 6) + "AssetBundle/";
        Debug.Log(path);
        asset = AssetBundle.LoadFromFile(path+"loginuiprefab");

        //���ز���2��ͨ�����ƣ���Դ�����ƣ��������ڲ���Դ
        Object login = asset.LoadAsset<Object>("Login 1");

      Object  obj= Object.Instantiate(login, GameObject.Find("/Canvas").transform) ;
        GameObject game = obj as GameObject;
        Transform trs = game.transform;
       

        Debug.Log(obj.name);
         
    }
    [MenuItem("����/ж��AB���ļ�")]
    public static void Free()
    {
        asset.Unload(false);
    }

}
