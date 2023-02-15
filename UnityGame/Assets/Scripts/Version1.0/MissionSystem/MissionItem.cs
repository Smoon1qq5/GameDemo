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
    [Tooltip("��ǰ������Ʒ����")]
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
        //ÿ��enable
        DisplayInfo(data);
        pathfinding.onClick.AddListener(OnClickPathfinding);
    }

    //�Զ�Ѱ·
    private void OnClickPathfinding()
    {
        //Ѱ·���������ĵص�

        GameLifeCycle.Instance.aa.Init();
        GameLifeCycle.Instance.ap.Init();

        pathfinding.gameObject.SetActive(false);
        pathfinding.onClick.RemoveListener(OnClickPathfinding);

    }



    //��������������

    public void DisplayInfo(MissionData mission)
    {

        switch (mission.require.goodsType)
        {
            case GoodsType.Experience:

                break;
            case GoodsType.GoldCoin:
                int currentGoldCoin = 0;
                des.text = mission.description;
                require.text = "������" + currentGoldCoin.ToString() + "/" + mission.require.goodsCount.ToString();
                if (currentGoldCoin >= mission.require.goodsCount && MissionManager.Instance._Flag_isFinish)
                {

                    get.gameObject.SetActive(true);
                    get.onClick.AddListener(() =>
                    {

                         //��ý���  
                        Debug.Log("��������ý�����");

                         //�������
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
                require.text = "������" + currentGoods.ToString() + "/" + mission.require.goodsCount.ToString();
                if (currentGoods >= mission.require.goodsCount && MissionManager.Instance._Flag_isFinish)
                {

                    get.gameObject.SetActive(true);
                    get.onClick.AddListener(() =>
                    {
                         //��ý���  
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

        reward.text = "��ã�" + mission.reward.goodsType.ToString() + mission.reward.goodsCount;


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
