using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMission : MonoBehaviour
{
    [Tooltip("������")]
    public int mission_Index = 1;

    //�洢��NPC������
  

    //����npc����ĵ���
    public GameObject accept_MissionPanel;
    private AcceptMissionPanel acceptMissionPanel;

    private void OnEnable()
    {
        //Debug.Log("NPCִ���ˣ�����������������������");
        //accept_MissionPanel = GameLifeCycle.Instance.accept.gameObject;
        //Debug.Log(GameObject.Find("MainPanel"));
        //Debug.Log(accept_MissionPanel);
        acceptMissionPanel = accept_MissionPanel.GetComponent<AcceptMissionPanel>();
        //Debug.Log(acceptMissionPanel);
    }
    private void Awake()
    {
        //Debug.Log("����ɾ��");
        //MissionManager.Instance.Init();    
     //   acceptMissionPanel = accept_MissionPanel.GetComponent<AcceptMissionPanel>();
    }

    private void OnTriggerEnter(Collider other)
    {
       // Debug.Log(GameLifeCycle.Instance.accept);
        if (other.tag == "Player" &&other.name==GameLifeCycle.Instance.username)
        {
            acceptMissionPanel.gameObject.SetActive(true);
            acceptMissionPanel.ShowMissionInfo( MissionManager.Instance.missions[mission_Index - 1]);
        }
    }

    private void Update()
    {
        Debug.Log(acceptMissionPanel);
    }

}
