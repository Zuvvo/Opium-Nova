using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    public int Id;
    public Sprite BackgroundImage;
    public bool IsBlocked;
    public string MapName;
    
   // public List<LevelInfoButton> LevelInfoButtons;

    public List<LevelContainer> Missions;


    //public void ShowMap()
    //{
    //    gameObject.SetActive(true);
    //    LevelInfoButtons[0].SelectLevel();
    //    UnlockMaps();
    //}

    //public void HideMap()
    //{
    //    gameObject.SetActive(false);
    //}

    //private void UnlockMaps()
    //{
    //    Player player = Player.Instance;
    //    for (int i = 0; i < LevelInfoButtons.Count; i++)
    //    {
    //        if (player.IsLevelUnlocked(LevelInfoButtons[i].LevelContainer.LevelInfo))
    //        {
    //            LevelInfoButtons[i].UnblockLevel();
    //        }
    //        else
    //        {
    //            LevelInfoButtons[i].BlockLevel();
    //        }
    //    }
    //}

}