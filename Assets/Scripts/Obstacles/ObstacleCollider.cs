using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollider : MonoBehaviour
{
    public Obstacle Obstacle;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Obstacle.Collision(collision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Obstacle.TriggerEnter(collision);
    }
}
