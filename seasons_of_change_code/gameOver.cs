using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameOver : MonoBehaviour {

	public int timer;

	// Use this for initialization
	void Start () {
		timer = 60;
	}
	
	// Update is called once per frame
	void Update () {
		if (this.GetComponent<Player>().currentHealth < 1){
			timer--;
		}
		if (timer == 0){
			Application.LoadLevel(Application.loadedLevel);
		}
	}
}
