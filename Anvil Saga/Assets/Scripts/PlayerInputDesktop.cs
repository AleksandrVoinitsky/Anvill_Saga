using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Класс отвечает за управление персонажем на Desktop*/
public class PlayerInputDesktop : MonoBehaviour
{
    public DrivingComponent Motor;
    public BaseGun gun;

    private Camera mainCamera;
    private Vector2 mousePosition;
    private Rigidbody2D rigidBodyComponent;
    private Main main;

    void Start()
    {
        rigidBodyComponent = Motor.GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        main = GameObject.Find("MAIN").GetComponent<Main>();
    }


    void Update()
    {
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDirection = mousePosition - rigidBodyComponent.position;
        float newAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;

        if (Input.GetMouseButton(0))
        {
            Motor.MoveVector = Motor.gameObject.transform.up;
            Motor.LookVector = lookDirection;
            Motor.MoveFlag = true;
        }
        else
        {
            Motor.MoveFlag = false;
        }
        Motor.RotationFlag = true;
        Motor.RotationAngle = newAngle;

        if (Input.GetMouseButtonDown(1))
        {
            gun.fire = true;
        }
        else
        {
            gun.fire = false;
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            main.ShowMainMenu();
        }
        
    }
}
