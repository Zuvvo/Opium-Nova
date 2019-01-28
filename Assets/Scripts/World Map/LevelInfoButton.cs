using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelInfoButton : MonoBehaviour
{
    public UiMenuWindowLevelSelect LevelInfoPanel;
    public Button Button;
    public LevelContainer LevelContainer;

    private Ship _ship;

    private void Start()
    {
        Button.onClick.AddListener(() => SelectLevel());
        _ship = StaticTagFinder.Player.PlayerSelects.SelectedShip;
    }

    public void SelectLevel()
    {
        Debug.Log("selecting level");
        StaticTagFinder.UiMapNavigation.LoadLevelData(LevelContainer.LevelData);
     //   LevelInfoPanel.LoadLevelData(LevelContainer.LevelData);
    }

    public void BlockLevel()
    {
        Debug.Log("blocking level");
        Button.interactable = false;
    }

    public void UnblockLevel()
    {
        Debug.Log("unblocking level");
        Button.interactable = true;
    }
}