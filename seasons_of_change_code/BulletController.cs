using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public GunController gun; // script that controls the gun
    public int lifespan; // how long the bullet lasts before it is destroyed
    public int speed; // how fast the bullet moves (refers to gun script)
    public int damageToGive; // how much damage the bullet inflicts

    private void Start()
    {
        gun = FindObjectOfType<GunController>(); // find the gun script
        if (gun.scale < 0) // if the gun/player is facing left
        {
            speed = -gun.speed; // bullet now moves from right to left
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z); // flip the bullet sprite as well
        }
        else { speed = gun.speed; }
    }

    // Update is called once per frame
    void Update()
    {
        if (lifespan <= 0) { Destroy(gameObject); } // destroy the bullet once the lifespan is zero
        else
        {
            // move the bullet if it has not died yet
            GetComponent<Rigidbody2D>().velocity = new Vector3(speed, GetComponent<Rigidbody2D>().velocity.y);
            lifespan--;
        }
    }

    // destroy the bullet once it collides with a wall or enemy
//    private void OnTriggerEnter2D(Collider2D other)
//    {
//        if (other.tag != gameObject.tag)
//        {
//            Destroy(gameObject);
//        }
//        if (other.tag == gameObject.tag) { other.GetComponent<EnemyHealthController>().GiveDamage(damageToGive); }
//    }
}
