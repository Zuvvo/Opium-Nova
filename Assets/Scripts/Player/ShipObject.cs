using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipObject : MonoBehaviour
{
    public event Action<PlayerGameData> OnStateChanged;

    private PlayerGameData _playerGameData;
    private Ship _ship;
    private Player _player;
    private UnlockedShip _unlockedShipData;

    public void InitPlayerGameData(Player player)
    {
        _player = player;
        _ship = player.PlayerSelects.SelectedShip;
        _unlockedShipData = player.GetUnlockedShipByID(_ship.Id);
        _playerGameData = new PlayerGameData()
        {
            UnlockedShipData = _unlockedShipData,
            Armor = _unlockedShipData.Armor,
            Health = _unlockedShipData.Health,
            Experience = _unlockedShipData.Experience,
            Level = _unlockedShipData.Level,
            ShipRef = _ship
        };
        StaticTagFinder.GameUI.UiShipBars.Init(this);
        RefreshUi();
    }

    public void ChangeHealth(int change)
    {
        _playerGameData.Health += change;
        RefreshUi();
    }

    public void ChangeArmor(int change)
    {
        _playerGameData.Armor += change;
        RefreshUi();
    }

    public void ChangeExperience(int change)
    {
        _playerGameData.Experience += change;
        if (IsAbleToLevelUp())
        {
            LevelUpShip();
        }
        RefreshUi();
    }

    private void LevelUpShip()
    {
        _playerGameData.Level += 1;
    }

    private bool IsAbleToLevelUp()
    {
        return _playerGameData.Experience >= ExperienceConst.ExpNeededForLevel(_playerGameData.Level + 1);
    }

    private void RefreshUi()
    {
        if(OnStateChanged != null && _playerGameData != null)
        {
            OnStateChanged(_playerGameData);
        }
    }
}