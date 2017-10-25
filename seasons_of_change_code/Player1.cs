using UnityEngine;
using System.Collections;

public class Player1 : MonoBehaviour {

    private Rigidbody2D myRigidbody2D;

    // movement attributes
    public float moveSpeed;
    public float jumpSpeed;
    public bool grounded;
    public int cooldown;

    public LayerMask whatIsGround;
    public Transform groundCheck;
    public float groundDistance;
    private bool doubleJumped;

    public bool attacking;
    public int character;

	public int maxHealth = 3;
    public int currentHealth;
    public int invincibilityTime = 120;
    public int flashTime = 2;
    public GameObject red;
    public GameObject gameOver;

    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
	private GameObject winter;
	public GameObject frozen;
	private double frozenDelay;




    public bool onLadder;
    public float climbSpeed;
    private float climbVelocity;
    private float gravityStore = 1.5f;

    public bool slide;

    //public GameObject weapon;


    // Use this for initialization
    void Start () {
        cooldown = 0;
        myRigidbody2D = GetComponent<Rigidbody2D>();
        character = GetComponent<CharacterSelect>().value;
		currentHealth = maxHealth;
        red.SetActive(false);
        gameOver.SetActive(false);
        //weapon.GetComponent<BoxCollider2D>();

		winter = GameObject.Find("WinterForm");
		frozen = GameObject.Find("Frozen Lake");

		frozenDelay = 0;
		frozen.GetComponent<Renderer>().material.color= new Color(1, 1, 1, 0);
//        count = getCount.Length;
//        zombieCount = 6;
		frozen.SetActive(false);
		slide = false;
    }

    void FixedUpdate() { grounded = Physics2D.OverlapCircle(groundCheck.position, groundDistance, whatIsGround); }

    // Update is called once per frame
    void Update () {
    	

        character = GetComponent<CharacterSelect>().value;
        switch (character)
        {
          case 1: moveSpeed = 8; jumpSpeed = 10; break;
          case 2: moveSpeed = 12; jumpSpeed = 10; break;
          case 3: moveSpeed = 8; jumpSpeed = 10; break;
          case 4: moveSpeed = 8; jumpSpeed = 10; break;
        }
        if (!slide){
        	Walk();
        }
        else{
			myRigidbody2D.velocity = new Vector2(20, myRigidbody2D.velocity.y);
        }
        Jump();
        Attack();
        if (cooldown > 0) { cooldown--; }

        if (onLadder)
        {
            //myRigidbody2D.gravityScale = 0f;

            climbVelocity = climbSpeed * Input.GetAxisRaw("Vertical");
            myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, climbVelocity);
        }
        if (!onLadder)
        {
            myRigidbody2D.gravityScale = gravityStore;
        }

		if (currentHealth < 1){
        	Destroy(this.gameObject);
        	gameOver.SetActive(true);

        }

        if (invincibilityTime > 0){
        invincibilityTime -=1;
        }
       

        if (flashTime > 0){
        	flashTime -=1;
        }
        else {
        	red.SetActive(false);
        }

		
    }


    public void Walk()
    {
        if (Input.GetKey(KeyCode.A)) { myRigidbody2D.velocity = new Vector2(-moveSpeed, myRigidbody2D.velocity.y); }
        else if (Input.GetKey(KeyCode.D)) { myRigidbody2D.velocity = new Vector2(moveSpeed, myRigidbody2D.velocity.y); }
        else { myRigidbody2D.velocity = new Vector2(0f, myRigidbody2D.velocity.y); }
        if (GetComponent<Rigidbody2D>().velocity.x < 0) { transform.localScale = new Vector3(-1f, 1f, 1f); }
        else if (GetComponent<Rigidbody2D>().velocity.x > 0) { transform.localScale = new Vector3(1f, 1f, 1f); }
    }


    public void Jump()
    {
        if (grounded) { doubleJumped = false; }
        if (Input.GetKeyDown(KeyCode.Space) && grounded) { myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, jumpSpeed); }
        if (Input.GetKeyDown(KeyCode.Space) && !grounded && !doubleJumped && character == 4)
        {
            doubleJumped = true;
            myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, jumpSpeed);
        }
    }

    public void Attack()
    {
        if (attacking) { attacking = false;}
        while (Input.GetKey(KeyCode.E) && cooldown <= 0)
        {
            attacking = true;
            cooldown = 20;
        }
		

    }

	private void OnTriggerEnter2D(Collider2D other)
    {
		if (other.tag == "Enemy" && invincibilityTime == 0){
        	currentHealth-=1;
        	//int heartno = currentHealth + 1;
        	if (currentHealth == 3){
        		heart1.SetActive(true);
				heart2.SetActive(true);
				heart3.SetActive(true);
        	}
        	else if (currentHealth == 2){
				heart1.SetActive(true);
				heart2.SetActive(true);
				heart3.SetActive(false);
        	}
        	else if (currentHealth == 1){
        		heart1.SetActive(true);
        		heart2.SetActive(false);
        		heart3.SetActive(false);
        	}
        	else {
				heart1.SetActive(false);
        		heart2.SetActive(false);
        		heart3.SetActive(false);
        	}

        	invincibilityTime = 120;

        	red.SetActive(true);
        	flashTime = 10;

        }

		if (other.tag == "Zombie" && cooldown == 0 && invincibilityTime == 0){
			currentHealth-=1;
        	//int heartno = currentHealth + 1;
        	if (currentHealth == 3){
        		heart1.SetActive(true);
				heart2.SetActive(true);
				heart3.SetActive(true);
        	}
        	else if (currentHealth == 2){
				heart1.SetActive(true);
				heart2.SetActive(true);
				heart3.SetActive(false);
        	}
        	else if (currentHealth == 1){
        		heart1.SetActive(true);
        		heart2.SetActive(false);
        		heart3.SetActive(false);
        	}
        	else {
				heart1.SetActive(false);
        		heart2.SetActive(false);
        		heart3.SetActive(false);
        	}

        	invincibilityTime = 120;

        	red.SetActive(true);
        	flashTime = 10;

        	}


        if (winter.activeInHierarchy && other.tag == "water"){
        	frozen.SetActive(true);
        	frozen.GetComponent<Renderer>().material.color= new Color(1, 1, 1, (float)(frozenDelay + 0.1));
        }

        if (other.tag == "Slide"){
        	slide = true;
        }
    }


	private void OnTriggerStay2D(Collider2D other){
		if (winter.activeInHierarchy && other.tag == "water"){
			if (frozenDelay < 1){
			frozenDelay += 0.1;
			}
        	frozen.GetComponent<Renderer>().material.color= new Color(1, 1, 1, (float)frozenDelay);
        }
	}

	private void OnTriggerExit2D(Collider2D other){
		slide = false;
//		frozenDelay = 0;
//		frozen.GetComponent<Renderer>().material.color= new Color(1, 1, 1, (float)frozenDelay);
//		frozen.SetActive(false);
	}

}
