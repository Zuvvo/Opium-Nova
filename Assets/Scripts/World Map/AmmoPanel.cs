using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoPanel : MonoBehaviour
{
    public bool IsSelected;
    public Ammo Ammo;
    public Button Button;
    public Image AmmoImage;
    public Text AmountTaken;

    public Image SelectBackgroundImage;
    public Image SelectImage;
    

    public UiMenuWindowGearSelect GearSelect;
    public Slider Slider;

    public GameObject BlockingPanel;

    private Sprite _selectedBackgroundSprite;
    private Sprite _selectedSprite;
    private Sprite _notSelectedBackgroundSprite;
    private Sprite _notSelectedSprite;

    public delegate void GearSelection();
    public static event GearSelection OnAmmoSelected;

    public void SetAmmoPanel(Ammo ammo)
    {
        Ammo = ammo;
        gameObject.SetActive(true);
        AmmoImage.sprite = ammo.Icon;
        AmountTaken.text = string.Format("{0}/{1}", ammo.Amount, ammo.MaxAmount);
        if (ammo.SetForGame)
        {
            SelectAmmo();
        }
        else
        {
            DeselectAmmo();
        }
        if (StaticTagFinder.Player.PlayerSelects.AmmoLinkedToSelectedWeapons().Contains(ammo))
        {
            BlockingPanel.SetActive(false);
        }
        else
        {
            BlockingPanel.SetActive(true);
        }
        SetSlider();
    }

    private void SetSlider()
    {
            Slider.minValue = 0;
            Slider.maxValue = Ammo.MaxAmount;
    }

    public void SetButtonSprites()
    {
        _selectedBackgroundSprite = ButtonColors.Instance.SelectedBackgroundSprite;
        _selectedSprite = ButtonColors.Instance.SelectedSprite;
        _notSelectedBackgroundSprite = ButtonColors.Instance.NotSelectedBackgroundSprite;
        _notSelectedSprite = ButtonColors.Instance.NotSelectedSprite;
    }
    
    public void DeselectAmmo()
    {
        Debug.Log("deselect");
        IsSelected = false;
        SelectImage.sprite = _notSelectedSprite;
        SelectBackgroundImage.sprite = _notSelectedBackgroundSprite;
        Ammo.SetForGame = false;
        Slider.value = 0;
    }

    public void SelectAmmo()
    {
        Debug.Log("select");
        IsSelected = true;
        SelectImage.sprite = _selectedSprite;
        SelectBackgroundImage.sprite = _selectedBackgroundSprite;
        Ammo.SetForGame = true;
    }

    public void RecalculateAmmo()
    {
        int sliderValue = (int)Slider.value;
        Ammo.SetAmount(sliderValue);
        AmountTaken.text = string.Format("{0}/{1}", Ammo.Amount, Ammo.MaxAmount);
        SetButtonsInteractable();
        if(sliderValue == 0)
        {
            DeselectAmmo();
        }
        else if (!IsSelected)
        {
            SelectAmmo();
        }
    }
    private void SetButtonsInteractable()
    {
        GearSelect.SetStartButtonInteractable(StaticTagFinder.Player.PlayerSelects.IsAbleToStartGame());
    }

    private bool AnyAmmoAmountTaken()
    {
        for (int i = 0; i < GearSelect.AmmoPanels.Count; i++)
        {
            if(GearSelect.AmmoPanels[i].Slider.value > 0)
            {
                return true;
            }
        }
        return false;
    }

    private bool IsAnyAmmoTaken()
    {
        for (int i = 0; i < GearSelect.AmmoPanels.Count; i++)
        {
            if (GearSelect.AmmoPanels[i].IsSelected)
            {
                return true;
            }
        }
        return false;
    }

    private void Start()
    {
        Button.onClick.AddListener(() => ButtonClick());
        SetButtonSprites();
    }

    private void ButtonClick()
    {
        if (!IsSelected)
        {
            SelectAmmo();
        }
        else
        {
            DeselectAmmo();
        }

    }

    private void BlockOption()
    {
        BlockingPanel.SetActive(true);
    }
    private void UnblockOption()
    {
        BlockingPanel.SetActive(false);
    }
}