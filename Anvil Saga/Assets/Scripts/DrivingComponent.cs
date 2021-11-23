using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrivingComponent : MonoBehaviour
{
    [Range(0, 20)] public float Speed;
    [Range(0, 20)] public float Acceleration;
    [Range(0, 20)] public float StopDistance;
    [Range(0, 20)] public float RotationSpeed;

    [HideInInspector] public float RotationAngle;
    [HideInInspector] public Vector2 MoveVector;
    [HideInInspector] public Vector2 LookVector;
    [HideInInspector] Rigidbody2D rigidBodyComponent;
    [HideInInspector] public bool RotationFlag = false;
    [HideInInspector] public bool MoveFlag = false;

    RegisterObjects register;

    private Vector2 min;
    private Vector2 max;

    void Start()
    {
        rigidBodyComponent = GetComponent<Rigidbody2D>();
        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); 
        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        register = GameObject.FindGameObjectWithTag("Register").GetComponent<RegisterObjects>();
        register.AddObjectToScene(gameObject);
    }

    void FixedUpdate()
    {
        CalculateCameraScope();

        if (LookVector.magnitude > StopDistance && MoveFlag)
        {
            MoveObject(MoveVector, Speed, Acceleration);
        }
        else
        {
            StopObject(Acceleration);
        }

        if (RotationFlag)
        {
            RotateObject(RotationAngle, RotationSpeed);
        }
    }
    /// <summary>
    /// Направляет обьект по вектору с заданной максимальной скорость и ускорением
    /// </summary>
    /// <param name="vector">вектор направления движения</param>
    /// <param name="speed">максимально возможная скорость движения</param>
    /// <param name="acceleration">модификатор ускорения</param>
    void MoveObject(Vector3 vector, float speed, float acceleration)
    {
        rigidBodyComponent.velocity = Vector2.Lerp(rigidBodyComponent.velocity,
            vector * speed, acceleration * Time.fixedDeltaTime);
    }
    /// <summary>
    /// Останавливает обьект с заданным ускорением
    /// </summary>
    /// <param name="acceleration">Скорость остановки обьекта</param>
    void StopObject(float acceleration)
    {
        rigidBodyComponent.velocity = Vector2.Lerp(rigidBodyComponent.velocity,
            new Vector2(0, 0), acceleration * Time.fixedDeltaTime);
    }
    /// <summary>
    /// Вращает обьект до определенного унла с заданной скоростью
    /// </summary>
    /// <param name="angle">Угол поворота</param>
    /// <param name="speed">Скорость вращения</param>
    void RotateObject(float angle, float speed)
    {
        rigidBodyComponent.SetRotation(Mathf.LerpAngle(rigidBodyComponent.rotation,
            angle, speed * Time.fixedDeltaTime));
    }
    /// <summary>
    /// Прилагает к обьекту силу противоположную его удаленности от границ камеры
    /// </summary>
    void CalculateCameraScope()
    {
        if (rigidBodyComponent.position.x < min.x) rigidBodyComponent.velocity       
                -= new Vector2(rigidBodyComponent.position.x - min.x, 0);
        if (rigidBodyComponent.position.x > max.x) rigidBodyComponent.velocity
                -= new Vector2(rigidBodyComponent.position.x - max.x, 0);
        if (rigidBodyComponent.position.y < min.y) rigidBodyComponent.velocity
                -= new Vector2(0,rigidBodyComponent.position.y - min.y);
        if (rigidBodyComponent.position.y > max.y) rigidBodyComponent.velocity
                -= new Vector2(0,rigidBodyComponent.position.y - max.y);
    }
}
