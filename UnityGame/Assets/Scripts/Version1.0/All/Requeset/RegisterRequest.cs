using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterRequest : BaseRequest
{
    private RegisterPanel registerPanel;
    private Mainpack mainpack;
    protected override void Init()
    {
        registerPanel=GetComponent<RegisterPanel>();
        actionCode = ActionCode.Register;
        request = Request.User;
        base.Init();
    }

    private void Update()
    {
        if (mainpack != null)
        {
            registerPanel.OnResponse(mainpack);
            mainpack = null;
        }
    }
  

    public override void OnResponse(Mainpack pack)
    {
        mainpack = pack;
    }
    public  void SendRequest(string user,string pas)
    {
        LoginPack loginPack = new LoginPack() {Username=user,Password=pas ,Playerindex=0,Playerlevel=0};
        Mainpack pack = new Mainpack() { Actioncode=actionCode,Request=request,Loginpack=loginPack};

        base.SendRequest(pack);
    }
}
