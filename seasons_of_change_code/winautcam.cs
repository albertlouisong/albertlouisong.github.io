using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winautcam : MonoBehaviour {
	public GameObject player;
	public Camera cam;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (player.transform.position.x > 265){
			transform.position = new Vector3(player.transform.position.x, player.transform.position.y+5f, -0.35f);
			cam.orthographicSize = 17.97f;
		}
		else {
			transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -0.35f);
			cam.orthographicSize = 5;
		}
	}
}
