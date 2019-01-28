using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HangarWeaponButton : MonoBehaviour
{
    public Button Button;
    public Hangar Hangar;

    public Weapon WeaponOnButton;

    private void Start()
    {
        Button.onClick.AddListener(() => SelectWeapon());
    }

    private void SelectWeapon()
    {
        Debug.Log("Selected ship: " + WeaponOnButton.Name);
        Hangar.SelectWeapon(WeaponOnButton, Button);
    }
}