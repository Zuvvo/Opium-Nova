using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabBehaviour : EnemyBehaviour
{
    public float FirstMoveTime = 1.5f;
    public bool MovedToSecondPoint;

    private bool _firstMoveStarted;
    private bool _movingPoint;
    private Vector3 _startRotation = new Vector3(0, 0, 90);

    private float _waypointMoveTimeMax = 0.5f;
    private float _waypointMoveTimeMin = 0.3f;

    private bool _rotated;

    protected override void Start()
    {
        transform.Rotate(_startRotation);
        base.Start();
        //   Debug.Log(string.Format("start pos: {0}, desired: {1}", transform.position, _transforms.CrabSecondMovePositions[3].Point.position));
        //   Debug.Log("rotation: " + Vector2.Angle(transform.position, _transforms.CrabSecondMovePositions[3].Point.position));
        // MoveToPoint(_transforms.CrabStartMovePoint, FirstMoveTime);
        MoveToSecondPoint();
        _firstMoveStarted = true;
    }

    protected override void Update()
    {
        base.Update();

        if(State == EnemyBehaviourState.Idle && !MovedToSecondPoint)
        {
            MoveToSecondPoint();
            //else if (_movedToSecondPoint && _firstMoveStarted && _idleTimer > IdleTime)
            //{
            //    TryToMove();
            //}
        }

        if(State == EnemyBehaviourState.Idle && MovedToSecondPoint && !_rotated)
        {
            transform.rotation = Quaternion.Euler(_startRotation);
            _rotated = true;
        }
    }

    public void TryToMove()
    {
        EnemyPoint pointToMove = _actualPoint.NextWaypoint;

        if (pointToMove != null)
        {
            MoveToNextPoint(pointToMove);
        }
    }

    private void MoveToSecondPoint()
    {
        EnemyPoint pointToMove = FindRandomNotOccupiedEnemyPoint(_transforms.CrabSecondMovePositions);
        float rotation = Vector2.Angle(transform.position, pointToMove.Point.transform.position);
        transform.Rotate(new Vector3(0,0,-rotation));

        if (pointToMove != null)
        {
            MoveToNextPoint(pointToMove);
        }

        MovedToSecondPoint = true;
    }

    private void MoveToNextPoint(EnemyPoint point)
    {
        _movingPoint = !_movingPoint;
        MoveToPoint(point, UnityEngine.Random.Range(_waypointMoveTimeMin, _waypointMoveTimeMax));
    }
}