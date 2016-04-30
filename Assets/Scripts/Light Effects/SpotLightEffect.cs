using UnityEngine;
using System.Collections;

public class SpotLightEffect : MonoBehaviour {
	public GameObject spotLight;
	public GameObject puckLight;
	// Use this for initialization
	void Start () {
		GameObject spotlight = GameObject.FindGameObjectWithTag ("Main Light");
		if (GameStates.partyLights) {
			spotlight.GetComponent <Light> ().intensity = 4;
			createLights ();
		} else {
			spotlight.GetComponent <Light> ().intensity = 6;
		}
	}

	void createLights () {
		for (int x = -20; x <= 20; x += 10) {
			for (int z = -30; z <= 30; z += 6) {
				Instantiate (spotLight, new Vector3 (x, 15, z), spotLight.transform.rotation);
			}
		}

		//Light that follows puck
		Instantiate (puckLight, new Vector3 (0, 15, 0), puckLight.transform.rotation);
	}
}
