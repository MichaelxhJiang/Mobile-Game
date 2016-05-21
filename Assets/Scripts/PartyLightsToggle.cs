using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PartyLightsToggle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Toggle>().isOn = GameStates.partyLights;
	}

	public void togglePartyLights () {
		GameStates.partyLights = gameObject.GetComponent<Toggle>().isOn;
		Debug.Log ("party lights is set to " + GameStates.partyLights);
	}

}
