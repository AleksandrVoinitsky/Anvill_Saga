using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Класс отвечает за спавн персонажа и врагов*/
public class Spawner : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public GameObject PlayerPrefab;
    [Range(1, 999)] [SerializeField] int EnemyCount = 1;
    [Range(0, 100)] [SerializeField] float SpeedSpawn = 1;
    [Range(0, 100)] [SerializeField] float WaitStartSpawn = 1;
    public bool Spawn = false;

    RegisterObjects register;
    float speedSpawnTimer = 0;

    void Start()
    {
        speedSpawnTimer += WaitStartSpawn;
        register = GameObject.FindGameObjectWithTag("Register").GetComponent<RegisterObjects>();
        StartSpawn();
    }

    void Update()
    {
        SpawnObjects(Time.deltaTime);
    }

    /// <summary>
    /// Старт спавна обьектов
    /// </summary>
    public void StartSpawn()
    {
        register.AddObjectToScene(PlayerPrefab, PlayerPrefab.GetComponent<Player>(), transform);
        Spawn = true; ;
    }

    /// <summary>
    /// Спавн обьектов
    /// </summary>
    /// <param name="time">Time</param>
    void SpawnObjects(float time)
    {
        if (!Spawn) return;
        if (speedSpawnTimer > 0)
        {
            speedSpawnTimer -= time;
        }
        if (EnemyCount > 0 && speedSpawnTimer <= 0)
        {
            transform.position = RandomizePosipion();
            register.AddObjectToScene(EnemyPrefab, EnemyPrefab.GetComponent<EnemyAI>(), gameObject.transform);
            EnemyCount--;
            speedSpawnTimer = SpeedSpawn;
        }
    }

    /// <summary>
    /// Возвращает случайную позицию на границе обзора камеры
    /// </summary>
    /// <returns></returns>
    Vector3 RandomizePosipion()
    {
        int changeVector = Random.Range(0, 2);
        float randomPoint = Random.Range(0, 1f);
        Vector2 pos;
        if(changeVector == 0)
        {
             pos =  Camera.main.ViewportToWorldPoint(new Vector2(randomPoint, Random.Range(0,2)));
        }
        else
        {
             pos = Camera.main.ViewportToWorldPoint(new Vector2(Random.Range(0, 2) , randomPoint));
        }
        return  pos;
    }
}
