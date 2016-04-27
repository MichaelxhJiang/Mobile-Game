using UnityEngine;
using System.Collections;

public class GameStates : MonoBehaviour {

	public static bool partyLights;
	// Use this for initialization
	void Start () {
		partyLights = false;
	}

	public void togglePartyLights () {
		partyLights = !partyLights;
	}
}
