using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public bool Initialized;
    public List<UnlockedShip> UnlockedShips = new List<UnlockedShip>();
    public List<UnlockedWeapon> UnlockedWeapons = new List<UnlockedWeapon>();
    public List<UnlockedAmmo> UnlockedAmmunition = new List<UnlockedAmmo>();
    public List<UnlockedLevels> UnlockedLevels = new List<UnlockedLevels>();
    public List<PlayerResources> PlayerResources = new List<PlayerResources>();

    public PlayerData(int playerId)
    {
        PlayerData data = StaticTagFinder.Player.PlayersTempDatabase.PlayerData;

        UnlockedShips = data.UnlockedShips;
        UnlockedWeapons = data.UnlockedWeapons;
        UnlockedAmmunition = data.UnlockedAmmunition;
        UnlockedLevels = data.UnlockedLevels;
        PlayerResources = data.PlayerResources;
        Initialized = true;
    }

    public UnlockedAmmo GetUnlockedAmmoData(int id)
    {
        for (int i = 0; i < UnlockedAmmunition.Count; i++)
        {
            if(id == UnlockedAmmunition[i].ID)
            {
                return UnlockedAmmunition[i];
            }
        }
        Debug.LogWarning("Can't find unlocked ammo with id " + id);
        return null;
    }
}