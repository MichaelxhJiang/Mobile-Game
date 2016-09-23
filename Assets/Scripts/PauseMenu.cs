using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class PauseMenu : MonoBehaviour {
	public bool paused;
	public GUISkin buttonSkin;
	public Texture pauseImage;
	private GUIStyle pauseBtnStyle;

	void OnGUI(){
		GUI.skin = buttonSkin;

		if (!paused) {
			if (GUI.Button (new Rect (Screen.width - Screen.width / 7, 0, Screen.width / 7, Screen.height / 10), pauseImage)) {
				PauseGame ();
			}
		} else {
			if (GUI.Button (new Rect (Screen.width / 8.0f, Screen.height / 3, Screen.width * 3 / 4 , Screen.height / 8), "Resume")) {
				PauseGame ();
			}
			if (GUI.Button (new Rect (Screen.width / 8.0f, Screen.height / 3 + Screen.height / 8, Screen.width * 3 / 4, Screen.height / 8), "Main menu")) {
				PauseGame ();
				if (Advertisement.IsReady())
				{
					Advertisement.Show();
				}
				SceneManager.LoadScene (0); 
			}
		}
	}

	public void PauseGame () {
		//Turn off lighting
		GameObject light = GameObject.FindGameObjectWithTag ("Main Light");

		paused = !paused;
		if (paused) {
			Time.timeScale = 0;
			light.GetComponent<Light> ().enabled = false;
			//Debug.Log (Time.timeScale);
		} else {
			Time.timeScale = 1;
			light.GetComponent<Light> ().enabled = true;
			//Debug.Log (Time.timeScale);
		}
	}

	// Use this for initialization
	void Start () {
		paused = false;

		pauseBtnStyle = new GUIStyle ();
		pauseBtnStyle.stretchWidth = true;
		pauseBtnStyle.stretchHeight = true;
	}
}
