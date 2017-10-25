using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleHut : MonoBehaviour {
    public GameObject full, half, third, defeated; // the four states of the hut
    BoxCollider2D c;
    public double health; // current health of the vine
    public double initialHealth; // starting health of the vine

    void Start()
    {
        initialHealth = GetComponent<EnemyHealthController>().enemyHealth;
        c = GetComponent<BoxCollider2D>();
        c.enabled = true;
        full.SetActive(true);
        half.SetActive(false);
        third.SetActive(false);
        defeated.SetActive(false);
    }

    void Update()
    {
        health = GetComponent<EnemyHealthController>().enemyHealth;

        if (health <= 2 * initialHealth / 3)
        {
            full.SetActive(false);
            half.SetActive(true);
            third.SetActive(false);
            defeated.SetActive(false);
        }
        if (health <= initialHealth / 3)
        {
            full.SetActive(false);
            half.SetActive(false);
            third.SetActive(true);
            defeated.SetActive(false);
        }
        if (health <= 0)
        {
            full.SetActive(false);
            half.SetActive(false);
            third.SetActive(false);
            defeated.SetActive(true);
            c.enabled = false;
			this.tag = "Melted Ice";
        }
    }
}
