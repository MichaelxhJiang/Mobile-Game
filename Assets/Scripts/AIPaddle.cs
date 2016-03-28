using UnityEngine;
using System.Collections;

public class AIPaddle : MonoBehaviour {
	public AudioClip hitSound;
	//Speed of AI paddle when returning to position
	public float speed = 10;
	//destination point
	private Vector3 endPoint;
	//Bounce force for puck
	public float bounceForce = 10.0f;
	//The puck game object
	public GameObject targ;
	//check if AI just recently touched the puck
	private bool hitPuck;
	//How long the AI needs to wait before chasing puck again

	private AudioSource source;
	private float volHigh = 1f;
	private float volLow = .5f;

	void Awake(){
		source = GetComponent<AudioSource> ();
	}

	void Start () {
		hitPuck = false;
	}

	// Update is called once per frame
	void Update () {
		float translation = speed * Time.deltaTime;

		if (targ.transform.position.z < 0) {//if the puck is on the other side
			transform.position = Vector3.MoveTowards (transform.position, new Vector3 (Mathf.Clamp(targ.transform.position.x, -5.0F, 5.0F), targ.transform.position.y, 30), translation);
		} else if (hitPuck) {//if we hit the puck away wait
			StartCoroutine (WaitToChasePuck (0.5f));
		} else if (transform.position.z + (GetComponent<CapsuleCollider> ().radius * transform.localScale.x) > targ.transform.position.z + (targ.transform.localScale.z*1.5f)) {
			float dis = Mathf.Sqrt (Mathf.Abs((transform.position.x - targ.transform.position.x) + (transform.position.z - targ.transform.position.z)));
			if (dis < 5) 
				transform.position = Vector3.MoveTowards (transform.position, targ.transform.position, translation + ((5-dis)/10));
			else
				transform.position = Vector3.MoveTowards (transform.position, targ.transform.position, translation);
		} else {
			transform.position = Vector3.MoveTowards (transform.position, new Vector3 (targ.transform.position.x * 0.5f, targ.transform.position.y, targ.transform.position.z + (targ.transform.localScale.z*1.5f)), translation);
		}
			
	}

	void OnCollisionEnter(Collision hit) {
		if (hit.gameObject.tag == "Puck") {
			float vol = Random.Range (volLow, volHigh);
			source.PlayOneShot(hitSound,vol);
			hit.rigidbody.AddForceAtPosition(-1 * hit.contacts [0].normal * bounceForce, hit.contacts[0].normal, ForceMode.Impulse);
		}
	}
		

	void OnCollisionStay(Collision hit){
		hitPuck = true;
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
