using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
	public bool paused;
	public GUISkin buttonSkin;
	public Texture pauseImage;
	public Texture resumeImage;

	void OnGUI(){
		GUI.skin = buttonSkin;

		if (!paused) {
			if (GUI.Button (new Rect (Screen.width - Screen.width / 6, 0, Screen.width / 6, Screen.height / 10), pauseImage)) {
				PauseGame ();
			}
		} else {
			if (GUI.Button (new Rect (Screen.width / 8.0f, Screen.height / 3, Screen.width * 3 / 4 , Screen.height / 8), "Resume")) {
				PauseGame ();
			}
			if (GUI.Button (new Rect (Screen.width / 8.0f, Screen.height / 3 + Screen.height / 8, Screen.width * 3 / 4, Screen.height / 8), "Main menu")) {
				PauseGame ();
				SceneManager.LoadScene (0); 
			}
		}
			
	}

	public void PauseGame () {
		paused = !paused;
		if (paused) {
			Time.timeScale = 0;
			Debug.Log (Time.timeScale);
		} else {
			Time.timeScale = 1;
			Debug.Log (Time.timeScale);
		}
	}

	// Use this for initialization
	void Start () {
		paused = false;
	}
}
