using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grow : MonoBehaviour {

	private float scale;
	public GameObject myObject;
	public GameObject enemy;
	public GameObject flat;
	private bool spawn;
	private GameObject spawn1;
	private GameObject spawn2;
	private GameObject spawn3;
	private Vector3 pos1;
	private Vector3 pos2;
	private Vector3 pos3;


	// Use this for initialization
	void Start () {
		scale = 0;
		spawn = false;
		spawn1 = GameObject.Find("enemySpawn");
		spawn2 = GameObject.Find("enemySpawn (1)");
		spawn3 = GameObject.Find("enemySpawn (2)");
		pos1 = new Vector3(spawn1.transform.position.x, spawn1.transform.position.y, spawn1.transform.position.z);
		pos2 = new Vector3(spawn2.transform.position.x, spawn2.transform.position.y, spawn2.transform.position.z);
		pos3 = new Vector3(spawn3.transform.position.x, spawn3.transform.position.y, spawn3.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		
		myObject.transform.localScale = new Vector3(scale, scale, 1);
		if (scale < 5){
		scale+= 0.05f;
		}
		else if (!spawn){
			GameObject enemy1 = Instantiate(enemy, pos1, Quaternion.identity);
			GameObject flat1 = Instantiate(flat, pos1, Quaternion.identity);
			Destroy(myObject);

			GameObject enemy2 = Instantiate(enemy, pos2, Quaternion.identity);
			GameObject flat2 = Instantiate(flat, pos2, Quaternion.identity);
			Destroy(myObject);

			GameObject enemy3 = Instantiate(enemy, pos3, Quaternion.identity);
			GameObject flat3 = Instantiate(flat, pos3, Quaternion.identity);
			Destroy(myObject);
			spawn = true;
		}

	}
}
