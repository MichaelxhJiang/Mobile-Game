using UnityEngine;
using System.Collections;

public class RotateLight : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.Translate(0.0f, Time.deltaTime * 10.0f, 0.0f, Space.Self); 
		gameObject.transform.Rotate ( new Vector3 (0.0f, Time.deltaTime * 50.0f, 0.0f), Space.World);
	}
}
