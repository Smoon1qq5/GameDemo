using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncPosition : MonoBehaviour
{
    private UpdateStateRequest updatePosRequest;
   

    private void Start()
    {
      
        updatePosRequest =GetComponent<UpdateStateRequest>();
        InvokeRepeating("UpdatePositionFunc", 1, 1/10f);
    }

    private void UpdatePositionFunc()
    {
        Vector3 pos = transform.position;
        float rot = transform.eulerAngles.y;

        updatePosRequest.SendRequest(pos, rot);
    }

    
}
