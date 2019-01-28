using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingResourcesUI : MonoBehaviour
{
    public List<Image> ResourceImages;
    public List<Text> ResourcesAmount;

    private List<PlayerResources> _playerResources;

    public void LoadResources() // trzeba by lepiej zrobić to wyciąganie, a nie po indeksie z listy
    {
        ResourceManager resourceManager = ResourceManager.Instance;
        _playerResources = StaticTagFinder.Player.PlayerData.PlayerResources;

        for (int i = 0; i < _playerResources.Count; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
            Resource resource = resourceManager.AvailableResources[i];
            ResourceImages[i].sprite = resource.Icon;
            ResourcesAmount[i].text = _playerResources[i].Amount.ToString();
        }
    }



}