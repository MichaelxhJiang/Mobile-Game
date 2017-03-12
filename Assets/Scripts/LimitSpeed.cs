using UnityEngine;
using System.Collections;

public class LimitSpeed : MonoBehaviour {
	//test
	//private float speed;
	//Vector3 lastPosition = Vector3.zero;
	private float maxSpeed = 80.0f;//Replace with your max speed

	void FixedUpdate()
	{
		if(gameObject.GetComponent<Rigidbody>().velocity.magnitude > maxSpeed){
			gameObject.GetComponent<Rigidbody>().velocity = Vector3.ClampMagnitude(gameObject.GetComponent<Rigidbody>().velocity, maxSpeed);
		}
	}
}
