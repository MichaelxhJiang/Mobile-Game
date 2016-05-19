using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

	private string powerUpName;
	//puck prefab
	public GameObject puck;
	private GameObject[] clonedPucks;

	// Use this for initialization
	void Start () {
		string character = GameStates.characterSelection;

		if (character.Equals ("Time Warper")) {
			powerUpName = "WARP" + "\r\n" + "TIME";
		} else if (character.Equals ("Cloner")) {
			powerUpName = "CLONE";
			clonedPucks = new GameObject[3];
		}
	}

	void OnGUI(){
		GUI.skin.box.fontSize = Screen.height/30;

		if (GUI.Button (new Rect (0, Screen.height/2.5f, Screen.width / 6, Screen.height / 8), powerUpName)) {
			activatePowerUp ();
		}
	}

	private void activatePowerUp () {
		if (powerUpName.Equals("CLONE")) {
			for (int i = 0; i < 3; i++) {
				clonedPucks [i] = (GameObject)Instantiate (puck, new Vector3 (gameObject.transform.position.x, 0.5f, 
					gameObject.transform.position.z + 1.0f), puck.transform.rotation);
			}

			clonedPucks[0].GetComponent<Rigidbody> ().AddForce (new Vector3 (1.0f, 0.0f, 1.0f) * 40);
			clonedPucks[1].GetComponent<Rigidbody> ().AddForce (new Vector3 (0.0f, 0.0f, 1.0f) * 40);
			clonedPucks[2].GetComponent<Rigidbody> ().AddForce (new Vector3 (-1.0f, 0.0f, 1.0f) * 40);
		}
	}
}
