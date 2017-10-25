using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menuScript : MonoBehaviour {

	public Text press;
	public Text textOne;
	public float timer;
	public bool increase;
	public Color temp;

	// Use this for initialization
	void Start () {
		timer = 1;
		temp.a = 1;
		press.material.color = temp;
		press.color = temp;
	}
	
	// Update is called once per frame
	void Update () {
		temp.a = timer;
		//press.GetComponent<Text>().color(1, 1, 1, 1); = new Vector4.Lerp(1, 1, 1, 1);
		press.color = temp;

		if (increase){
			timer+= 0.01f;
		}
		else {
			timer -= 0.01f;
		}

		if (timer > 1){
			increase = false;
		}
		if (timer < 0){
			increase = true;
		}

		if (Input.anyKey ){
			if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Alpha5))
			{}
			else{
			SceneManager.LoadScene("AutumnSeason");
			}
		}
	
	}
}
