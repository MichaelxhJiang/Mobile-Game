using UnityEngine;
using System.Collections;

public class PuckParticleEffect : MonoBehaviour {

	void onCollisionEnter (Collision hit) {
		Debug.Log ("Hit something");
		if (hit.collider.tag == "Wall") {
			Debug.Log ("Bounced :)");
			GetComponent<EllipsoidParticleEmitter> ().Emit();
		}
	}
}
