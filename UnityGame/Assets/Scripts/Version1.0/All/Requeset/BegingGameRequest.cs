using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BegingGameRequest : BaseRequest 
{
    private Mainpack mainpack = null;

    public RoomPanel roomPanel;
    protected override void Init()
    {
        actionCode = ActionCode.Beginning;
        request = Request.Room;
        base.Init();
    }

    private void Update()
    {
        if (mainpack != null && SceneManager.GetActiveScene().name=="BattleScene")
        {
            //PlayerManager.Instance.AddPlayer(mainpack);
           
            roomPanel.BeginingGameResponse(mainpack);
 
            mainpack = null;
        }
    }
   
    public override void OnResponse(Mainpack pack)
    {
        mainpack = pack;
    }

}
