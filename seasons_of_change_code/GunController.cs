using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// script to control the gun weapons

public class GunController : MonoBehaviour {

    public Player player; // player script
    public float scale; // player scale, determines if they are facing left (-scale) or right (+scale)
    public int speed; // how fast the gun bullet moves
    public GameObject projectile; // the bullet
    public Transform tip; // the tip of the gun

    private void Start()
    {
        // if the player is facing left, flip the speed so that the bullet moves from right to left
        if (player.transform.localScale.x < 0) { speed = -speed; }
    }

    void Update()
    {
        scale = player.transform.localScale.x;
        // if the attack is pressed, shoot a bullet
        if (player.attacking) { Instantiate(projectile, tip.position, tip.rotation);} 
    }
}
