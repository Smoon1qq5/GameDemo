using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpdateStateRequest : BaseRequest
{



    protected override void Init()
    {
        actionCode = ActionCode.UpdateState;
        request = Request.Game;
        base.Init();
    }
    private void OnEnable()
    {
        base.Init();
       // AddRequest();
    }
    private void OnDestroy()
    {
        //RemoveRequest(actionCode);
    }

    public override void OnResponse(Mainpack pack)
    {

        switch (SceneManager.GetActiveScene().name)
        {
            case "MainCity":
                PlayerManager.Instance.SyncPosition(pack, PlayerManager.Instance.Player_dic);
                break;
            case "BattleScene":
                PlayerManager.Instance.SyncPosition(pack, GameLifeCycle.Instance.player_Battle_dic);
                break;
        }

    }

    public void SendRequest(Vector3 pos, float userRot)
    {

        PosPack posPack = new PosPack() { PosX = pos.x, PosY=pos.y,PosZ = pos.z, RotY = userRot };
        PlayerPack playerPack = new PlayerPack() { PlayerName = GameLifeCycle.Instance.username, Pos = posPack };
        Mainpack pack = new Mainpack() { Actioncode = actionCode, Request = request };
        pack.Playerpack.Add(playerPack);

        base.SendRequestByUdp(pack);
    }
    private void OnApplicationQuit()
    {
        RemoveRequest(actionCode);
    }
}
