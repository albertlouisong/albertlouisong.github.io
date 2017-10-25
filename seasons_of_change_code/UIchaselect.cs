using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIchaselect : MonoBehaviour {
	GameObject summer, spring, autumn, winter, snowball;
	Image one, two, three, four;
	public Color temp;
	// Use this for initialization
	void Start () {
		summer = GameObject.Find("SummerForm");
        spring = GameObject.Find("SpringForm");
        autumn = GameObject.Find("AutumnForm");
        winter = GameObject.Find("WinterForm");
        snowball = GameObject.Find("SnowballForm");
	}
	
	// Update is called once per frame
	void Update () {
		if (autumn.activeInHierarchy){
			temp.a = 0.5f;
			one.GetComponent<Image>().material.color = temp;
		}
		else {
			temp.a = 1f;
			one.GetComponent<Image>().material.color = temp;
		}

		if (spring.activeInHierarchy){
			temp.a = 0.5f;
			two.GetComponent<Image>().material.color = temp;
		}
		else {
			temp.a = 1f;
			two.GetComponent<Image>().material.color = temp;
		}

		if (winter.activeInHierarchy){
			temp.a = 0.5f;
			three.GetComponent<Image>().material.color = temp;
		}
		else {
			temp.a = 1f;
			three.GetComponent<Image>().material.color = temp;
		}

		if (summer.activeInHierarchy){
			temp.a = 0.5f;
			four.GetComponent<Image>().material.color = temp;
		}
		else {
			temp.a = 1f;
			four.GetComponent<Image>().material.color = temp;
		}
	}
}
