using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    public Image AmmoImage;
    public Text AmmoName;
    public Text Amount;
    public Text AmmoIndex;

    private Ammo _ammo;

    public void SetUI(Ammo ammo, int index, int count)
    {
        _ammo = ammo;
        AmmoImage.sprite = ammo.Icon;
        AmmoName.text = ammo.Name;
        Amount.text = ammo.Amount.ToString();
        AmmoIndex.text = string.Format("{0}/{1}", index, count);
    }

    public void RefreshAmount()
    {
        Amount.text = _ammo.Amount.ToString();
    }
}