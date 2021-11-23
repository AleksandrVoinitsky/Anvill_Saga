using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PoolableObject
{
    [Range(0,1000)] [SerializeField] float BulletSpeed;
    [Range(0, 100)] [SerializeField] public float LifeTime;

    public Rigidbody2D rigidbody;

    float currentLifeTime;
    RegisterObjects register;
    TrailRenderer trail;
    

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        register = GameObject.FindGameObjectWithTag("Register").GetComponent<RegisterObjects>();
        trail = GetComponent<TrailRenderer>();
    }

    void Update()
    {
        if (currentLifeTime > 0)
        {
            currentLifeTime -= Time.deltaTime;
        }
        if (currentLifeTime < 0)
        {
            SleepBullet();
        }
    }

    /// <summary>
    /// используетс€ дл€ установки времени жизни пули и приложени€ силы по вектору
    /// </summary>
    /// <param name="moveVector">¬ектор направлени€ дл€ полета пули</param>
    public void AwakeBullet(Vector3 moveVector)
    {
        currentLifeTime = LifeTime;
        rigidbody.AddForce(moveVector * BulletSpeed);
    }
    /// <summary>
    /// ќтправл€ем пулю в пул(деактивируем)
    /// </summary>
    public void SleepBullet()
    {
        trail.Clear();
        rigidbody.velocity = new Vector3(0, 0,0);
        register.RemoveObject(gameObject, this);
    }
}
