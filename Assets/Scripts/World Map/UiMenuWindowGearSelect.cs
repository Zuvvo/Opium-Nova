using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiMenuWindowGearSelect : UiMenuWindowBase
{
    public Image BackGroudnImage;
    public Button StartButton;

    [Header("SHIP")]
    public Button ShipButton;
    public Image ShipButtonImage;
    public Text ShipName;
    public Text ShipDescription;
    public Image ShipImage;
    public ShipProgressBar ShipProgressBar;
    public GameObject ShipPanelHolder;
    public List<Image> WeaponImages;
    public List<Image> AmmoImages;
    public List<Text> AmmoCountText;

    [Header("WEAPONS")]
    public Button WeaponsButton;
    public Image WeaponsButtonImage;
    public List<WeaponPanel> WeaponPanels;
    public GameObject WeaponPanelHolder;
    public GameObject BlockWeaponsPanel;

    [Header("AMMO")]
    public Button AmmoButton;
    public Image AmmoButtonImage;
    public List<AmmoPanel> AmmoPanels;
    public GameObject AmmoPanelHolder;
    public GameObject BlockAmmoPanel;

    private Color _selectedColor;
    private Color _notSelectedColor;

    private Sprite _selectedSprite;
    private Sprite _notSelectedSprite;

    private Player _player;
    private Ship _ship;

    private bool _anyAmmoSelected;
    private bool _anyWeaponSelected;


    private void Start()
    {
        Hangar.OnShipSelected += Reload;
    }

    private void Init()
    {
        _player = StaticTagFinder.Player;
        BackGroudnImage.sprite = UiMenuWindowMapSelect.SelectedMap.BackgroundImage;
        LoadButtonSprites();
        ShowShipButton();
        gameObject.SetActive(true);
    }

    public void BackClick()
    {
        CloseWindow();
        StaticTagFinder.UiMenuWindowManager.LevelSelect.ReActivateWindow();
    }
    
    public void ShowShipButton()
    {
        ShipButtonImage.sprite = _selectedSprite;
        WeaponsButtonImage.sprite = _notSelectedSprite;
        AmmoButtonImage.sprite = _notSelectedSprite;
        LoadShip();
        BlockStartIfNotAble();
    }

    public void ShowWeaponsButton()
    {
        ShipButtonImage.sprite = _notSelectedSprite;
        WeaponsButtonImage.sprite = _selectedSprite;
        AmmoButtonImage.sprite = _notSelectedSprite;
        LoadWeapons();
        BlockStartIfNotAble();
    }


    public void ShowAmmoButton()
    {
        ShipButtonImage.sprite = _notSelectedSprite;
        WeaponsButtonImage.sprite = _notSelectedSprite;
        AmmoButtonImage.sprite = _selectedSprite;
        LoadAmmo();
        BlockStartIfNotAble();
    }

    public void SetStartButtonInteractable(bool value)
    {
        StartButton.interactable = value;
    }

    private void LoadShip()
    {
        ShipPanelHolder.SetActive(true);
        WeaponPanelHolder.SetActive(false);
        AmmoPanelHolder.SetActive(false);
        _ship = _player.PlayerSelects.SelectedShip;
        ShipName.text = _ship.Name;
        ShipImage.sprite = _ship.Sprite;
        ShipDescription.text = _ship.ShipDescription;
        UnlockedShip unlockedShip = _player.GetUnlockedShipByID(_ship.Id);
        LoadSelectedWeaponsAndAmmo();
        ShipProgressBar.SetProgressBar(unlockedShip.Level, unlockedShip.Experience);
    }

    private void LoadSelectedWeaponsAndAmmo()
    {
        for (int i = 0; i < WeaponImages.Count; i++)
        {
            WeaponImages[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < AmmoImages.Count; i++)
        {
            AmmoImages[i].gameObject.SetActive(false);
        }
        List<Ammo> setForGameAmmo = StaticTagFinder.Player.PlayerSelects.AmmoLinkedToSelectedWeapons();
        List<Weapon> setForGameWeapons = StaticTagFinder.Player.PlayerSelects.GetSetForGameWeapons();

        for (int i = 0; i < setForGameAmmo.Count; i++)
        {
            if(setForGameAmmo[i].Amount > 0)
            {
                AmmoImages[i].gameObject.SetActive(true);
                AmmoImages[i].sprite = setForGameAmmo[i].Icon;
                AmmoCountText[i].text = setForGameAmmo[i].Amount.ToString();
            }
        }

        for (int i = 0; i < setForGameWeapons.Count; i++)
        {
            WeaponImages[i].gameObject.SetActive(true);
            WeaponImages[i].sprite = setForGameWeapons[i].WeaponIcon;
        }
    }

    public void BlockStartIfNotAble()
    {
        bool ableToStart = StaticTagFinder.Player.PlayerSelects.IsAbleToStartGame();
        bool isAmmoSelected = StaticTagFinder.Player.PlayerSelects.IsAnyAmmoSelectedLinkedToSetWeapons();
        bool isWeaponSelected = StaticTagFinder.Player.PlayerSelects.IsAnyWeaponSelected();
        BlockWeaponsPanel.SetActive(!isWeaponSelected);
        BlockAmmoPanel.SetActive(!isAmmoSelected);
        SetStartButtonInteractable(ableToStart);
    }

    private void LoadWeapons()
    {
        ShipPanelHolder.SetActive(false);
        WeaponPanelHolder.SetActive(true);
        AmmoPanelHolder.SetActive(false);

        for (int i = 0; i < WeaponPanels.Count; i++)
        {
            WeaponPanels[i].gameObject.SetActive(false);
            WeaponPanels[i].SetButtonSprites();
        }
        for (int i = 0; i < _ship.Weapons.Count; i++)
        {
            WeaponPanels[i].SetWeaponPanel(_ship.Weapons[i]);
        }
    }

    private void Reload(Ship ship)
    {
        _ship = ship;
        ShowShipButton();
    }

    private void LoadAmmo()
    {
        ShipPanelHolder.SetActive(false);
        WeaponPanelHolder.SetActive(false);
        AmmoPanelHolder.SetActive(true);

        for (int i = 0; i < AmmoPanels.Count; i++)
        {
            AmmoPanels[i].gameObject.SetActive(false);
            AmmoPanels[i].SetButtonSprites();
        }

        List<Ammo> ammo = StaticTagFinder.Player.PlayerSelects.SelectedAmmo;
        for (int i = 0; i < ammo.Count; i++)
        {
                AmmoPanels[i].SetAmmoPanel(_ship.Ammo[i]);
        }
    }

    public void DeactivateLinkedAmmoToWeapon(Weapon weapon)
    {
        for (int i = 0; i < AmmoPanels.Count; i++)
        {
            for (int j = 0; j < weapon.AvailableAmmoIds.Length; j++)
            {
                if (AmmoPanels[i].Ammo.Id == weapon.AvailableAmmoIds[j])
                {
                    AmmoPanels[i].DeselectAmmo();
                }
            }
        }
    }

    private void LoadButtonSprites()
    {
        _selectedSprite = ButtonColors.Instance.SelectedButtonSpriteGearSelect;
        _notSelectedSprite = ButtonColors.Instance.NotSelectedButtonSpriteGearSelect;
    }

    protected override void CloseThisWindow()
    {

    }

    protected override void OpenThisWindow()
    {
        Init();
    }
}