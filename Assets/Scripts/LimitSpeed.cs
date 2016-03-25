using UnityEngine;
using System.Collections;

public class LimitSpeed : MonoBehaviour {
	//test
	public float speed;
	Vector3 lastPosition = Vector3.zero;
	public float maxSpeed = 5f;//Replace with your max speed

	void FixedUpdate()
	{
		speed = (transform.position - lastPosition).magnitude;
		lastPosition = transform.position;
		if(speed > maxSpeed)
		{
			GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * maxSpeed;
		}
	}
}
