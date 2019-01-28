using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selections : MonoBehaviour {

    public GameObject ShipGO;
    public Image ShipImage;
    public List<Image> WeaponImages = new List<Image>();

    public void Start()
    {
        Hangar.OnShipSelected += BuildShip;
    }

    public void BuildShip(Ship ship)
    {
        ShipImage.enabled = true;
        ShipImage.sprite = ship.Sprite;

        ClearWeapons();
        for (int i = 0; i < ship.Weapons.Count; i++)
        {
            WeaponImages[i].sprite = ship.Weapons[i].WeaponIcon;
        }
    }
    public void LoadDefaultShip(Ship ship)
    {
        BuildShip(ship);
    }
    
    private void ClearWeapons()
    {
        for (int i = 0; i < WeaponImages.Count; i++)
        {
            WeaponImages[i].sprite = null;
        }
    }
}
