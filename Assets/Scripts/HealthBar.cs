using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    public Transform bar; 
    public SpriteRenderer barRender;

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

            if (barsize > 0.3)
            {
                barRender.color = Color.green;
            } else barRender.color = Color.red;




        }
        else {
            barsize = Player.Temprature / 100;

            if(barsize > 0.9)
            {
                barRender.color = Color.red;
            }

            if (barsize < 0.9 && barsize > 0.2)
            {
                barRender.color = Color.yellow;
            }

            if(barsize < 0.2)
            {
                barRender.color = Color.cyan;
            }


        }

        SetSize(barsize);
    }

    void SetSize(float normalized) {
        bar.localScale = new Vector3(normalized, 1f);
    }
}
