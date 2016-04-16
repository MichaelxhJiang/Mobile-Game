using UnityEngine;
using System.Collections;

public class RotateLight : MonoBehaviour {

	private int rotation;
	private float rotateSpeed;
	private float moveSpeed;
	void Start () {
		if (Random.Range (0.0f, 1.0f) < 0.5f) {
			rotation = 1;
		} else {
			rotation = -1;
		}
		float rand = Random.Range (0.0f, 1.0f);
		if (rand < 0.25f) {
			gameObject.GetComponent<Light> ().color = Color.blue;
		} else if (rand < 0.5) {
			gameObject.GetComponent<Light> ().color = Color.red;
		} else if (rand < 0.75) {
			gameObject.GetComponent<Light> ().color = Color.yellow;
		} else {
			gameObject.GetComponent<Light> ().color = Color.white;
		}

		rotateSpeed = 70 * Random.Range (1.0f, 3.0f);
		moveSpeed = 15 * Random.Range (0.5f, 2.0f);
		gameObject.GetComponent<Light>().range = Random.Range (6.0f, 12.0f);
		gameObject.GetComponent<Light> ().range = Random.Range (4.0f, 6.0f);
	}

	// Update is called once per frame
	void Update () {
		gameObject.transform.Translate(0.0f, Time.deltaTime * moveSpeed, 0.0f, Space.Self); 
		gameObject.transform.Rotate ( new Vector3 (0.0f, Time.deltaTime * rotateSpeed * rotation, 0.0f), Space.World);
	}
}
