using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grapple : MonoBehaviour {

	public LineRenderer line;
	DistanceJoint2D joint;
	Vector3 targetPos;
	//Vector3 smallTargetPos;
	private RaycastHit2D hit;
	private RaycastHit2D[] allHit;
	public float distance=10f;
	public LayerMask mask;
	public Camera cam;
	public GameObject test;
	private GameObject spring;

	// Use this for initialization
	void Start () {
		joint = GetComponent<DistanceJoint2D> ();
		joint.enabled=false;
		test = GameObject.Find("ledge");
		spring = GameObject.Find("SpringForm");
		line.sortingOrder = 20;
		line.sortingLayerName = "Character";
		line.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButton(0)){
			targetPos= cam.ScreenToWorldPoint(Input.mousePosition);
			//smallTargetPos = targetPos;


			//Debug.Log(targetPos);
			targetPos.z=-0f;


			line.SetPosition(0, new Vector3(transform.position.x, transform.position.y, -10f));
			//line.SetPosition(1, smallTargetPos);


		
			hit = Physics2D.Raycast(transform.position + new Vector3(0, 1, 0), targetPos-transform.position);
			//hit = Physics2D.Raycast(transform.position, targetPos-transform.position);
			//hit = Physics2D.Raycast(transform.position, new Vector2(targetPos.x, targetPos.y));
			//hit = allHit[0];
//			Debug.Log(transform.position);
//			Debug.Log(targetPos);
//			Debug.Log(distance);
//			Debug.Log(mask);
//			if (hit.collider != null && hit.collider.gameObject.GetComponent<Rigidbody2D>()!=null){
//				Debug.Log("notnull!");
//			}
		//hit=Physics2D.RayCast(transform.position, targetPos-transform.position, distance, mask);
			//joint.connectedBody=hit.collider.gameObject.GetComponent<Rigidbody2D>();
			//joint.connectedAnchor = test.transform.position;
			//joint.connectedAnchor = hit.collider.gameObject.transform.position;

		if (hit.collider != null && hit.rigidbody!=null && hit.collider.tag ==  "Summer" && spring.activeInHierarchy){
				
				joint.enabled=true;
			joint.connectedBody=hit.rigidbody;
			//joint.connectedAnchor = new Vector2(hit.collider.transform.position.x, hit.collider.transform.position.y) - hit.point;

				line.enabled = true;
				line.SetPosition(0, new Vector3(transform.position.x, transform.position.y, -10f));
				line.SetPosition(1, new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y, -10f));

			//joint.distance= Vector2.Distance(transform.position, hit.point);
			//joint.connectedBody=hit.collider.gameObject.GetComponent<Rigidbody2D>();


		}


		}

		if(Input.GetMouseButtonUp(0)){
			joint.enabled=false;
			line.enabled = false;
			joint.connectedBody = null;
		}


	}

//	void  OnMouseEnter() {
//		 Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
//         RaycastHit hit;
//         
//         if( Physics.Raycast( ray, out hit, 100 ) )
//         {
//             Debug.Log( hit.transform.gameObject.name );
//         }
//     }
	
	

	}
