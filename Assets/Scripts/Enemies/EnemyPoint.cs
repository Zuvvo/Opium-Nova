using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoint : MonoBehaviour
{
    public Transform Point;
    public EnemyPoint NextWaypoint;
    public bool IsOccupied;
    private EnemyBehaviour _enemyBehaviour;

    private void Start()
    {
        if(Point == null)
        {
            Debug.LogWarning("Niepodpiety Transform na EnemyPoint");
            Point = transform;
        }
    }

    public void OccupyPoint(EnemyBehaviour enemyBehaviour)
    {
        _enemyBehaviour = enemyBehaviour;
        IsOccupied = true;
    }

    public void StopOccupying()
    {
        _enemyBehaviour = null;
        IsOccupied = false;
    }
}