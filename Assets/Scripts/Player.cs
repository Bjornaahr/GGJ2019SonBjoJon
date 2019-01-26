using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float Food;      
    public float Temprature;

    [SerializeField]
    float speed, jumpSpeed, windSpeed, acceleration;

    Rigidbody2D rigidbod;
    bool isGrounded = true;

    [SerializeField]
    Transform rayGround;

    float startTime;
    float timeNow;

    bool isNearFire = false;


    // Start is called before the first frame update
    void Start()
    {
        rigidbod = GetComponent<Rigidbody2D>();
        startTime = Time.time;
        Food = 100;
        Temprature = 37;
    }

    // Update is called once per frame
    void Update()
    {
        timeNow = Time.time - startTime;
        Movement();
        CheckGround();
        Wind();
        FoodDecrease();
        TempratureHandler();
    }

    void Movement()
    {
        if(isGrounded && Input.GetButtonDown("Jump"))
        {
            rigidbod.velocity = Vector2.up * jumpSpeed;
        }

        speed = Input.GetAxis("Horizontal") * acceleration;
        rigidbod.velocity = new Vector2(speed - windSpeed, rigidbod.velocity.y);

    }

    void CheckGround()
    {
        //Sends a raystraight down 0.1 blocks
        RaycastHit2D hitDown = Physics2D.Raycast(rayGround.position, Vector2.down, 0.1f);
        //Chcks if ray hits ground
        if (hitDown)
        {
            isGrounded = true;
        } else isGrounded = false;
    }

    //Increases wind with respect to time
    void Wind()
    {
        windSpeed = (timeNow / 1000) + 1;
    }

    //Decreases food
    void FoodDecrease()
    {
        Food -= (timeNow / 500);

        if(Food <= 0)
        {
            //Kill player and show death screen
        }

    }

    //Decreases temprature while not near a fireplace
    void TempratureHandler()
    {
        //Check if player is near fire and raise or lower temp
        if (!isNearFire)
        {
            Temprature -= (timeNow / 5000);
        } else Temprature += 0.005f;
 
        //Kills the player :)
        if(Temprature <= 21 && !isNearFire)
        {
            //Kill player and show death screen
        }

        if(Temprature >= 44 && isNearFire)
        {
           //Kill player and show death screen
        }
    }

    //Check if player collides with apple
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Apple")
        {
            Destroy(col.gameObject);
            Debug.Log("I got them apples");
            //Gives more food and keeps value between 0 and 100
            Food = Mathf.Clamp(Food + 5, 0f, 100f);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Check if player if near fire
        if (col.gameObject.tag == "Fire")
        {
            isNearFire = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        //Checks when player leaves fire
        if (col.gameObject.tag == "Fire")
        {
            isNearFire = false;
        }
    }

}
