using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCheck : MonoBehaviour {

	public double health;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		health = GetComponent<EnemyHealthController>().enemyHealth;
		if (health <= 0) {
			this.tag = "Melted Ice";
		}
	}
}
