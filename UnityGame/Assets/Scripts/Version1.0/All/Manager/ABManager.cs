using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// AB��������Դ
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
    //���ab����·��
    private string fileway = Application.dataPath;

    //ab�嵥������Dependencies �����
    private AssetBundleManifest manifest;
    //�洢 �������Ƽ��ص������
    private Dictionary<string, AssetBundle> bundles_dic = new Dictionary<string, AssetBundle>();
    private Dictionary<string, Object> obj_dic = new Dictionary<string, Object>();

    public void Init()
    {
        fileway = fileway.Substring(0, fileway.Length - 6) + "AssetBundle";

        LoadAllPack();
    }
    private void LoadAllPack()
    {
       
        //�ܵ�AB��
        AssetBundle all_asset = AssetBundle.LoadFromFile(fileway + "/AssetBundle");
        //�ܰ���manifest  AB���嵥
        manifest = all_asset.LoadAsset("AssetBundleManifest") as AssetBundleManifest;
        //ж��һ��AssetBundle���ͷ���������
        all_asset.Unload(false);
    }

    /// <summary>
    /// �������Ƽ�����Դ
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
    /// ��������Ҫ�ļ�
    /// </summary>
    /// <param name="main_key">��Ҫ�ļ�</param>
    /// <param name="fliename">���������</param>
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
