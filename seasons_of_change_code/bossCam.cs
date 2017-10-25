using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bossCam : MonoBehaviour {

	public Camera cam;
	public GameObject player;
	public Text bossText;
	private bool full;
	public int timer;
	public GameObject bossPhase;
	public GameObject speech;
	public GameObject bamboo;
	public Slider slide;
	public Canvas ui;

	// Use this for initialization
	void Start () {
		cam.transform.position = new Vector3(player.transform.position.x, -2.4f, -17.5f);
		cam.orthographicSize = 3.61f;
		full = false;
		timer = 0;
		bossText.enabled = false;
		bossPhase.SetActive(false);
		speech.SetActive(false);
		slide.GetComponent<RectTransform>().localScale = new Vector3 (0,0,0);
		ui.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	if (timer < 802 && full){
		timer++;
	}
	if (player.transform.position.x < -25 && !full){
		cam.transform.position = new Vector3(player.transform.position.x, cam.transform.position.y, cam.transform.position.z);
		}
	else {
	full = true;
			bamboo.transform.localScale = new Vector3 (bamboo.transform.localScale.x, 20f, bamboo.transform.localScale.z);
			bamboo.transform.position = new Vector3(bamboo.transform.position.x, -7.5f, bamboo.transform.position.z);
			cam.transform.position = new Vector3(0.6f, 13.6f, -17.5f);
			cam.orthographicSize = 22.92f;
			bossText.enabled = true;
			speech.SetActive(true);
			if (timer < 200){
			bossText.text = "So, you finally made it here. And with all the weather orbs no less. ";
			}
			else if (timer < 400){
			bossText.text = "I rather enjoyed watching you struggle. And all I had to do was change a few lines of code";
			}
			else if (timer < 600){
			bossText.text = "At first I'd only wanted to destroy your little machine, but seeing what you're able to do...";
			}
			else if (timer < 800){
			bossText.text = "I think now, after I defeat you, I'll take it for myself!";
			}
			else {
				bossText.enabled = false;
				bossPhase.SetActive(true);
				speech.SetActive(false);
				slide.GetComponent<RectTransform>().localScale = new Vector3 (1,1,1);
				ui.enabled = true;
			}

	}
	}
}
