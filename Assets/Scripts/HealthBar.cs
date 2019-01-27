using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform bar;
    void Start()
    {
        Transform bar = transform.Find("Bar");
    }

    public void SetSize(float normalized) {
        bar.localScale = new Vector3(normalized, 1f);
    }
}
