using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
///  ��λ�����UI����   ��Ӱ ����ʱ��
/// </summary>
public  class BaseUIPanel : MonoBehaviour
{
    protected UIManager ui_manager;
    public UIManager UI_Manager
    {
        set { ui_manager = value; }
    }
    private  void Start()
    {
        InitPanel();
    }
    
    protected virtual void InitPanel() { }

    public virtual void OnPause()
    {

    }
    public virtual void OnEnter() { }
    public virtual void OnRecovery() { }
    public virtual void OnExit() { }

    public virtual void OnResponse(Mainpack pack)
    {

    }

}



