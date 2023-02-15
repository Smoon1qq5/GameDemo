using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class MissionDataBase
{
    private static  MissionDataBase instance;
    public static  MissionDataBase Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MissionDataBase();
            }
            return instance;
        }
    }
    public static  string filePath = Directory.GetCurrentDirectory().Substring(0, Directory.GetCurrentDirectory().Length - 9)+ "MissionJson/MissionJson.json";

    public  List<MissionData> missionDatas = new List<MissionData>();

    public MissionDataBase()
    {
        missionDatas=GetJsonData();
    }
    ~MissionDataBase()
    {
        instance = null;
        missionDatas.Clear();
    }
    public  List<MissionData> GetJsonData()
    {
        List<MissionData> missions = new List<MissionData>();

        string json = File.ReadAllText(filePath);
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
}


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
    /// 任务描述
    /// </summary>
    public string description;
    /// <summary>
    /// 任务编号
    /// </summary>
    public int id;
    /// <summary>
    /// 任务条件/需求
    /// </summary>
    public MissionGoods require;
    /// <summary>
    /// 任务奖励
    /// </summary>
    public MissionGoods reward;


}

