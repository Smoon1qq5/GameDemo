using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGameRequest : BaseRequest
{
   
    private Mainpack mainpack = null;
    protected override void Init()
    {
        actionCode = ActionCode.ExitGame;
        request = Request.Game;
        base.Init();
    }
    private void Update()
    {
        if (mainpack != null)
        {
            //ÍË³öbattle
          //  PlayerManager.Instance.BattleExit();
            mainpack = null;
        }
    }
   

    public  void SendRequest()
    {
        Mainpack pack = new Mainpack() { Actioncode = actionCode, Request = request, Description = GameLifeCycle.Instance.username };

        base.SendRequest(pack);
    }

    public override void OnResponse(Mainpack pack)
    {
        mainpack = pack;
    }
}
