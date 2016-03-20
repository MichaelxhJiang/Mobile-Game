using UnityEngine;
using System.Collections;

public class WallPhysics : MonoBehaviour {
	public float reducePuckSpeed = 0.3f;

	void OnCollisionEnter(Collision hit) {
		if (hit.gameObject.tag == "Puck") {
			float velX = hit.rigidbody.velocity.x;
			float velZ = hit.rigidbody.velocity.z;
			//Reduce speed by adding force opposite to the direction of puck
			hit.rigidbody.AddForce(new Vector3(-velX * reducePuckSpeed, 0.0f, -velZ * reducePuckSpeed));
		}
	}
}
