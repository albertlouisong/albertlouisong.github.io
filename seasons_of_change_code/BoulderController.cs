using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderController : MonoBehaviour {
    public GameObject boulder;
    public EnemyGunController spawn; // script that controls the spawn
    public int lifespan; // how long the bullet lasts before it is destroyed
    public int speed; // how fast the bullet moves (refers to gun script)
    public int damageToGive; // how much damage the bullet inflicts

    private void Start()
    {
        spawn = FindObjectOfType<EnemyGunController>();
        speed = spawn.speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (lifespan <= 0) { Destroy(gameObject); } // destroy the boulder once the lifespan is zero
        else
        {
            // move the boulder if it has not died yet
            GetComponent<Rigidbody2D>().velocity = new Vector3(speed, GetComponent<Rigidbody2D>().velocity.y);
            lifespan--;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == boulder.tag)
        {
            Destroy(collision.gameObject);
        }
    }
}
