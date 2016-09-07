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
	public Texture2D barMask;
	public Texture2D barFill;
	public Texture2D powerUpLabel;
	private bool playEffect;

	public GUISkin powerUpSkin;

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
		} else {
			powerUpName = "NONE";
		}
		if (!powerUpName.Equals ("NONE")) {
			InvokeRepeating ("fillUpPowerBar", 1.0f, 0.2f);
		}
	}

	void OnGUI(){
		GUI.skin = powerUpSkin;

		if (!powerUpName.Equals("NONE")) {
			//Update bar
			GUI.DrawTexture(new Rect(20, Screen.height * 10 / 11, Screen.width * 3 / 4, Screen.height / 11), barMask);
			GUI.DrawTexture (new Rect(30, Screen.height * 10 / 11 + 10, (Screen.width * 3 / 4 - 20) * percentageBar, Screen.height / 11 - 20), barFill);
			GUI.DrawTexture (new Rect(40, Screen.height * 10 / 11 + 20, (Screen.width * 3 / 4 - 30), Screen.height / 11 - 30), powerUpLabel);
		}

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
			percentageBar += 0.01f;
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

			clonedPucks [0].GetComponent<Rigidbody> ().AddForce (new Vector3 (-50.0f, 0.0f, 100.0f), ForceMode.Impulse);
			clonedPucks [1].GetComponent<Rigidbody> ().AddForce (new Vector3 (0.0f, 0.0f, 100.0f), ForceMode.Impulse);
			clonedPucks [2].GetComponent<Rigidbody> ().AddForce (new Vector3 (50.0f, 0.0f, 100.0f), ForceMode.Impulse);
			GameObject ai = GameObject.FindGameObjectWithTag ("AI");

			ai.GetComponent<newAIPaddle> ().findClosestPuck ();
			cloned = true;
			StartCoroutine (powerUpDuration (8));
		} else if (powerUpName.Equals ("WARP TIME")) {
			GameObject ai = GameObject.FindGameObjectWithTag ("AI");
			ai.GetComponent<newAIPaddle> ().speed /= 20;
			ai.GetComponent<ParticleSystem> ().Play ();
			warped = true;
			StartCoroutine (powerUpDuration (10));
		}
	}

	public void destroyClonedPucks () {
		foreach (GameObject clone in clonedPucks) {
			Destroy (clone);
		}
	}

	public void forceStopPowerUp () {
		GameObject powerUpEffect = GameObject.FindGameObjectWithTag ("PowerUp");
		powerUpEffect.GetComponent<ParticleSystem> ().Stop ();
		newAIPaddle ai = GameObject.FindGameObjectWithTag ("AI").GetComponent<newAIPaddle>();
		if (cloned) {
			ai.resetPuckTarget ();
			cloned = false;
			destroyClonedPucks ();
			Debug.Log ("destroyed clones");
		} else if (warped) {
			warped = false;

			ai.speed *= 5;
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

			ai.speed = GameStates.AIspeed;
			ai.GetComponent<ParticleSystem> ().Stop ();
		}
	}
}
