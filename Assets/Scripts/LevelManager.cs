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
    public int minSpawnHeight = 2; //should be slightly above the ground  
    public float appleScale = 1f;
    public float fireScale = 1f;
    public int firstAppleSpawn = 20;
    public int firstFireSpawn = 30;

    public static int LAYER_COLLECTIBLES = 31;

    // Start is called before the first frame update
    void Start()
    {
        int firstApple = Random.Range(1, 10) + firstAppleSpawn;
        int firstFire = Random.Range(5, 15) + firstFireSpawn;
        List<int> spawnPostions = new List<int>();
        for(int i = firstApple; i < lvlWidth; i+= appleSpawnInterval) 
        {
            appleSpawnInterval += spawnIntervalIncrease;
            int randomDeviation = Random.Range(-3, 3);
            i += randomDeviation;

            if(spawnPostions.Contains(i)) 
            {
                i -= appleSpawnInterval;
                appleSpawnInterval -= spawnIntervalIncrease;
                continue; 
            }

            spawnPostions.Add(i);

            int randomHeight = Random.Range(minSpawnHeight, lvlHeight);
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(i, randomHeight), new Vector2(0, -1));

            if(hit.point == new Vector2(i, randomHeight) || hit.point.x.Equals(0f)) {
                i -= appleSpawnInterval;
                appleSpawnInterval -= spawnIntervalIncrease;
                continue;
            }

            GameObject apple = new GameObject();

            apple.tag = "Apple";
            apple.layer = LAYER_COLLECTIBLES;

            apple.transform.localScale = new Vector3(appleScale, appleScale, 0f);

            SpriteRenderer spriteRend = apple.AddComponent<SpriteRenderer>();
            Sprite s = Resources.Load<Sprite>("Sprites/apple");
            spriteRend.sprite = s;
            spriteRend.sortingOrder = 1;

            BoxCollider2D box2d = apple.AddComponent<BoxCollider2D>();
            box2d.size = s.bounds.size;
            box2d.isTrigger = true;

            apple.transform.position = new Vector3(hit.point.x, hit.point.y+0.5f);
        }

        for (int i = firstFire; i < lvlWidth; i += fireSpawnInterval)
        {
            fireSpawnInterval += spawnIntervalIncrease;
            int randomDeviation = Random.Range(-3, 3);
            i += randomDeviation;

            if (spawnPostions.Contains(i))
            {
                i -= fireSpawnInterval;
                fireSpawnInterval -= spawnIntervalIncrease;
                continue;
            }

            spawnPostions.Add(i);

            int randomHeight = Random.Range(minSpawnHeight, lvlHeight);

            RaycastHit2D hit = Physics2D.Raycast(new Vector2(i, randomHeight), new Vector2(0, -1));

            if (hit.point == new Vector2(i, randomHeight) || hit.point.x.Equals(0f)){
                i -= fireSpawnInterval;
                fireSpawnInterval -= spawnIntervalIncrease;
                continue;
            }
            GameObject fire = new GameObject();

            fire.tag = "Fire";
            fire.layer = LAYER_COLLECTIBLES;

            fire.transform.localScale = new Vector3(fireScale, fireScale, 0f);

            SpriteRenderer spriteRend = fire.AddComponent<SpriteRenderer>();
            Sprite s = Resources.Load<Sprite>("Sprites/Bonfire");
            spriteRend.sprite = s;
            spriteRend.sortingOrder = 1;

            CircleCollider2D box2d = fire.AddComponent<CircleCollider2D>();
            box2d.radius = 6f;
            box2d.isTrigger = true;

            Animator anim = fire.AddComponent<Animator>();
            RuntimeAnimatorController an = Resources.Load<RuntimeAnimatorController>("Animations/fireAnimator");
            anim.runtimeAnimatorController = an;


            fire.transform.position = new Vector3(hit.point.x, hit.point.y+0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
