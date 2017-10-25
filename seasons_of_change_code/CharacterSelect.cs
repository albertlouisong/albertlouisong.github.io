using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour {
    public int value;
    public int minValue, maxValue;
    GameObject summer, spring, autumn, winter, snowball;
    public bool atLevel1, atLevel2, atLevel3;


    // Use this for initialization
    void Start () {
        summer = GameObject.Find("SummerForm");
        spring = GameObject.Find("SpringForm");
        autumn = GameObject.Find("AutumnForm");
        winter = GameObject.Find("WinterForm");
        snowball = GameObject.Find("SnowballForm");

        // default value is 1 for the normal form
        value = 1;
        // by default player has access to all forms
		minValue = 1;
        maxValue = 4;
    }
	
	// Update is called once per frame
	void Update () {

        /*
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
			value = 1;
        }
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			value = 2;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			value = 3;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			value = 4;
		}
        */

        // what level the player on determines how many forms they can select
        if (atLevel1) { maxValue = 1; minValue = 1; }
        else if (atLevel2) { maxValue = 2; minValue = 1; }
        else if (atLevel3) { maxValue = 3; minValue = 1; }
        else { maxValue = 4; minValue = 1; }

        // cycle through the forms
		if (Input.GetKeyDown(KeyCode.E) && value != 5)
        {
            if (value == maxValue) { value = minValue; }
            else { value += 1; }
        }

		if (Input.GetKeyDown(KeyCode.Q) && value != 5)
		{
			if (value == minValue) { value = maxValue; }
			else { value -= 1; }
		}

        switch (value)
        {
            case 1:
                summer.SetActive(false);
                autumn.SetActive(true);
                winter.SetActive(false);
                spring.SetActive(false);
                snowball.SetActive(false);
                break;
            case 2:
                summer.SetActive(false);
                autumn.SetActive(false);
                winter.SetActive(false);
                spring.SetActive(true);
                snowball.SetActive(false);
                break;
            case 3:
                summer.SetActive(true);
                autumn.SetActive(false);
                winter.SetActive(false);
                spring.SetActive(false);
                snowball.SetActive(false);
                break;
            case 4:
                summer.SetActive(false);
                autumn.SetActive(false);
                winter.SetActive(true);
                spring.SetActive(false);
                snowball.SetActive(false);
                break;
            case 5:
                summer.SetActive(false);
                autumn.SetActive(false);
                winter.SetActive(false);
                spring.SetActive(false);
                snowball.SetActive(true);
                break;
        }

		if (Input.GetMouseButton(1))
        {
            if (value == 4)
            {
                value = 5;
            }
        }

		if (Input.GetMouseButtonUp(1) && (value == 5))
        {
            value = 4;
        }
    }
}
