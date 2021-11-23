
using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    public GameObject SpawnItem;
    public int poolStartAmount;
    public List<GameObject> Deactive = new List<GameObject>();
    public List<GameObject> Active = new List<GameObject>();
}
