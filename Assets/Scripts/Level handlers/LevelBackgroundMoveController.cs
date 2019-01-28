using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBackgroundMoveController : MonoBehaviour
{
    private float _scrollSpeed = 1;
    private float _tileSizeY = 42;

    private Vector3 _backgroundStartPosition;
    private Vector3 _obstaclesStartPosition;
    private LevelBackground _background;
    private LevelObstacles _obstacles;
    private bool _moving;

    public Dictionary<int, LevelBackground> Dict = new Dictionary<int, LevelBackground>();

    public void Init(LevelBackground background, LevelObstacles obstacles)
    {
        _obstacles = obstacles;
        _background = background;
        _backgroundStartPosition = _background.transform.position;
        _obstaclesStartPosition = obstacles.transform.position;
    }

    public void StartMoving()
    {
        _moving = true;
    }

    public void Stop()
    {
        _moving = false;
    }

    private void Update()
    {
        if (_moving)
        {
            float newPosition = Mathf.Repeat(Time.time * _scrollSpeed, _tileSizeY);
            _background.transform.position = _backgroundStartPosition + Vector3.left * newPosition;
            _obstacles.transform.position = _obstaclesStartPosition + Vector3.left * (Time.time * _scrollSpeed);
        }
    }
}