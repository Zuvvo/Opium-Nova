using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelsDatabase", menuName = "DB/LevelsDB")]
public class LevelsDatabase : ScriptableObject
{
    public List<LevelHandler> LevelScripts;

    public LevelHandler GetLevelHandler(int id)
    {
        for (int i = 0; i < LevelScripts.Count; i++)
        {
            if(LevelScripts[i].Id == id)
            {
                return LevelScripts[i];
            }
        }
        Debug.LogWarning("Can't find level with id " + id);
        return null;
    }
}