using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
	public GameObject AI;
	public GameObject player;
	public ScoreVariables sv;
	public Rigidbody rb;
	public MeshRenderer mr;
	public float countdown = 0;
	public float timer = 0;
	// Use this for initialization
	void OnGUI(){
		GUI.Box(new Rect(Screen.width - 70, 0, 70, 30), ""+timer);
	}
	void Start () {
		sv = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<ScoreVariables> ();
		rb = GetComponent<Rigidbody> ();
		mr = GetComponent<MeshRenderer> ();
		AI = GameObject.FindGameObjectWithTag ("AI");
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (countdown >= Time.time) {
			timer = countdown - Time.time;
		}
		if (countdown < Time.time) {
			AI.GetComponent<AIPaddle> ().enabled = true;
			player.GetComponent<PlayerPaddleController> ().enabled = true;
		}
		if (transform.position.z > 37) {
			sv.increasePlayerScore ();
			transform.position = new Vector3 (0f, 0.5f, 0f);
			rb.velocity = Vector3.zero;
			AI.GetComponent<AIPaddle> ().enabled = false;
			player.GetComponent<PlayerPaddleController> ().enabled = false;
			AI.transform.position = new Vector3 (0, 1, 30);
			player.transform.position = new Vector3 (0, 1, -30);
			countdown = Time.time + 1;
		} else if (transform.position.z < -38) {
			sv.increaseAIScore ();
			transform.position = new Vector3 (0f, 0.5f, 0f);
			rb.velocity = Vector3.zero;
			AI.GetComponent<AIPaddle> ().enabled = false;
			player.GetComponent<PlayerPaddleController> ().enabled = false;
			AI.transform.position = new Vector3 (0, 1, 30);
			player.transform.position = new Vector3 (0, 1, -30);
			countdown = Time.time + 1;
		}	
	}
}
