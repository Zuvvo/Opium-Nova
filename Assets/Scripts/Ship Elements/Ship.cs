using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ship
{
    public string Name;
    public bool SetForGame;
    public int Id;
    public ShipObject ShipObject;
    public int MaxHealth;
    public int MaxArmor;
    public Sprite Sprite;
    public int Level;
    public float Speed;
    public Difficulty Difficulty;
    public Hardness Hardness;
    
    public List<Weapon> Weapons = new List<Weapon>();
    public List<Ammo> Ammo = new List<Ammo>();
    public int[] AvailableWeaponsIds;
    public string ShipDescription
    {
        get
        {
            return string.Format("Speed: {0} \nDifficulty: {1} \nHardness: {2}", Speed, Difficulty, Hardness);
        }
    }

    public bool HaveAmmoOfID(int id)
    {
        for (int i = 0; i < Ammo.Count; i++)
        {
            if(Ammo[i].Id == id)
            {
                return true;
            }
        }
        return false;
    }

    public void SetAllGearActiveForGame()
    {
        SetForGame = true;
        for (int i = 0; i < Weapons.Count; i++)
        {
            Weapons[i].SetForGame = true;
        }

        for (int i = 0; i < Ammo.Count; i++)
        {
            Ammo[i].SetForGame = true;
        }
        StaticTagFinder.Player.PlayerSelects.SelectedShip = this;
    }

    public bool HaveAnyWeaponAndAmmoSetForGame()
    {
        for (int i = 0; i < Weapons.Count; i++)
        {
            if (Weapons[i].SetForGame)
            {
                for (int j = 0; j < Weapons[i].Ammo.Count; j++)
                {
                    if (Weapons[i].Ammo[j].SetForGame)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
}

public enum Difficulty
{
    Easy,
    Normal,
    Hard
}

public enum Hardness
{
    Low,
    Medium,
    High
}