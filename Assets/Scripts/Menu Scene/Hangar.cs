using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//dopisać jakąś flagę (albo znalezc lepsze rozwiazanie) na przeładowanie mapki po zapisaniu statku, gdy sie edytuje statek z mapki


public class Hangar : MonoBehaviour
{
    public delegate void ShipSelection(Ship ship);
    public static event ShipSelection OnShipSelected;

    public GameObject HangarPanel;
    public List<Image> ShipImages = new List<Image>();
    public List<HangarShipButton> HangarShipButtons = new List<HangarShipButton>();
    public WeaponSelector WeaponSelector;

    public Ship SelectedShipInHangar;
    public List<Weapon> SelectedWeaponsInHangar = new List<Weapon>();

    private GameManager _GM;
    private ColorBlock _buttonDisabledColorBlock;

    private int _firstShipIndex = 1;
    private int _shipsInPreview = 3;

    public void Init()
    {
        gameObject.SetActive(true);
        if(_GM == null)
        {
            _GM = StaticTagFinder.GameManager;
        }
        if (StaticTagFinder.Player.PlayerSelects.SelectedShip != null)
        {
            LoadShips(StaticTagFinder.Player.PlayerSelects.SelectedShip.Id);
        }
        else
        {
            LoadShips(_firstShipIndex);
        }
    }

    public void CancelButton()
    {
        HangarPanel.SetActive(false);
    }

    public void SelectWeapon(Weapon weaponOnButton, Button button)
    {
        throw new NotImplementedException();
    }

    public void SaveShipConfigButton()
    {
        StaticTagFinder.Player.PlayerSelects.SelectedShip = SelectedShipInHangar;
        OnShipSelected(SelectedShipInHangar);
        HangarPanel.SetActive(false);
    }

    public void SelectShip(Ship ship, Button button)
    {
        SelectedShipInHangar = ship;
        for (int i = 0; i < HangarShipButtons.Count; i++)
        {
            HangarShipButtons[i].Button.colors = ButtonColors.Instance.ButtonColorBlock;
        }
        button.colors = ButtonColors.Instance.ButtonSelectedColorBlock;
        OnShipSelected(ship);
    }

    public void PreviousShip()
    {
        _firstShipIndex--;
        if (_firstShipIndex == 0)
        {
            _firstShipIndex = _GM.AvailableShips.Count;
        }
        LoadShips(_firstShipIndex);
    }

    public void NextShip()
    {
        _firstShipIndex++;
        if (_firstShipIndex == _GM.AvailableShips.Count+1)
        {
            _firstShipIndex = 1;
        }
        LoadShips(_firstShipIndex);
    }

    private void LoadShips(int shipID)
    {
        Debug.Log(shipID);
        SelectedWeaponsInHangar.Clear();
        int index = 0;
        for (int i = 0; i < ShipImages.Count; i++)
        {
            Ship ship = _GM.GetShipByID(shipID + index);
            ShipImages[i].sprite = ship.Sprite;
            HangarShipButtons[i].ShipOnButton = ship;
            index++;

            if (shipID + index == _GM.AvailableShips.Count+1)
            {
                shipID = 1;
                index = 0;
            }
            HangarShipButtons[i].Button.colors = ButtonColors.Instance.ButtonColorBlock;
            if (ship == SelectedShipInHangar)
            {
                HangarShipButtons[i].Button.colors = ButtonColors.Instance.ButtonSelectedColorBlock;
            }
            if (!StaticTagFinder.Player.IsShipUnlocked(ship))
            {
                HangarShipButtons[i].Button.interactable = false;
            }
            else
            {
                HangarShipButtons[i].Button.interactable = true;
            }
        }
    }
    
    private void Start()
    {
        OnShipSelected += WeaponSelector.LoadWeapons;
        WeaponSelector.OnWeaponSelected += ManageWeaponList;
        HangarShipButtons[0].Click();
    }

    private void ManageWeaponList(Weapon weapon, bool state)
    {
        if (state)
        {
            SelectedWeaponsInHangar.Add(weapon);
        }
        else
        {
            SelectedWeaponsInHangar.Remove(weapon);
        }
    }
}