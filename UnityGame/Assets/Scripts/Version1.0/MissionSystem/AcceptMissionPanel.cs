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
        //Debug.Log("执行了!!!!！");
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

    //接受任务
    private void OnClickAccept()
    {

        //向服务器发送客户端接受任务状态
       // missionRequest.SendRequest(data);     
        //给玩家任务UI添加具体任务
        MissionManager.Instance.AddMission2User(data);
        accept.onClick.RemoveAllListeners();
        accept.GetComponentInChildren<Text>().text = "已接受任务";
        //accept.GetComponent<Image>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }
    public void ShowMissionInfo(MissionData data)
    {
        this.data = data;
        //为UI上布置任务
        des_text.text =
        "任务描述：" + data.description + "\r\n" +
        "需要的材料为：" + data.require.goodsType + "，数量为：" + data.require.goodsCount + "\r\n" +
        "奖励为：" + data.reward.goodsType + "，数量为：" + data.reward.goodsCount;
    }


    
}
