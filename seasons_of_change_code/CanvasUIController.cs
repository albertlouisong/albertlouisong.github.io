using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvasUIController : MonoBehaviour {

	public int peopleUnfrozen;
	public int iceMelted;

	public Text meltText;

	GameObject[] meltedIce;
	GameObject[] meltedPeople;

	GameObject summer, spring, autumn, winter, snowball;
	Image one, two, three, four;
	public Color temp;

	// Use this for initialization
	void Start () {
		peopleUnfrozen = 0;
		iceMelted = 0;

		meltText.text = "People Unfrozen: " + peopleUnfrozen + "/7\n" + "Ice Melted: " + iceMelted + "/15";

		summer = GameObject.Find("SummerForm");
        spring = GameObject.Find("SpringForm");
        autumn = GameObject.Find("AutumnForm");
        winter = GameObject.Find("WinterForm");
        snowball = GameObject.Find("SnowballForm");
	}
	
	// Update is called once per frame
	void Update () {
		updateWinterObjectives ();

		if (autumn.activeInHierarchy){
			temp.a = 0.5f;
			one.GetComponent<Image>().material.color = temp;
		}
		else {
			temp.a = 1f;
			one.GetComponent<Image>().material.color = temp;
		}

		if (spring.activeInHierarchy){
			temp.a = 0.5f;
			two.GetComponent<Image>().material.color = temp;
		}
		else {
			temp.a = 1f;
			two.GetComponent<Image>().material.color = temp;
		}

		if (winter.activeInHierarchy){
			temp.a = 0.5f;
			three.GetComponent<Image>().material.color = temp;
		}
		else {
			temp.a = 1f;
			three.GetComponent<Image>().material.color = temp;
		}

		if (summer.activeInHierarchy){
			temp.a = 0.5f;
			four.GetComponent<Image>().material.color = temp;
		}
		else {
			temp.a = 1f;
			four.GetComponent<Image>().material.color = temp;
		}
	}


	void updateWinterObjectives () {
		meltedIce = GameObject.FindGameObjectsWithTag ("Melted Ice");
		meltedPeople = GameObject.FindGameObjectsWithTag ("Melted People");
		iceMelted = meltedIce.Length;
		peopleUnfrozen = meltedPeople.Length;
		meltText.text = "People Unfrozen: " + peopleUnfrozen + "/7\n" + "Ice Melted: " + iceMelted + "/15";
	}
}
