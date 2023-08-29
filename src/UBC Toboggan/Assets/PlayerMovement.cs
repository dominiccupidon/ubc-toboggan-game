using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D body;
    public Collider2D headCollisionTrigger;
    public Collider2D touchingGroundTrigger;
    public float jumpHeight;
    public float boostSpeed;
    public float boostTime;
    public float rotateBy;
    UIManager manager;
    Timer boostTimer;
    float dX;
    float dY;
    float jumpSpeed;
    bool isJumping;
    bool canJump;
    bool canRotate;

    // Start is called before the first frame update
    void Start()
    {
        boostTimer = new Timer(boostTime);
        manager = GetComponentInParent<UIManager>();
        jumpSpeed = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * body.gravityScale));
    }

    // Update is called once per frame
    void Update()
    {
        dX = Input.GetAxisRaw("Horizontal");
        dY = Input.GetAxisRaw("Vertical");
        isJumping = Input.GetKey(KeyCode.Space);
    }

    void FixedUpdate()
    {
        if (dX > 0.1f && !boostTimer.isTimerPaused && !boostTimer.isTimerComplete) 
        {
            body.AddForce(new Vector2(dX * boostSpeed, 0f), ForceMode2D.Impulse);
            boostTimer.countDownBy(Time.deltaTime);
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
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (headCollisionTrigger.IsTouching(collider))
        {
            boostTimer.pauseTimer();
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
        }
        
        if (!touchingGroundTrigger.IsTouching(collider))
        {
            canJump = false;
            canRotate = true;
        }
    }

    private IEnumerator LoadGameOverScreen()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        manager.ShowGameOverScreen();
    }
}
