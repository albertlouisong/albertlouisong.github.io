using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelSwitch : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Alpha0)){
			SceneManager.LoadScene("Menu");
		}
		else if(Input.GetKeyDown(KeyCode.Alpha1)){
			SceneManager.LoadScene("AutumnSeason");
		}
		else if(Input.GetKeyDown(KeyCode.Alpha2)){
			SceneManager.LoadScene("SpringScene");
		}
		else if(Input.GetKeyDown(KeyCode.Alpha3)){
			SceneManager.LoadScene("Summer - Winter");
		}
		else if(Input.GetKeyDown(KeyCode.Alpha4)){
			SceneManager.LoadScene("Winter-Autumn");
		}
		else if(Input.GetKeyDown(KeyCode.Alpha5)){
			SceneManager.LoadScene("Boss 1");
		}
	}
}
