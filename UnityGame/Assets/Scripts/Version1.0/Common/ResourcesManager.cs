using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

public class ResourcesManager
{
    public static List<MissionData> missions = new List<MissionData>();

    #region  策划配置任务表
    public static void ReadMissionConfig()
    {
        string file = ConfigReader.GetConfigFile(ConfigFile.MissionConfig);

        Reader(file);


    }
    public static void Reader(string fileContent)
    {
        using (StringReader stringReader = new StringReader(fileContent))
        {
            MissionData missionData = new MissionData();
            string line;
            while ((line = stringReader.ReadLine()) != null)
            {

                string[] keyValue = line.Split('=');
                switch (keyValue[0])
                {
                    case "id":
                        missionData.id = int.Parse(keyValue[1]);
                        break;
                    case "description":
                        missionData.description = keyValue[1];
                        break;
                    case "require":
                        missionData.require.goodsType = (GoodsType)Enum.Parse(typeof(GoodsType), keyValue[1].Split('+')[0]);
                        missionData.require.goodsCount = int.Parse(keyValue[1].Split('+')[1]);
                        break;
                    case "reward":
                        missionData.reward.goodsType = (GoodsType)Enum.Parse(typeof(GoodsType), keyValue[1].Split('+')[0]);
                        missionData.reward.goodsCount = int.Parse(keyValue[1].Split('+')[1]);
                        break;
                }
                if (missionData.reward.goodsType != GoodsType.None)
                {
                    missions.Add(missionData);
                    missionData = default(MissionData);
                }
            }

        }

    }

    #endregion

    /// <summary>
    /// 从本地json文件中加载任务列表
    /// </summary>
    /// <returns></returns>
    public static List<MissionData> GetJsonData()
    {
        List<MissionData> missions = new List<MissionData>();

        string json = File.ReadAllText(ConfigFile.MissionJsonPath);
        JsonData d = JsonMapper.ToObject(json);
        if (d.Count == 0) return null;
        for (int i = 0; i < d.Count; i++)
        {
            MissionData data = new MissionData();
            data.description = d[i]["Description"].ToString();
            data.id = (int)d[i]["ID"];
            data.require.goodsType = (GoodsType)Enum.Parse(typeof(GoodsType), d[i]["Require"][0]["goodsType"].ToString());
            data.require.goodsCount = (int)d[i]["Require"][0]["goodsCount"];
            data.reward.goodsType = (GoodsType)Enum.Parse(typeof(GoodsType), d[i]["Reward"][0]["goodsType"].ToString());
            data.reward.goodsCount = (int)d[i]["Reward"][0]["goodsCount"];
            missions.Add(data);
        }

        return missions;
    }



    //从本地json文件中加载背包文件
    public static List<Items> GetPackageDatabase()
    {
        List<Items> database = new List<Items>();
        if (!File.Exists(ConfigFile.PackageDatabasePath)) return null;
        string json = File.ReadAllText(ConfigFile.PackageDatabasePath);
        if (string.IsNullOrEmpty(json)) return null;
        JsonData d = JsonMapper.ToObject(json);
        for (int i = 0; i < d.Count; i++)
        {
            Items res = new Items();
            res.itemName = d[i]["ItemName"].ToString();
            res.itemCount = d[i]["ItemCount"].ToString();
            res.isStack = (bool)d[i]["IsStack"];
            res.itemInfo = d[i]["Description"].ToString();
            res.type = (ItemType)Enum.Parse(typeof(ItemType), d[i]["ItemType"].ToString());
            res.sprite_Name = d[i]["SpriteName"].ToString();

            res.itemImage = Resources.Load<Sprite>("Items/" + res.sprite_Name);


            database.Add(res);
        }

        return database;
    }
    public static void SetPackageDatabase(Items obj)
    {
        if (!File.Exists(ConfigFile.PackageDatabasePath))
        {
            JsonData d = new JsonData();
            Additem(d, obj);
            return;
        }
        string json = File.ReadAllText(ConfigFile.PackageDatabasePath);
        JsonData _packdata = JsonMapper.ToObject(json);
        Additem(_packdata, obj);
    }
    private static void Additem(JsonData d, Items obj)
    {
        try
        {
            JsonData row = new JsonData();

            row["ItemName"] = obj.itemName;
            row["ItemCount"] = obj.itemCount;
            row["IsStack"] = obj.isStack;
            row["Description"] = obj.itemInfo;
            row["ItemType"] = obj.type.ToString();
            row["SpriteName"] = obj.itemImage.ToString().Substring(0, 4);

            d.Add(row);
            string data = JsonMapper.ToJson(d);
            data = Regex.Unescape(data);//正则表达式   转义符问题导致出现乱码
            File.WriteAllText(ConfigFile.PackageDatabasePath, data);
        } 
        catch (Exception e)
        {

         Debug.Log("写入数据异常！" +e.Message);
        }
       
    }

    public static void RefeshPackageDatabase(List<Items> items)
    {   
        string json = File.ReadAllText(ConfigFile.PackageDatabasePath);
        JsonData _packdata = JsonMapper.ToObject(json);
        _packdata.Clear();
        foreach (var item in items)
        {
            Additem(_packdata, item);
        }
       
    }

    //保存sprite 需要重新用其他方式
    //private byte[] GetSpriteSerializaInfo(Sprite image)
    //{
    //    return image.texture.GetRawTextureData();
    //}
    //private Sprite RebuildSprite(byte[] res)
    //{
    //    Texture2D tex = new Texture2D(4, 4);
    //    tex.LoadImage(res);
    //    return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
    //}

}






public class ConfigReader
{

    public static string GetConfigFile(string fileName)
    {
        string url;
#if UNITY_EDITOR || UNITY_STANDALONE
        url = "file://" + Application.dataPath + "/StreamingAssets/" + fileName;

#elif UNITY_IPHONE
        url = "file://" + Application.dataPath + /Raw/+ fileName;
#elif UNITY_ANDROID
        url = "jar:file://" + Application.dataPath + !/Assets/+ fileName;
#endif
        //获取生成的资源配置表格文件ConfigMap.txt
        UnityWebRequest www = UnityWebRequest.Get(url);
        www.SendWebRequest();
        while (true)
        {
            if (www.downloadHandler.isDone)
                return www.downloadHandler.text;
        }
    }
    public static void Reader(string fileContent, Action<string> Handle)
    {
        using (StringReader stringReader = new StringReader(fileContent))
        {
            string line;
            while ((line = stringReader.ReadLine()) != null)
            {
                Handle(line);

            }

        }

    }

}
