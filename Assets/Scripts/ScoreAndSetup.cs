using UnityEngine;
using System.Collections;

public class ScoreAndSetup : MonoBehaviour {
	private GameObject AI;
	private GameObject player;

	private Rigidbody rb;
	public int countDown = 3;
	//public GameObject puck;
	private double timer;


	//Set up timer UI
	void OnGUI(){
		GUI.Box(new Rect(Screen.width - 70, 0, 70, 30), ""+timer);
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
		AI.GetComponent<newAIPaddle> ().enabled = false;
		player.GetComponent<PlayerPaddleController> ().enabled = false;
		rb.transform.position = new Vector3 (0f, 0.5f, 0f);
		rb.velocity = Vector3.zero;
		AI.transform.position = new Vector3 (0, 1, 30);
		player.transform.position = new Vector3 (0, 1, -30);
		StartCoroutine(waitForSeconds());
	}



	//Starts a countdown before the game begins
	IEnumerator waitForSeconds () {
		for (int i = countDown; i >=0; i--) {
			yield return new WaitForSeconds (1);
			timer = i * 1.0;
		}
		AI.GetComponent<newAIPaddle> ().enabled = true;
		player.GetComponent<PlayerPaddleController> ().enabled = true;
	}
}


