using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class CustomLobbyPlayer : NetworkLobbyPlayer
{
	[SyncVar] public bool seeker;
	public CustomLobbyManager lobbyManager;

	// Initialization
	void Start(){
		DontDestroyOnLoad (this);
		CmdSetSeeker (false);
	}

	[Command] public void CmdSetSeeker(bool isSeeker){
		seeker = isSeeker;
	}

	// Shows GUI in lobby scene for each lobby player
	void OnGUI()
	{
		if (!ShowLobbyGUI)
			return;

		var lobby = NetworkManager.singleton as NetworkLobbyManager;
		if (lobby)
		{
			if (!lobby.showLobbyGUI)
				return;

			string loadedSceneName = SceneManager.GetSceneAt(0).name;
			if (loadedSceneName != lobby.lobbyScene)
				return;
		}

		Rect rec = new Rect(100 + slot * 100, 200, 90, 20);

		if (isLocalPlayer)
		{
			string youStr;
			if (readyToBegin)
			{
				youStr = "(Ready)";
			}
			else
			{
				youStr = "(Not Ready)";
			}
			GUI.Label(rec, youStr);

			if (readyToBegin)
			{
				rec.y += 25;
				if (GUI.Button(rec, "STOP"))
				{
					SendNotReadyToBeginMessage();
				}
			}
			else
			{
				rec.y += 25;
				if (GUI.Button(rec, "START"))
				{
					SendReadyToBeginMessage();
				}

				rec.y += 25;
				if (GUI.Button(rec, "Remove"))
				{
					ClientScene.RemovePlayer(GetComponent<NetworkIdentity>().playerControllerId);
				}
				rec.y += 25;
				if(!seeker){
					if (GUI.Button(rec, "Hider"))
					{
						CmdSetSeeker (true);
					}
				}else{
					if (GUI.Button(rec, "Seeker"))
					{
						CmdSetSeeker (false);
					}
				}
				}
		}
		else
		{
			GUI.Label(rec, "Player [" + netId + "]");
			rec.y += 25;
			GUI.Label(rec, "Ready [" + readyToBegin + "]");
		}
	}
}
