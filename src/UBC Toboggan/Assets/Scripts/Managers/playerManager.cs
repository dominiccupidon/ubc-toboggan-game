using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class playerManager : MonoBehaviour
{
    public Rigidbody2D rb;
    public Collider2D skiTrigger;
    public Collider2D playerTrigger;

    public float angurlarAcceleration = 10;
    public float thrust = 2f;
    public float jumpThrust = 10f;
    public float maxBoost = 10f;
    public float boostAmount = 10f;
    public float boostPerSecond = 1f;
    public float jumpCost = 0.5f;
    public float deathTime = 2f;
    public bool grounded = false;
    public int hatRecoil = 1000;
    
    public AudioSource slideAudio;
    public AudioSource windAudio;
    public AudioSource boostAudio;
    public AudioSource boostStartAudio;
    public AudioSource pointsAudio;
    public AudioSource jumpAudio;

    public GameObject scoreSystem;
    scoreManager scoreManagerScript;

    public GameObject soundSystem;
    soundManager soundManagerScript;

    public GameObject hatObject;
    
    
    public Animator fire;

    bool boosting = false;
    bool alive = true;
    int numberOfSpins = 0;
    float angleFromStart = 0f;
    float previousEulerAngle = 0f;
    float currentEulerAngle = 0f;
    float trickStartAngle = 0f;
    float airTime = 0f;
    int intAirTime = 0;

    public bool wearingHat = false;

    public Vector2 defaultVelocity = new Vector2(0,0);

    // Start is called before the first frame update
    void Start()
    {
        // setup controller support
        // controls = new PlayerControls();
        // controls.Gameplay.Boost += ctx => boost();

        scoreManagerScript = scoreSystem.GetComponent<scoreManager>();
        soundManagerScript = soundSystem.GetComponent<soundManager>();
        
        rb.velocity = defaultVelocity;
    }

    // void jump() {

    // }

    // void boost() {

    // }

    // void

    // Update is called once per frame
    void Update()
    {
        if (alive && Time.timeScale == 1f) {
            // update current player angle
            currentEulerAngle = transform.eulerAngles.z;
            // test for player passing the 360 mark
            if (Mathf.Abs(currentEulerAngle - previousEulerAngle) > 180) {
                if (currentEulerAngle > 180) {
                    numberOfSpins += 1;
                }
                else {
                    numberOfSpins -= 1;
                }
            }
            
            angleFromStart = numberOfSpins*360-currentEulerAngle;

            // handle flips
            if (!grounded && Mathf.Abs(angleFromStart - trickStartAngle) >= 360) {
                trickStartAngle = angleFromStart;
                scoreManagerScript.addFlipScore();
                soundManagerScript.playSound(pointsAudio);
            }

            previousEulerAngle = currentEulerAngle;

            // handle air time
            if (!grounded) {
                airTime += Time.deltaTime;

                if (Mathf.FloorToInt(airTime) > intAirTime) {
                    intAirTime = Mathf.FloorToInt(airTime);
                    scoreManagerScript.addAirTimeScore(intAirTime);
                    soundManagerScript.playSound(pointsAudio);
                }
            }

            // ROTATE: test is a/d is pressed and rotate
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                rb.angularVelocity = rb.angularVelocity + angurlarAcceleration*Time.deltaTime*-Input.GetAxis("Horizontal"); // try only in air
                Debug.Log(Input.GetAxis("Horizontal"));
            }
            
            // JUMP: test if w/s is pressed, ignore s presses and trigger jump
            if (Input.GetButtonDown("Vertical"))
            {
                if (Input.GetAxisRaw("Vertical") > 0 && grounded)
                {
                    rb.AddRelativeForce(Vector3.up * jumpThrust);

                    soundManagerScript.playSound(jumpAudio);
                }
            }
            
            // BOOST: test if space is pressed and handle boost
            if (Input.GetButton("Jump") && boostAmount > 0)
            {
                rb.AddRelativeForce(Vector3.right * thrust * Time.deltaTime);

                boostAmount -= boostPerSecond*Time.deltaTime;
                
                if (boosting == false) {
                    fire.SetFloat("isBoosting", 1f); // check if can make bool
                    soundManagerScript.playSound(boostAudio);
                    soundManagerScript.playSound(boostStartAudio);
                    boosting = true;
                }
            }
            else {
                if (boosting == true) {
                    fire.SetFloat("isBoosting", 0f);
                    soundManagerScript.stopSound(boostAudio);
                    soundManagerScript.stopSound(boostStartAudio);
                    boosting = false;
                }
            }
        }
    }

    // handles collisions with landing on the ground or getting killed by ground or falling in a pit
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (skiTrigger.IsTouching(collider) && collider.tag == "ground")
        {
            grounded = true;
            intAirTime = 0;
            airTime = 0f;
            slideAudio.volume = 0f;
            soundManagerScript.playSound(slideAudio);
        }
        if (skiTrigger.IsTouching(collider) && collider.tag == "pit")
        {
            alive = false;
            fire.SetFloat("isBoosting", 0f);
            boostAmount = 0f;
            StartCoroutine(LoadGameOverScreen());
        }

        if (playerTrigger.IsTouching(collider) && collider.tag == "ground") {
            if (wearingHat) {
                wearingHat = false;
                setHatSprite(false);
                rb.AddRelativeForce(Vector3.down * hatRecoil);
            } else {
                alive = false;
                fire.SetFloat("isBoosting", 0f);
                boostAmount = 0f;
                StartCoroutine(LoadGameOverScreen());
            }
        }
    }
    
    // handles leaving the ground
    void OnTriggerExit2D(Collider2D collider) {
        if (!skiTrigger.IsTouching(collider) && collider.tag == "ground") {
            grounded = false;
            trickStartAngle = angleFromStart;

            soundManagerScript.stopSound(slideAudio);
        }
    }

    // handles collecting beer
    public bool collectBeer(float amount) {
        if (boostAmount < maxBoost) {
            boostAmount += amount;
            if (boostAmount > maxBoost) {
                boostAmount = maxBoost;
            }
            return true;
        }
        else {
            return false;
        }
    }

    public bool collectHat() {
        if (wearingHat == false) {
            wearingHat = true;
            setHatSprite(true);
            return true;
        }
        return false;
    }

    void setHatSprite(bool displaying) {
        hatObject.GetComponent<SpriteRenderer>().enabled = displaying;
    }

    private IEnumerator LoadGameOverScreen()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(1.0f);
        UIManager.Instance.ShowGameOverScreen();
    }
}

