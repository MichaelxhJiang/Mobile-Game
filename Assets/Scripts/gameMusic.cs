using UnityEngine;
using System.Collections;

public class gameMusic : MonoBehaviour {
	
	void Awake()
	{
		if (GameStates.toggleMusic == true) {
			GetComponent<AudioSource>().Play ();
		} 
	}
}
