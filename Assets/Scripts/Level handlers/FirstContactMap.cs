using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstContactMap : LevelHandler
{
    public float SpawnDelay = 0.3f;
    public float CrabsToSpawnAtOnce = 1;
    public float IdleTime = 5f;

    private List<CrabBehaviour> _crabBehaviours = new List<CrabBehaviour>();
    private int _count = 0;

    private float _idleTimer;
    private bool _waitingForMove;

    private void OnDestroy()
    {
        _onEnemyDied -= UnregisterBehaviour;
    }


    protected override void LevelInit()
    {
        SpawnBackground();
        StartMyNextWave();
        _onEnemyDied += UnregisterBehaviour;
    }

    private CrabBehaviour EnemyToCrabBehaviour(Enemy enemy)
    {
        return (CrabBehaviour)enemy.BehaviourScript;
    }

    protected override void StartWave()
    {
        Debug.Log("current wave: " + CurrentWave);
        SpawnEnemies();
    }

    public override void OnEnemyStateChanged(Enemy enemy, EnemyBehaviourState state) //UWAGA UWZGLEDNIAC SMIERC PRZY LICZENIU COUNT
    {
        CrabBehaviour crab = EnemyToCrabBehaviour(enemy);
        if(crab.MovedToSecondPoint && state == EnemyBehaviourState.Idle)
        {
            _count++;
            if (_count == _crabBehaviours.Count)
            {
                _count = 0;
                _waitingForMove = true;
            }
        }
    }

    protected override void SpawnEnemies()
    {
        AddEnemiesToSpawnList(CurrentWave);
        _spawning = true;
    }

    private void AddEnemiesToSpawnList(int currentWave)
    {
        for (int i = 0; i < CrabsToSpawnAtOnce; i++)
        {
            EnemyData enemyData = _levelData.Enemies[0];
            _enemiesToSpawn.Add(enemyData);
        }
    }

    private void Update()
    {
        if (_spawning)
        {
            _spawnTimer += Time.deltaTime;
            if(_spawnTimer >= SpawnDelay)
            {
                SpawnNextEnemy();
                _spawnTimer = 0;
            }
        }
        else if (_waitingForMove)
        {
            _idleTimer += Time.deltaTime;
            if(_idleTimer >= IdleTime)
            {
                _waitingForMove = false;
                _idleTimer = 0;
                MoveAll();
            }
        }
    }

    private void MoveAll()
    {
        for (int i = 0; i < _crabBehaviours.Count; i++)
        {
            _crabBehaviours[i].TryToMove();
        }
    }

    private void SpawnNextEnemy()
    {
        if(_enemiesToSpawn.Count > 0)
        {
            Vector3 spawnPoint = StaticTagFinder.EnemyTransforms.CrabSpawnPoint.Point.position;
            Enemy enemy = _enemyManager.InstantiateEnemyWithId(_enemiesToSpawn[0].Id, spawnPoint);
            _crabBehaviours.Add((CrabBehaviour)enemy.BehaviourScript);
            _spawnedEnemies.Add(enemy);
            _enemiesToSpawn.RemoveAt(0);
        }
        else
        {
            _spawning = false;
        }
    }

    private void UnregisterBehaviour(Enemy enemy)
    {
        Debug.Log("unregister");
        _crabBehaviours.Remove((CrabBehaviour)enemy.BehaviourScript);
        if(_crabBehaviours.Count == 0)
        {
            StartMyNextWave();
        }
    }
}
