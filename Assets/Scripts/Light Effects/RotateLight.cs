using UnityEngine;
using System.Collections;

public class RotateLight : MonoBehaviour {

	private int rotation;
	private float rotateSpeed;
	void Start () {
		if (Random.Range (0.0f, 1.0f) < 0.5f) {
			rotation = 1;
		} else {
			rotation = -1;
		}

		rotateSpeed = 50 * Random.Range (1.0f, 1.5f);
	}

	// Update is called once per frame
	void Update () {
		gameObject.transform.Translate(0.0f, Time.deltaTime * 10.0f, 0.0f, Space.Self); 
		gameObject.transform.Rotate ( new Vector3 (0.0f, Time.deltaTime * rotateSpeed * rotation, 0.0f), Space.World);
	}
}
