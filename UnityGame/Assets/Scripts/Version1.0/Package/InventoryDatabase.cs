using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;



[System.Serializable]
public class InventoryDatabase : MonoBehaviour
{
    public List<Items> items = new List<Items>(36);

    public static InventoryDatabase Instance;

    //public InventoryDatabase()
    //{
    //    ReadDatabaseItem();
    //}
    private void Awake()
    {

        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
        WriteDatabaseItem();
        ReadDatabaseItem();

        Debug.Log(ConfigFile.PackageDatabasePath);

    }
    //提供读写
    public void ReadDatabaseItem()
    {
        List<Items> list = ResourcesManager.GetPackageDatabase();
        if (list == null)
        {
            Debug.Log("背包里什么也没有-----------------");
            return;
        }
        Debug.Log(list.Count);
        for (int i = 0; i < list.Count; i++)
        {
            items[i] = list[i];
        }

    }
    public void WriteDatabaseItem()
    {
        if (File.Exists(ConfigFile.PackageDatabasePath)) return;
        foreach (var item in items)
        {
            ResourcesManager.SetPackageDatabase(item);
        }

    }
    public void RefeshDatabase()
    {
        Debug.Log("flag!---------------" + items.Count);

        ResourcesManager.RefeshPackageDatabase(items);




    }

}
