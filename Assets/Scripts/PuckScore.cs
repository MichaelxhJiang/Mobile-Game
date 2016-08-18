using UnityEngine;
using System.Collections;

public class PuckScore : MonoBehaviour {
	private ScoreVariables sv;
	private ScoreAndSetup sas;
	// Use this for initialization
	void Start () {
		sv = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<ScoreVariables> ();
		sas = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<ScoreAndSetup> ();
	}
	
	//Check if the puck has hit a goal
	void OnCollisionEnter (Collision hit) {
		if (hit.gameObject.tag == "Player Net") {
			hit.gameObject.GetComponent<ParticleSystem> ().Play ();
			sv.increaseAIScore ();
			sas.resetPositions ();
		} else if (hit.gameObject.tag == "AI Net") {
			hit.gameObject.GetComponent<ParticleSystem> ().Play ();
			sv.increasePlayerScore ();
			sas.resetPositions ();
		} else if (hit.gameObject.tag == "Wall") {
			GetComponent<EllipsoidParticleEmitter> ().Emit();
		}
	}
}
