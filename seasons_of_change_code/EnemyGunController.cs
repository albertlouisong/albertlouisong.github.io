using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunController : MonoBehaviour {

    public GameObject enemy; // enemy object
    public float scale; // enemy scale, determines if they are facing left (-scale) or right (+scale)
    public int speed; // how fast the gun bullet moves
    public GameObject projectile; // the bullet
    public Transform tip; // the tip of the gun
    public int coolDown; // cooldown between shots
    public int coolDownTimer; // cooldown timer

    private void Start()
    {
        // if the enemy is facing left, flip the speed so that the bullet moves from right to left
        if (enemy.transform.localScale.x < 0) { speed = -speed; }
        coolDownTimer = coolDown;
    }

    void Update()
    {
        scale = enemy.transform.localScale.x;
        if (coolDownTimer <= 0)
        {
            Instantiate(projectile, tip.position, tip.rotation);
            coolDownTimer = coolDown;
        }
        coolDownTimer--;
    }
}
