using UnityEngine;
using System.Collections;

public class PuckScore : MonoBehaviour {
	public AudioClip hitSound;
	public AudioClip scoreSound;
	private AudioSource source;

	private ScoreVariables sv;
	private ScoreAndSetup sas;
	// Use this for initialization
	void Start () {
		sv = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<ScoreVariables> ();
		sas = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<ScoreAndSetup> ();
		source = GetComponent<AudioSource> ();
	}
	
	//Check if the puck has hit a goal
	void OnCollisionEnter (Collision hit) {
		if (hit.gameObject.tag == "Player Net") {
			hit.gameObject.GetComponent<ParticleSystem> ().Play ();
			sv.increaseAIScore ();
			sas.resetPositions ();
			if (GameStates.toggleSoundFX && !sv.gameEnd) {
				source.PlayOneShot (scoreSound, 1.0f);
			}
		} else if (hit.gameObject.tag == "AI Net") {
			hit.gameObject.GetComponent<ParticleSystem> ().Play ();
			sv.increasePlayerScore ();
			sas.resetPositions ();
			if (GameStates.toggleSoundFX) {
				source.PlayOneShot (scoreSound, 1.0f);
			}
		} else if (hit.gameObject.tag == "Wall" || hit.gameObject.tag == "Player" || hit.gameObject.tag == "AI" && GameStates.toggleSoundFX) {
			GetComponent<EllipsoidParticleEmitter> ().Emit();
			if (hit.gameObject.tag == "Wall" && GameStates.toggleSoundFX) {
				source.PlayOneShot(hitSound,0.3f);
			}
		}
	}
}
