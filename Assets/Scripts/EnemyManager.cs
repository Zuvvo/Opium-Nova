using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<EnemyData> EnemyData;
    public List<Enemy> EnemyPrefabs;

    private bool _dicInitialized;
    [SerializeField]
    private Dictionary<int, Enemy> _enemyPrefabsDic = new Dictionary<int, Enemy>();
    [SerializeField]
    private Dictionary<int, Enemy> _registredEnemies = new Dictionary<int, Enemy>();

    public void Init()
    {
        if (!_dicInitialized)
        {
            InitDic();
        }
    }

    public Enemy GetEnemyById(int id)
    {
        for (int i = 0; i < EnemyPrefabs.Count; i++)
        {
            if(EnemyPrefabs[i].Id == id)
            {
                return EnemyPrefabs[i];
            }
        }
        Debug.LogWarning(string.Format("Can't find enemy with id {0}", id));
        return null;
    }

    public EnemyData GetEnemyData(int id)
    {
        for (int i = 0; i < EnemyData.Count; i++)
        {
            if(EnemyData[i].Id == id)
            {
                return EnemyData[i];
            }
        }
        Debug.LogWarning(string.Format("Can't find enemy with id {0}", id));
        return null;
    }

    private void InitDic()
    {
        for (int i = 0; i < EnemyPrefabs.Count; i++)
        {
            _enemyPrefabsDic.Add(EnemyPrefabs[i].Id, EnemyPrefabs[i]);
        }
        _dicInitialized = true;
    }

    public Enemy InstantiateEnemyWithId(int id, Vector3 spawnPoint)
    {
        Enemy enemy = Instantiate(_enemyPrefabsDic[id], spawnPoint, Quaternion.identity);
        enemy.InitData(GetEnemyData(enemy.Id));
        _registredEnemies.Add(enemy.GetInstanceID(), enemy);
        return enemy;
    }

    public void DestroyEnemy(int instanceId)
    {
        if (_registredEnemies.ContainsKey(instanceId))
        {
            Destroy(_registredEnemies[instanceId].gameObject);
            _registredEnemies.Remove(instanceId);
        }
        else
        {
            Debug.LogWarning("No enemy with instanceID: " + instanceId);
        }
    }
}