using UnityEngine;
using System.Collections;

public class ScoreAndSetup : MonoBehaviour {
	private GameObject AI;
	private GameObject player;
	private ScoreVariables sv;
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
		sv = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<ScoreVariables> ();
		rb = GetComponent<Rigidbody> ();
		AI = GameObject.FindGameObjectWithTag ("AI");
		player = GameObject.FindGameObjectWithTag ("Player");
		timer = countDown;

		//Instantiate the puck
		//Instantiate (puck, new Vector3 (0.0f, 0.5f, 0.0f), puck.transform.rotation);

		//Start the count down
		CountDown ();
	}
	//Count down starts coroutine which invokes the waitForSeconds method
	void CountDown () {
		AI.GetComponent<newAIPaddle> ().enabled = false;
		player.GetComponent<PlayerPaddleController> ().enabled = false;
		StartCoroutine(waitForSeconds());

	}
	//Resets the positions of game objects
	void resetPositions() {
		transform.position = new Vector3 (0f, 0.5f, 0f);
		rb.velocity = Vector3.zero;
		AI.transform.position = new Vector3 (0, 1, 30);
		player.transform.position = new Vector3 (0, 1, -30);
		CountDown ();
	}

	//Check if the puck has hit a goal
	void OnCollisionEnter (Collision hit) {
		if (hit.gameObject.tag == "Player Net") {
			sv.increaseAIScore ();
			resetPositions ();
		} else if (hit.gameObject.tag == "AI Net") {
			sv.increasePlayerScore ();
			resetPositions ();
		}
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


