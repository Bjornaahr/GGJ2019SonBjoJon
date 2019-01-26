using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    public int lvlHeight = 10;
    public int lvlWidth = 500;
    public int appleSpawnInterval = 10;
    public int fireSpawnInterval = 15;
    public int spawnIntervalIncrease = 3;

    public static int LAYER_COLLECTIBLES = 9;

    // Start is called before the first frame update
    void Start()
    {
        int firstApple = Random.Range(1, 10);
        int firstFire = Random.Range(5, 15);
        for(int i = firstApple; i < lvlWidth; i+= appleSpawnInterval) 
        {
            appleSpawnInterval += spawnIntervalIncrease;
            int randomDeviation = Random.Range(-3, 3);
            i += randomDeviation;

            GameObject apple = GameObject.CreatePrimitive(PrimitiveType.Cube);
            DestroyImmediate(apple.GetComponent<BoxCollider>());
            apple.tag = "Apple";
            apple.layer = LAYER_COLLECTIBLES;


            //TODO: set apple texture 
            Material mat = apple.GetComponent<Renderer>().material;
            mat.color = Color.green;

            BoxCollider2D box2d = apple.AddComponent<BoxCollider2D>();

            //Apple should fall from the sky and land on smth -> ground / platform 
            Rigidbody2D rBody = apple.AddComponent<Rigidbody2D>();
            rBody.mass = 100000;
            rBody.angularDrag = 0;
            rBody.drag = 0;
            rBody.constraints = RigidbodyConstraints2D.FreezePositionX;
            rBody.gravityScale = 100;


            apple.transform.position = new Vector3(i, lvlHeight);
        }

        for (int i = firstFire; i < lvlWidth; i += fireSpawnInterval)
        {
            fireSpawnInterval += spawnIntervalIncrease;
            int randomDeviation = Random.Range(-3, 3);
            i += randomDeviation;

            GameObject fire = GameObject.CreatePrimitive(PrimitiveType.Cube);
            DestroyImmediate(fire.GetComponent<BoxCollider>());
            fire.tag = "Fire";
            fire.layer = LAYER_COLLECTIBLES;


            //TODO: set apple texture 
            Material mat = fire.GetComponent<Renderer>().material;
            mat.color = Color.red;

            BoxCollider2D box2d = fire.AddComponent<BoxCollider2D>();

            //Apple should fall from the sky and land on smth -> ground / platform 
            Rigidbody2D rBody = fire.AddComponent<Rigidbody2D>();
            rBody.mass = 100000;
            rBody.angularDrag = 0;
            rBody.drag = 0;
            rBody.constraints = RigidbodyConstraints2D.FreezePositionX;
            rBody.gravityScale = 100;


            fire.transform.position = new Vector3(i, lvlHeight);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
