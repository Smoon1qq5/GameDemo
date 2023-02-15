using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionItem : MonoBehaviour
{
    public int id;
    public Text name;
    public Text des;
    public Text require;
    public Text reward;
    public Button get, pathfinding;
    public MissionDatabase missionDatabase;

    bool is_Finish = false;
    [Tooltip("当前任务物品数量")]
    public int currentCount = 0;

    [HideInInspector]
    public MissionData data;
    void Awake()
    {
        Debug.Log(Application.persistentDataPath);

        pathfinding.onClick.AddListener(OnClickPathfinding);
        get.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        //每次enable
        DisplayInfo(data);
        pathfinding.onClick.AddListener(OnClickPathfinding);
    }

    //自动寻路
    private void OnClickPathfinding()
    {
        //寻路到完成任务的地点

        GameLifeCycle.Instance.aa.Init();
        GameLifeCycle.Instance.ap.Init();

        pathfinding.gameObject.SetActive(false);
        pathfinding.onClick.RemoveListener(OnClickPathfinding);

    }



    //检查任务材料数量

    public void DisplayInfo(MissionData mission)
    {

        switch (mission.require.goodsType)
        {
            case GoodsType.Experience:

                break;
            case GoodsType.GoldCoin:
                int currentGoldCoin = 0;
                des.text = mission.description;
                require.text = "条件：" + currentGoldCoin.ToString() + "/" + mission.require.goodsCount.ToString();
                if (currentGoldCoin >= mission.require.goodsCount && MissionManager.Instance._Flag_isFinish)
                {

                    get.gameObject.SetActive(true);
                    get.onClick.AddListener(() =>
                    {

                         //获得奖励  
                        Debug.Log("完成任务获得奖励！");

                         //完成任务
                        MissionManager.Instance.CompleteMission(data);
                        is_Finish = true;
                        get.onClick.RemoveAllListeners();
                        Destroy(get.transform.gameObject);
                    });
                }
                break;
            case GoodsType.Material:
                int currentGoods = MissionManager.Instance.characterMission.require_count;
                des.text = mission.description;
                require.text = "条件：" + currentGoods.ToString() + "/" + mission.require.goodsCount.ToString();
                if (currentGoods >= mission.require.goodsCount && MissionManager.Instance._Flag_isFinish)
                {

                    get.gameObject.SetActive(true);
                    get.onClick.AddListener(() =>
                    {
                         //获得奖励  
                        GetMissionReward(data);

                        is_Finish = true;
                        get.onClick.RemoveAllListeners();
                        get.transform.parent.gameObject.SetActive(false);
                    });
                }
                break;
            default:
                break;
        }

        reward.text = "获得：" + mission.reward.goodsType.ToString() + mission.reward.goodsCount;


    }

  

    private void GetMissionReward(MissionData missionData)
    {
        switch (missionData.reward.goodsType)
        {
            case GoodsType.None:
                break;
            case GoodsType.Experience:
                LevelManager.Instance.RefeshLevel(missionData.reward.goodsCount);
                break;
            case GoodsType.GoldCoin:
                break;
            case GoodsType.Material:
                break;
            default:
                break;
        }
    }
}
