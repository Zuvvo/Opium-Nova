using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Id;
    public EnemyBehaviour BehaviourScript;

    private EnemyData _data;
    private float _health;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            ChangeHealth(-bullet.Ammo.Damage);
            Destroy(bullet.gameObject);
        }
    }

    private void OnDestroy()
    {
        // używaj Kill();
    }


    public void InitData(EnemyData data)
    {
        _health = data.Hp;
        _data = data;
        if(BehaviourScript != null)
        {
            BehaviourScript.InitData(data);
        }
        else
        {
            Debug.LogWarning("niepodczepiony EnemyBehaviour.cs");
        }
    }

    public void ChangeHealth(float change)
    {
        _health += change;
        if(_health <= 0)
        {
            Kill();
        }
        Debug.Log("Hp: " + _health);
    }

    public void Kill()
    {
        StaticTagFinder.LevelsManager.GetCurrentLevelHandler().UnregisterEnemy(this);
        BehaviourScript.StopOccupyingActualPoint();
        Debug.Log("Destroy");
        Destroy(gameObject);
    }
}