using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelects : MonoBehaviour
{
    private bool _selectsInitialized;
    public Ship SelectedShip;
    public LevelContainer SelectedLevel;
    public List<Weapon> SelectedWeapons
    {
        get
        {
            return SelectedShip.Weapons;
        }
    }
    public List<Ammo> SelectedAmmo
    {
        get
        {
            return SelectedShip.Ammo;
        }
    }
    #region SelectGear
    public void SelectShip(Ship ship)
    {
        SelectedShip = CloneShip(ship);
        SetDefaultSelects(ship.Id);
    }

    internal void SetDebugSelects()
    {
        throw new NotImplementedException();
    }

    public void SelectWeapon(int id)
    {
        Weapon weapon = GetWeaponWithId(id);
        weapon.SetForGame = true;
    }

    public void SelectAmmo(int id)
    {
        Ammo ammo = GetAmmoWithId(id);
        ammo.SetForGame = true;
    }

    public void SelectLevel(int id)
    {
        SelectedLevel = StaticTagFinder.LevelsManager.GetLevelContainer(id);
    }
    #endregion
    #region set defaults
    private void SetDefaultSelects(int shipId)
    {
        Ship ship = StaticTagFinder.GameManager.GetShipByID(shipId);
        SelectedShip = CloneShip(ship);
        SetDefaultWeapons(SelectedShip);
        SetDefaultAmmo(SelectedShip);
    }

    private void SetDefaultWeapons(Ship ship)
    {
        for (int i = 0; i < ship.AvailableWeaponsIds.Length; i++)
        {
            ship.Weapons.Add(StaticTagFinder.GameManager.GetWeaponByID(ship.AvailableWeaponsIds[i]).CloneWeapon());
        }
    }

    private void SetDefaultAmmo(Ship ship)
    {
        for (int i = 0; i < ship.AvailableWeaponsIds.Length; i++)
        {
            ship.Ammo.Add(StaticTagFinder.GameManager.GetAmmoByID(ship.AvailableWeaponsIds[i]).CloneAmmo());
        }
    }
    #endregion
    private Ship CloneShip(Ship ship)
    {
        Ship clonedShip = new Ship()
        {
            Name = ship.Name,
            ShipObject = ship.ShipObject,
            Id = ship.Id,
            MaxHealth = ship.MaxHealth,
            MaxArmor = ship.MaxArmor,
            Sprite = ship.Sprite,
            Level = ship.Level,
            Speed = ship.Speed,
            Difficulty = ship.Difficulty,
            Hardness = ship.Hardness,
            AvailableWeaponsIds = (int[])ship.AvailableWeaponsIds.Clone()
        };
        return clonedShip;
    }
    #region Special Getters
    private Ammo GetAmmoWithId(int id)
    {
        for (int i = 0; i < SelectedAmmo.Count; i++)
        {
            if(SelectedAmmo[i].Id == id)
            {
                return SelectedAmmo[i];
            }
        }
        Ammo ammoDB = StaticTagFinder.GameManager.GetAmmoByID(id);
        if(ammoDB != null)
        {
            ammoDB = ammoDB.CloneAmmo();
            ammoDB.SetForGame = false;
            SelectedAmmo.Add(ammoDB);
            return ammoDB;
        }
        Debug.LogWarning("Can't find ammo in player selects with id " + id);
        return null;
    }

    private Weapon GetWeaponWithId(int id)
    {
        for (int i = 0; i < SelectedWeapons.Count; i++)
        {
            if(SelectedWeapons[i].Id == id)
            {
                return SelectedWeapons[i];
            }
        }
        Weapon weaponDB = StaticTagFinder.GameManager.GetWeaponByID(id);
        if(weaponDB != null)
        {
            weaponDB = weaponDB.CloneWeapon();
            weaponDB.SetForGame = false;
            SelectedWeapons.Add(weaponDB);
            return weaponDB;
        }
        Debug.LogWarning("Can't find weapon in player selects with id " + id);
        return null;
    }
    #endregion

    public void SetAmmoForWeapons(bool checkIfSetForGame = true)
    {
        for (int i = 0; i < SelectedWeapons.Count; i++)
        {
            SelectedWeapons[i].Ammo = new List<Ammo>();
            for (int j = 0; j < SelectedWeapons[i].AvailableAmmoIds.Length; j++)
            {
                int ammoId = SelectedWeapons[i].AvailableAmmoIds[j];
                Ammo ammo = GetAmmoWithId(ammoId);
                if (checkIfSetForGame)
                {
                    if (ammo.SetForGame && ammo.IsUnlocked())
                    {
                        SelectedWeapons[i].Ammo.Add(ammo);
                    }
                }
                else if (ammo.IsUnlocked())
                {
                    SelectedWeapons[i].Ammo.Add(ammo);
                }
            }
        }
        Debug.Log("Ammo for weapons set");
    }

    public bool IsAbleToStartGame() // sprawdza czy jest jakaś broń wybrana i amunicja
    {
        List<Ammo> ammoLinkedToWeapons = AmmoLinkedToSelectedWeapons();
        for (int i = 0; i < ammoLinkedToWeapons.Count; i++)
        {
            if(ammoLinkedToWeapons[i].Amount > 0)
            {
                return true;
            }
        }
        return false;
    }

    public bool IsAnyWeaponSelected()
    {
        for (int i = 0; i < SelectedWeapons.Count; i++)
        {
            if (SelectedWeapons[i].SetForGame)
            {
                return true;
            }
        }
        return false;
    }

    public bool IsAnyAmmoSelectedLinkedToSetWeapons()
    {
        List<Ammo> ammoLinkedToWeapons = AmmoLinkedToSelectedWeapons();

        for (int i = 0; i < ammoLinkedToWeapons.Count; i++)
        {
            if (ammoLinkedToWeapons[i].SetForGame)
            {
                return true;
            }
        }
        return false;
    }

    public List<Ammo> AmmoLinkedToSelectedWeapons()
    {
        List<Ammo> output = new List<Ammo>();
        for (int i = 0; i < SelectedWeapons.Count; i++)
        {
            for (int j = 0; j < SelectedWeapons[i].Ammo.Count; j++)
            {
                if (SelectedWeapons[i].SetForGame)
                {
                    Ammo ammo = SelectedWeapons[i].Ammo[j];
                    if (!output.Contains(ammo))
                    {
                        output.Add(ammo);
                    }
                }
            }
        }
        return output;
    }

    public List<Weapon> GetSetForGameWeapons()
    {
        List<Weapon> output = new List<Weapon>();
        for (int i = 0; i < SelectedWeapons.Count; i++)
        {
            if (SelectedWeapons[i].SetForGame)
            {
                output.Add(SelectedWeapons[i]);
            }
        }
        return output;
    }

    private void SetStartAmmoMaxAmount()
    {
        for (int i = 0; i < SelectedAmmo.Count; i++)
        {
            int amount = StaticTagFinder.Player.PlayerData.GetUnlockedAmmoData(SelectedAmmo[i].Id).Amount;
            SelectedAmmo[i].MaxAmount = amount;
        }
    }

    public void Init(bool debugMode)
    {
        //wczytanie z jakiegoś sejwa/bazy
        if (debugMode)
        {
            SetDebugSelects(1);
        }
        else
        {
            SetDefaultSelects(1);
            SetAmmoForWeapons(false);
            SetStartAmmoMaxAmount();
        }
        _selectsInitialized = true;
    }

    public void SetDebugSelects(int shipID)
    {
        SelectShip(StaticTagFinder.GameManager.GetShipByID(shipID));
        SetAmmoForWeapons(false);
        SelectedShip.SetAllGearActiveForGame();
        SetStartAmmoMaxAmount();
        for (int i = 0; i < SelectedAmmo.Count; i++)
        {
            SelectedAmmo[i].Amount = SelectedAmmo[i].MaxAmount;
        }
    }
    
}