using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    public Transform bar;

    private static float barsize;

    void Start()
    {
        SetSize(1f);
    }

    void Update()
    {
        if (gameObject.name == "HealthBar")
        {
            barsize = Player.Food / 100;
        }
        else barsize = Player.Temprature / 100;

        SetSize(barsize);
    }

    void SetSize(float normalized) {
        bar.localScale = new Vector3(normalized, 1f);
    }
}
