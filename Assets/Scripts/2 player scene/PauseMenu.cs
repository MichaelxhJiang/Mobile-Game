using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {
	public bool paused;
	public float temp;
	void OnGUI(){
		GUI.skin.box.fontSize = Screen.height/30;
		if (!paused) {
			if (GUI.Button (new Rect (0 + Screen.width / 3, 0, Screen.width / 3, Screen.height / 20), "Pause")) {
				PauseGame ();
			}
		} else {
			if (GUI.Button (new Rect (0 + Screen.width / 3, 0, Screen.width / 3, Screen.height / 20), "Pause")) {
				PauseGame ();
			}
			if (GUI.Button (new Rect (0 + Screen.width / 6, 0 + Screen.width / 6, Screen.width / 1.5f, Screen.height / 1.5f), "Back to main menu")) {
				PauseGame ();
				Application.LoadLevel ("Title Screen"); 
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
	
	// Update is called once per frame
	void Update () {
	
	}
}
