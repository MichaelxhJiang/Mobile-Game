using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameStates : MonoBehaviour {
	public static bool toggleSoundFX = true;
	public static bool toggleMusic = true;
	public static float AIspeed = 0;
	public static float AIbounceforce =  0;
	public static string characterSelection = "";

	public void easyAI () {
		AIspeed = 25;
		AIbounceforce = 30;
	}

	public void mediumAI () {
		AIspeed = 35;
		AIbounceforce = 40;
	}

	public void hardAI () {
		AIspeed = 50;
		AIbounceforce = 50;
	}

	public void insaneAI () {
		AIspeed = 75;
		AIbounceforce = 50;
	}

	public void timeWarper () {
		characterSelection = "Time Warper";
	}

	public void cloner () {
		characterSelection = "Cloner";
	}
}
