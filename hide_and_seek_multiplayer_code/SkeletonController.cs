using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Networking;

public class SkeletonController : MonoBehaviour {

	public Animator animator;
	public static float globalGravity = -9.81f;
	private float gravityScale = 5.0f;
	private Vector3 movement;
	private float speed = 30.0f;
	public float velocityForce;
	public Transform cam;
	private float distToGround;
	private bool jumping;
	private bool attacking;
	private Rigidbody mBody;
	private SetupLocalPlayer localPlayer;
	public bool canMove;
	private float moveDelay;

	// Use this for initialization
	void Start () {
		canMove = false;
		moveDelay = 30.0f;
		cam = GameObject.Find ("Main Camera").transform;
		distToGround = GetComponent<Collider> ().bounds.extents.y;
		mBody = GetComponent<Rigidbody> ();
		mBody.useGravity = false;
		animator = this.GetComponent<Animator> ();
		jumping = false;
		localPlayer = GetComponent<SetupLocalPlayer> ();
	}

	// Update is called once per frame
	void Update () {
		if(moveDelay <= 0){
			startMovement();
		}
	}

	// Set player movement
	void FixedUpdate(){
		mBody.velocity = new Vector3 (0, mBody.velocity.y, 0);
		movement.x = Input.GetAxis ("Horizontal");
		movement.z = Input.GetAxis ("Vertical");
		Vector2 mouseMovement;
		mouseMovement.x = Input.GetAxis("Mouse X");
		mouseMovement.y = Input.GetAxis("Mouse X");
		if(mouseMovement != Vector2.zero && (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))){
			transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, cam.localEulerAngles.y, transform.localEulerAngles.z);	
		}
		float translation = Time.deltaTime * speed;
		animator.SetBool ("Run", false);
		localPlayer.CmdChangeAnimState ("Idle");
		if ((!this.animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && canMove) || (!isGrounded() && canMove))
		{
			if (Input.GetKey(KeyCode.S))
			{
				transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, cam.localEulerAngles.y - 180, transform.localEulerAngles.z);
				if (Input.GetKey(KeyCode.A))
				{
					transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, cam.localEulerAngles.y - 135, transform.localEulerAngles.z);
				}
				else if (Input.GetKey(KeyCode.D))
				{
					transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, cam.localEulerAngles.y - 225, transform.localEulerAngles.z);
				}
				animator.SetBool("Run", true);
				localPlayer.CmdChangeAnimState ("Run");
				mBody.velocity = new Vector3(transform.forward.x * velocityForce * Time.deltaTime, mBody.velocity.y, transform.forward.z * velocityForce * Time.deltaTime);
			}
			else if (Input.GetKey(KeyCode.W))
			{
				transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, cam.localEulerAngles.y, transform.localEulerAngles.z);
				if (Input.GetKey(KeyCode.A))
				{
					transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, cam.localEulerAngles.y - 45, transform.localEulerAngles.z);
				}
				else if (Input.GetKey(KeyCode.D))
				{
					transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, cam.localEulerAngles.y + 45, transform.localEulerAngles.z);
				}
				animator.SetBool("Run", true);
				localPlayer.CmdChangeAnimState ("Run");
				mBody.velocity = new Vector3(transform.forward.x * velocityForce * Time.deltaTime, mBody.velocity.y, transform.forward.z * velocityForce * Time.deltaTime);
			}
			if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
			{
				if (Input.GetKey(KeyCode.A))
				{
					transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, cam.localEulerAngles.y - 90, transform.localEulerAngles.z);
					translation = Time.deltaTime * speed;
					animator.SetBool("Run", true);
					localPlayer.CmdChangeAnimState ("Run");
					mBody.velocity = new Vector3(transform.forward.x * velocityForce * Time.deltaTime, mBody.velocity.y, transform.forward.z * velocityForce * Time.deltaTime);
				}
				else if (Input.GetKey(KeyCode.D))
				{
					transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, cam.localEulerAngles.y + 90, transform.localEulerAngles.z);
					translation = Time.deltaTime * speed;
					animator.SetBool("Run", true);
					localPlayer.CmdChangeAnimState ("Run");
					mBody.velocity = new Vector3(transform.forward.x * velocityForce * Time.deltaTime, mBody.velocity.y, transform.forward.z * velocityForce * Time.deltaTime);
				}
			}

			if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
			{
				mBody.velocity = new Vector3(mBody.velocity.x, 0, mBody.velocity.z);
				mBody.AddForce(0, 1000.0f, 0);
				jumping = true;
			}

		}
		if (Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.05f) && jumping && mBody.velocity.y < 0)
		{
			jumping = false;
		}
		Vector3 gravity = globalGravity * gravityScale * Vector3.up;
		mBody.AddForce(gravity, ForceMode.Acceleration);
		mBody.velocity = new Vector3(mBody.velocity.x, Mathf.Clamp(mBody.velocity.y, -9999.0f, 20.0f), mBody.velocity.z);
		if (Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.05f) && !jumping && mBody.velocity.y > 0)
		{
			mBody.velocity = new Vector3(mBody.velocity.x, 0, mBody.velocity.z);
		}
		if (Input.GetMouseButtonDown(0) && !animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && canMove)
		{
			animator.SetTrigger("Attack");
			localPlayer.CmdChangeAnimState ("Attack");
			attacking = true;
			localPlayer.CmdSeekerAttack ();
		}	
		attacking = false;
	}

	// Checks if player is grounded
	private bool isGrounded(){
		return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.05f);
	}

	// Starts the skeleton movement
	private void startMovement(){
		canMove = true;
	}

	// Shows when the skeleton can move
	void OnGUI(){
		if(moveDelay > 0){
			Rect rec = new Rect(Screen.width/2 - 75, 20, 150, 20);
			string youStr = moveDelay.ToString("#.00");
			GUI.Label(rec, "Can move in: " + youStr);
			moveDelay -= Time.deltaTime;
		}
	}

}