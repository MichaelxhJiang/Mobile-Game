using UnityEngine;
using System.Collections;

public class ScoreAndSetup : MonoBehaviour {
	private GameObject AI;
	private GameObject player;

	private Rigidbody rb;
	public int countDown = 3;
	//public GameObject puck;
	private double timer;
	//initially true
	private bool showTimer = true;

	public GUISkin boxSkin;

	//Set up timer UI
	void OnGUI(){
		GUI.skin = boxSkin;
		if (showTimer) {
			GUI.Box(new Rect(Screen.width/2 - Screen.width / 4, Screen.height /2 - Screen.height / 8, Screen.width / 2, Screen.height / 4), ""+timer);
		}
	}

	void Awake () {
		//Get all the reference objects needed

		rb = GameObject.FindGameObjectWithTag("Puck").GetComponent<Rigidbody> ();
		AI = GameObject.FindGameObjectWithTag ("AI");
		player = GameObject.FindGameObjectWithTag ("Player");
		timer = countDown;

		AI.GetComponent<newAIPaddle> ().enabled = false;
		player.GetComponent<PlayerPaddleController> ().enabled = false;
		//Start the count down
		StartCoroutine(waitForSeconds());
	}

	//Resets the positions of game objects
	public void resetPositions() {
		showTimer = true;
		rb.velocity = Vector3.zero;
		rb.transform.position = new Vector3 (0f, 0.5f, 0f);
		AI.transform.position = new Vector3 (0, 1, 30);
		player.transform.position = new Vector3 (0, 1, -30);
		if (player.GetComponent<PowerUp> ().cloned) {
			player.GetComponent<PowerUp> ().destroyClonedPucks ();
		}
		AI.GetComponent<newAIPaddle> ().resetPuckTarget ();
		AI.GetComponent<newAIPaddle> ().enabled = false;
		player.GetComponent<PlayerPaddleController> ().enabled = false;
		player.GetComponent<PowerUp> ().enabled = false;
		player.GetComponent<PlayerPaddleController> ().flag = false;
		player.GetComponent<PowerUp> ().forceStopPowerUp ();
		player.GetComponent<PowerUp> ().percentageBar = 0;

		StartCoroutine(waitForSeconds());
	}



	//Starts a countdown before the game begins
	IEnumerator waitForSeconds () {
		for (int i = countDown; i >=0; i--) {
			timer = i * 1.0;
			yield return new WaitForSeconds (1);
		}
		showTimer = false;

		AI.GetComponent<newAIPaddle> ().enabled = true;
		player.GetComponent<PlayerPaddleController> ().enabled = true;
		player.GetComponent<PowerUp> ().enabled = true;

		//start clock on scoreboard
		gameObject.GetComponent<ScoreVariables>().startClock ();
	}
}


