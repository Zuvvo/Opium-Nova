using System;
using System.Collections.Generic;
using UnityEngine;

public class GameLoader : MonoBehaviour
{
    public Transform PlayerSpawnPoint;
    public FireController FireController;
    public MoveController MoveController;

    public DebugStartGame DebugStart;

    private WeaponObject _weaponPrefab;
    private Vector3 _weaponPos;
    private Ship _ship;
    private ShipObject _shipGO;
    private GameObject _bulletSpawnPoint;
    private Player _player;
    private PlayerSelects _playerSelects;

    public void Init(Player player)
    {
        Debug.Log("GAME LOADER");
        Time.timeScale = 1;
        _player = player;
        _playerSelects = _player.PlayerSelects;
        _ship = _playerSelects.SelectedShip;

        DeleteNotSelectedGear();
        SpawnShipAndWeapons();
        InitShipObject();
        SetControllers();
        LoadLevelScript(player.PlayerSelects.SelectedLevel.LevelData.Id);
        MoveController.IsPlayerBlocked = false;
    }

    private void Awake()
    {
        if (!DebugStart.DebugMode)
        {
            Player player = StaticTagFinder.Player;
            Init(player); // temp 1
        }
    }

    private void LoadLevelScript(int levelId)
    {
        StaticTagFinder.LevelsManager.InitLevelHandler(levelId);
    }

    private void DeleteNotSelectedGear()
    {
        for (int i = _ship.Weapons.Count-1; i >= 0; i--)
        {
            Weapon weapon = _ship.Weapons[i];
            if (!weapon.SetForGame)
            {
                _ship.Weapons.Remove(weapon);
            }
        }

        _ship.Ammo.RemoveAll(a => !a.SetForGame || a.Amount <= 0);

        for (int i = 0; i < _ship.Ammo.Count; i++)
        {
            Ammo ammo = _ship.Ammo[i];
            if (!ammo.SetForGame || ammo.Amount <=0)
            {
                _ship.Ammo.Remove(ammo);
            }
        }
    }

    private void SpawnShipAndWeapons()
    {
        _shipGO =  Instantiate(_ship.ShipObject, PlayerSpawnPoint.position, Quaternion.identity);
        _shipGO.transform.Rotate(new Vector3(0, 0, -90));
        for (int i = 0; i < _ship.Weapons.Count; i++)
        {
            Debug.Log("weapon: " + _ship.Weapons[i].Name);
            _weaponPrefab = StaticTagFinder.GameManager.GetWeaponByID(_ship.Weapons[i].Id).WeaponPrefab;
            _weaponPos = PlayerSpawnPoint.position + _weaponPrefab.transform.position;
            WeaponObject weapon = Instantiate(_weaponPrefab, _weaponPos, Quaternion.identity, _shipGO.transform);
            weapon.gameObject.SetActive(false);
            _ship.Weapons[i].WeaponObject = weapon;
        }
    }

    private void SetControllers()  // zawsze pierwsza broń! do fixa
    {
        FireController.SetFireController(_shipGO, _ship.Weapons, _ship.Ammo);
        MoveController.SetMoveController(_shipGO, _ship.Speed);
    }

    private void InitShipObject()
    {
        _shipGO.InitPlayerGameData(_player);
    }
}