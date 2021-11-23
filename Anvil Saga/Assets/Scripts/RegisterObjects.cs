using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*����� �������� �� ����������� ������� �� ����� ��� ������� � �������� ��������� ��*/
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
    /// ��������� ������ �� ���� �� �����
    /// </summary>
    /// <param name="obj">������</param>
    /// <param name="poolType">��� �������</param>
    /// <param name="position">������� ������</param>
    /// <returns></returns>
    public GameObject AddObjectToScene(GameObject obj, PoolableObject poolType, Transform position)
    {
        GameObject temp = pool.Spawn(poolType.ObjectType, position);
        AddObjectTosceneRegisterList(temp);
        return temp;
    }

    /// <summary>
    /// ��������� ������ �� ���� �� �����
    /// </summary>
    /// <param name="obj">������</param>
    /// <param name="poolType">��� �������</param>
    /// <param name="position">������� ������</param>
    /// <param name="AddSceneObjects">��������� �� ������ ��� ��������� ��</param>
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
    /// ��������� ������ �� ���� �� �����
    /// </summary>
    /// <param name="obj">������</param>
    /// <returns></returns>
    public GameObject AddObjectToScene(GameObject obj)
    {
        AddObjectTosceneRegisterList(obj);
        return obj;
    }

    /// <summary>
    /// ������� ������ �� ����� � ����������� ������������ � ���
    /// </summary>
    /// <param name="obj">������/param>
    /// <param name="pooltype">��� �������</param>
    public void RemoveObject(GameObject obj, PoolableObject pooltype)
    {
        sceneObjects.Remove(obj);
        pool.BackToPool(obj, pooltype.ObjectType);
    }

    /// <summary>
    /// ������� ������ �� �����
    /// </summary>
    /// <param name="obj">������/param>
    public void RemoveObject(GameObject obj)
    {
        sceneObjects.Remove(obj);
    }

    /// <summary>
    /// ���������� ������ ��������� �������� �� ����� 
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
    /// ��������� ������ � ������ ����� �������� �� �����
    /// </summary>
    /// <param name="obj">������</param>
    public void AddObjectTosceneRegisterList(GameObject obj)
    {
        sceneObjects.Add(obj);
    }

    /// <summary>
    /// ������� ������ �� ������ ����� �������� �� �����
    /// </summary>
    /// <param name="obj">������</param>
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
