using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon
{
    public string Name;
    public bool SetForGame;
    public int Id;
    public float FireSpeed;
    public float FireDelay;
    public float BulletLifeLength;
    public Sprite WeaponIcon;
    public WeaponObject WeaponPrefab;
    public List<Ammo> Ammo;
    public Ammo CurrentAmmo;
    public Ammo ActiveBullet;
    public WeaponObject WeaponObject;

    public int[] AvailableAmmoIds;
    public bool IsUnlocked()
    {
        List<UnlockedWeapon> unlockedWeapons = StaticTagFinder.Player.PlayerData.UnlockedWeapons;
        for (int i = 0; i < unlockedWeapons.Count; i++)
        {
            if (unlockedWeapons[i].ID == Id)
            {
                return unlockedWeapons[i].Unlocked;
            }
        }
        Debug.LogWarning("Can't find this weapon!");
        return false;
    }
    
    public Weapon CloneWeapon()
    {
        return new Weapon()
        {
            Name = Name,
            SetForGame = SetForGame,
            Id = Id,
            FireSpeed = FireSpeed,
            FireDelay = FireDelay,
            BulletLifeLength = BulletLifeLength,
            WeaponIcon = WeaponIcon,
            WeaponObject = WeaponObject,
            AvailableAmmoIds = (int[])AvailableAmmoIds.Clone()
        };
    }

    public bool HaveAmmo(Ammo ammo)
    {
        for (int i = 0; i < Ammo.Count; i++)
        {
            if(Ammo[i] == ammo)
            {
                return true;
            }
        }
        return false;
    }

    public List<Ammo> AvailableAmmoForWeapon()
    {
        List<Ammo> output = new List<Ammo>();
        GameManager GM = StaticTagFinder.GameManager;
        for (int i = 0; i < AvailableAmmoIds.Length; i++)
        {
            output.Add(GM.GetAmmoByID(AvailableAmmoIds[i]));
        }
        return output;
    }
}
