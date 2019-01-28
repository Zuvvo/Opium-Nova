using BitStrap;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenClick : MonoBehaviour
{
    public SafeAction Action = new SafeAction();

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void Click()
    {
        Action.Call();
    }
}
