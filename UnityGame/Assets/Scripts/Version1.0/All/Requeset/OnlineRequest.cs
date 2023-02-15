using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnlineRequest : BaseRequest
{
    Mainpack mainpack;
    [SerializeField]
    private MainPanel panel;
    protected override void Init()
    {
        
        request = Request.Room;
        actionCode=ActionCode.Online;
        base.Init();
    }
      
    private void Update()
    {
        //Debug.Log("下发的包是否为空："+mainpack == null);
        //&& SceneManager.GetActiveScene().name=="MainCity"
        if (mainpack != null && SceneManager.GetActiveScene().name == "MainCity")
        {
            panel.OnlinePlayerInfo(mainpack);
            
            mainpack = null;
        }
      
    }
    public override void OnResponse(Mainpack pack)
    {
        mainpack=pack;
    }
    public  void SendRequest()
    {
        Mainpack pack = new Mainpack();
        pack.Actioncode = actionCode;
        pack.Request = request;
        pack.Description = GameLifeCycle.Instance.username;

        base.SendRequest(pack);
    }
}
