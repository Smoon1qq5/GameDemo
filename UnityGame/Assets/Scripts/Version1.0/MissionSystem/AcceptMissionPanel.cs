using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AcceptMissionPanel : BaseUIPanel
{
    //private void Awake()
    //{
    //    GameLifeCycle.Instance.accept = this;
    //    OnExit();
    //}
    private void OnEnable()
    {
        //Debug.Log("ִ����!!!!��");
        //OnEnter();
        //GameLifeCycle.Instance.accept = this;
        //OnExit();
    }
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

    public Button accept, exit;
    public Text des_text;
    public AcceptMissionRequest missionRequest;

    private MissionData data;
    protected override void InitPanel()
    {
        base.InitPanel();
        accept.onClick.AddListener(OnClickAccept);
        exit.onClick.AddListener(OnClickExit);
        
    }

    private void OnClickExit()
    {
        OnExit();
    }

    //��������
    private void OnClickAccept()
    {

        //����������Ϳͻ��˽�������״̬
       // missionRequest.SendRequest(data);     
        //���������UI��Ӿ�������
        MissionManager.Instance.AddMission2User(data);
        accept.onClick.RemoveAllListeners();
        accept.GetComponentInChildren<Text>().text = "�ѽ�������";
        //accept.GetComponent<Image>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }
    public void ShowMissionInfo(MissionData data)
    {
        this.data = data;
        //ΪUI�ϲ�������
        des_text.text =
        "����������" + data.description + "\r\n" +
        "��Ҫ�Ĳ���Ϊ��" + data.require.goodsType + "������Ϊ��" + data.require.goodsCount + "\r\n" +
        "����Ϊ��" + data.reward.goodsType + "������Ϊ��" + data.reward.goodsCount;
    }


    
}
