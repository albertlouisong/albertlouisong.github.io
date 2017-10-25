using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleVines : MonoBehaviour {
    public GameObject full, half, third; // the three states of the vine
    public double health; // current health of the vine
    public double initialHealth; // starting health of the vine
	BoxCollider2D c;

    void Start () {
        initialHealth = 10;
        full.SetActive(true);
        half.SetActive(false);
        third.SetActive(false);
		c = GetComponent<BoxCollider2D>();
		c.enabled = true;
    }

    void Update () {
		health = GetComponent<EnemyHealthController>().enemyHealth;

        if (health <= 2 * initialHealth/3)
        {
            full.SetActive(false);
            half.SetActive(true);
            third.SetActive(false);
        }
        if (health <= initialHealth/3)
        {
            full.SetActive(false);
            half.SetActive(false);
            third.SetActive(true);
        }
        if (health <= 0)
        {
			full.SetActive(false);
			half.SetActive(false);
			third.SetActive(false);
			c.enabled = false;
        }
    }
}
