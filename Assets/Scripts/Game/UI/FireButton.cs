using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FireButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public FireController FireController;

    public void OnPointerDown(PointerEventData eventData)
    {
        FireController.IsFiring = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        FireController.IsFiring = false;
    }
}