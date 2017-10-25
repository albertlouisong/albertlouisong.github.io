using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingRocksCopy : MonoBehaviour {

public bool rockAttack;
//public int force;
public GameObject rock;
	//private Rigidbody2D myRigidbody2D;
	public float moveSpeed;
	public float deathTimer;


 void Start () {
		//myRigidbody2D = GetComponentInParent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (rockAttack && Random.Range(0,100) > 98){
			GameObject rock1 = Instantiate(rock, new Vector3(33.9f, Random.Range(-35f, 35f), 0), Quaternion.identity) as GameObject;

			//rock.transform.position = new Vector3(transform.position.x * moveSpeed* Time.deltaTime, transform.position.y, transform.position.z);
			rock1.transform.localScale = new Vector3(5f, 5f, 1f);
			rock1.GetComponent<Rigidbody2D>().velocity= transform.localScale.x*Vector2.left* moveSpeed;
			//rock1.GetComponent<Rigidbody2D>().rotation= transform.localRotation.x*moveSpeed;
			//rock.transform.localRotation = Quaternion.Slerp(rock.transform.localRotation, Quaternion.Euler(180, 0, 0), Time.time * moveSpeed);
			//rock.transform.Rotate ( Vector3.up * ( moveSpeed * Time.deltaTime ) );

			rock1.transform.Rotate (0, 0, 180 * Time.deltaTime);
			//float scale = Random.Range(0, 3);
			//float rotation = Random.Range(0, 180);
			//rock.transform.localScale = new Vector3(scale, scale, 1);

			//rock.transform.Rotate(Time.deltaTime,20,20);
			//rock.transform.localRotation = Quaternion.Euler(0,rock.transform.localRotation.x++ , 0);
			//transform.Rotate (0, rotation * Time.deltaTime, 0);
			Destroy(rock1, deathTimer);

			//Vector3.down *moveSpeed* Time.deltaTime;
			//myRigidbody2D.velocity = new Vector2(-moveSpeed, myRigidbody2D.velocity.y);
			//rock1.GetComponent<Rigidbody2D>().velocity = new Vector2(10f,rock1.GetComponent<Rigidbody2D>().velocity.y);
		}




		}
		}

