using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScene : MonoBehaviour {

    public bool playerInZone;
    public string nextLevel;

    void Start()
    {
        playerInZone = false;
    }

    void Update()
    {
        if (playerInZone && Input.GetKeyDown(KeyCode.W))
        {
            Application.LoadLevel(nextLevel);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" || other.name == "Player")
        {
            playerInZone = true;
        }
    }
}
