using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleWeapon : MonoBehaviour
{
    public WeaponSelector WeaponSelector;

    public Image Image;
    public Button Button;

    public Weapon Weapon;

    public bool IsSelected;

    public void Hide()
    {
        Weapon = null;
        gameObject.SetActive(false);
    }

    public void Show(Weapon weapon)
    {
        Weapon = weapon;
        gameObject.SetActive(true);
        Image.sprite = weapon.WeaponIcon;
        if (!weapon.IsUnlocked())
        {
            Button.interactable = false;
        }
        else
        {
            Button.interactable = true;
        }
    }

    public void DeselectWeapon()
    {
        if (IsSelected)
        {
            SelectOrDeselectWeapon();
        }
    }

    private void Start()
    {
        Button.onClick.AddListener(() => SelectOrDeselectWeapon());
        Button.colors = ButtonColors.Instance.ButtonColorBlock;
        IsSelected = false;
    }

    private void SelectOrDeselectWeapon()
    {
        if (!IsSelected)
        {
            Debug.Log("Selecting weapon: " + Weapon.Name);
            WeaponSelector.SelectWeapon(Weapon);
            IsSelected = true;
            Button.colors = ButtonColors.Instance.ButtonSelectedColorBlock;
        }
        else
        {
            Debug.Log("Deselecting weapon: " + Weapon.Name);
            WeaponSelector.DeselectWeapon(Weapon);
            IsSelected = false;
            Button.colors = ButtonColors.Instance.ButtonColorBlock;
        }
    }
}