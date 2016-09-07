using UnityEngine;
using System.Collections;

public class TwoPlayerScoring : MonoBehaviour {

	private GameObject player2;
	private GameObject player1;
	public GameObject gm;
	private TwoPlayerScoreVariables sv;
	private Rigidbody rb;
	public int countDown;
	public GUISkin boxSkin;
	private double timer;
	private bool showTimer;

	//Set up timer UI
	void OnGUI(){
		GUI.skin = boxSkin;
		if (showTimer)
			GUI.Box(new Rect(Screen.width/2 - Screen.width / 4, Screen.height /2 - Screen.height / 8, Screen.width / 2, Screen.height / 4), ""+timer);
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
		showTimer = true;
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
		if (hit.gameObject.tag == "Player Net") {
			Debug.LogError ("score");
			sv.increasePlayer2Score ();
			hit.gameObject.GetComponent<ParticleSystem> ().Play ();
			resetPositions ();
		} else if (hit.gameObject.tag == "AI Net") {
			Debug.LogError ("score");
			sv.increasePlayer1Score ();
			hit.gameObject.GetComponent<ParticleSystem> ().Play ();
			resetPositions ();
		} else if (hit.gameObject.tag == "Wall" || hit.gameObject.tag == "Player 1" || hit.gameObject.tag == "Player 2") {
			GetComponent<EllipsoidParticleEmitter> ().Emit();
		}
	}

	//Starts a countdown before the game begins
	IEnumerator waitForSeconds () {
		for (int i = countDown; i >=0; i--) {
			yield return new WaitForSeconds (1);
			timer = i * 1.0;
		}
		showTimer = false;
		gm.GetComponent<Better2playercontroller> ().enabled = true;
	}
}
