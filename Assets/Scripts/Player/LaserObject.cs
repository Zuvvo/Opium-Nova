using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserObject : WeaponObject
{
    public LineRenderer LineRenderer;

    private Vector2 _laserDirection = Vector2.right;

    private LayerMask _mask;
    private RaycastHit2D _hit;

    private float _cool100PercentTime = 5;

    private float _overHeatingPercent = 0;

    private float _coolingTimer;
    private float _coolStartTime = 1;

    private float _startedCoolingTimer;

    private float _cachedCoolStartTime;
    private float _cachedOverHeatingPercent;
    private bool _isCachedCoolStartTime;

    private bool _fireBlockedByOverheat;

    public override void FireAmmo(int amount)
    {
        if(!_fireBlockedByOverheat)
        {
            //  || UnityEngine.Random.Range(1,100) > 20
            Fire(amount);
            AddHeat();
        }
        else
        {
            if(UnityEngine.Random.Range(1, 100) < 25)
            {
                _fireBlockedByOverheat = false;
                Fire(amount);
            }
            else
            {
                _fireBlockedByOverheat = true;
            }
        }
    }

    private void Fire(int amount)
    {
        base.FireAmmo(amount);
        if (_hit.collider != null) // todo: sprowadzić cele do klasy bazowej np. target
        {
            Enemy enemy = _hit.collider.GetComponent<Enemy>();
            Obstacle obstacle = _hit.collider.GetComponent<Obstacle>();
            if (enemy != null)
            {
                enemy.ChangeHealth(-ActiveAmmo.Damage);
                StaticTagFinder.EffectsController.CreateLaserBeamEffect(enemy.transform);
            }
            else if (obstacle != null)
            {
                StaticTagFinder.EffectsController.CreateLaserBeamEffect(obstacle.transform);
                obstacle.ChangeHealth(-ActiveAmmo.Damage);
            }
        }
    }

    private void AddHeat()
    {
        _overHeatingPercent += 0.05f;
        if(_overHeatingPercent >= 1)
        {
            _fireBlockedByOverheat = true;
        }
        StaticTagFinder.GameUI.UiLaserOverheat.SetOverHeatUi(_overHeatingPercent);
    }

    private void LowerHeat()
    {
        _fireBlockedByOverheat = false;
        _overHeatingPercent = Mathf.Lerp(_cachedCoolStartTime/ _cool100PercentTime, 0, _startedCoolingTimer/ _cachedCoolStartTime);
        StaticTagFinder.GameUI.UiLaserOverheat.SetOverHeatUi(_overHeatingPercent);
     //   Debug.Log(string.Format("heat: {0}, t: {1}/{2}, percent: {3}", _overHeatingPercent, _startedCoolingTimer, _cachedCoolStartTime, _startedCoolingTimer / _cachedCoolStartTime));
    }

    private void Update()
    {
        if(IsActive && IsFiring && !_fireBlockedByOverheat)
        {
            LineRenderer.enabled = true;
            RayCastForObjects();
        }
        else
        {
            LineRenderer.enabled = false;
        }

        if(!IsFiring)
        {
            _coolingTimer += Time.deltaTime;
        }
        else
        {
            _isCachedCoolStartTime = false;
            _startedCoolingTimer = 0;
            _coolingTimer = 0;
        }

        if(_coolingTimer >= _coolStartTime && _overHeatingPercent > 0)
        {
            CacheCoolingStartPercent();
            _startedCoolingTimer += Time.deltaTime;
            LowerHeat();
        }
    }

    private void CacheCoolingStartPercent()
    {
        if (!_isCachedCoolStartTime)
        {
            _cachedCoolStartTime = _overHeatingPercent * _cool100PercentTime;
            _isCachedCoolStartTime = true;
        }
    }

    private void RayCastForObjects()
    {
        _hit = Physics2D.Raycast(BulletSpawnPoint.position, _laserDirection, 200f, _mask);

        if (_hit.collider != null)
        {
            float distanceToEnemy = _hit.collider.transform.position.x - BulletSpawnPoint.transform.position.x;
            LineRenderer.SetPosition(0, new Vector3(distanceToEnemy, 0, 0));
        }
        else
        {
            LineRenderer.SetPosition(0, new Vector3(20, 0, 0));
        }
    }

    private void Start()
    {
        _mask = LayerMask.GetMask("Obstacle", "Enemy");
    }
}