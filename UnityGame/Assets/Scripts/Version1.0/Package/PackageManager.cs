using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageManager
{
    private static PackageManager instance;
    public static PackageManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PackageManager();
            }
            return instance;
        }
    }

    public PackageManager()
    {
        Init();
    }
    public PackagePanel packagePanel;
    public InitInventoryRequest initInventoryRequest;
    public GetInventoryRequest getInventoryRequest;
    public RefeshInventoryRequest refeshInventoryRequest;
    public void Init()
    {

    }
    public void CollectMaterial(GameObject obj)
    {
        packagePanel.CollectObject(obj);
    }
    //public void InitInventoryRequest(Items items)
    //{
    //    initInventoryRequest.SendRequest(items);
    //}
    //public void GetInventoryRequest()
    //{
    //    getInventoryRequest.SendRequest();
    //}
    //public void RefeshInventoryRequest()
    //{
    //    refeshInventoryRequest.SendRequest();
    //}
}
