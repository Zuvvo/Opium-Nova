using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player : MonoBehaviour
{
    public PlayerSelects PlayerSelects;
    public PlayerData PlayerData
    {
        get
        {
            if(!_playerData.Initialized)
            {
                _playerData = new PlayerData(0);
            }
            return _playerData;
        }
        private set
        {
            _playerData = value;
        }
    }
   // [HideInInspector]
    public PlayersTempDatabase PlayersTempDatabase;
    [SerializeField]
    private PlayerData _playerData;

    public bool IsShipUnlocked(Ship ship)
    {
        for (int i = 0; i < _playerData.UnlockedShips.Count; i++)
        {
            if(_playerData.UnlockedShips[i].ID == ship.Id)
            {
                return _playerData.UnlockedShips[i].Unlocked;
            }
        }
        Debug.LogWarning("Can't find this ship!");
        return false;
    }

    public bool IsShipUnlocked(int id)
    {
        for (int i = 0; i < _playerData.UnlockedShips.Count; i++)
        {
            if (_playerData.UnlockedShips[i].ID == id)
            {
                return _playerData.UnlockedShips[i].Unlocked;
            }
        }
        Debug.LogWarning("Can't find this ship!");
        return false;
    }

    public bool IsWeaponUnlocked(Weapon weapon)
    {
        for (int i = 0; i < _playerData.UnlockedWeapons.Count; i++)
        {
            if (_playerData.UnlockedWeapons[i].ID == weapon.Id)
            {
                return _playerData.UnlockedWeapons[i].Unlocked;
            }
        }
        Debug.LogWarning("Can't find this weapon!");
        return false;
    }

    public bool IsAnyLevelUnlocked()
    {
        for (int i = 0; i < PlayerData.UnlockedLevels.Count; i++)
        {
            if (PlayerData.UnlockedLevels[i].Unlocked)
            {
                return true;
            }
        }
        return false;
    }

    public bool IsLevelUnlocked(LevelData levelData)
    {
        for (int i = 0; i < _playerData.UnlockedLevels.Count; i++)
        {
            if (_playerData.UnlockedLevels[i].LevelContainer.LevelData == levelData)
            {
                return _playerData.UnlockedLevels[i].Unlocked;
            }
        }
        Debug.LogWarning("Can't find this level!");
        return false;
    }

    public UnlockedShip GetUnlockedShipByID(int id)
    {
        for (int i = 0; i < _playerData.UnlockedShips.Count; i++)
        {
            if(_playerData.UnlockedShips[i].ID == id)
            {
                return _playerData.UnlockedShips[i];
            }
        }
        Debug.LogWarning("can't find your ship");
        return null;
    }

    public UnlockedAmmo GetUnlockedAmmoByID(int id)
    {
        for (int i = 0; i < _playerData.UnlockedAmmunition.Count; i++)
        {
            if (_playerData.UnlockedAmmunition[i].ID == id)
            {
                return _playerData.UnlockedAmmunition[i];
            }
        }
        Debug.LogWarning("can't find ammo with id: " + id);
        return null;
    }

    //private void Start()
    //{
    //    Hangar.OnShipSelected += SetShip;
    //}

    //private void SetShip(Ship ship)
    //{
    //    PlayerSelects.SelectedShip = ship;
    //}
}