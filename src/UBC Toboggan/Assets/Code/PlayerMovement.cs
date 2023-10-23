using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class PlayerMovement : MonoBehaviour
{
    public GameObject scoreText;
    // public GameObject soundSystem;
    public Rigidbody2D body;
    public Collider2D headCollisionTrigger;
    public Collider2D touchingGroundTrigger;
    public float jumpHeight;
    public float boostSpeed;
    public float boostTime;
    public float rotateBy;
    public Animator fireAnimator;

    soundManager sm;
    UIManager manager;
    BoostTimer boostTimer;
    ScoreManager scoreManager;
    float dX;
    float dY;
    float jumpSpeed;
    float airTime = 0f;
    int intAirTime = 0;
    int numberOfSpins = 0;
    float angleFromStart = 0f;
    float previousEulerAngle = 0f;
    float currentEulerAngle = 0f;
    float trickStartAngle = 0f;
    bool isJumping;
    bool isBoosting = false;
    bool canJump;
    bool canRotate;

    public bool isInAir => canRotate;

    public BoostTimer boost => boostTimer;

    // Start is called before the first frame update
    void Start()
    {
        boostTimer = new BoostTimer(boostTime);
        manager = GetComponentInParent<UIManager>();
        jumpSpeed = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * body.gravityScale));
        scoreManager = scoreText.GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        dX = Input.GetAxisRaw("Horizontal");
        isBoosting = dX > 0.1f ? true : false;
        dY = Input.GetAxisRaw("Vertical");
        isJumping = Input.GetKey(KeyCode.Space);
    }

    void FixedUpdate()
    {
        if (isBoosting)
        {
            if (canBoost())
            {
                body.AddForce(new Vector2(dX * boostSpeed, 0f), ForceMode2D.Impulse);
                boostTimer.countDownBy(Time.deltaTime);
                fireAnimator.SetFloat("isBoosting", 1f);
            } else 
            {
                fireAnimator.SetFloat("isBoosting", 0f);
            }
        } else
        {
            fireAnimator.SetFloat("isBoosting", 0f);
        }

        if (isJumping && canJump) 
        {
            body.AddForce(new Vector2(0f, jumpSpeed), ForceMode2D.Impulse);
        }
        
        if ((dY > 0.1f || dY < -0.1f) && canRotate)
        {
            float impulse = (Mathf.Sign(dY) * rotateBy * Mathf.Deg2Rad) * body.inertia;
            if (body.angularVelocity >= -500f && body.angularVelocity <= 500f)
            {
                body.AddTorque(impulse, ForceMode2D.Impulse);
            }
            else if (body.angularVelocity > 500f && impulse < 0)
            {
                body.AddTorque(impulse, ForceMode2D.Impulse);
            }
            else if (body.angularVelocity < -500f  && impulse > 0)
            {
                body.AddTorque(impulse, ForceMode2D.Impulse);
            }
        }

        if (canRotate)
        {
            updateScore();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (headCollisionTrigger.IsTouching(collider))
        {
            boostTimer.pauseTimer();
            fireAnimator.SetFloat("isBoosting", 0f);
            canJump = false;
            canRotate = false;
        } else if (touchingGroundTrigger.IsTouching(collider))
        {
            canJump = true;
            canRotate = false;
	    }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (headCollisionTrigger.IsTouching(collider))
        {
            boostTimer.pauseTimer();
            fireAnimator.SetFloat("isBoosting", 0f);
            canJump = false;
            canRotate = false;
            if (!collider.isTrigger)
            {
                StartCoroutine(LoadGameOverScreen());
            }
        } else if (touchingGroundTrigger.IsTouching(collider))
        {
            canJump = true;
            canRotate = false;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (!headCollisionTrigger.IsTouching(collider))
	    {
	        boostTimer.playTimer();
            if (isBoosting && canBoost())
            {
                fireAnimator.SetFloat("isBoosting", 1f);
            }
        }
        
        if (!touchingGroundTrigger.IsTouching(collider))
        {
            canJump = false;
            canRotate = true;
        }
    }

    bool canBoost()
    {
        return !boostTimer.isTimerPaused && !boostTimer.isTimerComplete;
    }

    void updateScore()
    {
        currentEulerAngle = transform.eulerAngles.z;
        if (Mathf.Abs(currentEulerAngle - previousEulerAngle) > 180)
        {
            if (currentEulerAngle > 180)
            {
                numberOfSpins += 1;
            } else 
            {
                numberOfSpins -= 1;
            }
        }
        angleFromStart = (numberOfSpins * 360) - currentEulerAngle;

        if (Mathf.Abs(angleFromStart - trickStartAngle) >= 360)
        {
            trickStartAngle = angleFromStart;
            scoreManager.addFlipScore();
            // soundManager.playSound(pointsAudio);
        }
        previousEulerAngle = currentEulerAngle;

        airTime += Time.deltaTime;
        if (Mathf.FloorToInt(airTime) > intAirTime)
        {
            intAirTime = Mathf.FloorToInt(airTime);
            scoreManager.addAirTimeScore(intAirTime);
            // soundManager.playSound(pointsAudio);
        }
    }

    public bool collectBeer(float amount) {
        bool ret = false;
        if (!boostTimer.isTimerComplete) {
            boostTimer.countDownBy(-amount);    
            ret = true;
        }
        return ret;
    }

    private IEnumerator LoadGameOverScreen()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        manager.ShowGameOverScreen();
    }
}
