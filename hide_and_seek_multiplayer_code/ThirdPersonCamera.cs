using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {

	private const float Y_ANGLE_MIN = 5.0f;
	private const float Y_ANGLE_MAX = 50.0f;

	public Transform lookAt;
	public Transform cameraTransform;

	private Camera cam;

	private float distance = 10.0f;
	private float currentX = 0.0f;
	private float currentY = 0.0f;
	private float sensitivityX = 4.0f;
	private float sensitivityY = 1.0f;

	// Use this for initialization
	void Start () {
		cameraTransform = GameObject.Find("Main Camera").transform;
		cam = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		lookAt = this.transform;

	}
	
	// Update is called once per frame
	void Update () {
		currentX += Input.GetAxis ("Mouse X");
		currentY -= Input.GetAxis ("Mouse Y");
		currentY = Mathf.Clamp (currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
	}

	// Updates camera position and rotation
	private void LateUpdate(){
		Vector3 dir = new Vector3 (0, 0, -distance);
		Quaternion rotation = Quaternion.Euler (currentY, currentX, 0);
		cameraTransform.position = lookAt.position + rotation * dir;
		cameraTransform.position = new Vector3 (cameraTransform.position.x, cameraTransform.position.y, cameraTransform.position.z);
		cameraTransform.LookAt (lookAt.position);
	}
}
