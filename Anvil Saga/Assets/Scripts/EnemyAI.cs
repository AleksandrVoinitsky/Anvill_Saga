using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*����� �������� ���� ������*/
public class EnemyAI : PoolableObject
{
    public Vector3 Target;
    public GameObject Player;
    public DrivingComponent Motor;
    public Gun gun;

    [Range(0, 1000)] [SerializeField] float EscapeDistance;
    [Range(0, 1000)] [SerializeField] float AttackDistance;
    
    Main main;
    Rigidbody2D rigidBodyComponent;
    RegisterObjects register;
    TrailRenderer trail;
    Vector3 CurrentPosition;
    Vector3[] SceneObjectsPosition;
    Vector2 lookDirection;
    float newAngle;
    float behaviorCheckTimer;
    bool Escape = false;

    void Start()
    {
        rigidBodyComponent = Motor.GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
        register = GameObject.FindGameObjectWithTag("Register").GetComponent<RegisterObjects>();
        trail = GetComponent<TrailRenderer>();
        main = GameObject.FindGameObjectWithTag("Main").GetComponent<Main>();
    }

    void Update()
    {
        if (behaviorCheckTimer > 0)
        {
            behaviorCheckTimer -= Time.deltaTime; 
        }
        if (behaviorCheckTimer <= 0)
        {
            behaviorCheckTimer = 0.1f;
            CurrentPosition = rigidBodyComponent.position;
            SceneObjectsPosition = register.GetObjectsPosition();
            ChangeBehavior();
        }
    }

    #region Public
    /// <summary>
    /// ���������� ��� �������� ����������� ������
    /// </summary>
    public override void Dying()
    {
        main.AddScore();
        trail.Clear();
        register.RemoveObject(gameObject, this);
    }

    #endregion

    #region Private
    /// <summary>
    /// ����� ����� ���������
    /// </summary>
    void ChangeBehavior()
    {
        for (int i = 0; i < SceneObjectsPosition.Length; i++)
        {
            if (SceneObjectsPosition[i] == CurrentPosition) continue;
            float objDistance = CalculateDistance(SceneObjectsPosition[i]);
            if (objDistance < EscapeDistance)
            {

                if (objDistance < CalculateDistance(Target)) Target = SceneObjectsPosition[i];
                Move(lookDirection, -newAngle + 90);
                Escape = true;
            }
            else { Escape = false; }
        }
        if (!Escape)
        {
            Target = Player.transform.position;
            if (CalculateDistance(Target) > AttackDistance)
            {
                Move(lookDirection, newAngle);
            }
            else if (CalculateDistance(Target) <= AttackDistance && CalculateDistance(Target) >= EscapeDistance)
            {
                Attack(newAngle);
            }
        }
        lookDirection = CalculateLookDiretion(Target);
        newAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
    }

    /// <summary>
    /// ������ ��������� �������� � ����
    /// </summary>
    /// <param name="look">������ ��� �������� � ����� Motor �������</param>
    /// <param name="rotate">����������� ��������</param>
    void Move(Vector2 look, float rotate)
    {
        Motor.MoveVector = Motor.gameObject.transform.up;
        Motor.LookVector = look;
        Motor.MoveFlag = true;
        MoveRotate(rotate);
        gun.fire = false;
    }

    /// <summary>
    /// ������ ��������� �����
    /// </summary>
    /// <param name="rotate">���� ��� �������� ��� �����</param>
    void Attack(float rotate)
    {
        MoveRotate(rotate + 90);
        Motor.MoveFlag = false;
        gun.fire = true;
    }
    /// <summary>
    /// ����� ������������� ���� �������� ��� ������ Motor �������
    /// </summary>
    /// <param name="angle"></param>
    void MoveRotate(float angle)
    {
        Motor.RotationFlag = true;
        Motor.RotationAngle = angle;
    }

    /// <summary>
    /// ������ ��������� �� ������� �� Vector3
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    float CalculateDistance(Vector3 vector)
    {
        Vector3 dir = transform.position - vector;
        float distance = dir.magnitude;
        return distance;
    }
    
    /// <summary>
    /// ������ ������� ����������� ������������ �������
    /// </summary>
    /// <param name="vector">������ ��� ������� �������������� �������</param>
    /// <returns></returns>
    Vector2 CalculateLookDiretion(Vector3 vector)
    {
        Vector2 direction = new Vector2(vector.x, vector.y) - rigidBodyComponent.position;
        return direction;
    }
    #endregion
}
