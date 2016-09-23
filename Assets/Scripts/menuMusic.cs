using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class menuMusic : MonoBehaviour {
	public static bool AudioBegin = false; 
	static bool playing = false;

	void Awake()
	{
		if (!AudioBegin && GameStates.toggleMusic == true) {
			print ("wtf");
			GetComponent<AudioSource>().Play ();
			DontDestroyOnLoad (gameObject);
			AudioBegin = true;
			playing = true;
		} 
	}
	void Update () {
		if ((SceneManager.GetActiveScene ().name == "main" || SceneManager.GetActiveScene ().name == "2 player") && playing == true) {
			print ("glitch");
			GetComponent<AudioSource> ().Stop ();
			AudioBegin = false;
			playing = false;
			//Destroy (gameObject);
		} 

		if (!GameStates.toggleMusic && playing == true) {
			GetComponent<AudioSource> ().Stop ();
			playing = false;
			AudioBegin = false;
		} else if (GameStates.toggleMusic && playing == false && (SceneManager.GetActiveScene ().name != "main" && SceneManager.GetActiveScene ().name != "2 player")) {
			print ("glightsdlkfjlskdfj");
			GetComponent<AudioSource> ().Play ();
			playing = true;
			AudioBegin = true;
		}
	}
}
