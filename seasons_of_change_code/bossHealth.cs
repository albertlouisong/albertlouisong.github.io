using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class bossHealth : MonoBehaviour {
	public int health;
	public bool cooldown;
	public Slider slide;
	public Text youWin;
	public Image blackScreen;
	public int timer;

	public GameObject winter;
	public GameObject summer;
	public GameObject autumn;
	public GameObject spring;
	public bool dead;
	// Use this for initialization
	void Start () {
		slide.value = 8;
		youWin.enabled = false;
		timer = 100;
	}
	
	// Update is called once per frame
	void Update () {
		slide.value = health;

		if (health == 0){
			this.gameObject.transform.localScale = new Vector3(0,0,0);
			//Destroy(this.gameObject);
			youWin.enabled = true;
			blackScreen.enabled = true;
			spring.SetActive(false);
			summer.SetActive(false);
			winter.SetActive(false);
			autumn.SetActive(false);
			timer--;


		}
		if (timer < 0){
			SceneManager.LoadScene("Menu");

		}
	}

	private void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "AutumnWeapon" && !cooldown){
		health--;
		cooldown = true;
	}
	}
}
