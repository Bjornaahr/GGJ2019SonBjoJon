﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float Food;      
    public float Temprature;

    [SerializeField]
    float speed, jumpSpeed, windSpeed, acceleration;
    [SerializeField]
    Rigidbody2D rigidbod;
    bool isGrounded = true;

    [SerializeField]
    Transform rayGround;

    float startTime;
    float timeNow;

    bool isNearFire = false;
    bool isDead = false;

    public Text txt_Score;
    public Text txt_Health;
    public Text txt_Temperature;
    private int startCoordX;

    [SerializeField]
    GameObject DeathScreen;
    [SerializeField]
    Text txt_DeathMessage;
    [SerializeField]
    Button restart;


    [SerializeField]
    string[] messagePit;
    [SerializeField]
    string[] messageFood;
    [SerializeField]
    string[] messageFreezing;
    [SerializeField]
    string[] messageOverheating;



    
    void Awake()
    {
        DeathScreen.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbod = GetComponent<Rigidbody2D>();
        startTime = Time.time;
        Food = 100;
        Temprature = 37;
        startCoordX = (int)transform.position.x;
        restart.onClick.AddListener(Retry);
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
        updateUI();
    }

    void updateUI() 
    {
        txt_Score.text = "Score: " + ((int)transform.position.x-startCoordX);
        txt_Health.text = "Health: " + (int)Food;
        txt_Temperature.text = "Temperature: " + (int)Temprature;
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

        if(Food <= 0 && !isDead)
        {
            KillPlayer("Starvation");
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
        if(Temprature <= 21 && !isNearFire && !isDead)
        {
            KillPlayer("Hypothermia");
        }

        if(Temprature >= 44 && isNearFire && !isDead)
        {
            KillPlayer("Hyperthermia");
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Check if player if near fire
        if (col.gameObject.tag == "Fire")
        {
            isNearFire = true;
        }

        if(col.gameObject.tag == "Pit")
        {
            KillPlayer("Pit");
        }

        if (col.gameObject.tag == "Apple")
        {
            Destroy(col.gameObject);
            Debug.Log("I got them apples");
            //Gives more food and keeps value between 0 and 100
            Food = Mathf.Clamp(Food + 5, 0f, 100f);
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

    void KillPlayer(string reason)
    {

        DeathScreen.SetActive(true);
        windSpeed = 0;
        acceleration = 0;
        jumpSpeed = 0;

        if(reason == "Pit")
        {

        }

        if(reason == "Starvation")
        {
            txt_DeathMessage.text = messageFood[Random.Range(0, messageFood.Length)];
        }

        if(reason == "Hypothermia")
        {
            txt_DeathMessage.text = messageFreezing[Random.Range(0, messageFreezing.Length)];

        }

        if(reason == "Hyperthermia")
        {
            txt_DeathMessage.text = messageOverheating[Random.Range(0, messageOverheating.Length)];
        }

    }

    void Retry()
    {
        SceneManager.LoadScene("SampleScene");
    }

}
