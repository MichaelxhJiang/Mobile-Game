using UnityEngine;
using System.Collections;

public class HittingPuck : MonoBehaviour {
	public float speed = 0;
	Vector3 lastPosition = Vector3.zero;
	public float bounceForce = 60.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		speed = (transform.position - lastPosition).magnitude;
		lastPosition = transform.position;
	}

	void OnCollisionEnter(Collision hit) {
		if (hit.gameObject.tag == "Puck" && speed > 0.7) {
			hit.rigidbody.AddForceAtPosition (-1 * hit.contacts [0].normal * (speed * bounceForce), hit.contacts [0].normal, ForceMode.Impulse);
		} else {
			hit.rigidbody.AddForceAtPosition (-1 * hit.contacts [0].normal * speed, hit.contacts [0].normal, ForceMode.Impulse);
		}
	}
}
