using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiLaserOverheat : MonoBehaviour
{
    public Image OverheatImage;
    public void SetOverHeatUi(float percent)
    {
        OverheatImage.fillAmount = percent;
    }

    public void SetActive(bool state)
    {
        gameObject.SetActive(state);
    }
}
