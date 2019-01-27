using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{

    public static float Food;      
    public static float Temprature;
    public AudioClip fireSound;
    public AudioSource fireSource;
    public AudioClip appleSound;
    public AudioSource appleSource;
    public AudioClip wilheimSound;
    public AudioSource wilheimSource;

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
    [SerializeField]
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
    List<string> messagePit;
    [SerializeField]
    List<string> messageFood;
    [SerializeField]
    List<string> messageFreezing;
    [SerializeField]
    List<string> messageOverheating;
    [SerializeField]
    List<string> messageGeneral;



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
        Temprature = 89;
        startCoordX = (int)transform.position.x;
        ReadDeath();
        restart.onClick.AddListener(Retry);
        fireSource.clip = fireSound;
        appleSource.clip = appleSound;
        wilheimSource.clip = wilheimSound;
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

        Animator anim = this.GetComponent<Animator>();
        RuntimeAnimatorController an;
        if (speed > 0 ) 
        {
             an = Resources.Load<RuntimeAnimatorController>("Animations/Player walking right");
        }
        else if(speed < 0) 
        {
            an = Resources.Load<RuntimeAnimatorController>("Animations/Player walking left");
        }
        else 
        {
            an = null;
        }
        anim.runtimeAnimatorController = an;

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
        if(!isDead){
            Food -= (timeNow / 500);
        }

        if(Food <= 0 && !isDead)
        {
            KillPlayer("Starvation");
        }
    }

    //Decreases temprature while not near a fireplace
    void TempratureHandler()
    {
        //Check if player is near fire and raise or lower temp
        if (!isDead) {
            if (!isNearFire)
            {
                Temprature -= (timeNow / 1000);
            } else Temprature += 0.01f;
        }
 
        //Kills the player :)
        if(Temprature <= 0 && !isNearFire && !isDead)
        {
            KillPlayer("Hypothermia");
        }

        if(Temprature >= 100 && isNearFire && !isDead)
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
            fireSource.Play();
        }

        if(col.gameObject.tag == "Pit")
        {
            KillPlayer("Pit");
            wilheimSource.Play();
        }

        if (col.gameObject.tag == "Apple")
        {
            appleSource.Play();
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
            fireSource.Stop();
        }
    }

    void KillPlayer(string reason)
    {
        isDead = true;
        DeathScreen.SetActive(true);
        windSpeed = 0;
        acceleration = 0;
        jumpSpeed = 0;


        int i = Random.Range(0, 10);
        if (i < 5) {
            txt_DeathMessage.text = messageGeneral[Random.Range(0, messageGeneral.Count)];
        }

        if(reason == "Pit")
        {
          txt_DeathMessage.text = messagePit[Random.Range(0, messagePit.Count)];
        }

        if(reason == "Starvation")
        {
            txt_DeathMessage.text = messageFood[Random.Range(0, messageFood.Count)];
        }

        if(reason == "Hypothermia")
        {
            txt_DeathMessage.text = messageFreezing[Random.Range(0, messageFreezing.Count)];

        }

        if(reason == "Hyperthermia")
        {
            txt_DeathMessage.text = messageOverheating[Random.Range(0, messageOverheating.Count)];
        }

    }

    void Retry()
    {
        SceneManager.LoadScene("SampleScene");
    }

    void ReadDeath()
    {
        string path = "Assets/Text/Death messages.txt";
        StreamReader reader = new StreamReader(path);
        string line;
        reader.ReadLine();
        while ((line = reader.ReadLine()) != "--Falls--")
        {
            messageGeneral.Add(line);
        }

        while ((line = reader.ReadLine()) != "--Fire--")
        {
            messagePit.Add(line);
        }

        while ((line = reader.ReadLine()) != "--Apples--")
        {
            messageFreezing.Add(line);
        }

        while ((line = reader.ReadLine()) != null)
        {
            messageFood.Add(line);
        }



        reader.Close();
    }

}
