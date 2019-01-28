using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehaviour : MonoBehaviour
{
    public Enemy Enemy;
    public EnemyBehaviourState State
    {
        get
        {
            return _state;
        }
        set
        {
            _state = value;
            if(_currentLevelHandler != null)
            {
                _currentLevelHandler.OnEnemyStateChanged(Enemy, _state);
            }
        }
    }

    private EnemyBehaviourState _state;

    protected EnemyData _enemyData;
    protected EnemyTransforms _transforms;
    protected Enemy _enemy;
    
    protected float _idleTimer = 0;
    protected float _moveTimer = 0;
    protected EnemyPoint _actualPoint;

    protected float _timeToMove;
    protected Vector3 _startMovePosition;
    protected Vector3 _endMovePosition;
    protected LevelHandler _currentLevelHandler;

    protected virtual void Start()
    {
        _transforms = StaticTagFinder.EnemyTransforms;
        _currentLevelHandler = StaticTagFinder.LevelsManager.GetCurrentLevelHandler();
    }

    public void InitData(EnemyData data)
    {
        _enemyData = data;
    }

    protected void MoveToPoint(EnemyPoint point, float time)
    {
        _endMovePosition = point.Point.position;
        point.OccupyPoint(this);
        StopOccupyingActualPoint();
        _actualPoint = point;
        _startMovePosition = transform.position;
        _currentLevelHandler.ChangeStateOfEnemy(Enemy, EnemyBehaviourState.Idle);
        State = EnemyBehaviourState.MovingToPoint;
        _timeToMove = time;
        _idleTimer = 0;
        _moveTimer = 0;
    }

    protected virtual void Update()
    {
        if (State == EnemyBehaviourState.MovingToPoint)
        {
            _moveTimer += Time.deltaTime;
            transform.position = Vector3.Lerp(_startMovePosition, _endMovePosition, _moveTimer / _timeToMove);
            if(_moveTimer >= _timeToMove)
            {
                StopMoving();
            }
        }
        else if(State == EnemyBehaviourState.Idle)
        {
            _idleTimer += Time.deltaTime;
        }
    }

    private void StopMoving()
    {
        State = EnemyBehaviourState.Idle;

    }

    protected EnemyPoint FindRandomNotOccupiedEnemyPoint(EnemyPoint[] points)
    {
        EnemyPoint result = null;
        EnemyPoint[] shuffledPoints = new EnemyPoint[points.Length];
        points.CopyTo(shuffledPoints, 0);
        new System.Random().Shuffle(shuffledPoints);
        for (int i = 0; i < shuffledPoints.Length; i++)
        {
            if (!shuffledPoints[i].IsOccupied)
            {
                return shuffledPoints[i];
            }
        }
        return result;
    }

    public void StopOccupyingActualPoint()
    {
        if (_actualPoint != null)
        {
            _actualPoint.StopOccupying();
        }
    }
}