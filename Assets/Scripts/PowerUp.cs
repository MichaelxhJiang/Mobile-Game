using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

	private string powerUpName;
	//puck prefab
	public GameObject puck;
	private GameObject[] clonedPucks;
	public bool cloned;
	public bool warped;

	//For the power up bar
	public float percentageBar;
	public GameObject powerBarPrefab;
	private GameObject powerBar;
	private bool playEffect;

	public GUISkin buttonSkin;

	// Use this for initialization
	void Start () {
		string character = GameStates.characterSelection;
		cloned = false;
		warped = false;

		playEffect = false;
		percentageBar = 0;

		if (character.Equals ("Time Warper")) {
			powerUpName = "WARP TIME";
		} else if (character.Equals ("Cloner")) {
			powerUpName = "CLONE";
			clonedPucks = new GameObject[3];
		}
		powerBar = Instantiate (powerBarPrefab);
		powerBar.GetComponent<GUIBarScript> ().SetNewValue (0);

		InvokeRepeating ("fillUpPowerBar", 1.0f, 0.2f);
	}

	void OnGUI(){
		GUI.skin = buttonSkin;
		if (!cloned && !warped && percentageBar >= 1) {
			if (GUI.Button (new Rect (Screen.width - Screen.height / 8, Screen.height - Screen.height / 8, Screen.height / 8, Screen.height / 8), "")) {
				activatePowerUp ();
				percentageBar = 0;
				playEffect = false;
				GameObject powerUpEffect = GameObject.FindGameObjectWithTag ("PowerUp");
				powerUpEffect.GetComponent<ParticleSystem> ().Stop ();
			}
		}

		if (percentageBar >= 1 && !playEffect) {
			Debug.Log ("Power up effect on");
			playEffect = true;
			GameObject powerUpEffect = GameObject.FindGameObjectWithTag ("PowerUp");
			powerUpEffect.GetComponent<ParticleSystem> ().Play ();
		}
	}

	public void fillUpPowerBar () {
		if (percentageBar < 1) {
			//print (percentageBar);
			percentageBar += 0.01f;
			powerBar.GetComponent<GUIBarScript> ().SetNewValue (percentageBar);
		}
	}

	private void activatePowerUp () {
		if (powerUpName.Equals ("CLONE")) {
			for (int i = 0; i < 3; i++) {
				float x = gameObject.transform.position.x + i * 5.0f - 5.0f;
				if (x > 23.8) {
					x = 23;
				} else if ( x < -24) {
					x = -23.5f;
				}
				clonedPucks [i] = (GameObject)Instantiate (puck, new Vector3 (x, 0.5f, 
					gameObject.transform.position.z + 5.0f), puck.transform.rotation);
			}

			clonedPucks [0].GetComponent<Rigidbody> ().AddForce (new Vector3 (-50.0f, 0.0f, 100.0f) * 600 * Time.deltaTime, ForceMode.Impulse);
			clonedPucks [1].GetComponent<Rigidbody> ().AddForce (new Vector3 (0.0f, 0.0f, 100.0f) * 600 * Time.deltaTime, ForceMode.Impulse);
			clonedPucks [2].GetComponent<Rigidbody> ().AddForce (new Vector3 (50.0f, 0.0f, 100.0f) * 600 * Time.deltaTime, ForceMode.Impulse);
			GameObject ai = GameObject.FindGameObjectWithTag ("AI");

			ai.GetComponent<newAIPaddle> ().findClosestPuck ();
			cloned = true;
			StartCoroutine (powerUpDuration (8));
		} else if (powerUpName.Equals ("WARP TIME")) {
			GameObject ai = GameObject.FindGameObjectWithTag ("AI");
			ai.GetComponent<newAIPaddle> ().speed /= 3;
			ai.GetComponent<ParticleSystem> ().Play ();
			warped = true;
			StartCoroutine (powerUpDuration (8));
		}
	}

	public void destroyClonedPucks () {
		foreach (GameObject clone in clonedPucks) {
			Destroy (clone);
		}
	}

	public void forceStopPowerUp () {
		newAIPaddle ai = GameObject.FindGameObjectWithTag ("AI").GetComponent<newAIPaddle>();
		if (cloned) {
			ai.resetPuckTarget ();
			cloned = false;
			destroyClonedPucks ();
			Debug.Log ("destroyed clones");
		} else if (warped) {
			warped = false;

			ai.speed *= 3;
			Debug.Log ("stopped particle system");
			ai.GetComponent<ParticleSystem> ().Stop ();
		}
	}

	IEnumerator powerUpDuration (float waitTime) {
		yield return new WaitForSeconds (waitTime);
		newAIPaddle ai = GameObject.FindGameObjectWithTag ("AI").GetComponent<newAIPaddle>();
		if (cloned) {
			ai.resetPuckTarget ();
			cloned = false;
			destroyClonedPucks ();
			Debug.Log ("destroyed clones");
		} else if (warped) {
			warped = false;

			ai.speed *= 3;
			Debug.Log ("stopped particle system");
			ai.GetComponent<ParticleSystem> ().Stop ();
		}
	}
}
