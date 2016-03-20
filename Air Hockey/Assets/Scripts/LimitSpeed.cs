using UnityEngine;
using System.Collections;

public class LimitSpeed : MonoBehaviour {

	public float maxSpeed = 200f;//Replace with your max speed
	void FixedUpdate()
	{
		if(GetComponent<Rigidbody>().velocity.magnitude > maxSpeed)
		{
			GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * maxSpeed;
		}
	}
}
