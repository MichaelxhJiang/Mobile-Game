using UnityEngine;
using System.Collections;

public class AIPaddle : MonoBehaviour {
	public AudioClip hitSound;
	//Speed of AI paddle
	public float speed = 0.5f;
	//destination point
	private Vector3 endPoint;
	//Bounce force for puck
	public float bounceForce = 50.0f;
	//The puck game object
	public GameObject targ;
	//check if AI just recently touched the puck
	private bool hitPuck;
	//How long the AI needs to wait before chasing puck again

	private AudioSource source;
	private float volHigh = 1f;
	private float volLow = .5f;

	public float actualspeed = 0;
	Vector3 lastPosition = Vector3.zero;

	void Awake(){
		source = GetComponent<AudioSource> ();
	}

	void Start () {
		hitPuck = false;
	}

	// Update is called once per frame
	void Update () {
		actualspeed = (transform.position - lastPosition).magnitude;
		lastPosition = transform.position;
		if (targ.transform.position.z < 0) {
			transform.position = Vector3.MoveTowards (transform.position, new Vector3 (targ.transform.position.x, targ.transform.position.y, 30), speed);
		} else if (hitPuck) {
			StartCoroutine (WaitToChasePuck (0.2f));
		} else if (transform.position.z + (GetComponent<CapsuleCollider> ().radius * transform.localScale.x) > targ.transform.position.z + (targ.transform.localScale.z*1.5f)) {
			float dis = Mathf.Sqrt (Mathf.Abs((transform.position.x - targ.transform.position.x) + (transform.position.z - targ.transform.position.z)));
			if (dis < 5) 
				transform.position = Vector3.MoveTowards (transform.position, targ.transform.position, speed + ((5-dis)/4));
			else
				transform.position = Vector3.MoveTowards (transform.position, targ.transform.position, speed);
		} else {
			transform.position = Vector3.MoveTowards (transform.position, new Vector3 (targ.transform.position.x * 0.5f, targ.transform.position.y, targ.transform.position.z + (targ.transform.localScale.z*1.5f)), speed);
		}

		if (transform.position.z + (GetComponent<CapsuleCollider> ().radius * transform.localScale.x) < 0) {
			float z = 0 + (GetComponent<CapsuleCollider> ().radius * transform.localScale.x);
			transform.position = new Vector3 (transform.position.x, transform.position.y, z);
		}
	}

	void OnCollisionEnter(Collision hit) {
		if (hit.gameObject.tag == "Puck") {
			hit.rigidbody.AddForceAtPosition(-1 * hit.contacts [0].normal * (actualspeed * bounceForce), hit.contacts[0].normal, ForceMode.Impulse);
			StopChasing (0.4f);
		}
	}

	void OnCollisionStay(Collision hit){
		float vol = Random.Range (volLow, volHigh);
		hitPuck = true;
		source.PlayOneShot (hitSound, vol);
		//Debug.Log ("Glitch");
	}

	IEnumerator StopChasing(float waitTime){
		yield return new WaitForSeconds (waitTime);
		hitPuck = true;
	}

	IEnumerator WaitToChasePuck (float waitTime) {
		yield return new WaitForSeconds (waitTime);
		hitPuck = false;
	}
}
