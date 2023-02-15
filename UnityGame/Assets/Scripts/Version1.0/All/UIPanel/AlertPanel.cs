using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertPanel : BaseUIPanel
{
    private Text text;
    private Button exit_but;
   
    public override void OnEnter()
    {
        gameObject.SetActive(true);
        UIManager.Instance.SetAlertPanel(this);
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

    protected  new  void InitPanel()
    {
        text = TransformHelper.FindChildByName(transform, "Text").GetComponent<Text>();

        exit_but = TransformHelper.FindChildByName(transform, "Button").GetComponent<Button>();
        exit_but.onClick.AddListener(OnClickExit);
    }

    private void OnClickExit()
    {
        // ui_manager.PopPanel();
        OnExit();
    }

    internal void ShowMessage(string str)
    {
        if (text == null)
        {
            InitPanel();
        }
       
        text.text = str;
    }
}
