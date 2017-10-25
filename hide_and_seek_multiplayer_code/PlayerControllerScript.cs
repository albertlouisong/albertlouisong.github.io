using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour {

	public AudioClip[] stepAudioClips;
	public SetupLocalPlayer localPlayer;
	public int playerID;
	public bool seeker;
	public Vector3 lastPos;
	public Color color;
	public Material skeletonMaterial;
	public int steps;
	private float stepTime, stepTimer, stepDelay;
	private List<Player> otherPlayers;
	private Player hostPlayer;
	private bool finished;
	private bool messageSent;
	private bool canUpdate;

	// Use this for initialization
	void Start () {
		finished = false;
		canUpdate = false;
		messageSent = false;
		steps = 0;
		foreach(SkinnedMeshRenderer r in GetComponentsInChildren<SkinnedMeshRenderer>()){
			r.material = Instantiate (r.material) as Material;
		}
		stepDelay = 0.05f;
		stepTime = stepTimer = 0;
		StartCoroutine(LateCall(1f));
		localPlayer = GetComponent<SetupLocalPlayer> ();
	}

	// Calls late start
	IEnumerator LateCall(float waitTime){
		yield return new WaitForSeconds(waitTime);
		LateStart ();
	}


	// When every player has loaded in from the network this is called to initialize data
	private void LateStart(){
		otherPlayers = new List<Player> ();
		foreach(GameObject player in GameObject.FindGameObjectsWithTag ("Player")){
			otherPlayers.Add (new Player(player));
		}
		if(seeker){
			GetComponent<AudioSource> ().enabled = false;
			foreach(Player player in otherPlayers){
				if(player.playerController.seeker){
					otherPlayers.Remove (player);
					break;
				}
			}
		}else{
			foreach(Player player in otherPlayers){
				if(player.playerController.seeker){
					hostPlayer = player;
					break;
				}
			}
		}
		canUpdate = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(canUpdate){
			if(seeker){
				if(otherPlayers.Count == 0){
					finished = true;
				}
				if(finished){
					if(!messageSent){
						Invoke ("EndGame", 2f);
					}

				}
				foreach(Player hider in otherPlayers){
					if(!hider.player.GetComponent<SetupLocalPlayer>().alive){
						otherPlayers.Remove (hider);
						return;
					}
					if(!hider.audio.enabled){
						hider.audio.enabled = true;
						hider.audio.clip = stepAudioClips [0];
					}
					if(!hider.audio.isPlaying){
						if(hider.audio.clip == stepAudioClips [0]){
							hider.audio.clip = stepAudioClips [1];
						}else{
							hider.audio.clip = stepAudioClips [0];
						}
						hider.audio.Play ();
					}
					if(Mathf.Abs((hider.playerController.lastPos.x + hider.playerController.lastPos.z)
						- (hider.transform.position.x + hider.transform.position.z)) > 0.01f){
						float volumeIncrease = hider.audio.volume;
						volumeIncrease += 0.3f;
						Mathf.Clamp (volumeIncrease,0,1);
						hider.audio.volume = volumeIncrease;
					}else{
						float volumeDecrease = hider.audio.volume;
						volumeDecrease -= 0.1f;
						Mathf.Clamp (volumeDecrease,0,1);
						hider.audio.volume = volumeDecrease;
					}
					if(Vector3.Distance(gameObject.transform.position, hider.transform.position) >= 10
						&& Vector3.Distance(gameObject.transform.position, hider.transform.position) < 120){
						float transparency = 1.0f - (Vector3.Distance(gameObject.transform.position, hider.transform.position) / 120.0f);
						changeAlpha (transparency, hider);
					}else if(Vector3.Distance(gameObject.transform.position, hider.transform.position) > 120){
						changeAlpha (0.0f, hider);
					}else if (Vector3.Distance(gameObject.transform.position, hider.transform.position) < 10){
						changeAlpha (1.0f, hider);
					}
					hider.playerController.lastPos = hider.transform.position;
				}
			}else{
				if (Mathf.Abs ((lastPos.x + lastPos.z)
				   - (transform.position.x + transform.position.z)) > 0.01f) {
					stepTimer += Time.deltaTime;
					if(stepTimer >= stepDelay){
						stepTimer = 0;
						steps++;
						localPlayer.CmdChangeStep(steps);
					}
				}
				lastPos = transform.position;

				if(!localPlayer.alive){
					GetComponent<HiderController> ().enabled = false;
					GetComponent<PlayerControllerScript> ().enabled = false;
					GetComponent<ThirdPersonCamera> ().lookAt = hostPlayer.transform;
				}
				if(!hostPlayer.audio.enabled){
					hostPlayer.audio.enabled = true;
					hostPlayer.audio.loop = true;
					hostPlayer.audio.Play ();
				}
				if(Vector3.Distance(gameObject.transform.position, hostPlayer.transform.position) >= 10
					&& Vector3.Distance(gameObject.transform.position, hostPlayer.transform.position) < 120){
					float transparency = 1.0f - (Vector3.Distance(gameObject.transform.position, hostPlayer.transform.position) / 120.0f);
					changeAlpha (transparency, hostPlayer);
				}else if(Vector3.Distance(gameObject.transform.position, hostPlayer.transform.position) > 120){
					changeAlpha (0.0f, hostPlayer);
				}else if (Vector3.Distance(gameObject.transform.position, hostPlayer.transform.position) < 10){
					changeAlpha (1.0f, hostPlayer);
				}
			}
		}
	}

	// Changes alpha value of the material in the player prefabs
	private void changeAlpha(float transparency, Player player){
		for(int i = 0; i < player.render.Length; i++){
			player.render[i].material.color = new Color(player.render[i].material.color.r,
				player.render[i].material.color.g, player.render[i].material.color.b, transparency);
		}
	}

	// Detect if the hider is hit by the seekers attack
	private void OnTriggerEnter(Collider other){
		if(other.tag.Equals("Attack") && !seeker){
			localPlayer.CmdChangeAlive (false);
			GetComponent<Animator> ().enabled = false;
			foreach(SkinnedMeshRenderer s in GetComponentsInChildren<SkinnedMeshRenderer> ()){
				s.enabled = false;
			}
			foreach(CapsuleCollider c in GetComponents<CapsuleCollider>()){
				c.enabled = false;
			}
		}
	}

	// End the game and return all players to lobby
	private void EndGame(){
		localPlayer.returnToLobby ();
	}

	// Show the amount of steps required for the seeker to wins
	void OnGUI(){
		if(!seeker && GetComponent<SetupLocalPlayer>().currentStepCount >= 1){
			Rect rec = new Rect(Screen.width/2 - 100, 20, 200, 20);
			string youStr = "Steps required to win " + GetComponent<SetupLocalPlayer>().currentStepCount.ToString();
			GUI.Label(rec, youStr);
		}
	}

	// Returns the seeker
	public Player getSeeker(){
		return hostPlayer;
	}

	// Player model class that holds references to scripts
	public class Player{
		public GameObject player;
		public Transform transform;
		public AudioSource audio;
		public PlayerControllerScript playerController;
		public SkinnedMeshRenderer[] render;
		public Player(GameObject player){
			this.player = player;
			transform = player.transform;
			audio = player.GetComponent<AudioSource>();
			playerController = player.GetComponent<PlayerControllerScript>();
			render = player.GetComponentsInChildren<SkinnedMeshRenderer>();
		}

	}

}
