using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponObject : MonoBehaviour
{
    public bool FiringBlocked;
    public bool IsActive;
    public bool IsFiring;
    public Ammo ActiveAmmo;
    public Transform BulletSpawnPoint;

    private bool _isFiring;

    virtual public void FireAmmo(int amount)
    {
        ActiveAmmo.RemoveAmmo(amount);
    }
}