using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*Класс отвечает за таймер в начале уровня*/
public class StartTimer : MonoBehaviour
{
    public Text text;
    public int StartTime = 3;
    private float timer = 1;

    void Start()
    {
        text = GetComponent<Text>();
        text.text = StartTime.ToString();
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0)
        {
            if (StartTime == 0) gameObject.SetActive(false);
            timer = 1f;
            StartTime--;
            text.text = StartTime.ToString();
        }
    }
}
