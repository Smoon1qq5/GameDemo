using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

[System.Serializable]
public struct Items
{
    public string itemName;

    public string itemCount;
    public bool isStack;
   
    public string itemInfo;
    public ItemType type;


   

    //精灵的名称
    public string sprite_Name;
}
[System.Serializable]
public enum ItemType
{
    Food,

    Material,
    Default

}
public class PackageData
{
    public static string filePath = Directory.GetCurrentDirectory().Substring(0, Directory.GetCurrentDirectory().Length - 9) + "MissionJson/PackageDatabase.json";

    public PackageData()
    {
    }
    public void AddItems2Package(Items items, Client client)
    {
        try
        {
            string files = File.ReadAllText(filePath);
            JsonData d = JsonMapper.ToObject(files);
            JsonData row = new JsonData();
            row["User"] = client.GetUserInfo.UserName;
            row["ItemName"] = items.itemName;
            row["ItemCount"] = items.itemCount;
            row["ItemType"] = items.type.ToString();

            d.Add(row);
            string data = JsonMapper.ToJson(d);
            data = Regex.Unescape(data);//正则表达式   转义符问题导致出现乱码
            File.WriteAllText(filePath, data);
        }
        catch (System.Exception e)
        {

            System.Console.WriteLine("写入背包数据异常:" + e.Message);
        }

    }
    //public static void Test(Items items, string name)
    //{
    //    try
    //    {
    //        string files = File.ReadAllText(filePath);
    //        JsonData d = JsonMapper.ToObject(files);
    //        JsonData row = new JsonData();
    //        row["User"] = name;
    //        row["ItemName"] = items.itemName;
    //        row["ItemCount"] = items.itemCount;
    //        row["ItemType"] = items.type.ToString();

    //        d.Add(row);

    //        string data = JsonMapper.ToJson(d);
    //        data = Regex.Unescape(data);//正则表达式   转义符问题导致出现乱码
    //        File.WriteAllText(filePath, data);
    //    }
    //    catch (System.Exception e)
    //    {

    //        System.Console.WriteLine("写入背包数据异常:" + e.Message);
    //    }
    //}


    #region  往服务器同步背包状态
    /*
    public static List<Items> GetPackageDatabase()
    {
        List<Items> database = new List<Items>();
        if (!File.Exists(filePath)) return null;
        string json = File.ReadAllText(filePath);
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

            //res.itemImage = Resources.Load<Sprite>("Items/" + res.sprite_Name);


            database.Add(res);
        }

        return database;
    }

    public static void SetPackageDatabase(Mainpack pack)
    {
        if (!File.Exists(filePath))
        {
            JsonData d = new JsonData();
            Additem(d, pack.Inventorypack);
            return;
        }
        string json = File.ReadAllText(filePath);
        JsonData _packdata = JsonMapper.ToObject(json);
        Additem(_packdata, pack.Inventorypack);
    }
    private static void Additem(JsonData d, InventoryPack obj)
    {
        try
        {
            JsonData row = new JsonData();

            row["ItemName"] = obj.ItemName;
            row["ItemCount"] = obj.ItemCount;
            row["IsStack"] = obj.IsStack;
            row["Description"] = obj.Description;
            row["ItemType"] = obj.ItemType.ToString();
            row["SpriteName"] =obj.SpriteName; 

            d.Add(row);
            string data = JsonMapper.ToJson(d);
            data = Regex.Unescape(data);//正则表达式   转义符问题导致出现乱码
            File.WriteAllText(filePath, data);
        }
        catch (Exception e)
        {

            Console.WriteLine ("写入数据异常！" + e.Message);
        }

    }
    public static void RefeshPackageDatabase(List<Items> items)
    {
        string json = File.ReadAllText(filePath);
        JsonData _packdata = JsonMapper.ToObject(json);
        _packdata.Clear();
        foreach (var item in items)
        {
          //  Additem(_packdata, item);
        }

    }
    */
    #endregion
}

