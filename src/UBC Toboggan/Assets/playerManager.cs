using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class playerManager : MonoBehaviour
{
    public Rigidbody2D rb;

    public float angurlarAcceleration = 10;
    public float thrust = 2f;
    public float jumpThrust = 10f;
    public Collider2D skiTrigger;
    public Collider2D playerTrigger;

    public float maxBoost = 10f;
    public float boostAmount = 10f;
    public float boostPerSecond = 1f;
    public float jumpCost = 0.5f;
    public float deathTime = 2f;
    public float flipBonus = 5f;
    public float airTimeMultiplier = 1f;

    // https://youtu.be/lKEKTWK9efE?t=336
    public GameObject floatingTextPrefab;

    GameObject flipBonusText;
    GameObject airBonusText;

    public AudioSource slideAudio;
    public AudioSource windAudio;
    public AudioSource boostAudio;
    public AudioSource boostStartAudio;


    float flipScore = 0f;
    float airScore = 0f;
    bool boosting = false;

    bool grounded = false;
    bool alive = true;
    bool showingBonus = false;

    float bonusCooldown = 0f;

    int numberOfSpins = 0;
    float angleFromStart = 0f;
    float previousEulerAngle = 0f;
    float currentEulerAngle = 0f;
    float trickStartAngle = 0f;
    float airTime = 0f;
    int intAirTime = 0;

    public Animator fire;



    // Start is called before the first frame update
    void Start()
    {
        flipBonusText = GameObject.FindGameObjectWithTag("flipBonusText");
        airBonusText = GameObject.FindGameObjectWithTag("airBonusText");
                    
        flipBonusText.SetActive(false);
        airBonusText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (alive) {

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

            // test if player flipped
            if (!grounded && Mathf.Abs(angleFromStart - trickStartAngle) >= 360) {
                trickStartAngle = angleFromStart;
                Flipped();
            }

            previousEulerAngle = currentEulerAngle;

            // test air time
            if (!grounded) {
                airTime += Time.deltaTime;

                if (Mathf.FloorToInt(airTime) > intAirTime) {
                    intAirTime = Mathf.FloorToInt(airTime);
                    handleAirTime(intAirTime);
                }
            }

            // test is a/d is pressed and rotate
            if (Input.GetButton("Horizontal"))
            {
                rb.angularVelocity = rb.angularVelocity + angurlarAcceleration*Time.deltaTime*-Input.GetAxisRaw("Horizontal"); // try only in air
            }
            // test if w/s is pressed, ignore s presses and trigger jump
            if (Input.GetButtonDown("Vertical"))
            {
                if (Input.GetAxisRaw("Vertical") == 1 && grounded && boostAmount > 0)
                {
                    rb.AddRelativeForce(Vector3.up * jumpThrust);

                    boostAmount -= jumpCost;
                }
            }
            // test if space is pressed and handle boost
            if (Input.GetButton("Jump") && boostAmount > 0)
            {
                rb.AddRelativeForce(Vector3.right * thrust);

                boostAmount -= boostPerSecond*Time.deltaTime;
                
                if (boosting == false) {
                    fire.SetFloat("isBoosting", 1f); // check if can make bool
                    boostAudio.Play();
                    boostStartAudio.Play();
                    boosting = true;
                }
            }
            else {
                if (boosting == true) {
                    fire.SetFloat("isBoosting", 0f);
                    boostAudio.Stop();
                    boostStartAudio.Stop();
                    boosting = false;
                }
            }
            
            // begin showing score text when a point is gained in the air
            if (!grounded && !showingBonus && (flipScore > 0f || airScore > 0f)) {
                updateFlipText();
                updateAirText();

                flipBonusText.SetActive(true);
                airBonusText.SetActive(true);
                showingBonus = true;
            }
            
            // handle removing the bonus text if player is on ground for x seconds
            if (showingBonus && grounded) {
                bonusCooldown += Time.deltaTime;
                
                if (bonusCooldown > 0.25) {
                    showingBonus = false;

                    scoreManager.score += airScore;
                    scoreManager.score += flipScore;

                    flipScore = 0f;
                    airScore = 0f;
                    
                    flipBonusText.SetActive(false);
                    airBonusText.SetActive(false);
                }

            }
            // update audio levels based on velocity
            if (grounded) {
                slideAudio.volume = rb.velocity.magnitude/100f;
            }
            windAudio.volume = rb.velocity.magnitude/50f+0.1f;
        }
        // if died wait a bit and respawn after specified delay
        else {
            deathTime -= Time.deltaTime;
            
            if (deathTime <= 0) {
                SceneManager.LoadScene("Farm");
            }
        }
    }

    void Flipped() {
        flipScore += flipBonus;
        updateFlipText();
    }

    void handleAirTime(int seconds) {
        // add and display points
        float addedScore = seconds * airTimeMultiplier;
        airScore += addedScore;
        updateAirText();
    }

    void updateFlipText() {
        flipBonusText.GetComponent<Text>().text = "Flip Bonus: +" + flipScore.ToString();
    }

    void updateAirText() {
        airBonusText.GetComponent<Text>().text = "Air Bonus: +" + airScore.ToString();
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (skiTrigger.IsTouching(collider) && collider.tag == "ground") {
            grounded = true;
            intAirTime = 0;
            airTime = 0f;
            slideAudio.volume = 0f;
            slideAudio.Play();
        }
        if (playerTrigger.IsTouching(collider) && collider.tag == "ground") {
            alive = false;
            fire.SetFloat("isBoosting", 0f);
            boostAmount = 0f;
        }
    }
    
    void OnTriggerExit2D(Collider2D collider) {
        if (!skiTrigger.IsTouching(collider) && collider.tag == "ground") {
            grounded = false;
            trickStartAngle = angleFromStart;
            
            bonusCooldown = 0f;
            slideAudio.Stop();
        }
    }

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

    // void showPoints(string text) {
    //     if (floatingTextPrefab) {
    //         GameObject prefab = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
    //         prefab.GetComponentInChildren<TextMesh>().text = text;
    //     }
    // }
}
