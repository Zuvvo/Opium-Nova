using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiMenuWindowMapSelect : UiMenuWindowBase
{
    public UiMenuWindowLevelSelect UiMapNavigation;
    public GameObject WorldMapPanel;

    public GameObject SelectedGearPanel;

    public UiMenuWindowGearSelect GearSelect;

    public static Map SelectedMap;

    public GameObject HidingPanel;
    public GameObject MissionsPanel;

    private List<Map> _maps;
    private bool _missionsShown = false;

    public void StartMissionButton()
    {
        StaticTagFinder.GameManager.StartGame();
    }

    public void ShowMap(Map map)
    {
        SelectedMap = map;
        UiMapNavigation.LoadMap(map);
        SelectedGearPanel.SetActive(false);
    }

    public void ShowOrHideMissions(Map map)
    {
        _missionsShown = !_missionsShown;
        HidingPanel.SetActive(_missionsShown);
        MissionsPanel.SetActive(_missionsShown);
    }

    public void OpenGearSelect()
    {
        StaticTagFinder.UiMenuWindowManager.GearSelect.OpenWindow();
    }

    public void HideMap(Map map)
    {
      //  map.HideMap(); // wylaczone
        SelectedGearPanel.SetActive(false);
    }

    public void CloseWorldMap()
    {
        WorldMapPanel.SetActive(false);
        SelectedGearPanel.SetActive(false);
    }

    private void Init()
    {
        _maps = StaticTagFinder.LevelsManager.Maps;
        SelectedGearPanel.SetActive(false);
        WorldMapPanel.SetActive(true);
    }

    protected override void CloseThisWindow()
    {

    }

    protected override void OpenThisWindow()
    {

    }
}