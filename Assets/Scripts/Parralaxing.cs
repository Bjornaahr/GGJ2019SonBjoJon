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
    [SerializeField]
    GameObject[] trees;
    GameObject[] hills;

    [SerializeField]
    Transform cam;

    // Start is called before the first frame update
    void Start()
    {
        trees = GameObject.FindGameObjectsWithTag("Trees");
        hills = GameObject.FindGameObjectsWithTag("Hills");
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;

    }

    // Update is called once per frame
    void Update()
    {
        pSpeed = player.velocity.x;
        trees[0].transform.Translate(Vector2.right * pSpeed * speedTrees);
        trees[1].transform.Translate(Vector2.right * pSpeed * speedTrees);
        trees[2].transform.Translate(Vector2.right * pSpeed * speedTrees);

        if (distance(trees[2]) < -40)
        {

            trees[2].transform.position = new Vector2(trees[0].transform.position.x + 30, trees[2].transform.position.y);

            swap(ref trees[0], ref trees[2]);
            swap(ref trees[2], ref trees[1]);

        }


        hills[0].transform.Translate(Vector2.right * pSpeed * speedHills);
        hills[1].transform.Translate(Vector2.right * pSpeed * speedHills);
        hills[2].transform.Translate(Vector2.right * pSpeed * speedHills);



        if (distance(hills[2]) < -40)
        {

            hills[2].transform.position = new Vector2(hills[0].transform.position.x + 30, hills[2].transform.position.y);

            swap(ref hills[0], ref hills[2]);
            swap(ref hills[2], ref hills[1]);


        }

    

}


static void swap(ref GameObject a, ref GameObject b)
    {
        GameObject temp = a;
        a = b;
        b = temp;
    }

    float distance(GameObject obj)
    {
        float dis = 0f;
        dis = obj.transform.position.x - cam.position.x;
        return dis;
    }

}
