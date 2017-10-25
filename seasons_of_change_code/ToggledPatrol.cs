using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggledPatrol : MonoBehaviour {

    private Rigidbody2D enemyRigidBody2D;
    public double health; // current health of the enemy
    public double initialHealth; // starting health of the enemy
    public float moveSpeed;
    bool moveRight;
    public int turn;
    int turnTimer;

    void Start()
    {
        initialHealth = GetComponent<EnemyHealthController>().enemyHealth;
        enemyRigidBody2D = GetComponent<Rigidbody2D>();
        enemyRigidBody2D.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    void Update()
    {
        health = GetComponent<EnemyHealthController>().enemyHealth;
        if (health <= 0)
        {
            enemyRigidBody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            if (turnTimer == 0)
            {
                if (moveRight) { moveRight = false; }
                else { moveRight = true; }
                turnTimer = turn;
            }

            if (moveRight) { GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y); }
            else { GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y); }
            if (GetComponent<Rigidbody2D>().velocity.x < 0) { transform.localScale = new Vector3(-1f, 1f, 1f); }
            else if (GetComponent<Rigidbody2D>().velocity.x > 0) { transform.localScale = new Vector3(1f, 1f, 1f); }
            turnTimer -= 1;
        }
    }
}
