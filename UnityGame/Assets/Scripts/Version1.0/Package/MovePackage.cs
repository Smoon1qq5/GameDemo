using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovePackage : MonoBehaviour ,IDragHandler
{
    RectTransform rect;
    public void OnDrag(PointerEventData eventData)
    {
        rect.anchoredPosition += eventData.delta;
    }

    void Start()
    {
        rect=GetComponent<RectTransform>();
    }

   
    void Update()
    {
        
    }
}
