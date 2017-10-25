using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockSpin : MonoBehaviour {

	public GameObject myObject;
	public GameObject[] objects;

	// Use this for initialization
	void Start () {
		

	}
	
	// Update is called once per frame
	void Update () {
		objects = GameObject.FindGameObjectsWithTag("flying");
		for ( int i = 0; i<objects.Length; i++){
			objects[i].transform.Rotate(0, 0, 180 * Time.deltaTime);
    	}

		//myObject.transform.Rotate (0, 0, 180 * Time.deltaTime);
	}
}
