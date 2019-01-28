using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class FireController : MonoBehaviour
{
    public bool IsFiring = false;
    public GameUI GameUI;
    public Button FireButton;
    public List<Weapon> Weapons;
    public Weapon ActiveWeapon;
    public Text WeaponText;

    public List<Ammo> Ammo;
    public Ammo ActiveAmmo;

    public GameObject BulletHolder;
    public Globals Globals;

    public GameObject HidingImage;

    public event Action<bool> OnLaserActive;

    private ShipObject _playerGO;
    private float _bulletTimer;
    private Communique _communique;
    private Transform _bulletSpawnPoint;

    public void SetFireController(ShipObject shipGO, List<Weapon> weapons, List<Ammo> ammo)
    {
        Weapons = weapons;
        ActiveWeapon = Weapons[0];
        Ammo = ammo;
        ActiveAmmo = Ammo[0];

        _bulletSpawnPoint = ActiveWeapon.WeaponObject.BulletSpawnPoint;

        WeaponText.text = ActiveWeapon.Name;

        _playerGO = shipGO;
        _communique = Globals.Communique;
        ActiveWeapon = Weapons[0];
        ActiveWeapon.WeaponObject.gameObject.SetActive(true);
        SetAmmo(ActiveWeapon.Ammo[0]);
        GameUI.WeaponUI.SetUI(ActiveWeapon);
        OnLaserActive += GameUI.UiLaserOverheat.SetActive;
    }

    public void SwitchWeaponButton()
    {
        SwitchWeapon();
    }

    public void SwitchAmmoButton()
    {
        SwitchAmmo();
    }

    private void SwitchAmmo() // sprwadzic te metode
    {
        int activeAmmoIndex = GetIndexOfActiveAmmo();
        if (activeAmmoIndex == ActiveWeapon.Ammo.Count - 1)
        {
            ActiveAmmo = ActiveWeapon.Ammo[0];
        }
        else
        {
            ActiveAmmo = ActiveWeapon.Ammo[activeAmmoIndex + 1];
        }
        SetAmmo(ActiveAmmo);
    }

    private void SetAmmo(Ammo ammo)
    {
        Debug.Log("SetAmmo: " + ammo.Name);
        ActiveAmmo = ammo;
        ActiveWeapon.WeaponObject.ActiveAmmo = ammo;
        GameUI.AmmoUI.SetUI(ActiveAmmo, GetIndexOfActiveAmmo()+1, ActiveWeapon.Ammo.Count);
        _communique.SetCommunique(string.Format("Ammo to:\n{0}", ActiveAmmo.Name));
    }

    private int GetIndexOfActiveAmmo()
    {
        for (int i = 0; i < ActiveWeapon.Ammo.Count; i++)
        {
            if (ActiveAmmo == ActiveWeapon.Ammo[i])
            {
                return i;
            }
        }
        Debug.LogError("Can't find active weapon index!");
        return 0;
    }

    private void OnDestroy()
    {
        OnLaserActive -= GameUI.UiLaserOverheat.SetActive;
    }

    private void Update()
    {
        CheckForWeaponSwitch();
      //  CheckForFireButton();
        if (IsFiring)
        {
            _bulletTimer += Time.deltaTime;
            if(_bulletTimer > ActiveWeapon.FireDelay)
            {
                Fire();
                _bulletTimer = 0;
            }
        }
        ActiveWeapon.WeaponObject.IsFiring = IsFiring;
    }
    private void FixedUpdate()
    {
        CheckForFireButton();
    }

    private void CheckForWeaponSwitch()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            SwitchWeapon();
        }
    }

    private void Fire()
    {
        switch (ActiveAmmo.BulletType)
        {
            case BulletType.Default:
                CreateBullet();
                break;
            case BulletType.Laser:
                CreateLaserBeam();
                break;
        }
        ActiveWeapon.WeaponObject.FireAmmo(1); // magic number - konsumuje 1 amunicje
        GameUI.AmmoUI.RefreshAmount();

        if (ActiveAmmo.Amount <= 0) // a tak w razie gdyby
        {
            Ammo ammo = ActiveAmmo;
            ActiveWeapon.Ammo.Remove(ammo);
            if (ActiveWeapon.Ammo.Count == 0)
            {
                Weapon weapon = ActiveWeapon;
                SwitchWeapon();
                Weapons.Remove(weapon); // tu nie powinno usuwwać tylko blokować w razie gdyby dostało się amunicję do tej broni
            }
            else
            {
                SwitchAmmo();
            }
            Ammo.Remove(ammo);
            // czyszczenie refki z activeweapon.ammo i ammo list a potem ustawienie innego ammo
        }
    }

    private void CreateBullet()
    {
        GameObject bullet = CreateBullet(ActiveAmmo.BulletPrefab);
        MoveBullet(bullet);
    }

    private void CreateLaserBeam()
    {
        LaserObject laserObj = ActiveWeapon.WeaponObject as LaserObject;
      //  laserObj.InitLaserBeam();
    }

    private void CheckForFireButton()
    {
        IsFiring = CrossPlatformInputManager.GetButton("Fire");
    }

    private GameObject CreateBullet(GameObject bullet)
    {
        GameObject bulletTemp;
        bulletTemp = Instantiate(bullet, _bulletSpawnPoint.position, Quaternion.identity) as GameObject;
        bulletTemp.GetComponent<Bullet>().Ammo = ActiveAmmo;
        bulletTemp.transform.SetParent(BulletHolder.transform);
        return bulletTemp;
    }

    private void MoveBullet(GameObject bullet)
    {
        Rigidbody2D rigidBody = bullet.GetComponent<Rigidbody2D>();
        rigidBody.AddForce(Vector3.right * ActiveWeapon.FireSpeed);
        Destroy(bullet, ActiveWeapon.BulletLifeLength);
    }

    private void SwitchWeapon() // przez to musi przechodzić koniecznie zmiana broni (updatowane elementy Ui)
    {
        ActiveWeapon.WeaponObject.gameObject.SetActive(false);
        ActiveWeapon.WeaponObject.IsActive = false;
        ActiveWeapon.WeaponObject.IsFiring = false;
        int activeWeaponIndex = GetIndexOfActiveWeapon();
        if(activeWeaponIndex==Weapons.Count - 1)
        {
            ActiveWeapon = Weapons[0];
        }
        else
        {
            ActiveWeapon = Weapons[activeWeaponIndex + 1];
        }
        SetWeaponUi();
        _communique.SetCommunique(string.Format("Weapon switched to:\n{0}", ActiveWeapon.Name));
        SetAmmo(ActiveWeapon.Ammo[0]); // ustawia pierwsza amunicje na broni
    }

    private void SetWeaponUi()
    {
        GameUI.WeaponUI.SetUI(ActiveWeapon);
        ActiveWeapon.WeaponObject.gameObject.SetActive(true);
        ActiveWeapon.WeaponObject.IsActive = true;
        WeaponText.text = ActiveWeapon.Name;

        LaserObject laserObj = ActiveWeapon.WeaponObject as LaserObject;
        if(OnLaserActive != null)
        {
            OnLaserActive(laserObj != null);
        }
    }

    private int GetIndexOfActiveWeapon()
    {
        for (int i = 0; i < Weapons.Count; i++)
        {
            if(ActiveWeapon == Weapons[i])
            {
                return i;
            }
        }
        Debug.LogError("Can't find active weapon index!");
        return 0;
    }
}
