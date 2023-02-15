using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatRequest : BaseRequest
{
    private string chatStr = null;
    [SerializeField]
    private  MainPanel chatPanel;
    protected override void Init()
    {
        
        request = Request.Room;
        actionCode = ActionCode.Chat;
        base.Init();
    }
    private void Update()
    {
        if (chatStr != null)
        {
           
            chatPanel.ChatResonse(chatStr);
            chatStr = null;
        }
      
    }
  

    public override void OnResponse(Mainpack pack)
    {
        chatStr = pack.Description;
    }
    public  void SendRequest(string  str)
    {
        Mainpack pack = new Mainpack();
        pack.Actioncode = actionCode;
        pack.Request = request;
        pack.Description = str;
        base.SendRequest(pack);
    }
}
