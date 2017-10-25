using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// script to control the player

public class Player : MonoBehaviour {

    private Rigidbody2D myRigidbody2D;

    // player attributes
    public float moveSpeed; // how fast the player moves
    public float jumpHeight; // how high the player jumps
    public bool grounded; // checks if the player is grounded
    public bool leftGrounded; // boolean for left ground check
    public bool rightGrounded; // boolean for right ground check
    public int cooldown; // cooldown time for weapons
    public int cooldownTimer; // cooldown timer for weapon/form currently used
    public bool attacking; // boolean set when player is attacking
    public int character; // character select value
    public float jumpCharge; // current jump charge
    public float maxJumpCharge; // the maximum jump charge
	public float dashSpeed;
	public float dashTime;
	public bool dashing;
	public bool forward;
	public int dashCooldown;
	public int dashTimer;


    public int gemCount;
    public Text DeathText;
    public Text gemText;
    public int deathtimer;

    // attribute to check if player can jump
    public LayerMask whatIsGround;
    public Transform leftGroundCheck;
    public Transform rightGroundCheck;
    public float groundDistance;
    private bool doubleJumped;

    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    private GameObject winter;
    public GameObject frozen;
    private double frozenDelay;

    public int maxHealth = 3;
    public int currentHealth;
    public int invincibilityTime = 120;
    public int flashTime = 2;
    public GameObject red;
    public GameObject gameOver;

    public bool onLadder;
    public float climbSpeed;
    private float climbVelocity;
    private float gravityStore = 4f;

    public bool slide;

    void Start () {
        cooldownTimer = 0;
        jumpCharge = 0;
        myRigidbody2D = GetComponent<Rigidbody2D>();
        character = GetComponent<CharacterSelect>().value;
        gemCount = 0;
        DeathText.enabled = false;
        deathtimer = 0;
        currentHealth = maxHealth;
        red.SetActive(false);
        gameOver.SetActive(false);
		dashing = false;
		dashSpeed = 200f;
		dashTime = 0.1f;
		dashCooldown = 300;
		dashTimer = 0;
		forward = true;
		myRigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

	void dashReset()
	{
		dashing = false;
	}

	void levelReset()
	{
		Application.LoadLevel (Application.loadedLevel);
	}


    void FixedUpdate() {
        // check if the player is on the ground
        leftGrounded = Physics2D.OverlapCircle(leftGroundCheck.position, groundDistance, whatIsGround);
        rightGrounded = Physics2D.OverlapCircle(rightGroundCheck.position, groundDistance, whatIsGround);

		if (dashing) {
			Dash ();
			Invoke ("dashReset", dashTime);
		}
    }

    void Update () {
        if (leftGrounded || rightGrounded)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        // swtich character
        character = GetComponent<CharacterSelect>().value; // receive current character select value
        switch (character)
        {
          case 1: moveSpeed = 18; jumpHeight = 19; cooldown = 10; maxJumpCharge = 0; break; // case 1: autumn form
          case 2: moveSpeed = 10; jumpHeight = 19; cooldown = 5; maxJumpCharge = 0; break; // case 2: spring form
          case 3: moveSpeed = 10; jumpHeight = 19; cooldown = 5; maxJumpCharge = 15; break; // case 3: summer form
          case 4: moveSpeed = 10; jumpHeight = 19; cooldown = 60; maxJumpCharge = 0; break; // case 4: winter form
          case 5: moveSpeed = 10; jumpHeight = 19; cooldown = 5; maxJumpCharge = 0; break; // case 5: snowball form
        }

        if (character == 5)
        {
            myRigidbody2D.constraints = RigidbodyConstraints2D.None;
        }
        else
        {
            transform.localRotation = Quaternion.identity;
            myRigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        // action functions
		Walk(); Jump(); Attack(); Special();
        if (cooldownTimer > 0) { cooldownTimer--; }
		if (dashTimer > 0) { dashTimer--; }

        if (onLadder)
        {
            myRigidbody2D.gravityScale = 0f;

            climbVelocity = climbSpeed * Input.GetAxisRaw("Vertical");
            myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, climbVelocity);
        }
        if (!onLadder)
        {
            myRigidbody2D.gravityScale = gravityStore;
        }
        if (DeathText.enabled == true)
        {
            deathtimer--;
            
        }
        else
        {
            deathtimer = 100;
        }
        if (deathtimer == 0)
        {
            DeathText.enabled = false;
            gameOver.SetActive(false);
            currentHealth = maxHealth;
        }
        if (currentHealth < 1)
        {
            //Destroy(this.gameObject);
            gameOver.SetActive(true);
			red.SetActive(true);
			flashTime = 200;
			Invoke ("levelReset", 3);
        }

        if (invincibilityTime > 0)
        {
            invincibilityTime -= 1;
        }


        if (flashTime > 0)
        {
            flashTime -= 1;
        }
        else
        {
            red.SetActive(false);
        }

    }

    // function to move the player left and right
    public void Walk()
    {
        // move the player left and right
        if (Input.GetKey(KeyCode.A))
        {
            if (character == 5)
            {
                transform.Rotate(Vector3.left * Time.deltaTime);
                myRigidbody2D.velocity = new Vector2(-moveSpeed, myRigidbody2D.velocity.y);

            }
            else { myRigidbody2D.velocity = new Vector2(-moveSpeed, myRigidbody2D.velocity.y); }
        }
        else if (Input.GetKey(KeyCode.D)) {
            if (character == 5)
            {
                transform.Rotate(Vector3.right * Time.deltaTime);
                myRigidbody2D.velocity = new Vector2(moveSpeed, myRigidbody2D.velocity.y);

            }
            else { myRigidbody2D.velocity = new Vector2(moveSpeed, myRigidbody2D.velocity.y); }
        }
        else { myRigidbody2D.velocity = new Vector2(0f, myRigidbody2D.velocity.y); }

        // flip the player sprite based on if they are facing/moving left or right
		if (GetComponent<Rigidbody2D>().velocity.x < 0) { transform.localScale = new Vector3(-1f, 1f, 1f); forward = false;}
		else if (GetComponent<Rigidbody2D>().velocity.x > 0) { transform.localScale = new Vector3(1f, 1f, 1f); forward = true;}
    }

    // function to make the player jump
	public void Jump()
	{
		// single jump
		if (grounded) { doubleJumped = false; } 
		if (character == 3 && grounded)
		{
			if (Input.GetMouseButton(1))
			{
				if (jumpCharge < maxJumpCharge) { jumpCharge += Time.deltaTime * 10f; }
			}
			else
			{
				if (jumpCharge > 0)
				{
					myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, jumpHeight + jumpCharge);
					jumpCharge = 0;
				}
			}
		}
		if (Input.GetKeyDown(KeyCode.Space) && grounded) { myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, jumpHeight); }

