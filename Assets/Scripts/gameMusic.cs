using UnityEngine;
using System.Collections;

public class gameMusic : MonoBehaviour {
	
	void Awake() {
		StartCoroutine(pause());
	}

	IEnumerator pause () {
		yield return new WaitForSeconds (3);
		if (GameStates.toggleMusic == true) {
			GetComponent<AudioSource>().Play ();
		} 
	}
}
