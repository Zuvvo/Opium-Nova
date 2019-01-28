using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;

public class MovePanel : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public Joystick Joystick;

    private Vector3 _vector;

    public void OnDrag(PointerEventData eventData)
    {
        Joystick.gameObject.transform.position = GetMousePosition();
        Joystick.Drag(eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Joystick.JoystickImage.enabled = true;
        Joystick.gameObject.transform.position = GetMousePosition();
        Joystick.StartPosition = GetMousePosition();
        Joystick.DistanceFromCenter = 0;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Joystick.DistanceFromCenter = 0;
        Joystick.JoystickImage.enabled = false;
    }

    private Vector3 GetMousePosition()
    {
        return Input.mousePosition;
        _vector = Input.touches[0].position;
        for (int i = 0; i < Input.touchCount; i++)
        {
            if(Input.touches[i].position.x > _vector.x)
            {
                _vector = Input.touches[i].position;
            }
        }
        return _vector;
    }
}