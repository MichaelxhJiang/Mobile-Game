using UnityEngine;
using System.Collections;

public class AIPaddle : MonoBehaviour {
	//Speed of AI paddle
	public float speed = 0.25f;
	//destination point
	private Vector3 endPoint;
	//Bounce force for puck
	public float bounceForce = 50.0f;
	//The puck game object
	public GameObject targ;
	//check if AI just recently touched the puck
	private bool hitPuck;
	//How long the AI needs to wait before chasing puck again
	private float waitTime;

	void Start () {
		hitPuck = false;
		waitTime = 0.2f;
	}

	// Update is called once per frame
	void Update () {
		if (targ.transform.position.z < 0) {
			transform.position = Vector3.MoveTowards (transform.position, new Vector3 (targ.transform.position.x, targ.transform.position.y, 30), speed);
		} else if (hitPuck) {
			transform.position = Vector3.MoveTowards (transform.position, targ.transform.position, -speed);
			StartCoroutine (WaitToChasePuck ());
		} else if (transform.position.z + (GetComponent<CapsuleCollider> ().radius * transform.localScale.x) > targ.transform.position.z + (targ.transform.localScale.z*0.1f)) {
			transform.position = Vector3.MoveTowards (transform.position, targ.transform.position, speed);
		} else {
			transform.position = Vector3.MoveTowards (transform.position, new Vector3 (targ.transform.position.x * 0.5f, targ.transform.position.y, targ.transform.position.z + (targ.transform.localScale.z*1.0f)), speed);
		}

		if (transform.position.z + (GetComponent<CapsuleCollider> ().radius * transform.localScale.x) < 0) {
			float z = 0 + (GetComponent<CapsuleCollider> ().radius * transform.localScale.x);
			transform.position = new Vector3 (transform.position.x, transform.position.y, z);
		}
	}

	void OnCollisionEnter(Collision hit) {
		if (hit.gameObject.tag == "Puck") {
			hit.rigidbody.AddForceAtPosition(-1 * hit.contacts[0].normal * bounceForce, hit.contacts[0].normal, ForceMode.Impulse);
			hitPuck = true;
		}
	}
	void OnCollisionStay(Collision hit){
		hitPuck = true;
		//Debug.Log ("Glitch");
	}

	IEnumerator WaitToChasePuck () {
		yield return new WaitForSeconds (waitTime);
		hitPuck = false;
	}
}
