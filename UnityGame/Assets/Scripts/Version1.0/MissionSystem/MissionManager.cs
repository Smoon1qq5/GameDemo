using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager
{
    private static MissionManager instance;
    public static MissionManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MissionManager();
                
            }
            return instance;
        }
    }

    public MissionPanel missionPanel;
    public CharacterMission characterMission;
    public AcceptMissionRequest missionRequest;


    public bool _Flag_isFinish = false;
    public void Init()
    {
        missions = ResourcesManager.GetJsonData();
        
    }
    

    //配置文件中的任务
    public List<MissionData> missions;

    public void RemoveMission(MissionData data)
    {
        missions.Remove(data);
    }

    public void AddMission2User(MissionData data)
    {
        missionPanel.AddMission2List(data);
    }
    public void CompleteMission(MissionData data)
    {
        missionPanel.CompleteMission(data);
        missions.Remove(data);
    }

   
}
