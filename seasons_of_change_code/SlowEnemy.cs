using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowEnemy : MonoBehaviour {
    public EnemyPatrol patrol; // enemy patrol script
    public EnemyHealthController enemy; // enemy health controller
    public double health;
    public double initialHealth;
    float halfSpeed;
    float quarterSpeed;

	// Use this for initialization
	void Start () {
        initialHealth = GetComponent<EnemyHealthController>().enemyHealth;
        halfSpeed = patrol.moveSpeed * 0.5f;
        quarterSpeed = patrol.moveSpeed * 0.25f;
	}
	
	// Update is called once per frame
	void Update () {
        health = GetComponent<EnemyHealthController>().enemyHealth;

        if (health <= 2 * initialHealth / 3)
        {
            patrol.moveSpeed = halfSpeed;
        }
        if (health <= initialHealth / 3)
        {
            patrol.moveSpeed = quarterSpeed;
        }
    }
}
