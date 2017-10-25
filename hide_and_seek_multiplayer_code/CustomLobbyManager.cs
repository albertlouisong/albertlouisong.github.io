using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CustomLobbyManager : NetworkLobbyManager {

	// When a player is loaded into the game scene set up if the player is a seeker or not
	public override bool OnLobbyServerSceneLoadedForPlayer(GameObject lobbyPlayer, GameObject gamePlayer){
		base.OnLobbyServerSceneLoadedForPlayer (lobbyPlayer, gamePlayer);
		var player = gamePlayer.GetComponent<SetupLocalPlayer> ();
		player.seeker = lobbyPlayer.GetComponent<CustomLobbyPlayer> ().seeker;
		return true;
	}

	// Switches players prefab to either hider or seeker
	public void SwitchPlayer(SetupLocalPlayer player, int cid){
		GameObject newPlayer = Instantiate (spawnPrefabs [cid],
           player.gameObject.transform.position,
           player.gameObject.transform.rotation);
		playerPrefab = spawnPrefabs [cid];
		newPlayer.GetComponent<SetupLocalPlayer> ().seeker = player.seeker;
		Destroy (player.gameObject);
		NetworkServer.ReplacePlayerForConnection (player.connectionToClient, newPlayer, 0);
	}
}
