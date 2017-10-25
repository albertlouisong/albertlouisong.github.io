using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderScript : MonoBehaviour {

    private Player thePlayer;

	// Use this for initialization
	void Start () {
        thePlayer = FindObjectOfType<Player>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "SummerForm" || collision.name == "AutumnForm" || collision.name == "WinterForm" || collision.name == "SpringForm")
        {
            thePlayer.onLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "SummerForm" || collision.name == "AutumnForm" || collision.name == "WinterForm" || collision.name == "SpringForm")
        {
            thePlayer.onLadder = false;
        }
    }

}
