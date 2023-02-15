using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRequest : BaseRequest
{
    protected override void Init()
    {
        request = Request.User;
        actionCode = ActionCode.LevelAction;
        LevelManager.Instance.levelRequest = this;
        base.Init();
    }

    public  void SendRequest(int  level)
    {
        Mainpack pack = new Mainpack() { Actioncode=actionCode,Request=request };
        PlayerPack playerPack = new PlayerPack();
        playerPack.Level = level;
        pack.Description = GameLifeCycle.Instance.username;

        pack.Playerpack.Add(playerPack);

        base.SendRequest(pack);
    }

}
