using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectPlayerPanel : BaseUIPanel
{
    private Button player01, player02, confirm;
    private Text playertext;
    private SelectPlayerRequest selectPlayerRequest;
    internal int playerIndex=-1;
    protected new void InitPanel()
    {
        player01 = TransformHelper.FindChildByName(transform, "Player01").GetComponent<Button>();
        player02 = TransformHelper.FindChildByName(transform, "Player02").GetComponent<Button>();
        confirm = TransformHelper.FindChildByName(transform, "Confirm").GetComponent<Button>();
        playertext = TransformHelper.FindChildByName(transform, "PlayerImage").GetComponentInChildren<Text>();
        selectPlayerRequest = GetComponent<SelectPlayerRequest>();
        player01.onClick.AddListener(OnClickPlayer01);
        player02.onClick.AddListener(OnClickPlayer02);
        confirm.onClick.AddListener(OnClickConfirm);
        base.InitPanel();
    }

    private void OnClickConfirm()
    {
        //发送数据给服务器记录 
        selectPlayerRequest.SendRequest();    

    }

    private void OnClickPlayer02()
    {
      
        playerIndex = 2;
        playertext.text= player02.name;
    }

    private void OnClickPlayer01()
    {
    
        playerIndex = 1;
        playertext.text = player01.name;
    }

    public override void OnEnter()
    {
        gameObject.SetActive(true);
        if (selectPlayerRequest == null)
        {
            InitPanel();
        }
       
    }

    public override void OnExit()
    {
        gameObject.SetActive(false);
    }

    public override void OnPause()
    {
        gameObject.SetActive(false);
    }

    public override void OnRecovery()
    {
        gameObject.SetActive(true);
    }

    public override void OnResponse(Mainpack pack)
    {
        switch (pack.Respone)
        {
            case Response.Succeed:
                ui_manager.PushPanel(PanelType.UserInfo);
                break;
            case Response.Failed:
                ui_manager.PushPanel(PanelType.Alert);
                ui_manager.ShouMessage("选择角色异常");
                break;
            case Response.NeedChose:
                ui_manager.PushPanel(PanelType.Alert);
                ui_manager.ShouMessage("没有角色");
                break;
        }
    }


  
}
