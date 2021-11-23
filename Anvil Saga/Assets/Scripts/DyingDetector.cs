using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*����� �������� �� ������������� �������� �������� � ����� ����������� �������*/
public class DyingDetector : MonoBehaviour
{
    [SerializeField] string Teg;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == Teg) collision.gameObject.GetComponent<PoolableObject>().Dying();
    }
}
