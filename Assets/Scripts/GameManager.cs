using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static bool Initialized = false;
    public RecipeManager RecipeManager;
    public List<Ship> AvailableShips;
    public List<Weapon> AvailableWeapons;
    public List<Ammo> AvailableAmmo;

    public Ship GetShipByID(int id)
    {
        for (int i = 0; i < AvailableShips.Count; i++)
        {
            if (id == AvailableShips[i].Id)
            {
                return AvailableShips[i];
            }
        }
        Debug.LogError("ship null");
        return null;
    }

    public void Init(bool debugMode = false)
    {
        if (!Initialized)
        {
            Debug.Log("Game manager init");
            SetUnlockedWeaponsForShip();
            DontDestroyOnLoad(gameObject);
            StaticTagFinder.Player.PlayerSelects.Init(debugMode);
            Initialized = true;
        }
        else
        {
            Debug.Log("destroying game manager here");
            Destroy(gameObject);
        }
    }

    public Weapon GetWeaponByID(int id)
    {
        for (int i = 0; i < AvailableWeapons.Count; i++)
        {
            if(AvailableWeapons[i].Id == id)
            {
                return AvailableWeapons[i];
            }
        }
        Debug.LogError("Can't find weapon with id: " + id);
        return null;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Start");
    }

    public Ammo GetAmmoByID(int id)
    {
        for (int i = 0; i < AvailableAmmo.Count; i++)
        {
            if (AvailableAmmo[i].Id == id)
            {
                return AvailableAmmo[i];
            }
        }
        Debug.LogError("Can't find ammo with id: " + id);
        return null;
    }

    private void Start()
    {
        if (!Initialized)
        {
            Init();
        }
    }

    private void SetUnlockedWeaponsForShip() // do przeniesienia na PlayerSelects.cs
    {
        for (int i = 0; i < AvailableShips.Count; i++)
        {
            Ship ship = AvailableShips[i];
            for (int j = 0; j < ship.AvailableWeaponsIds.Length; j++)
            {
                Weapon weapon = GetWeaponByID(ship.AvailableWeaponsIds[j]);
                if (weapon.IsUnlocked())
                {
                    AvailableShips[i].Weapons.Add(weapon);
                    SetUnlockedAmmoForShipAndWeapon(weapon, ship);
                }
            }
        }
    }

    private void SetUnlockedAmmoForShipAndWeapon(Weapon weapon, Ship ship)
    {
        for (int i = 0; i < weapon.AvailableAmmoIds.Length; i++)
        {
            int id = weapon.AvailableAmmoIds[i];
            Ammo ammo = GetAmmoByID(id);
            if (ammo.IsUnlocked())
            {
                if (!ship.HaveAmmoOfID(id))
                {
                    ship.Ammo.Add(ammo);
                }
                if (!weapon.HaveAmmo(ammo))
                {
                    weapon.Ammo.Add(ammo);
                }
            }
        }
    }

    //private void LoadShipForPlayer(Ship ship)
    //{
    //    for (int i = 0; i < ship.Weapons.Count; i++)
    //    {
    //        Weapon weapon = ship.Weapons[i];
    //        if (!weapon.IsUnlocked())
    //        {
    //            ship.Weapons.Remove(weapon);
    //        }
    //    }

    //    for (int i = 0; i < ship.Ammo.Count; i++)
    //    {
    //        Ammo ammo = ship.Ammo[i];
    //        if (!ammo.IsUnlocked())
    //        {
    //            ship.Ammo.Remove(ammo);
    //        }
    //    }
    //    Player.Instance.SelectedShip = ship;
    //}
}
