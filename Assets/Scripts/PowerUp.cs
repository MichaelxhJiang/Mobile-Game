using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

	private string powerUpName;
	//puck prefab
	public GameObject puck;
	private GameObject[] clonedPucks;
	public bool cloned;
	public bool warped;

	// Use this for initialization
	void Start () {
		string character = GameStates.characterSelection;
		cloned = false;
		warped = false;

		if (character.Equals ("Time Warper")) {
			powerUpName = "WARP TIME";
		} else if (character.Equals ("Cloner")) {
			powerUpName = "CLONE";
			clonedPucks = new GameObject[3];
		}
	}

	void OnGUI(){
		GUI.skin.box.fontSize = Screen.height/30;
		if (!cloned) {
			if (GUI.Button (new Rect (0, Screen.height / 2.5f, Screen.width / 6, Screen.height / 8), powerUpName)) {
				activatePowerUp ();
			}
		}
	}

	private void activatePowerUp () {
		if (powerUpName.Equals ("CLONE")) {
			for (int i = 0; i < 3; i++) {
				clonedPucks [i] = (GameObject)Instantiate (puck, new Vector3 (gameObject.transform.position.x, 0.5f, 
					gameObject.transform.position.z + 1.0f), puck.transform.rotation);
			}

			clonedPucks [0].GetComponent<Rigidbody> ().AddForce (new Vector3 (1.0f, 0.0f, 1.0f) * 40);
			clonedPucks [1].GetComponent<Rigidbody> ().AddForce (new Vector3 (0.0f, 0.0f, 1.0f) * 40);
			clonedPucks [2].GetComponent<Rigidbody> ().AddForce (new Vector3 (-1.0f, 0.0f, 1.0f) * 40);
			GameObject ai = GameObject.FindGameObjectWithTag ("AI");

			ai.GetComponent<newAIPaddle> ().findClosestPuck ();
			cloned = true;
			StartCoroutine (powerUpDuration (3));
		} else if (powerUpName.Equals ("WARP TIME")) {
			GameObject ai = GameObject.FindGameObjectWithTag ("AI");
			ai.GetComponent<newAIPaddle> ().speed /= 2;
			warped = true;
		}
	}

	IEnumerator powerUpDuration (float waitTime) {
		yield return new WaitForSeconds (waitTime);
		newAIPaddle ai = GameObject.FindGameObjectWithTag ("AI").GetComponent<newAIPaddle>();
		if (cloned) {
			ai.resetPuckTarget ();
			cloned = false;
			foreach (GameObject clone in clonedPucks) {
				Destroy (clone);
			}
			Debug.Log ("destroyed clones");
		} else if (warped) {
			warped = false;

			ai.speed *= 2;
		}
	}
}
