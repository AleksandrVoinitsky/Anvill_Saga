using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*����� ����������� ��� ����� Player*/
public class Player : PoolableObject
{ 
    Main main;
    public Gun PlayerGun;
    void Start()
    {
        main = GameObject.FindGameObjectWithTag("Main").GetComponent<Main>();
    }

    /// <summary>
    /// ����� ���������� ��� �������� ������� ������
    /// </summary>
    public override void Dying()
    {
        main.GameOver();
        //base.Dying();
    }
}
