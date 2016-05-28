using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
	public bool paused;

	void OnGUI(){
		GUI.skin.box.fontSize = Screen.height/30;
		GUIStyle textFont = new GUIStyle ();

		if (!paused) {
			if (GUI.Button (new Rect (0 + Screen.width / 4, 0, Screen.width / 2, Screen.height / 10), "Pause")) {
				PauseGame ();
			}
		} else {
			if (GUI.Button (new Rect (0 + Screen.width / 4, 0, Screen.width / 2, Screen.height / 10), "Resume")) {
				PauseGame ();
			}
			if (GUI.Button (new Rect (0 + Screen.width / 4, Screen.height / 3, Screen.width / 2, Screen.height / 10), "Main menu")) {
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
