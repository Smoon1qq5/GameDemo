using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginRequest : BaseRequest
{
    private LoginPanel loginPanel;
    private Mainpack mainpack;
    protected override void Init()
    {
        loginPanel = GetComponent<LoginPanel>();
        actionCode = ActionCode.Login;
        request = Request.User;
        base.Init();
    }

    public void SendRequest(string user, string pas)
    {
        LoginPack login = new LoginPack() { Password = pas, Username = user };
        Mainpack pack = new Mainpack() { Actioncode = actionCode, Request = request, Loginpack = login };

        base.SendRequest(pack);
    }
    private void Update()
    {
        if (mainpack != null)
        {
            loginPanel.OnResponse(mainpack);
            mainpack = null;
        }
    }


    public override void OnResponse(Mainpack pack)
    {
        
        mainpack = pack;
    

    }
}
