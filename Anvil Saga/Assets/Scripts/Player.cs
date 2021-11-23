using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Класс указывающий что обькт Player*/
public class Player : PoolableObject
{ 
    Main main;
    public Gun PlayerGun;
    void Start()
    {
        main = GameObject.FindGameObjectWithTag("Main").GetComponent<Main>();
    }

    /// <summary>
    /// Метод вызывается при сценарии смерити игрока
    /// </summary>
    public override void Dying()
    {
        main.GameOver();
        //base.Dying();
    }
}
