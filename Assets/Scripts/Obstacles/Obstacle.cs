using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int Damage = 30;
    public float Health = 100;

    private ShipObject _ship;

    public void Collision(Collision2D collision)
    {
        _ship = collision.collider.GetComponent<ShipObject>();

        if(_ship != null)
        {
            _ship.ChangeHealth(-Damage);
            Demolish();
        }
    }

    public void TriggerEnter(Collider2D collision)
    {
        if (collision.tag == CollisionTag.Bullet)
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            ChangeHealth(-bullet.Ammo.Damage);
            Destroy(bullet.gameObject);
        }
    }

    public void ChangeHealth(float change)
    {
        Health += change;
        if (Health <= 0)
        {
            Demolish();
        }
        Debug.Log("Hp: " + Health);
    }

    private void Demolish()
    {
        Destroy(gameObject);
    }
}
