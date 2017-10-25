using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lava : MonoBehaviour {

	// Use this for initialization

	public bool lavaUp;
	public bool lavaDown;
	private Vector3 currentPos;
	private Vector3 startPos;
	public int delayTimer;
	public float speed;
	private int speedDelay;

	void Start () {
		lavaUp = true;
		delayTimer = 120;
		speedDelay = 240;
		startPos = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
		speed=0.01f;
	}
	
	// Update is called once per frame
	void Update () {
		currentPos = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
		speedDelay -= 1;

		if (speedDelay == 0){
		speed = speed +speed;
		speedDelay = 120;
		}

		if (lavaUp && this.gameObject.transform.position.y < (startPos.y+20)){
		this.gameObject.transform.position = new Vector3(currentPos.x, currentPos.y + speed, currentPos.z);
		}

		if (lavaDown && this.gameObject.transform.position.y > startPos.y){
			this.gameObject.transform.position = new Vector3(currentPos.x, currentPos.y - 0.1f, currentPos.z);
		}

		if (currentPos.y>startPos.y+20 && delayTimer != 0){
			delayTimer-=1;
		}
		else if (currentPos.y>startPos.y+20 && delayTimer == 0){
			lavaUp = false;
			lavaDown = true;
		}

		if (currentPos.y == startPos.y && lavaDown){
			lavaUp = true;
		delayTimer = 120;
		speedDelay = 240;
		startPos = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
		speed=0.01f;
		}


	}
}
