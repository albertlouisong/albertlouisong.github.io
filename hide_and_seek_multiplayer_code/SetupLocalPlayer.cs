using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class SetupLocalPlayer : NetworkBehaviour {

	[SyncVar (hook = "OnChangeSeeker")] public bool seeker;
	[SyncVar (hook = "OnChangeAlive")] public bool alive;
	[SyncVar (hook = "OnChangeAnimation")] public string animationState = "Idle"; 
	[SyncVar (hook = "OnChangeStep")] public int step;
	public GameObject hider;
	public GameObject explosion;
	private Animator animator;
	public int requiredSteps, currentStepCount;
	private bool messageSent;

	// Initialize data and enable local players
	void Start () {
		var lobby = NetworkManager.singleton as NetworkLobbyManager;
		animator = GetComponent<Animator> ();
		animator.SetBool ("Run", false);
		alive = true;
		step = 0;
		currentStepCount = 1;
		messageSent = false;
		requiredSteps = 1250;
		Debug.Log (NetworkServer.connections.Count);

		if(isLocalPlayer){
			GetComponent<ThirdPersonCamera> ().enabled = true;
			GetComponent<PlayerControllerScript> ().enabled = true;
			CmdChangeSeeker (seeker);
			if(seeker){
				if(GetComponent<SkeletonController>() == null){
					CmdUpdatePlayerToSeeker ();
				}else{
					GetComponent<SkeletonController> ().enabled = true;
					GetComponent<SkeletonController> ().velocityForce = 2000.0f;
				}
			}else{
				GetComponent<HiderController> ().enabled = true;
				GetComponent<HiderController> ().velocityForce = 800.0f;
			}
		}	
		Debug.Log (seeker);
		GetComponent<PlayerControllerScript> ().seeker = seeker;
	}

	// Hook methods

	void OnChangeSeeker(bool n){
		seeker = n;
	}
	
	void OnChangeAlive(bool n){
		alive = n;
	}
	
	void OnChangeStep(int n){
		step = n;
	}

	void OnChangeAnimation(string n){
		if(isLocalPlayer){
			return;
		}
		UpdateAnimationState (n);
	}

	// Returns all players to lobby
	public void returnToLobby(){
		var lobby = NetworkManager.singleton as NetworkLobbyManager;
		lobby.SendReturnToLobby ();
	}

	private void UpdateAnimationState(string animState){
		if(animationState == animState){
			return;
		}
		animationState = animState;
		if(animationState.Equals("Run")){
				animator.SetBool ("Run", true);
		}else if(animationState.Equals("Idle")){
				animator.SetBool("Run", false);
		}else if(animationState.Equals("Attack")){
				animator.SetTrigger("Attack");
		}
	}

	// Command methods

	[Command] public void CmdChangeAnimState(string animState){
		UpdateAnimationState(animState);
	}
	
	[Command] public void CmdUpdatePlayerToSeeker(){
		NetworkManager.singleton.GetComponent<CustomLobbyManager> ().SwitchPlayer (this, 1);
	}

	[Command] public void CmdChangeSeeker(bool isSeeker){
		seeker = isSeeker;
		OnChangeSeeker (seeker);
	}
	
	[Command] public void CmdChangeAlive(bool isAlive){
		seeker = isAlive;
		OnChangeAlive (isAlive);
	}

	[Command] public void CmdChangeStep(int newStep){
		step = newStep;
		OnChangeStep (newStep);
	}
	
	[Command] public void CmdSeekerAttack(){
		GameObject attack = Instantiate (explosion, transform.position + (transform.forward*5), transform.rotation);
		NetworkServer.Spawn (attack);
		attack.transform.parent = this.transform;
	}

	// Update is called once per frame
	void Update () {
		if(isLocalPlayer){
				currentStepCount = requiredSteps - step;
		}
	}

	// Victory screen if hider wins
void OnGUI(){
	if(isLocalPlayer){
		if(currentStepCount <= 0){
			Rect rec = new Rect(Screen.width/2 - 45, 20, 90, 20);
			string youStr = "You Survived!";
			GUI.Label(rec, youStr);
			GetComponent<HiderController> ().enabled = false;
			GetComponent<ThirdPersonCamera> ().lookAt = GetComponent<PlayerControllerScript> ().getSeeker().transform;
			GetComponent<PlayerControllerScript> ().enabled = false;
			if(!messageSent){
					messageSent = true;
					Invoke ("returnToLobby", 2.0f);
			}
		}
	}
}

}