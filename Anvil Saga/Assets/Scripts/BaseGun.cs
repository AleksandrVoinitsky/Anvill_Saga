using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGun : MonoBehaviour
{
    public GameObject BulletPrefab;
    public Transform FirePoint;
    public int Ammo = 1;
    public int MaxAmmo = 1;
    public float GenerateBulletTime = 5;
    public float SpeedAttack = 1;
    public float speedAttackTimer = 0;
    public float ammotimer = 0;
    public bool fire = false;
    public bool CalculationEscape = false;

    public virtual void Fire() { Ammo--; }

    public virtual void AddAmmo() { Ammo++; }
}
