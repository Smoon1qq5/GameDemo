using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ABHelper
{
    [MenuItem("AB包/导出")]
    public static void ExportAB()
    {
        string path = Application.dataPath;
        path = path.Substring(0, path.Length - 6) + "AssetBundle";

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);


        BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
        Debug.Log("导出成功");
    }

   private static  AssetBundle asset;
    [MenuItem("测试/加载AB包文件")]
    public static  void FUNC()
    {
        //加载步骤1：通过AB包文件路径，加载AB包文件
        string path = Application.dataPath;
        path =path. Substring(0, Application.dataPath.Length - 6) + "AssetBundle/";
        Debug.Log(path);
        asset = AssetBundle.LoadFromFile(path+"loginuiprefab");

        //加载步骤2：通过名称（资源的名称），加载内部资源
        Object login = asset.LoadAsset<Object>("Login 1");

      Object  obj= Object.Instantiate(login, GameObject.Find("/Canvas").transform) ;
        GameObject game = obj as GameObject;
        Transform trs = game.transform;
       

        Debug.Log(obj.name);
         
    }
    [MenuItem("测试/卸载AB包文件")]
    public static void Free()
    {
        asset.Unload(false);
    }

}
