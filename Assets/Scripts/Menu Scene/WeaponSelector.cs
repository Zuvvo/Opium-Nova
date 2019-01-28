using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelector : MonoBehaviour
{

    public delegate void WeaponSelected (Weapon weapon, bool state);
    public static event WeaponSelected OnWeaponSelected;

    private GameManager _GM;

    private List<Weapon> _weapons;

    public List<SingleWeapon> SingleWeaponPanels;

    private void Start()
    {
        Hangar.OnShipSelected += LoadWeapons;
        _GM = StaticTagFinder.GameManager;
    }
    public void LoadWeapons(Ship ship)
    {
        _weapons = ship.Weapons;
        HideAllWeapons();
        ShowWeapons();
        DeselectAllWeapons();
    }

    private void DeselectAllWeapons()
    {
        for (int i = 0; i < SingleWeaponPanels.Count; i++)
        {
            SingleWeaponPanels[i].DeselectWeapon();
        }
    }

    public void SelectWeapon(Weapon weapon)
    {
        OnWeaponSelected(weapon, true);
    }

    public void DeselectWeapon(Weapon weapon)
    {
        OnWeaponSelected(weapon, false);
    }

    private void ShowWeapons()
    {
        for (int i = 0; i < _weapons.Count; i++)
        {
            SingleWeaponPanels[i].Show(_weapons[i]);
        }
    }

    private void HideAllWeapons()
    {
        for (int i = 0; i < SingleWeaponPanels.Count; i++)
        {
            SingleWeaponPanels[i].Hide();
        }
    }
}