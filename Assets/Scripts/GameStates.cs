using UnityEngine;
using System.Collections;

public class GameStates : MonoBehaviour {

	public static bool partyLights = false;
	public static float AIspeed = 0;
	public static float AIbounceforce =  0;

	public void togglePartyLights () {
		partyLights = !partyLights;
		Debug.Log ("party lights is set to " + partyLights);
	}

	public void easyAI () {
		AIspeed = 30;
		AIbounceforce = 30;
	}

	public void mediumAI () {
		AIspeed = 45;
		AIbounceforce = 30;
	}

	public void hardAI () {
		AIspeed = 60;
		AIbounceforce = 35;
	}

	public void insaneAI () {
		AIspeed = 70;
		AIbounceforce = 40;
		//Do some super cool AI algorithm 
		//to be implemented
	}
}
