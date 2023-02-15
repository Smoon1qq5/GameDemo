using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// AB包加载资源
/// </summary>
public class ABManager
{
    private static ABManager instance;
    public static ABManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ABManager();
            }
            return instance;
        }
    }
    //存放ab包的路径
    private string fileway = Application.dataPath;

    //ab清单（包含Dependencies 依赖项）
    private AssetBundleManifest manifest;
    //存储 根据名称加载的捆绑包
    private Dictionary<string, AssetBundle> bundles_dic = new Dictionary<string, AssetBundle>();
    private Dictionary<string, Object> obj_dic = new Dictionary<string, Object>();

    public void Init()
    {
        fileway = fileway.Substring(0, fileway.Length - 6) + "AssetBundle";

        LoadAllPack();
    }
    private void LoadAllPack()
    {
       
        //总的AB包
        AssetBundle all_asset = AssetBundle.LoadFromFile(fileway + "/AssetBundle");
        //总包的manifest  AB包清单
        manifest = all_asset.LoadAsset("AssetBundleManifest") as AssetBundleManifest;
        //卸载一个AssetBundle，释放它的数据
        all_asset.Unload(false);
    }

    /// <summary>
    /// 根据名称加载资源
    /// </summary>
    /// <param name="name"></param>
    public void LoadFile(string name)
    {
        if (bundles_dic.ContainsKey(name) &&bundles_dic[name] != null) return;
        string[] name_array = manifest.GetAllDependencies(name);
        for (int i = 0; i < name_array.Length; i++)
        {
            if (name_array[i] == null)
                LoadFile(name_array[i]);
        }

        bundles_dic[name] = AssetBundle.LoadFromFile(fileway + "/" + name);

    }


    /// <summary>
    /// 加载所需要文件
    /// </summary>
    /// <param name="main_key">主要文件</param>
    /// <param name="fliename">所需对象名</param>
    /// <returns></returns>
    public Object LoadNeedAsset(string main_key, string fliename)
    {
        LoadFile(main_key);
        if (obj_dic.ContainsKey(fliename)) return obj_dic[fliename];       

        Object result = bundles_dic[main_key].LoadAsset(fliename);
        obj_dic.Add(fliename, result);
        return obj_dic[fliename];
    }


}
