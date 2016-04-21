using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {
	bool paused;

	// Use this for initialization
	void Start () {
		paused = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	
	}

	public void PauseGame () {
		paused = !paused;

		if (paused) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}
	}
}
