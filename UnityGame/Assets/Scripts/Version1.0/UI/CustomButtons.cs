using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[System.Serializable]
public class CustomButtons : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [System.Serializable]
    public class OnDownHandler : UnityEvent<string> { }
    [System.Serializable]
    public class OnUPHandler : UnityEvent { }
    [SerializeField]
    public OnDownHandler onDown;
    [SerializeField]
    public OnUPHandler onUp;

    [SerializeField]
    public string buttonName;

    public void OnPointerDown(PointerEventData eventData)
    {
        onDown.Invoke(buttonName);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        onUp.Invoke();
    }
}
