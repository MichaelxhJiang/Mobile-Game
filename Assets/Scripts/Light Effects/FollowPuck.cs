using UnityEngine;
using System.Collections;

public class FollowPuck : MonoBehaviour {
	private GameObject puck;

	void Start () {
		puck = GameObject.FindGameObjectWithTag ("Puck");
	}

	// Update is called once per frame
	void Update () {
		gameObject.transform.position = new Vector3 (puck.transform.position.x, 15, puck.transform.position.z);
	}
}
