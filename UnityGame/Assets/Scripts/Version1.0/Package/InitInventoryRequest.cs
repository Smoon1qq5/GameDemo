using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitInventoryRequest : BaseRequest
{

    protected override void Init()
    {
        request = Request.Inventory;
        actionCode = ActionCode.InitInventory;
        PackageManager.Instance.initInventoryRequest= this;
        base.Init();
    }

    public  void SendRequest(Items obj)
    {
        Mainpack pack = new Mainpack() {Actioncode=actionCode ,Request=request };
        InventoryPack inventoryPack = new InventoryPack();
        inventoryPack.ItemName = obj.itemName;
        inventoryPack.ItemCount = obj.itemCount;
        inventoryPack.IsStack = obj.isStack;
        inventoryPack.Description = obj.itemInfo;
        inventoryPack.ItemType =(InventoryType) Enum.Parse(typeof(InventoryType), obj.type.ToString());
        inventoryPack.SpriteName = obj.itemImage.ToString().Substring(0, 4);
        pack.Inventorypack = inventoryPack;


        base.SendRequest(pack);
    }
}
