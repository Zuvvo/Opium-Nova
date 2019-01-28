using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaBullet : Bullet
{
    private float timer = 0;


    private void Start()
    {
        SetRandomAngle();
    }
    private void Update()
    {

    }

    private void SetRandomAngle()
    {
        transform.Rotate(new Vector3(0, 0, UnityEngine.Random.Range(0, 360)));
    }
}