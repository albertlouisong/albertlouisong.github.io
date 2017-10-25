using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// script to control melee weapons

public class MeleeWeaponController : MonoBehaviour {

    public int damageToGive; // how much damage the weapon inflicts

//    private void OnTriggerEnter2D(Collider2D other)
//    {
//        // if the weapon collides with an enemy, damage them
//        if (other.tag == gameObject.tag) { other.GetComponent<EnemyHealthController>().GiveDamage(damageToGive); }
//    }
}
