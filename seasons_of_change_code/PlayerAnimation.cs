using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// script to toggle player animation based on the player's state

public class PlayerAnimation : MonoBehaviour {

    public Player player; // player script
    public Rigidbody2D myRigidbody2D; // the player's rigidbody
    private Animator anim; // the player animation controller

    void Start()
    {
        player = GetComponentInParent<Player>();
        myRigidbody2D = GetComponentInParent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        anim.SetFloat("Speed", Mathf.Abs( myRigidbody2D.velocity.x)); // boolean for player movement
        anim.SetBool("Jump", !(player.grounded)); // boolean for player jumping
        anim.SetBool("Attack", player.attacking); // boolean for player attacking
        anim.SetFloat("Charge", player.jumpCharge); // boolean for player charging jump
    }
}
