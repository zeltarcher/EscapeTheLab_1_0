using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    // Config
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(25f, 25f);

    float gravityScaleAtStart;

    // State
    bool isAlive = true;

    // Cached component references
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider2D;
    BoxCollider2D myFeet;

    // Start is called before the first frame update
    // Message then methods
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider2D = GetComponent<CapsuleCollider2D>();
        gravityScaleAtStart = myRigidBody.gravityScale;
        myFeet = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) { return; }
        Run();
        Jump();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    private void Run() {
        // Check if the player pressed the button
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        // Assign the x and y values to vector2
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
        // Assign value to RigidBody2D.velocity
        myRigidBody.velocity = playerVelocity;

        // Change to running animation
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
            myAnimator.SetBool("Running", playerHasHorizontalSpeed);

    }
    private void ClimbLadder() {
        // Check if the player touches the ladder
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Climbing"))) 
        {
            // If false, the animation is not climbing
            myAnimator.SetBool("Climbing", false);

            // Assign the gravity to make sure the character does not slide down when on ladder
            myRigidBody.gravityScale = gravityScaleAtStart;
            return; 
        }

        // Check if the pressure the player presses the button. EX: How long
        float controlThrow = CrossPlatformInputManager.GetAxis("Vertical"); //Check if the player presses the button
        Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, controlThrow * climbSpeed); //Create vector 2D to add new values
        myRigidBody.velocity = climbVelocity;
        // Prevent player from sliding down when on ladder
        myRigidBody.gravityScale = 0f;

        // To insert the climbing animation
        bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon; // Check if the player has Vertical speed
        myAnimator.SetBool("Climbing", playerHasVerticalSpeed);

    }



    private void Jump() {
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))) {
            //myAnimator.SetBool("Jumping", false);
            return; 
        }

        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidBody.velocity += jumpVelocityToAdd;
            //bool playerHasVerticalJumpSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon; // Check if the player has Vertical speed
            //myAnimator.SetBool("Jumping", playerHasVerticalJumpSpeed);
        }
    }

    private void Die() {
        if ((myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy","Hazards"))) || 
            (myFeet.IsTouchingLayers(LayerMask.GetMask("Enemy","Hazards")))) {
            myAnimator.SetTrigger("Dying");
            GetComponent<Rigidbody2D>().velocity = deathKick;
            isAlive = false;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
    private void FlipSprite() {
        // Check if the player is starting moving or not
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        // If the player is moving
        if (playerHasHorizontalSpeed) {
            // Assign -1 or 1 to flip the character
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }
}


