using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossPhase : MonoBehaviour {

	public GameObject autumn;
	public GameObject spring;
	public GameObject summer;
	public GameObject winter;
	public int timer;
	public GameObject bubble;
	public GameObject boss;
	public double phase;
	public bool phase1Complete;
	private bool phase2Complete;
	private bool phase3Complete;
	private bool phase4Complete;
	private bool phase5Complete;
	private bool phase6Complete;
	private bool phase7Complete;
	private bool phase8Complete;
	private GameObject[] getCount;
	public int count;
	private bool spawned;
	private Vector3 pos;
	private Vector2 scale;
	private Vector3 newPos;
	private float lerpScale;

	public GameObject enemySpawn3;
	public GameObject enemySpawn4;
	public GameObject enemySpawn5;
	public GameObject enemySpawn6;
	public GameObject enemySpawn7;
	public GameObject enemySpawn8;

	// Use this for initialization
	void Start () {
		autumn.SetActive(false);
		spring.SetActive(false);
		summer.SetActive(false);
		winter.SetActive(false);
		timer = 0;
		phase1Complete = false;
		phase2Complete = false;
		phase3Complete = false;
		phase4Complete = false;
		phase5Complete = false;
		phase6Complete = false;
		phase7Complete = false;
		phase8Complete = false;

		phase = 1;
		spawned = false;
		pos = boss.transform.position;
		newPos = new Vector3(boss.transform.position.x, -2.3f, boss.transform.position.z);
		scale = boss.transform.localScale;
		lerpScale = 0;
		boss.GetComponent<bossHealth>().health = 8;
		boss.GetComponent<bossHealth>().cooldown = false;
	}
	
	// Update is called once per frame
	void Update () {

				Debug.Log("1");
	if (phase == 0 && lerpScale < 1){
	lerpScale += 0.01f;
	}
	else if (phase!= 0){
	lerpScale = 0;
	}

	getCount = GameObject.FindGameObjectsWithTag ("AutumnEnemy");
	count = getCount.Length;

		timer++;
	Debug.Log("2");
	if (phase == 0 && timer <800){
			bubble.SetActive(false);
			boss.transform.position = Vector3.Lerp(pos, newPos, lerpScale);
			boss.GetComponent<BossMove>().enabled = false;
			boss.transform.localScale = new Vector3 (5f, 5f, 1f);

			enemySpawn3.GetComponent<enemyGrowth>().doneDid = false;
			enemySpawn3.GetComponent<enemyGrowth>().spawn = false;

			enemySpawn4.GetComponent<enemyGrowth>().doneDid = false;
			enemySpawn4.GetComponent<enemyGrowth>().spawn = false;

			enemySpawn5.GetComponent<enemyGrowth>().doneDid = false;
			enemySpawn5.GetComponent<enemyGrowth>().spawn = false;

			enemySpawn6.GetComponent<enemyGrowth>().doneDid = false;
			enemySpawn6.GetComponent<enemyGrowth>().spawn = false;

			enemySpawn7.GetComponent<enemyGrowth>().doneDid = false;
			enemySpawn7.GetComponent<enemyGrowth>().spawn = false;

			enemySpawn8.GetComponent<enemyGrowth>().doneDid = false;
			enemySpawn8.GetComponent<enemyGrowth>().spawn = false;
			}
	if (phase == 0 && timer > 800 || phase == 0 && boss.GetComponent<bossHealth>().cooldown){
			bubble.SetActive(true);
			boss.GetComponent<bossHealth>().cooldown = false;
			boss.transform.position = pos;
			boss.transform.localScale = scale;
			if (phase7Complete){
				phase = 8;
				timer = 0;
			}
			else if (phase6Complete){
				phase = 7;
				timer = 0;
			}
			else if (phase5Complete){
				phase = 6;
				timer = 0;
			}
			else if (phase4Complete){
				phase = 5;
				timer = 0;
			}
			else if (phase3Complete){
				phase = 4;
				timer = 0;
			}
			else if (phase2Complete){
				phase = 3;
				timer = 0;
			}
			else if (phase1Complete){
				phase = 2;
				timer = 0;
			}
			}

	
			Debug.Log("pls");
	if (phase == 1 && timer < 800){
			Debug.Log("work you");
			autumn.SetActive(true);
			spring.SetActive(false);
			summer.SetActive(false);
			winter.SetActive(false);
			bubble.GetComponent<SpriteRenderer>().color = new Vector4(255, 90, 0, 1);
	}
	else if (phase == 1 && timer >800){
		autumn.SetActive(false);
		timer = 0;
		phase = 0;
		phase1Complete = true;
	}

	else if (phase == 2){
		autumn.SetActive(false);
		spring.SetActive(true);
		summer.SetActive(false);
		winter.SetActive(false);
		bubble.GetComponent<SpriteRenderer>().color = new Vector4(171, 255, 189, 1);

		if (count > 0){
			spawned = true;
		}

		if (phase == 2 && count == 0 && spawned){
		timer = 0;
		phase = 0;
		phase2Complete = true;
		spring.SetActive(false);
		}
	}




	else if (phase == 3 && timer < 800){
		autumn.SetActive(false);
		spring.SetActive(false);
		summer.SetActive(true);
		winter.SetActive(false);
		bubble.GetComponent<SpriteRenderer>().color = new Vector4(255, 0, 0, 1);
	}

	else if (phase == 3 & timer >800){
		timer = 0;
		phase = 0;
		phase3Complete = true;
	}

	else if (phase == 4 && timer < 800){
		autumn.SetActive(false);
		spring.SetActive(false);
		summer.SetActive(false);
		winter.SetActive(true);
		bubble.GetComponent<SpriteRenderer>().color = new Vector4(135, 245, 255, 1);
	}

	else if (phase == 4 & timer >800){
		timer = 0;
		phase = 0;
		phase4Complete = true;
		winter.SetActive(false);
	}

		else if (phase == 5){
		autumn.SetActive(true);
		spring.SetActive(true);
		summer.SetActive(false);
		winter.SetActive(false);
		bubble.GetComponent<SpriteRenderer>().color = new Vector4(171, 255, 189, 1);

		if (count > 0){
			spawned = true;
		}

		if (phase == 5 && count == 0 && spawned){
		timer = 0;
		phase = 0;
		phase5Complete = true;
		spring.SetActive(false);
		autumn.SetActive(false);
		}
	}

		else if (phase == 6 && timer < 800){
		autumn.SetActive(false);
		spring.SetActive(false);
		summer.SetActive(true);
		winter.SetActive(true);
		bubble.GetComponent<SpriteRenderer>().color = new Vector4(135, 245, 255, 1);
	}

	else if (phase == 6 & timer >800){
		timer = 0;
		phase = 0;
		phase6Complete = true;
		summer.SetActive(false);
		winter.SetActive(false);

	}

		else if (phase == 7 && timer < 800){
		autumn.SetActive(true);
		spring.SetActive(false);
		summer.SetActive(false);
		winter.SetActive(true);
		bubble.GetComponent<SpriteRenderer>().color = new Vector4(135, 245, 255, 1);
	}

	else if (phase == 7 & timer >800){
		timer = 0;
		timer = 0;
		phase = 0;
		autumn.SetActive(false);
		winter.SetActive(false);
		phase7Complete = true;
		spawned = false;
	}

		else if (phase == 8){
		autumn.SetActive(true);
		spring.SetActive(true);
		summer.SetActive(false);
		winter.SetActive(true);
		bubble.GetComponent<SpriteRenderer>().color = new Vector4(171, 255, 189, 1);

		if (count > 0){
			spawned = true;
		}

		if (phase == 8 && count == 0 && spawned){
		timer = 0;
		phase = 0;
		phase8Complete = true;
		spring.SetActive(false);
		autumn.SetActive(false);
		winter.SetActive(false);
		}
	}

			if (boss.GetComponent<bossHealth>().health > 0 && phase8Complete){
			phase = 5;
			phase5Complete = false;
			phase6Complete = false;
			phase7Complete = false;
			phase8Complete = false;
		}


	}



}
