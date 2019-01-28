using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crafting : MonoBehaviour
{
    public Button WeaponsOption;
    public Button AmmoOption;
    public ProgressBar ProgressBar;

    public CraftingAmmo CraftingAmmo;
    public CraftingWeapon CraftingWeapon;
    public CraftingResourcesUI CraftingResourcesUI;
    public CraftingCommunique CraftingCommunique;

    public void Init()
    {
        gameObject.SetActive(true);
        CraftingResourcesUI.LoadResources();
        AmmoClick(); // defaultowy wybór
    }

    public void WeaponClick()
    {
        WeaponsOption.image.sprite = ButtonColors.Instance.SelectedButtonSpriteGearSelect;
        AmmoOption.image.sprite = ButtonColors.Instance.NotSelectedButtonSpriteGearSelect;
        CraftingWeapon.gameObject.SetActive(true);
        CraftingAmmo.Close();
    }

    public void AmmoClick()
    {
        WeaponsOption.image.sprite = ButtonColors.Instance.NotSelectedButtonSpriteGearSelect;
        AmmoOption.image.sprite = ButtonColors.Instance.SelectedButtonSpriteGearSelect;
        CraftingWeapon.gameObject.SetActive(false);
        CraftingAmmo.Init();
    }

    public void BackClick()
    {
        gameObject.SetActive(false);
    }


}