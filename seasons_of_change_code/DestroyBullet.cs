using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// script to destroy bullet when it collides with another object

public class DestroyBullet : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}
