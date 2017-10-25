using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke ("Finish", 1.0f);
	}

	// Destroy self after timer
	private void Finish(){
		Destroy (this.gameObject);
	}

}
