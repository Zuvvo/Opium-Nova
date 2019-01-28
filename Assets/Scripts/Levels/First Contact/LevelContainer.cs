using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelContainer : MonoBehaviour
{
    public LevelData LevelData;
    public LevelHandler LevelScriptHandler
    {
        get
        {
            return _levelHandler ?? StaticTagFinder.LevelsManager.LevelsDB.GetLevelHandler(LevelData.Id);
        }
    }

    private LevelHandler _levelHandler;
}