using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyPatrol : MonoBehaviour {

    public float moveSpeed;
    bool moveRight;
    EnemyHealthController enemyHealthController;

    // enemy turn based on certain amount of time
    public int turn;
    int turnTimer;

	// Use this for initialization
	void Start () {
        enemyHealthController = GetComponent<EnemyHealthController>();
	}

    // Update is called once per frame
    void Update () {
    	if (SceneManager.GetActiveScene().name != "Boss 1"){

          if (turnTimer == 0)
        {
            if (moveRight) { moveRight = false; }
            else { moveRight = true; }
            turnTimer = turn;
        }

        if (!enemyHealthController.enemyDefeated)
        {
            if (moveRight) { GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y); }
            else { GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y); }
            if (GetComponent<Rigidbody2D>().velocity.x < 0) { transform.localScale = new Vector3(-1f, 1f, 1f); }
            else if (GetComponent<Rigidbody2D>().velocity.x > 0) { transform.localScale = new Vector3(1f, 1f, 1f); }
            turnTimer -= 1;
        }
        else if (enemyHealthController.enemyDefeated)
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
        }
    }
}
