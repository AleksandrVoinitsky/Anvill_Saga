using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Класс отвечает за регестрацию обьктов на сцене для пулинга и расчетов уклонения ИИ*/
public class RegisterObjects : MonoBehaviour
{
    public PoolCategory[] Pools;
    List<GameObject> sceneObjects;
    Vector3[] sceneObjectsPosition;
    PoolManager pool;

    public void Awake()
    {
        sceneObjects = new List<GameObject>();
        pool = new PoolManager(gameObject, gameObject);

        foreach (var item in Pools)
        {
            pool.CreatePool(item.prefab, item.itemType, item.StartCount);
        }
    }

    /// <summary>
    /// Добавляет обьект из пула на сцену
    /// </summary>
    /// <param name="obj">Обьект</param>
    /// <param name="poolType">Тип обьекта</param>
    /// <param name="position">Пизиция спавна</param>
    /// <returns></returns>
    public GameObject AddObjectToScene(GameObject obj, PoolableObject poolType, Transform position)
    {
        GameObject temp = pool.Spawn(poolType.ObjectType, position);
        AddObjectTosceneRegisterList(temp);
        return temp;
    }

    /// <summary>
    /// Добавляет обьект из пула на сцену
    /// </summary>
    /// <param name="obj">Обьект</param>
    /// <param name="poolType">Тип обьекта</param>
    /// <param name="position">Пизиция спавна</param>
    /// <param name="AddSceneObjects">Учитывать ли обьект для уклонения ИИ</param>
    /// <returns></returns>
    public GameObject AddObjectToScene(GameObject obj, PoolableObject poolType, Transform position, bool AddSceneObjects)
    {
        if (AddSceneObjects)
        {
            return AddObjectToScene(obj, poolType, position);
        }
        else
        {
            GameObject temp = pool.Spawn(poolType.ObjectType, position);
            return temp;
        }
        
    }


    /// <summary>
    /// Добавляет обьект из пула на сцену
    /// </summary>
    /// <param name="obj">Обьект</param>
    /// <returns></returns>
    public GameObject AddObjectToScene(GameObject obj)
    {
        AddObjectTosceneRegisterList(obj);
        return obj;
    }

    /// <summary>
    /// Удаляет обьект со сцены с последующим перемещением в пул
    /// </summary>
    /// <param name="obj">Обьект/param>
    /// <param name="pooltype">Тип обьекта</param>
    public void RemoveObject(GameObject obj, PoolableObject pooltype)
    {
        sceneObjects.Remove(obj);
        pool.BackToPool(obj, pooltype.ObjectType);
    }

    /// <summary>
    /// Удаляет обьект со сцены
    /// </summary>
    /// <param name="obj">Обьект/param>
    public void RemoveObject(GameObject obj)
    {
        sceneObjects.Remove(obj);
    }

    /// <summary>
    /// Возвращает массив координат обьектов на сцене 
    /// </summary>
    /// <returns></returns>
    public Vector3[] GetObjectsPosition()
    {
        sceneObjectsPosition = new Vector3[sceneObjects.Count];
        for (int i = 0; i < sceneObjects.Count; i++)
        {
            sceneObjectsPosition[i] = sceneObjects[i].transform.position;
        }
        return sceneObjectsPosition;
    }

    /// <summary>
    /// Добавляет обьект в список учета обьектов на сцене
    /// </summary>
    /// <param name="obj">Обьект</param>
    public void AddObjectTosceneRegisterList(GameObject obj)
    {
        sceneObjects.Add(obj);
    }

    /// <summary>
    /// Удаляет обьект из списка учета обьектов на сцене
    /// </summary>
    /// <param name="obj">Обьект</param>
    public void RemoveObjectTosceneRegisterList(GameObject obj)
    {
        sceneObjects.Remove(obj);
    }

}

[System.Serializable]
public struct PoolCategory
{
    public GameObject prefab;
    public PoolItems itemType;
    public int StartCount;
}
