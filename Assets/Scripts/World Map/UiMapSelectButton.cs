using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiMapSelectButton : MonoBehaviour
{
    public Button MapButton;
    public UiMenuWindowMapSelect WorldSelect;
    public Text Text;
    public int MapId;

    private Map _map;

    private void Start()
    {
        MapButton.onClick.RemoveAllListeners();
        _map = StaticTagFinder.LevelsManager.GetMap(MapId);
        MapButton.onClick.AddListener(() => WorldSelect.ShowMap(_map));
        if (_map != null)
        {
            Text.text = _map.MapName;
            MapButton.interactable = !_map.IsBlocked;
        }
        else
        {
            Text.text = "";
            MapButton.interactable = false;
        }
    }
}