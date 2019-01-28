using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiShipBars : MonoBehaviour
{
    public Image HealthBar;
    public Image ArmorBar;
    public Image ExperienceBar;
    public Text Level;
    public Text ArmorPercent;
    public Text ExpPercent;
    public Text HealthPercent;

    public void Init(ShipObject shipObject)
    {
        shipObject.OnStateChanged += Refresh;
    }

    public void Refresh(PlayerGameData playerGameData)
    {
        float healthPercent = (float)(playerGameData.Health) / (float)(playerGameData.ShipRef.MaxHealth);
        float armorPercent = (float)(playerGameData.Armor) / (float)(playerGameData.ShipRef.MaxArmor);
        float expPercent = (float)(ExperienceConst.ExpNeededForLevel(playerGameData.Level)) / (float)(ExperienceConst.ExpNeededForLevel(playerGameData.Level + 1));

        ArmorPercent.text = string.Format("{0}%", (int)(armorPercent * 100));
        HealthPercent.text = string.Format("{0}%", (int)(healthPercent * 100));
        ExpPercent.text = string.Format("{0}%", (int)(expPercent * 100));

        Level.text = string.Format("Ship level {0}", playerGameData.Level);

        HealthBar.fillAmount = healthPercent;
        ArmorBar.fillAmount = armorPercent;
        ExperienceBar.fillAmount = expPercent;
    }
}