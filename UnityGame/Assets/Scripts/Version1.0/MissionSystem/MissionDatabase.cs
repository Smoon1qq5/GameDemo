using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

public enum GoodsType
{
    None,
    Experience,
    GoldCoin,
    Material
}
[System.Serializable]
public struct MissionGoods
{
    public GoodsType goodsType;
    public int goodsCount;
}

[System.Serializable]
public struct MissionData
{
    /// <summary>
    /// ��������
    /// </summary>
    public string description;
    /// <summary>
    /// ������
    /// </summary>
    public int id;
    /// <summary>
    /// ��������/����
    /// </summary>
    public MissionGoods require;
    /// <summary>
    /// ������
    /// </summary>
    public MissionGoods reward;


}

public class MissionDatabase
{
    private  static MissionDatabase instance;
    public static MissionDatabase Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MissionDatabase();
            }
            return instance;    
        }
    }
    public List<MissionData> missionTasks;

    public MissionDatabase()
    {
        missionTasks = new List<MissionData>();

        CreateMission();
    }

    private void CreateMission()
    {
        ResourcesManager.ReadMissionConfig();
        missionTasks = ResourcesManager.missions;
        if (!File.Exists(ConfigFile.MissionJsonPath))
        {
            UpdateMissionData();
        }
    }
    public void UpdateMissionData()
    {
        ResourcesManager.ReadMissionConfig();
        missionTasks = ResourcesManager.missions;
        JsonData d = new JsonData();
        for (int i = 0; i < missionTasks.Count; i++)
        {
            AddJsonDataInfo(d, i);

        }
        string data = JsonMapper.ToJson(d);
        data = Regex.Unescape(data);//������ʽ   ת������⵼�³�������
        File.WriteAllText(ConfigFile.MissionJsonPath, data);
    }
    public void AddJsonDataInfo(JsonData d, int i)
    {       
        JsonData row = new JsonData();
        row["Description"] = missionTasks[i].description;
        row["ID"] = missionTasks[i].id;
        JsonData row2 = new JsonData();
        row2["goodsType"] = missionTasks[i].require.goodsType.ToString();
        row2["goodsCount"] = missionTasks[i].require.goodsCount;
        row["Require"] = new JsonData();
        row["Require"].Add(row2);
        JsonData row3 = new JsonData();
        row3["goodsType"] = missionTasks[i].reward.goodsType.ToString();
        row3["goodsCount"] = missionTasks[i].reward.goodsCount;
        row["Reward"] = new JsonData();
        row["Reward"].Add(row3);
        d.Add(row);
    }





}

public static class ConfigFile
{
    public static string MissionJsonPath = Application.persistentDataPath + "/MissionJson.json";
    public static string MissionConfig = "MissionConfig.txt";

    public static string PackageDatabasePath = Application.persistentDataPath + "/PackageDatabase.json";



}


