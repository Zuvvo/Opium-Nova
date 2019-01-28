using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    public List<LevelContainer> LevelContainers;
    public LevelsDatabase LevelsDB
    {
        get
        {
            if(_levelsDB == null)
            {
                _levelsDB = Resources.Load<LevelsDatabase>("ScriptableObjects/LevelsDatabase");
            }
            return _levelsDB;
        }
        private set
        {
            _levelsDB = value;
        }
    }
    public List<Map> Maps;

    private LevelHandler _currentLevelHandler;
    private Map _currentMap;
    private LevelsDatabase _levelsDB;

    public Map GetMap(int id)
    {
        for (int i = 0; i < Maps.Count; i++)
        {
            if(Maps[i].Id == id)
            {
                return Maps[i];
            }
        }
        Debug.LogWarning("Can't find map with id " + id);
        return null;
    }

    public LevelHandler GetCurrentLevelHandler()
    {
        return _currentLevelHandler;
    }

    public void InitLevelHandler(int id)
    {
        _currentLevelHandler = LevelsDB.GetLevelHandler(id);

        if (_currentLevelHandler != null)
        {
            _currentLevelHandler = Instantiate(_currentLevelHandler);
            _currentLevelHandler.Init(GetLevelData(id));
        }
    }

    public LevelContainer GetLevelContainer(int id)
    {
        for (int i = 0; i < LevelContainers.Count; i++)
        {
            if (LevelContainers[i].LevelData.Id == id)
            {
                return LevelContainers[i];
            }
        }
        Debug.LogWarning("Can't find level container with id " + id);
        return null;
    }

    private LevelData GetLevelData(int id)
    {
        for (int i = 0; i < LevelContainers.Count; i++)
        {
            if(LevelContainers[i].LevelData.Id == id)
            {
                return LevelContainers[i].LevelData;
            }
        }
        Debug.LogWarning("Can't find level with id " + id);
        return null;
    }


}