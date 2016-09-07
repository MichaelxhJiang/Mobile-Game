using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoundToggle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (gameObject.tag == "Sound FX Toggle")
			gameObject.GetComponent<Toggle> ().isOn = GameStates.toggleSoundFX;
		else if (gameObject.tag == "Music Toggle")
			gameObject.GetComponent<Toggle> ().isOn = GameStates.toggleMusic;
	}
	
	public void toggleSound () {
		if (gameObject.tag == "Sound FX Toggle") {
			GameStates.toggleSoundFX = gameObject.GetComponent<Toggle> ().isOn;
			print (GameStates.toggleSoundFX);
		} else if (gameObject.tag == "Music Toggle") {
			GameStates.toggleMusic = gameObject.GetComponent<Toggle> ().isOn;
			print (GameStates.toggleMusic);
		}
	}
}
