using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameStates : MonoBehaviour {

	public static bool partyLights = false;
	public static float AIspeed = 0;
	public static float AIbounceforce =  0;

	public void easyAI () {
		AIspeed = 40;
		AIbounceforce = 40;
	}

	public void mediumAI () {
		AIspeed = 50;
		AIbounceforce = 45;
	}

	public void hardAI () {
		AIspeed = 65;
		AIbounceforce = 50;
	}

	public void insaneAI () {
		AIspeed = 75;
		AIbounceforce = 50;
		//Do some super cool AI algorithm 
		//to be implemented
	}
}
