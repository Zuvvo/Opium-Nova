using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingAmmo : MonoBehaviour
{
    public List<Image> InputImages;
    public List<Text> InputText;

    public List<Button> AvailableAmmoButtons;
    public List<Image> AvailableAmmoImages;
    public List<GameObject> AvilableAmmoBlockPanels;

    public Color NotSelectedColor;
    public Color SelectedColor;

    public Text MinAmount;
    public Text MaxAmount;
    public Text Amount;
    public Text AmountOnAmmoImage;

    public Slider Slider;
    
    public Image OutputImage;
    public Crafting Crafting;

    public GameObject NotEnoughResourcesPanel;

    private Ammo _selectedAmmo;
    private GameManager _GM;
    private RecipeManager _recipeManager;
    private Player _player;
    private List<PlayerResources> _playerResources;
    [SerializeField]
    private AmmoRecipe _currentAmmoRecipe;
    private ResourceManager _resourceManager;

    private int _minValue;
    private int _maxValue;

    private bool _crafting = false;
    private float _craftingProgress = 0;
    private float _craftingTime = 5;

    [SerializeField]
    private int _amountToCraft;
    private int _amountToCraftSum;
    public void Init()
    {
        gameObject.SetActive(true);
        _GM = StaticTagFinder.GameManager;
        _resourceManager = ResourceManager.Instance;
        _recipeManager = StaticTagFinder.RecipeManager;
        _player = StaticTagFinder.Player;
        _playerResources = _player.PlayerData.PlayerResources;
        LoadRecipesInPanel();
        LoadRecipeButton(0); // load first recipe
    }

    public void LoadRecipeButton(int index)
    {
        if (_GM.AvailableAmmo[index].IsUnlocked())
        {
            LoadRecipe(_recipeManager.AmmoRecipes[index]);
        }
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void LoadRecipe(AmmoRecipe ammoRecipe)
    {
        Debug.Log("loading recipe...");
        _currentAmmoRecipe = ammoRecipe;
        _selectedAmmo = _GM.GetAmmoByID(_currentAmmoRecipe.OutputID);
        LoadInput();
        LoadOutput();
        SetAmmoButtonColors();
        SetSlider();
    }

    private void LoadRecipesInPanel()
    {
        for (int i = 0; i < AvailableAmmoButtons.Count; i++)
        {
            AvailableAmmoButtons[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < _GM.AvailableAmmo.Count; i++)
        {
            AvailableAmmoButtons[i].gameObject.SetActive(true);
            AvailableAmmoImages[i].sprite = _GM.AvailableAmmo[i].Icon;

            if (!_GM.AvailableAmmo[i].IsUnlocked())
            {
                AvilableAmmoBlockPanels[i].SetActive(true);
            }
            else
            {
                AvilableAmmoBlockPanels[i].SetActive(false);
            }
        }
    }

    private void SetAmmoButtonColors()
    {
        for (int i = 0; i < _GM.AvailableAmmo.Count; i++)
        {
            if(_selectedAmmo.Id == _GM.AvailableAmmo[i].Id)
            {
                AvailableAmmoButtons[i].image.color = SelectedColor;
            }
            else
            {
                AvailableAmmoButtons[i].image.color = NotSelectedColor;
            }
        }
    }

    private void LoadOutput()
    {
        OutputImage.sprite = _selectedAmmo.Icon;
        AmountOnAmmoImage.text = _amountToCraftSum.ToString();
    }

    private void LoadInput()
    {
        Debug.Log("loading input");
        for (int i = 0; i < InputImages.Count; i++)
        {
            InputImages[i].transform.parent.gameObject.SetActive(false);
        }

        for (int i = 0; i < _currentAmmoRecipe.Ingredients.Count; i++)
        {
            int id = _currentAmmoRecipe.Ingredients[i].ID;
            int amount = _currentAmmoRecipe.Ingredients[i].Amount;
            Resource resource = _resourceManager.AvailableResources[id - 1];
            InputImages[i].transform.parent.gameObject.SetActive(true);
            InputImages[i].sprite = resource.Icon;
            InputText[i].text = (_currentAmmoRecipe.Ingredients[i].Amount * _amountToCraft).ToString();
        }
    }

    private void SetSlider()
    {
        _minValue = 1;
        _maxValue = CalculateMaxValue();
        if (_maxValue >= 1)
        {
            NotEnoughResourcesPanel.SetActive(false);
            Slider.minValue = _minValue;
            Slider.maxValue = _maxValue;
            MinAmount.text = _minValue.ToString();
            MaxAmount.text = _maxValue.ToString();
        }
        else
        {
            NotEnoughResourcesPanel.SetActive(true);
            ResetSlider();
        }

    }
    public void CraftAmmo()
    {
        if (!_crafting)
        {
            Crafting.ProgressBar.gameObject.SetActive(true);
            _craftingProgress = 0;
            _crafting = true;
            SetButtonsInteractable(false);
        }
    }

    private void SetButtonsInteractable(bool value)
    {
        Slider.interactable = value;
        Crafting.WeaponsOption.interactable = value;
        Crafting.AmmoOption.interactable = value;
        for (int i = 0; i < AvailableAmmoButtons.Count; i++)
        {
            AvailableAmmoButtons[i].interactable = value;
        }
    }

    private void Update()
    {
        if (_crafting)
        {
            _craftingProgress += Time.deltaTime;
            Crafting.ProgressBar.Fill(_craftingProgress/_craftingTime);
            if (_craftingProgress >= _craftingTime)
            {
                FinishCrafting();
            }
        }
    }

    private void FinishCrafting() // uwaga, bierze z selected ammo (trzeba poblokować panel albo z czego innego pobrać)
    {
        Crafting.ProgressBar.Percent.text = "100%";
        Crafting.ProgressBar.gameObject.SetActive(false);
        _crafting = false;
        SetButtonsInteractable(true);
        Crafting.CraftingCommunique.SetCommunique(string.Format("You have crafted {0} {1}.", _amountToCraftSum, _selectedAmmo.Name));
        _player.GetUnlockedAmmoByID(_selectedAmmo.Id).Amount += _amountToCraftSum;
        RemoveCraftingResources();
        LoadRecipe(_currentAmmoRecipe);
        SetSlider();
        Crafting.CraftingResourcesUI.LoadResources();
        Slider.value = 1;
    }

    private void RemoveCraftingResources()
    {
        for (int i = 0; i < _currentAmmoRecipe.Ingredients.Count; i++)
        {
            _playerResources[i].Amount -= _currentAmmoRecipe.Ingredients[i].Amount * _amountToCraft;
        }
    }

    public void Recalculate()
    {
        _amountToCraft = (int)Slider.value;
        _amountToCraftSum = _amountToCraft * _currentAmmoRecipe.OutputAmount;
        Amount.text = string.Format("{0} x {1} = {2} bullets", _amountToCraft, _currentAmmoRecipe.OutputAmount, _amountToCraftSum);
        AmountOnAmmoImage.text = _amountToCraftSum.ToString();
        if (_currentAmmoRecipe == null)
        {
            return;
        }

        for (int i = 0; i < _currentAmmoRecipe.Ingredients.Count; i++)
        {
            InputText[i].text = (_currentAmmoRecipe.Ingredients[i].Amount * _amountToCraft).ToString();
        }
    }

    private void ResetSlider()
    {
        Slider.value = 1;
        Recalculate();
    }

    private int CalculateMaxValue()
    {
        int max = _playerResources[0].Amount / _currentAmmoRecipe.Ingredients[0].Amount;

        for (int i = 1; i < _currentAmmoRecipe.Ingredients.Count; i++)
        {
            int amount = _playerResources[i].Amount / _currentAmmoRecipe.Ingredients[i].Amount;
            if (amount < max)
            {
                max = amount;
            }
        }
        return max;
    }
}