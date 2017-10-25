using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class introText : MonoBehaviour {

	public int timer;
	public Text levelText;
	public GameObject blackScreen;
	public GameObject player;

	// Use this for initialization
	void Start () {
		timer = 1000;	//set how long the text should stay up for, the larger the longer
		levelText.enabled = true;
		blackScreen.SetActive(true);
		player.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (timer >0){
			timer--;
		}
		else {
			levelText.enabled = false;
			blackScreen.SetActive(false);
			player.SetActive(true);

		}
	}
}
