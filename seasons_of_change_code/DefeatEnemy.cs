using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// script to destroy enemies once they are defeated

public class DefeatEnemy : MonoBehaviour {

    EnemyHealthController enemy;

	// Use this for initialization
	void Start () {
        enemy = GetComponentInParent<EnemyHealthController>();
    }

    // Update is called once per frame
    void Update () {
		if (enemy.enemyDefeated)
        {
            Destroy(gameObject);
        }
	}
}
