using UnityEngine;
using System.Collections;

public class TwoPlayerScoring : MonoBehaviour {

	private GameObject player2;
	private GameObject player1;
	public GameObject gm;
	private TwoPlayerScoreVariables sv;
	private Rigidbody rb;
	public int countDown = 1;
	private double timer;

	//Set up timer UI
	void OnGUI(){
		GUI.skin.box.fontSize = Screen.height/30;
		GUI.Box(new Rect(Screen.width - Screen.width/3, 0, Screen.width/3, Screen.height/20), ""+timer);
	}

	void Start () {
		//Get all the reference objects needed
		sv = GameObject.FindGameObjectWithTag ("Two Player Game Manager").GetComponent<TwoPlayerScoreVariables> ();
		rb = GetComponent<Rigidbody> ();
		player2 = GameObject.FindGameObjectWithTag ("Player 2");
		player1 = GameObject.FindGameObjectWithTag ("Player 1");
		gm = GameObject.FindGameObjectWithTag ("Two Player Game Manager");
		timer = countDown;
		//Start the count down
		CountDown ();
	}
	//Count down starts coroutine which invokes the waitForSeconds method
	void CountDown () {
		gm.GetComponent<Better2playercontroller> ().enabled = false;
		StartCoroutine(waitForSeconds());

	}
	//Resets the positions of game objects
	void resetPositions() {
		Debug.Log ("reset positions");
		transform.position = new Vector3 (0f, 0.5f, 0f);
		rb.velocity = Vector3.zero;
		player2.transform.position = new Vector3 (0, 1, 30);
		player1.transform.position = new Vector3 (0, 1, -30);
		CountDown ();
	}

	//Check if the puck has hit a goal
	void OnCollisionEnter (Collision hit) {
		if (hit.gameObject.tag == "Player 1 Net") {
			sv.increasePlayer2Score ();
			resetPositions ();
		} else if (hit.gameObject.tag == "Player 2 Net") {
			sv.increasePlayer1Score ();
			resetPositions ();
		}
	}

	//Starts a countdown before the game begins
	IEnumerator waitForSeconds () {
		for (int i = countDown; i >=0; i--) {
			yield return new WaitForSeconds (1);
			timer = i * 1.0;
		}
		gm.GetComponent<Better2playercontroller> ().enabled = true;
	}
}
