using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public LevelBackground Background;
    public LevelObstacles Obstacles;
    public int Id;
    public bool IsBlocked;
    public string MissionName;
    public string MissionDescription;
    public List<EnemyData> Enemies;
    public List<EnemyWave> EnemyWaves;
  //  public List<ResourceInfo> ResourcesToFind;
}