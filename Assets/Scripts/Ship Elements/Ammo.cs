using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ammo
{
    public string Name;
    public bool SetForGame;
    public bool InifiteAmmo;
    public int Amount;
    public int MaxAmount;

    public int SetAmountForGame;

    public int Id;
    public float Speed;
    public float Damage;
    public BulletType BulletType;
    public GameObject BulletPrefab;
    public Sprite Icon;
    public string Info
    {
        get
        {
            return string.Format("Speed: {0} \nDamage: {1} \n Bullet Type: {2}", Speed, Damage, BulletType);
        }
    }

    public event Action<int> OnAmmoConsumed;

    private List<UnlockedAmmo> _unlockedAmmo;

    public Ammo CloneAmmo()
    {
        return new Ammo()
        {
            Name = Name,
            SetForGame = SetForGame,
            InifiteAmmo = InifiteAmmo,
            Id = Id,
            Speed = Speed,
            Damage = Damage,
            BulletType = BulletType,
            BulletPrefab = BulletPrefab,
            Icon = Icon,
            _unlockedAmmo = _unlockedAmmo
        };
    }

    public void RemoveAmmo(int amount)
    {
        Amount = Amount - 1;
        MaxAmount = MaxAmount - 1;
        //if(OnAmmoConsumed != null)
        //{
        //    OnAmmoConsumed(amount);
        //}
    }

    public void SetAmount(int amount)
    {
        Amount = amount;
    }

    public bool IsUnlocked()
    {
        List<UnlockedAmmo> unlockedAmmo = StaticTagFinder.Player.PlayerData.UnlockedAmmunition;
        for (int i = 0; i < unlockedAmmo.Count; i++)
        {
            if (unlockedAmmo[i].ID == Id)
            {
                return unlockedAmmo[i].Unlocked;
            }
        }
        Debug.LogWarning("Can't find this ammo!");
        return false;
    }

    public bool HaveWeaponForAmmo(Ship ship)
    {
        for (int i = 0; i < ship.Weapons.Count; i++)
        {
            Weapon weapon = ship.Weapons[i];
            if (weapon.SetForGame)
            {
                for (int j = 0; j < weapon.AvailableAmmoIds.Length; j++)
                {
                    if(weapon.AvailableAmmoIds[j] == Id)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public void ChangeSetForGameAmmoAmount(int amount) // przydałby się natychmiastowy dostęp, a nie iterowanie po liście
    {
        for (int i = 0; i < _unlockedAmmo.Count; i++)
        {
            if (_unlockedAmmo[i].ID == Id)
            {
                _unlockedAmmo[i].AmountSetForGame = amount;
            }
        }
    }

    private int AmmoAmountSetForGame()
    {
        AssignSingleton();
        for (int i = 0; i < _unlockedAmmo.Count; i++)
        {
            if (_unlockedAmmo[i].ID == Id)
            {
                return _unlockedAmmo[i].AmountSetForGame;
            }
        }
        Debug.LogWarning("Can't find this ammo!");
        return 0;

    }

    private int SetMaxAmmoAmount(int value)
    {
        for (int i = 0; i < _unlockedAmmo.Count; i++)
        {
            if (_unlockedAmmo[i].ID == Id)
            {
                _unlockedAmmo[i].Amount =  value;
                return _unlockedAmmo[i].Amount;
            }
        }
        Debug.LogWarning("something went wrong...");
        return value;
    }

    private int GetMaxAmmoAmount()
    {
        AssignSingleton();
        for (int i = 0; i < _unlockedAmmo.Count; i++)
        {
            if (_unlockedAmmo[i].ID == Id)
            {
                return _unlockedAmmo[i].Amount;
            }
        }
        Debug.LogWarning("Can't find this ammo!");
        return 0;
    }

    private void AssignSingleton()
    {
        if(_unlockedAmmo == null)
        {
            _unlockedAmmo = StaticTagFinder.Player.PlayerData.UnlockedAmmunition;
        }
    }
}

public enum BulletType
{
    Default,
    Laser
}