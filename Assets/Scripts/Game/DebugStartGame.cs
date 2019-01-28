using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugStartGame : MonoBehaviour
{
    public bool DebugMode = true;
    public GameManager GameManagerPrefab;
    public GameLoader GameLoader;
    public int level = 0;

    private Player _player;
    private GameManager _gameManager;

    private void Awake()
    {
        if (DebugMode)
        {
            _gameManager = Instantiate(GameManagerPrefab);
            _gameManager.Init(true);
            _player = StaticTagFinder.Player;
            _player.PlayerSelects.SelectLevel(level);
            GameLoader.Init(_player);
        }
    }
}