using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Text Percent;
    public Image BackGround;
    public Image FillImage;

    public void Fill(float amount)
    {
        FillImage.fillAmount = amount;
        Percent.text = string.Format("{0}%", ((int)(amount * 100)).ToString());
    }

    public void Reset()
    {
        FillImage.fillAmount = 0;
        Percent.text = "0%";
    }
}