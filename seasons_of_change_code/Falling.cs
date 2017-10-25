using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MonoBehaviour {

public bool rockAttack;
//public int force;
public GameObject icicle;
	//private Rigidbody2D myRigidbody2D;
	public float moveSpeed;


 void Start () {
		//myRigidbody2D = GetComponentInParent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (rockAttack && Random.Range(0,100) > 98){
			GameObject icicle1 = Instantiate(icicle, new Vector3(Random.Range(-29.8f, 30.4f), 37f, 0), Quaternion.identity) as GameObject;
			icicle1.name = "cold";
			float scale = Random.Range(0, 5);
			icicle.transform.localScale = new Vector3(scale, scale, 1);


			Destroy(icicle1, 3);
			
			//rock.transform.position = new Vector3(transform.position.x * moveSpeed* Time.deltaTime, transform.position.y, transform.position.z);

			//Vector3.down *moveSpeed* Time.deltaTime;
			//myRigidbody2D.velocity = new Vector2(-moveSpeed, myRigidbody2D.velocity.y);
			//rock1.GetComponent<Rigidbody2D>().velocity = new Vector2(10f,rock1.GetComponent<Rigidbody2D>().velocity.y);
		}




		}

//	private void OnCollisionEnter2D(Collider2D other)
//    	{
//        	other.GetComponent<health>().GiveDamage(1); 
//    	}

		}


