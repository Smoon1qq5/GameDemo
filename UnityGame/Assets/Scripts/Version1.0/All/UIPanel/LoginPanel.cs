using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : BaseUIPanel
{
    private InputField username, password;
    private Button login, register;
    private LoginRequest request;


    public override void OnEnter()
    {
        gameObject.SetActive(true);
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


    protected override void InitPanel()
    {
        username = TransformHelper.FindChildByName(transform, "UserName").GetComponent<InputField>();
        password = TransformHelper.FindChildByName(transform, "Password").GetComponent<InputField>();
        login = TransformHelper.FindChildByName(transform, "Login").GetComponent<Button>();
        register = TransformHelper.FindChildByName(transform, "Register").GetComponent<Button>();
        request = GetComponent<LoginRequest>();
        login.onClick.AddListener(OnLoginClick);
        register.onClick.AddListener(SwitchRegister);
    }

    private void OnLoginClick()
    {
        if (username.text == "" || password.text == "")
        {
            Debug.Log("ÓÃ»§ÃûÃÜÂë²»ÄÜÎª¿Õ");
            return;
        }

        //·¢ËÍµÇÂ¼ÇëÇó
        request.SendRequest(username.text, password.text);
    }

    private void SwitchRegister()
    {
        ui_manager.PushPanel(PanelType.Register);
    }

    //µÇÂ¼»ØÓ¦
    public override void OnResponse(Mainpack pack)
    {
        switch (pack.Respone)
        {
            case Response.Succeed:
                Debug.Log("µÇÂ¼³É¹¦");
                GameLifeCycle.Instance.username = pack.Loginpack.Username;
        
                ui_manager.PushPanel(PanelType.UserInfo);              

                break;
            case Response.Failed:
               
                ui_manager.ShouMessage("µÇÂ¼Ê§°Ü£¬Çë¼ì²éÕËºÅÃÜÂë!!");
                Debug.Log("µÇÂ¼Ê§°Ü");
                break;
            case Response.NeedChose:
                Debug.Log("Ñ¡Ôñ½ÇÉ«");
                GameLifeCycle.Instance.username = pack.Loginpack.Username;
                ui_manager.PushPanel(PanelType.SelectPlayer);
              
                break;
            case Response.ResponeNone:
                Debug.Log("×¢²áÓÐÎó");
                break;
        }
    }


}
