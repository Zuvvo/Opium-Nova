using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponPanel : MonoBehaviour
{
    public bool IsSelected;
    public Weapon Weapon;
    public Button Button;
    public Image WeaponImage;

    public Image SelectBackgroundImage;
    public Image SelectImage;
    public List<Image> AmmoImages;
    public List<GameObject> BlockingPanels;

    public UiMenuWindowGearSelect GearSelect;

    private Sprite _selectedBackgroundSprite;
    private Sprite _selectedSprite;
    private Sprite _notSelectedBackgroundSprite;
    private Sprite _notSelectedSprite;

    public void SetWeaponPanel(Weapon weapon)
    {
        Weapon = weapon;
        gameObject.SetActive(true);
        WeaponImage.sprite = weapon.WeaponIcon;
        if (weapon.SetForGame)
        {
            SelectWeapon();
        }
        else
        {
            DeselectWeapon();
        }
        ShowAvailableAmmo();
    }

    private void ShowAvailableAmmo()
    {
        List<Ammo> ammos = Weapon.AvailableAmmoForWeapon();
        for (int i = 0; i < ammos.Count; i++)
        {
            AmmoImages[i].gameObject.transform.parent.gameObject.SetActive(true);
            AmmoImages[i].sprite = ammos[i].Icon;
            if (!ammos[i].IsUnlocked())
            {
                BlockingPanels[i].SetActive(true);
            }
            else
            {
                BlockingPanels[i].SetActive(false);
            }
        }
    }

    private void Start()
    {
        Button.onClick.AddListener(() => ButtonClick());
        SetButtonSprites();
    }

    public void SetButtonSprites()
    {
        _selectedBackgroundSprite = ButtonColors.Instance.SelectedBackgroundSprite;
        _selectedSprite = ButtonColors.Instance.SelectedSprite;
        _notSelectedBackgroundSprite = ButtonColors.Instance.NotSelectedBackgroundSprite;
        _notSelectedSprite = ButtonColors.Instance.NotSelectedSprite;
    }

    private void ButtonClick()
    {
        if (!IsSelected)
        {
            SelectWeapon();
        }
        else
        {
            DeselectWeapon();
        }
        GearSelect.BlockStartIfNotAble();
    }

    public void DeselectWeapon()
    {
        Debug.Log("deselect");
        IsSelected = false;
        SelectImage.sprite = _notSelectedSprite;
        SelectBackgroundImage.sprite = _notSelectedBackgroundSprite;
        Weapon.SetForGame = false;

      //  GearSelect.DeactivateLinkedAmmoToWeapon(Weapon);
    }

    public void SelectWeapon()
    {
        Debug.Log("select");
        IsSelected = true;
        SelectImage.sprite = _selectedSprite;
        SelectBackgroundImage.sprite = _selectedBackgroundSprite;
        Weapon.SetForGame = true;
    }
}