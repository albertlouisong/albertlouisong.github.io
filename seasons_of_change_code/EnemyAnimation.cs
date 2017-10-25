using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour {
    public EnemyHealthController enemy;
    public Rigidbody2D myRigidbody2D;
    private Animator anim;

    // Use this for initialization
    void Start()
    {
        enemy= GetComponentInParent<EnemyHealthController>();
        myRigidbody2D = GetComponentInParent<Rigidbody2D>();
        anim = GetComponent<Animator>();
		myRigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Defeated", (enemy.enemyDefeated));
    }
}
