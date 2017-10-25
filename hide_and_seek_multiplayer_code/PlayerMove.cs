using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
	public static float globalGravity = -9.81f;
	private float gravityScale = 5.0f;
	private Vector3 movement;
	private float speed = 10.0f;
	public Transform camera;
	private float distToGround;
	private Rigidbody mBody;
	// Use this for initialization
	void Start () {
		camera = GameObject.Find ("Main Camera").transform;
		distToGround = GetComponent<Collider> ().bounds.extents.y;
		mBody = GetComponent<Rigidbody> ();
		mBody.useGravity = false;
	}

	// Update is called once per frame
	void Update () {
		movement.x = Input.GetAxis ("Horizontal");
		movement.z = Input.GetAxis ("Vertical");
		Vector3.ClampMagnitude (movement, 5.0f);
		transform.Translate (movement * speed * Time.deltaTime, Space.Self);
		transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, camera.localEulerAngles.y, transform.localEulerAngles.z);
	}

	// Move the player
	void FixedUpdate(){
		if(Input.GetKeyDown(KeyCode.Space) && isGrounded())
		{
			mBody.AddForce (0, 1000.0f, 0);
		}
		Vector3 gravity = globalGravity * gravityScale * Vector3.up;
		mBody.AddForce(gravity, ForceMode.Acceleration);
	}

	// Check if player is grounded
	private bool isGrounded(){
		return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.3f);
	}
}
