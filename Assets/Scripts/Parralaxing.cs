using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parralaxing : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D player;
    float pSpeed;
    [SerializeField]
    float speedTrees, speedHills;
    GameObject[] trees;
    GameObject[] hills;

    // Start is called before the first frame update
    void Start()
    {
        trees = GameObject.FindGameObjectsWithTag("Trees");
        hills = GameObject.FindGameObjectsWithTag("Hills");

    }

    // Update is called once per frame
    void Update()
    {
        pSpeed = player.velocity.x;
        trees[0].transform.Translate(Vector2.right * pSpeed * speedTrees);
        trees[1].transform.Translate(Vector2.right * pSpeed * speedTrees);
        trees[2].transform.Translate(Vector2.right * pSpeed * speedTrees);

        hills[0].transform.Translate(Vector2.right * pSpeed * speedHills);
        hills[1].transform.Translate(Vector2.right * pSpeed * speedHills);
        hills[2].transform.Translate(Vector2.right * pSpeed * speedHills);
    }
}
