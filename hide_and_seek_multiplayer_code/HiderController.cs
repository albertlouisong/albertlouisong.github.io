using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiderController : MonoBehaviour {

	public Animator animator;
	public static float globalGravity = -9.81f;
	private float gravityScale = 5.0f;
	private Vector3 movement;
	private float speed = 30.0f;
	public Transform cam;
	private float distToGround;
	public float velocityForce;
	private bool jumping;
	private Rigidbody mBody;
	private SetupLocalPlayer localPlayer;
	// Use this for initialization
	void Start () {
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
		if(Input.GetKey(KeyCode.S)){
			transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, cam.localEulerAngles.y - 180, transform.localEulerAngles.z);
			if(Input.GetKey(KeyCode.A)){
				transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, cam.localEulerAngles.y - 135, transform.localEulerAngles.z);
			}else if(Input.GetKey(KeyCode.D)){
				transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, cam.localEulerAngles.y - 225, transform.localEulerAngles.z);
			}
			animator.SetBool ("Run", true);
			localPlayer.CmdChangeAnimState ("Run");
			mBody.velocity = new Vector3(transform.forward.x* velocityForce*Time.deltaTime,mBody.velocity.y,transform.forward.z* velocityForce*Time.deltaTime);
		}else if(Input.GetKey(KeyCode.W)){
			transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, cam.localEulerAngles.y, transform.localEulerAngles.z);
			if(Input.GetKey(KeyCode.A)){
				transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, cam.localEulerAngles.y - 45, transform.localEulerAngles.z);
			}else if(Input.GetKey(KeyCode.D)){
				transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, cam.localEulerAngles.y + 45, transform.localEulerAngles.z);
			}
			animator.SetBool ("Run", true);
			localPlayer.CmdChangeAnimState ("Run");
			mBody.velocity = new Vector3(transform.forward.x* velocityForce*Time.deltaTime,mBody.velocity.y,transform.forward.z* velocityForce*Time.deltaTime);
		}
		if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S)){
			if(Input.GetKey(KeyCode.A)){
				transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, cam.localEulerAngles.y - 90, transform.localEulerAngles.z);
				translation = Time.deltaTime * speed;
				animator.SetBool ("Run", true);
				localPlayer.CmdChangeAnimState ("Run");
				mBody.velocity = new Vector3(transform.forward.x* velocityForce*Time.deltaTime,mBody.velocity.y,transform.forward.z* velocityForce*Time.deltaTime);
			}else if(Input.GetKey(KeyCode.D)){
				transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, cam.localEulerAngles.y + 90, transform.localEulerAngles.z);
				translation = Time.deltaTime * speed;
				animator.SetBool ("Run", true);
				localPlayer.CmdChangeAnimState ("Run");
				mBody.velocity = new Vector3(transform.forward.x* velocityForce*Time.deltaTime,mBody.velocity.y,transform.forward.z* velocityForce*Time.deltaTime);
			}
		}
		if(Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.05f) && jumping && mBody.velocity.y < 0){
			jumping = false;
		}
		if(Input.GetKeyDown(KeyCode.Space) && isGrounded())
		{
			mBody.velocity = new Vector3 (mBody.velocity.x, 0, mBody.velocity.z);
			mBody.AddForce (0, 1000.0f, 0);
			jumping = true;
		}
		Vector3 gravity = globalGravity * gravityScale * Vector3.up;
		mBody.AddForce(gravity, ForceMode.Acceleration);
		mBody.velocity = new Vector3 (mBody.velocity.x,Mathf.Clamp(mBody.velocity.y,-9999.0f, 20.0f),mBody.velocity.z);
		if(Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.05f) && !jumping && mBody.velocity.y > 0){
			mBody.velocity = new Vector3 (mBody.velocity.x, 0, mBody.velocity.z);
		}
	}

	// Is the player grounded
	private bool isGrounded(){
		return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.05f);
	}

}
