using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleOvergrowth : MonoBehaviour {
    public GameObject enemy;
    public GameObject overgrowth;
    public double health;
    public int enemyCount = 0;

	// Update is called once per frame
	void Update () {
        health = GetComponent<EnemyHealthController>().enemyHealth;
        if (health <= 0)
        {
            overgrowth.SetActive(false);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
