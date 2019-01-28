using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipProgressBar : MonoBehaviour
{
    public Text LevelText;
    public Image BackgroundImage;
    public Image ProgressImage;

    private float _expForNextLevel;
    private float _expForThisLevel;

    public void SetProgressBar(int level, float experience)
    {
        _expForThisLevel = ExperienceConst.ExpNeededForLevel(level);
        _expForNextLevel = ExperienceConst.ExpNeededForLevel(level + 1);
        LevelText.text = string.Format("Level {0}\tExp: {1}/{2}",level, experience, (int)(_expForNextLevel-_expForThisLevel));
        ProgressImage.fillAmount = experience / _expForNextLevel;
    }
}