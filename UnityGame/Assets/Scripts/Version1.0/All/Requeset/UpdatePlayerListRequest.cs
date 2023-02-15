using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePlayerListRequest : BaseRequest
{
    Mainpack mainpack = null;
    public GamePanel gamePanel;
    protected override void Init()
    {
        request = Request.Game;
        actionCode = ActionCode.UpdatePlayerList;
        base.Init();
    }
    private void Update()
    {
        if (mainpack != null)
        {
            gamePanel.UpdatePlayerInfo(mainpack);
            //˳��ɾ��������Ϸ���������
            gamePanel.RemovePlayer(mainpack);
            mainpack = null;
        }
    }

    public override void OnResponse(Mainpack pack)
    {
        mainpack = pack;
    }
}
