using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterPanel : BaseUIPanel
{
    private InputField username, password,rpassword;
    private Button login, register;
    private RegisterRequest request;
  
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
        rpassword= TransformHelper.FindChildByName(transform, "RPassword").GetComponent<InputField>();
        login = TransformHelper.FindChildByName(transform, "Login").GetComponent<Button>();
        register = TransformHelper.FindChildByName(transform, "Register").GetComponent<Button>();
        request=GetComponent<RegisterRequest>();
        login.onClick.AddListener(SwitchLogin);
        register.onClick.AddListener(OnClickRegister);

    }

    private void OnClickRegister()
    {
        if (username.text == "" || password.text == ""|| rpassword.text=="")
        {
     
            ui_manager.ShouMessage("用户名密码不能为空");
            Debug.Log("用户名密码不能为空");
            return;
        }
        if (password.text != rpassword.text)
        {
     
            ui_manager.ShouMessage("两次密码不一致");
            Debug.Log("两次密码不一致");
            return;
        }

        //发送请求
        request.SendRequest(username.text, password.text);
    }

    private void SwitchLogin()
    {
        ui_manager.PopPanel();
    }
    
    public override void OnResponse(Mainpack pack)
    {
        switch (pack.Respone)
        {
            case Response.Succeed:
                Debug.Log("注册成功");
                
       
                ui_manager.ShouMessage("注册成功");
                break;
            case Response.Failed:
                ui_manager.PushPanel(PanelType.Alert);
                
                ui_manager.ShouMessage("注册失败，请检查账号密码!!");
                Debug.Log("注册失败");
                break;
            case Response.ResponeNone:
                Debug.Log("注册有误");
                break;
        }
    }
}
