using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class menuMusic : MonoBehaviour {
	public static bool AudioBegin = false; 
	static bool playing = false;

	void Awake()
	{
		if (!AudioBegin && GameStates.toggleMusic == true) {
			
			GetComponent<AudioSource>().Play ();
			DontDestroyOnLoad (gameObject);
			AudioBegin = true;
			playing = true;
		} 
	}
	void Update () {
		if (SceneManager.GetActiveScene ().name == "main" || SceneManager.GetActiveScene ().name == "2 player") {
			GetComponent<AudioSource> ().Stop ();
			AudioBegin = false;
			playing = false;
			//Destroy (gameObject);
		} 

		if (!GameStates.toggleMusic && playing == true) {
			GetComponent<AudioSource> ().Stop ();
			playing = false;
			AudioBegin = false;
		} else if (GameStates.toggleMusic && playing == false) {
			GetComponent<AudioSource> ().Play ();
			playing = true;
			AudioBegin = true;
		}
	}
}
