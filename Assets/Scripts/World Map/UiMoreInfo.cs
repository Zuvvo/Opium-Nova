using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiMoreInfo : MonoBehaviour
{
    public Text Name;
    public Image Image;
    public Text Statistics;

    public Button Back;


    public void Init(EnemyData enemyInfo)
    {
        gameObject.SetActive(true);
        Name.text = enemyInfo.Name;
        Image.sprite = enemyInfo.Sprite;
        Statistics.text = enemyInfo.Statistics;
    }

    public void Init(Resource resource)
    {
        gameObject.SetActive(true);
        Image.sprite = resource.Icon;
        Statistics.text = string.Empty;
        Name.text = resource.Name;
    }

    public void BackButton()
    {
        gameObject.SetActive(false);
        StaticTagFinder.UiMapNavigation.LoadSelectedLevel();
    }


}