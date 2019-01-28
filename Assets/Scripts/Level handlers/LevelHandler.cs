using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelHandler : MonoBehaviour
{
    public int Id;
    public int CurrentWave = 0;

    protected LevelData _levelData;
    protected EnemyManager _enemyManager;
    protected LevelBackgroundMoveController _backgroundController;

    protected float _spawnTimer;
    protected bool _spawning;
    protected LevelBackground _background;
    protected LevelObstacles _obstacles;
    
    protected List<EnemyData> _enemiesToSpawn = new List<EnemyData>();
    protected List<Enemy> _spawnedEnemies = new List<Enemy>();
    protected event Action<Enemy> _onEnemyDied;

    protected void SpawnMyEnemies()
    {
        SpawnEnemies();
    }

    protected abstract void SpawnEnemies();

    protected abstract void StartWave();

    protected void StartMyNextWave()
    {
        CurrentWave++;
        StartWave();
    }

    public void Init(LevelData levelData)
    {
        _levelData = levelData;
        _enemyManager = StaticTagFinder.EnemyManager;
        _backgroundController = StaticTagFinder.LevelBackgroundMoveController;
        _enemyManager.Init();
        gameObject.name = string.Format("LEVEL HANDLER: {0}", _levelData.MissionName);
        LevelInit();
    }

    protected abstract void LevelInit();

    public void SpawnBackground()
    {
        _background = Instantiate(_levelData.Background);
        if(_levelData.Obstacles != null)
        {
            _obstacles = Instantiate(_levelData.Obstacles);
        }
        _backgroundController.Init(_background, _obstacles);
        _backgroundController.StartMoving();
    }

    public void ChangeStateOfEnemy(Enemy enemy, EnemyBehaviourState targetState)
    {
        enemy.BehaviourScript.State = targetState;
        OnEnemyStateChanged(enemy, targetState);
    }

    public abstract void OnEnemyStateChanged(Enemy enemy, EnemyBehaviourState state);

    public void UnregisterEnemy(Enemy enemy)
    {
        if (_spawnedEnemies.Contains(enemy))
        {
            if(_onEnemyDied != null)
            {
                _onEnemyDied(enemy);
            }
            _spawnedEnemies.Remove(enemy);
        }
        else
        {
            Debug.LogWarning("No such enemy in Level handler system.");
        }
    }
}