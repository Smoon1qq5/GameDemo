using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionPanel : BaseUIPanel
{
    public Transform missionList;
    public GameObject missionItem_pef;

    private Dictionary<MissionData, GameObject> mis_obj = new Dictionary<MissionData, GameObject>();
    public List<MissionData> missionDatas = new List<MissionData>();
    private void Awake()
    {
      
    }
    protected override void InitPanel()
    {
        MissionManager.Instance.missionPanel = this;
        gameObject.SetActive(false);

        base.InitPanel();
    }

    public void AddMission2List(MissionData data)
    {
        GameObject obj = Instantiate(missionItem_pef, missionList);
        obj.GetComponent<MissionItem>().data = data;
        mis_obj.Add(data, obj);
        missionDatas.Add(data);
    }
    public void CompleteMission(MissionData data)
    {
        RemoveMissionInList(data);
    }
    private void RemoveMissionInList(MissionData data)
    {
        if (mis_obj.TryGetValue(data, out GameObject obj))
        {
            Destroy(obj);
        }
        if (mis_obj.ContainsKey(data))
        {
            mis_obj.Remove(data);
        }
        if (missionDatas.Contains(data))
        {
            missionDatas.Remove(data);
        }
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
}
