using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMission : MonoBehaviour
{


    public int require_count=0;
    //ÈÎÎñiD
    private int index=0;

    private void Awake()
    {
        MissionManager.Instance.characterMission = this;
      //  Debug.Log(ConfigFile.MissionJsonPath);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        { 
            require_count++;
            PackageManager.Instance.CollectMaterial(other.gameObject);
           

          SendRequest(MissionManager.Instance.missionPanel.missionDatas[index], require_count);
          //  index++;
        }
    }

    private void SendRequest(MissionData data ,int count )
    {  

        MissionManager.Instance.missionRequest.SendRequest(data,count);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (MissionManager.Instance.missionPanel.gameObject.activeSelf)
            {
                MissionManager.Instance.missionPanel.gameObject.SetActive(false);
            }
            else
            {
                MissionManager.Instance.missionPanel.gameObject.SetActive(true);
            }
        }
    }
}
