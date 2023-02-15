using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetInventoryRequest : BaseRequest
{
    protected override void Init()
    {
        request = Request.Inventory;
        actionCode = ActionCode.GetInventory;
        base.Init();
    }

    public override void OnResponse(Mainpack pack)
    {
        base.OnResponse(pack);
    }

    public  void SendRequest()
    {
        Mainpack pack = new Mainpack();
        base.SendRequest(pack);
    }
}
