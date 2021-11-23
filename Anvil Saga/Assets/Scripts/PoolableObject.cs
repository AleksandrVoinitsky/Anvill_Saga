using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolableObject : MonoBehaviour
{
    public PoolItems ObjectType;
    RegisterObjects register;
    public virtual void Dying(){}
}
