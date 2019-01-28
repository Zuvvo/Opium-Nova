using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    public Image WeaponImage;
    public Text WeaponName;

    public void SetUI(Weapon weapon)
    {
        WeaponImage.sprite = weapon.WeaponIcon;
        WeaponName.text = weapon.Name;
    }
}