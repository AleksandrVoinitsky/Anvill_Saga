using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*����� �������� �� ���������� ����� ��������*/
public class PoolManager
{
     Transform DefoultSpawner;
     Dictionary<PoolItems, Pool> ItemInfos = new Dictionary<PoolItems, Pool>();

    public PoolManager(GameObject poolSpawn, GameObject poolRegist)
    {
        DefoultSpawner = poolSpawn.transform;
    }

    /// <summary>
    /// ��������\������� �� ���� �������
    /// </summary>
    /// <param name="identity">��� �������</param>
    /// <param name="toTrans">����� ������</param>
    /// <returns></returns>
    public  GameObject Spawn(PoolItems identity, Transform toTrans)
    {
        Pool info = ItemInfos[identity];
        GameObject obj;
        if (info.Deactive.Count > 0)
        {
            obj = info.Deactive[0];
            info.Deactive.RemoveAt(0);
        }
        else
        {
            obj = GameObject.Instantiate(info.SpawnItem, DefoultSpawner);
        }
        info.Active.Add(obj);
        obj.SetActive(true);
        obj.transform.position = toTrans.position;
        return obj;
    }

    /// <summary>
    /// �������� ���� ��� �������� ������ ����
    /// </summary>
    /// <param name="item">������</param>
    /// <param name="identity">��� �������</param>
    /// <param name="poolStartAmount">�������������� �����������</param>
    public  void CreatePool(GameObject item,PoolItems identity, int poolStartAmount)
    {
        ItemInfos.Add(identity, new Pool());
        Pool info = ItemInfos[identity];
        info.SpawnItem = item;
        info.poolStartAmount = poolStartAmount;
        GameObject go;
        for (int i = 0; i < info.poolStartAmount; i++)
        {
            go = GameObject.Instantiate(info.SpawnItem, DefoultSpawner);
            go.transform.SetParent(DefoultSpawner);
            go.SetActive(false);
            info.Deactive.Add(go);
        }
    }

    /// <summary>
    /// ������� ������� � ���
    /// </summary>
    /// <param name="obj">������</param>
    /// <param name="identity">��� �������</param>
    public void BackToPool(GameObject obj,PoolItems identity)
    {
        Pool info = ItemInfos[identity];
        info.Active.Remove(obj);
        obj.SetActive(false);
        obj.transform.SetParent(DefoultSpawner);
        info.Deactive.Add(obj);
    }
}
