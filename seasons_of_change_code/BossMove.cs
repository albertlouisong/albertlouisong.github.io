using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour {

	public float smoothing;
	public Vector3 startPos;
	public GameObject boss;
	//public Vector3 newPos;

	// Use this for initialization
	void Start () {
		boss = GameObject.Find("boss");
		startPos = boss.transform.position;

	}
	
	// Update is called once per frame
	void Update () {
		if (Random.Range(0,5) >3){
			smoothing = 5;
		}
		else{
			smoothing = 0.5f;
		}

		if (Random.Range(0, 10) > 7){
		Vector3 newPos =new Vector3((startPos.x + Random.Range(0, 30)), startPos.y + Random.Range(0, 30), startPos.z + Random.Range(0, 30));
		if (Random.Range(0, 5) > 4){
			boss.transform.position = newPos;
		}
		else{
		boss.transform.position = Vector3.Lerp(startPos, newPos, smoothing * Time.deltaTime);
		}
	}
	}
}
