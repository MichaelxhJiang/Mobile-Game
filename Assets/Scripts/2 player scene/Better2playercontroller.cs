using UnityEngine;
using System.Collections;
using System;

public class Better2playercontroller : MonoBehaviour {
	public GameObject gamePaddle1;
	public GameObject gamePaddle2;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		foreach (Touch t in Input.touches) {
			Ray ray = Camera.main.ScreenPointToRay(t.position);
			RaycastHit hitInfo;
			if (Physics.Raycast(ray, out hitInfo)){
				Vector3 v = hitInfo.point;
				v = new Vector3 (v.x, 1, v.z);
				if (v.z < -1.5) {
					gamePaddle1.transform.position = Vector3.MoveTowards (gamePaddle1.transform.position, v, 5);
					//Clamps top
					if (gamePaddle1.transform.position.z > -1.5) {
						gamePaddle1.transform.position = new Vector3 (gamePaddle1.transform.position.x, gamePaddle1.transform.position.y, -1.5f);
					}
					//Clamps bottom
					if (gamePaddle1.transform.position.z < -35.2) {
						gamePaddle1.transform.position = new Vector3 (gamePaddle1.transform.position.x, gamePaddle1.transform.position.y, -35.2f);
					}
					//Clamps left
					if (gamePaddle1.transform.position.x < -18.5) {
						gamePaddle1.transform.position = new Vector3 (-18.5f, gamePaddle1.transform.position.y, gamePaddle1.transform.position.z);
					}
					//Clamps right
					if (gamePaddle1.transform.position.x > 18.5) {
						gamePaddle1.transform.position = new Vector3 (18.5f, gamePaddle1.transform.position.y, gamePaddle1.transform.position.z);
					}
				}
				else if (v.z > 1.5) {
					gamePaddle2.transform.position = Vector3.MoveTowards (gamePaddle2.transform.position , v, 5);
					//Clamps top
					if (gamePaddle2.transform.position.z < 1.5) {
						gamePaddle2.transform.position = new Vector3 (gamePaddle2.transform.position.x, gamePaddle2.transform.position.y, 1.5f);
					}
					//Clamps bottom
					if (gamePaddle2.transform.position.z > 35.2) {
						gamePaddle2.transform.position = new Vector3 (gamePaddle2.transform.position.x, gamePaddle2.transform.position.y, 35.2f);
					}
					//Clamps left
					if (gamePaddle2.transform.position.x < -18.5) {
						gamePaddle2.transform.position = new Vector3 (-18.5f, gamePaddle2.transform.position.y, gamePaddle2.transform.position.z);
					}
					//Clamps right
					if (gamePaddle2.transform.position.x > 18.5) {
						gamePaddle2.transform.position = new Vector3 (18.5f, gamePaddle2.transform.position.y, gamePaddle2.transform.position.z);
					}
				}
			}
		}
	}
}