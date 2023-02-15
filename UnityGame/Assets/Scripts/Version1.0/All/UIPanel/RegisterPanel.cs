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
     
            ui_manager.ShouMessage("�û������벻��Ϊ��");
            Debug.Log("�û������벻��Ϊ��");
            return;
        }
        if (password.text != rpassword.text)
        {
     
            ui_manager.ShouMessage("�������벻һ��");
            Debug.Log("�������벻һ��");
            return;
        }

        //��������
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
                Debug.Log("ע��ɹ�");
                
       
                ui_manager.ShouMessage("ע��ɹ�");
                break;
            case Response.Failed:
                ui_manager.PushPanel(PanelType.Alert);
                
                ui_manager.ShouMessage("ע��ʧ�ܣ������˺�����!!");
                Debug.Log("ע��ʧ��");
                break;
            case Response.ResponeNone:
                Debug.Log("ע������");
                break;
        }
    }
}
