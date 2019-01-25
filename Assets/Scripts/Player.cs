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
        TempratureDecrease();
    }

    void Movement()
    {
        if(isGrounded && Input.GetButtonDown("Jump"))
        {
            rigidbod.velocity = Vector2.up * jumpSpeed;
        }

        speed = Input.GetAxis("Horizontal") * acceleration;
        rigidbod.velocity = new Vector2(speed - windSpeed, rigidbod.velocity.y);

        //rigidbod.AddForce(transform.right * -windSpeed);

    }

    void CheckGround()
    {
        RaycastHit2D hitDown = Physics2D.Raycast(rayGround.position, Vector2.down, 0.1f);

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
    }

    //Decreases temprature while not near a fireplace
    void TempratureDecrease()
    {
        Temprature -= (timeNow / 5000);
    }

}
