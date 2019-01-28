using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticTagFinder : MonoBehaviour
{
    private static ArrayList arList = new ArrayList();

    private static UiManager _uiManager;
    public static UiManager UiManager
    {
        get
        {
            return _uiManager ?? (_uiManager = FindObjectOfType<UiManager>());
        }
    }

    private static UiMainMenu _uiMainMenu;
    public static UiMainMenu UiMainMenu
    {
        get
        {
            return _uiMainMenu ?? (_uiMainMenu = FindObjectOfType<UiMainMenu>());
        }
    }

    private static UiMenuWindowMapSelect _worldSelect;
    public static UiMenuWindowMapSelect WorldSelect
    {
        get
        {
            return _worldSelect ?? (_worldSelect = FindObjectOfType<UiMenuWindowMapSelect>());
        }
    }

    private static UiMenuWindowLevelSelect _uiMapNavigation;
    public static UiMenuWindowLevelSelect UiMapNavigation
    {
        get
        {
            return _uiMapNavigation ?? (_uiMapNavigation = FindObjectOfType<UiMenuWindowLevelSelect>());
        }
    }

    private static GameManager _gameManager;
    public static GameManager GameManager
    {
        get
        {
            return _gameManager ?? (_gameManager = FindObjectOfType<GameManager>());
        }
    }

    private static RecipeManager _recipeManager;
    public static RecipeManager RecipeManager
    {
        get
        {
            return _recipeManager ?? (_recipeManager = FindObjectOfType<RecipeManager>());
        }
    }

    private static LevelsManager _levelsManager;
    public static LevelsManager LevelsManager
    {
        get
        {
            return _levelsManager ?? (_levelsManager = FindObjectOfType<LevelsManager>());
        }
    }

    private static Player _player;
    public static Player Player
    {
        get
        {
            return _player ?? (_player = FindObjectOfType<Player>());
        }
    }

    private static EnemyManager _enemyManager;
    public static EnemyManager EnemyManager
    {
        get
        {
            return _enemyManager ?? (_enemyManager = FindObjectOfType<EnemyManager>());
        }
    }

    private static LevelBackgroundMoveController _levelBackgroundMoveController;
    public static LevelBackgroundMoveController LevelBackgroundMoveController
    {
        get
        {
            return _levelBackgroundMoveController ?? (_levelBackgroundMoveController = FindObjectOfType<LevelBackgroundMoveController>());
        }
    }

    private static GameUI _gameUI;
    public static GameUI GameUI
    {
        get
        {
            return _gameUI ?? (_gameUI = FindObjectOfType<GameUI>());
        }
    }

    private static EffectsController _effectsController;
    public static EffectsController EffectsController
    {
        get
        {
            return _effectsController ?? (_effectsController = FindObjectOfType<EffectsController>());
        }
    }

    private static EnemyTransforms _enemyTransforms;
    public static EnemyTransforms EnemyTransforms
    {
        get
        {
            return _enemyTransforms ?? (_enemyTransforms = FindObjectOfType<EnemyTransforms>());
        }
    }


    private static UiMenuWindowManager _uiMenuWindowManager;
    public static UiMenuWindowManager UiMenuWindowManager
    {
        get
        {
            return _uiMenuWindowManager ?? (_uiMenuWindowManager = FindObjectOfType<UiMenuWindowManager>());
        }
    }

    private static MoveController _moveController;
    public static MoveController MoveController
    {
        get
        {
            return _moveController ?? (_moveController = FindObjectOfType<MoveController>());
        }
    }

    private static CoroutineHeleper _coroutineHeleper;
    public static CoroutineHeleper CoroutineHeleper
    {
        get
        {
            return _coroutineHeleper ?? (_coroutineHeleper = FindObjectOfType<CoroutineHeleper>());
        }
    }

    private static SpeechDatabase _speechDB;
    public static SpeechDatabase SpeechDB
    {
        get
        {
            if (_speechDB == null)
            {
                _speechDB = Resources.Load<SpeechDatabase>("ScriptableObjects/SpeechDatabase");
            }
            return _speechDB;
        }
    }


    public static void NullCoroutineHelper()
    {
        _coroutineHeleper = null;
    }

    public static void NullAllStatics()
    {
        _uiManager = null;
        _uiMainMenu = null;
        _worldSelect = null;
        _uiMapNavigation = null;
        _gameManager = null;
        _recipeManager = null;
        _enemyManager = null;
        _levelBackgroundMoveController = null;
        _gameUI = null;
        _moveController = null;
    }

    private void OnLevelWasLoaded(int level)
    {
        NullAllStatics();
    }



    // testowo
    public static object GetObj(Type type)
    {
        var obj = FindObjectOfType(type);
        return obj;
    }

    public static T GetT<T>()
    {
        Type typeParam = typeof(T);
        UnityEngine.Object obj = FindObjectOfType(typeParam);
        for (int i = 0; i < arList.Count; i++)
        {
            if (arList[i].GetType() == typeof(T))
            {
                return (T)Convert.ChangeType(arList[i], typeof(T));
            }
        }
        arList.Add(obj);
        return (T)Convert.ChangeType(obj, typeof(T));
    }
}