		// double jump (applies only to spring form)
		if (Input.GetKeyDown(KeyCode.Space) && !grounded && !doubleJumped && character == 2)
		{
			doubleJumped = true;
			myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, jumpHeight);
		}
	}

    // function to let the player attacka
    public void Attack()
    {
        if (attacking) { attacking = false; } // if the player has attacked, revert the boolean
		while (Input.GetMouseButton(0) && cooldownTimer <= 0) {
            attacking = true; // set the boolean to true if the player attacks
            cooldownTimer = cooldown; // reset the cooldown timer
        }

    }

	public void Special()
	{
		while (Input.GetMouseButton(1) && dashTimer <= 0) {
			if (character == 1) {
				dashTimer = dashCooldown;
				attacking = true; // set the boolean to true if the player attacks
				cooldownTimer = cooldown; // reset the cooldown timer
				dashing = true;
			} 
			/*else if (character == 2) 
			{
				//Spring Grapple Hook
			}
			else if (character == 3) 
			{
				if (jumpCharge < maxJumpCharge) { jumpCharge += Time.deltaTime * 10f; }
			}*/

			else
			{
				cooldownTimer = cooldown; // reset the cooldown timer
				dashTimer = dashCooldown;
			}
		}
	}

	public void Dash ()
	{
		if (forward)
			myRigidbody2D.AddForce (new Vector2 (dashSpeed, 0), ForceMode2D.Impulse);
		else
			myRigidbody2D.AddForce (new Vector2 (-dashSpeed, 0), ForceMode2D.Impulse);
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
		if (other.tag == "AutumnEnemy" && invincibilityTime == 0 && cooldownTimer == 0 || other.tag == "flying" && invincibilityTime == 0)
        {
            currentHealth -= 1;
            //int heartno = currentHealth + 1;
            if (currentHealth == 3)
            {
                heart1.SetActive(true);
                heart2.SetActive(true);
                heart3.SetActive(true);
            }
            else if (currentHealth == 2)
            {
                heart1.SetActive(true);
                heart2.SetActive(true);
                heart3.SetActive(false);
            }
            else if (currentHealth == 1)
            {
                heart1.SetActive(true);
                heart2.SetActive(false);
                heart3.SetActive(false);
            }
            else
            {
                heart1.SetActive(false);
                heart2.SetActive(false);
                heart3.SetActive(false);
            }

            invincibilityTime = 120;

            red.SetActive(true);
            flashTime = 10;

        }

        if (other.tag == "Slide")
        {
            slide = true;
        }
        if (other.gameObject.tag == "Gem")
        {
            gemCount += 1;
            gemText.text = "Gems Collected: " + gemCount.ToString() + "/8";
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "Spike")
        {
			gameOver.SetActive(true);
			red.SetActive(true);
			flashTime = 200;
			Invoke ("levelReset", 3);
        }
	
    }

    private void OnTriggerStay2D(Collider2D other)
    {
//        if (winter.activeInHierarchy && other.tag == "water")
//        {
//            if (frozenDelay < 1)
//            {
//                frozenDelay += 0.1;
//            }
//            frozen.GetComponent<Renderer>().material.color = new Color(1, 1, 1, (float)frozenDelay);
//        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        slide = false;
        //		frozenDelay = 0;
        //		frozen.GetComponent<Renderer>().material.color= new Color(1, 1, 1, (float)frozenDelay);
        //		frozen.SetActive(false);
    }


}
