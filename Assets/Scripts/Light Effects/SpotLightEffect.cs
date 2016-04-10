using UnityEngine;
using System.Collections;

public class SpotLightEffect : MonoBehaviour {
	public GameObject spotLight;
	// Use this for initialization
	void Start () {
		Debug.Log ("Initializing lights");

		//Hardcode positions of lights for now :p
		Instantiate (spotLight, new Vector3 (20, 15, -25), spotLight.transform.rotation);		//bottom right
		Instantiate (spotLight, new Vector3 (-20, 15, -25), spotLight.transform.rotation);	//bottom left
		Instantiate (spotLight, new Vector3 (20, 15, 25), spotLight.transform.rotation);		//top right
		Instantiate (spotLight, new Vector3 (-20, 15, -25), spotLight.transform.rotation);	//top left
		Instantiate (spotLight, new Vector3 (0, 15, 0), spotLight.transform.rotation);		//centre
	}
}
