using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealthController : MonoBehaviour {
    public double enemyHealth;
    public bool enemyDefeated;
    public bool burned;
    public bool frozen;
    public int burntimer;
    public int frozentimer;

    void Start () {
    	enemyHealth = 10;
    	burntimer = 0;
    	frozentimer = 0;
    }
	// Update is called once per frame
	void Update () {
        if (enemyHealth <= 0) {
            enemyDefeated = true;
            frozen = false; frozentimer = 0;
            burned = false; burntimer = 0;
        }
        if (SceneManager.GetActiveScene().name == "Boss 1" && enemyDefeated){
        	Destroy(this.gameObject);
    	}
        if (burned)
        {
            frozen = false;
        	burntimer--;

        }
        if (burntimer > 0) {
            if (gameObject.tag == "SummerWeapon") { enemyHealth -= 0.25; }
            else { enemyHealth -= 0.05; }
        }
        if (burntimer == 0) { burned = false; }
   
		if (frozen)
        {
            burned = false;
        	frozentimer--;
        }

        if (frozentimer > 0) { gameObject.GetComponent<EnemyPatrol>().enabled = false; }
		if (frozentimer == 0)
        {
            frozen = false;
            gameObject.GetComponent<EnemyPatrol>().enabled = true;
        }
    }

	private void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "AutumnWeapon"){
            if (other.tag == gameObject.tag) { enemyHealth -= 5; }
            else { enemyHealth -= 2; }
        }
		else if (other.tag == "SummerWeapon"){
            if (!burned)
            {
                burned = true;
                if (other.tag == gameObject.tag) { burntimer = 100; }
                else { burntimer = 50; }
            }
	    }
	    else if (other.tag == "WinterWeapon"){
            if (!frozen)
            {
                frozen = true;
                if (other.tag == gameObject.tag) { frozentimer = 200; enemyHealth -= 3;}
                else { frozentimer = 100; enemyHealth -= 2;}
            }
	    }
    }
}
