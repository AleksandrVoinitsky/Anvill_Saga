using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : BaseGun
{
    PoolableObject poolType;
    RegisterObjects register;

    void Start()
    {
        poolType = BulletPrefab.GetComponent<PoolableObject>();
        register = GameObject.FindGameObjectWithTag("Register").GetComponent<RegisterObjects>();
    }

    void Update()
    {
        BulletTimeCast(Time.deltaTime);
    }

    #region Publick

    /// <summary>
    /// Добавляет 1 Bullet 
    /// </summary>
    public override void AddAmmo()
    {
        base.AddAmmo();
    }
    /// <summary>
    /// Выстрел из пушки
    /// </summary>
    public override void Fire()
    {
        base.Fire();
        register.AddObjectToScene(BulletPrefab, poolType, transform,CalculationEscape).
            GetComponent<Bullet>().AwakeBullet(transform.up);
    }

    #endregion

    #region Private

    /// <summary>
    /// Счетчики/таймеры класса Gun
    /// </summary>
    /// <param name="time">Time</param>
    void BulletTimeCast(float time)
    {
        if (ammotimer > 0)
        {
            ammotimer -= Time.deltaTime;
        }
        if (ammotimer <= 0 && Ammo < MaxAmmo)
        {
            ammotimer = GenerateBulletTime;
            AddAmmo();
        }

        if (speedAttackTimer > 0)
        {
            speedAttackTimer -= Time.deltaTime;
        }
        if (Ammo > 0 && fire && speedAttackTimer <= 0)
        {
            speedAttackTimer = SpeedAttack;

            Fire();
        }
    }

    #endregion
}
