using UnityEngine;
using System.Collections;

public class SpotLightEffect : MonoBehaviour {
	public GameObject spotLight;
	public GameObject puckLight;
	// Use this for initialization
	void Start () {
		Debug.Log ("Initializing lights");

		//Hardcode positions of lights for now :p
		Instantiate (spotLight, new Vector3 (10, 15, -15), spotLight.transform.rotation);		//bottom right
		Instantiate (spotLight, new Vector3 (-10, 15, -15), spotLight.transform.rotation);	//bottom left
		Instantiate (spotLight, new Vector3 (10, 15, 15), spotLight.transform.rotation);		//top right
		Instantiate (spotLight, new Vector3 (-10, 15, -15), spotLight.transform.rotation);	//top left
		Instantiate (spotLight, new Vector3 (0, 15, 0), spotLight.transform.rotation);		//centre

		//Light that follows puck
		Instantiate (puckLight, new Vector3 (0, 15, 0), puckLight.transform.rotation);
	}
}
