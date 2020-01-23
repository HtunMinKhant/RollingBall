using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player3 : MonoBehaviour
{
    protected Joystick joystick;                       //for joystick declare
    protected Joybutton joybutton;                    //for joybutton  declare

    protected bool jump =false;
    Rigidbody rb;


    [SerializeField] Text livesText;
    [SerializeField] Text scoreText;
    [SerializeField] Text winText;

    [SerializeField] float playerspeed = 10;

    [SerializeField] float throwSpeed = 10;

    float teleportCooldown = 1f;
    float teleportTimer = 0;
    bool isTeleported = false;

    int score = 0;
    int lives = 3;
    // Start is called before the first frame update
    Vector3 restartPoint;
    void Start()
    {
        joystick = FindObjectOfType<Joystick>();                         //To defined the type of joystick
        joybutton = FindObjectOfType<Joybutton>();                       //To defined the type of joybutton
        restartPoint = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    
    void Update()
    {

        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);

        var rigidbody = GetComponent<Rigidbody>();                                //declaring the Rigidbody
        rigidbody.velocity = new Vector3(joystick.Horizontal * 5f + Input.GetAxis("Horizontal") * 5f,           //making connect with joystick to move the player
                                          rigidbody.velocity.y ,

                                          joystick.Vertical * 5f +Input.GetAxis("Vertical") * 5f);



        if (transform.position.y < -10)
        {
            Die();
        }


        if (jump && (joybutton.Pressed || Input.GetKeyDown(KeyCode.Space))) //!joybutton.Pressed
        {
                rigidbody.velocity += Vector3.up * 7f;
                jump = false;
            
        }

        //if (jump && !joybutton.Pressed)
        //{
        //  jump = false;
        //}

        if (isTeleported)
        {
            teleportTimer += Time.deltaTime;
            if(teleportTimer > teleportCooldown)
            {
                teleportTimer = 0;
                isTeleported = false;
            }
        }
        //Teleported cooldown and function

    }
    private void OnCollisionEnter(Collision collision)
    {
        jump = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Goal"))
        {
            Destroy(other.gameObject);
            score = score + 1;
            UpdateScore(score);

        }
        else if (other.CompareTag("Trap"))
        {
            Die();
        }
        else if (other.CompareTag("Restart"))
        {
            Destroy(other.gameObject);
            restartPoint = transform.position;
        }
        else if (other.CompareTag("teleport") && !isTeleported)
        {
            transform.position = other.GetComponent<Teleport>().linkedTeleportRoom.position;
            rb.velocity *= 1;
            isTeleported = true;
            restartPoint = transform.position;
        }
    }
    void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score;
        if (score == 20)
        {
            if (SceneManager.GetActiveScene().buildIndex == 6)
            {
                SceneManager.LoadScene(7);
            }
        }
    }

    void Die()
    {
        rb.Sleep();
        transform.position = restartPoint;
        rb.WakeUp();

        lives--;
        UpdateLives(lives);
        if (lives < 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    void UpdateLives(int Lives)
    {
        livesText.text = "Lives: " + lives;
    }
}
