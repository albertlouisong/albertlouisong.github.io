using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyGrowth : MonoBehaviour {

public bool rockAttack;
public GameObject growth;
public float deathTimer;
public bool doneDid;

private float scale;
public GameObject myObject;
public GameObject enemy;
public GameObject flat;
public bool spawn;

private GameObject growth1;


 void Start () {
		//myRigidbody2D = GetComponentInParent<Rigidbody2D>();
		doneDid = false;
		scale = 1;
		spawn = false;
		growth.transform.localScale = new Vector3(1, 1, 1);
		growth.SetActive(true);
		enemy.SetActive(true);
		flat.SetActive(true);
	}

	// Update is called once per frame
	void Update () {
		//if (rockAttack && Random.Range(0,100) > 98 && !doneDid){
			if (Random.Range(0,100) >98 && !spawn){
			growth1 = Instantiate(growth, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z), Quaternion.identity) as GameObject;
			spawn = true;
			}

//			if (spawn){
//			growth1.transform.localScale= new Vector3(scale, scale, 1);}

			if (scale < 5){
			scale+= 0.05f;}
			else if (!doneDid){
			GameObject enemy1 = Instantiate(enemy, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z), Quaternion.identity);
			GameObject flat1 = Instantiate(flat, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z), Quaternion.identity);
			doneDid = true;
			Destroy(growth1);
			}
			//Destroy(myObject);
			//spawn = true;
			}

		}

//		growth1.transform.localScale = new Vector3(scale, scale, 1);
//		if (scale < 5){
//		scale+= 0.05f;
//		}
//		else if (!spawn){
//			GameObject enemy1 = Instantiate(enemy, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z), Quaternion.identity);
//			GameObject flat1 = Instantiate(flat, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z), Quaternion.identity);
//			Destroy(myObject);
//			spawn = true;
//			}
//
		


