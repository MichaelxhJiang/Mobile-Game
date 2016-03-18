using UnityEngine;
using System.Collections;

public class AIPaddle : MonoBehaviour {
	//Speed of AI paddle
	public float speed = 0.7f;
	//destination point
	private Vector3 endPoint;
	//alter this to change the speed of the movement of player / gameobject
	public float duration = 0.5f;
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
		waitTime = 0.5f;
	}

	// Update is called once per frame
	void Update () {
		if (hitPuck) {
			transform.position = Vector3.MoveTowards (transform.position, targ.transform.position, -speed * 0.5f);
			StartCoroutine (WaitToChasePuck());
		} else {
			transform.position = Vector3.MoveTowards (transform.position, targ.transform.position, speed);
		}
	}

	void OnCollisionEnter(Collision hit) {
		if (hit.gameObject.tag == "Puck") {
			hitPuck = true;
			float velX = GetComponent<Rigidbody>().velocity.x + 1;
			float velZ = GetComponent<Rigidbody>().velocity.z + 1;
			hit.rigidbody.AddForceAtPosition(new Vector3(velX * bounceForce, 0, velZ * bounceForce), hit.contacts[0].normal, ForceMode.Impulse);
		}
	}
	void OnCollisionStay(Collision hit){
		hitPuck = true;
		Debug.Log ("Glitch");
	}

	IEnumerator WaitToChasePuck () {
		yield return new WaitForSeconds (waitTime);
		hitPuck = false;
	}
}
