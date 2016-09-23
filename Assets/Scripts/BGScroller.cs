using UnityEngine;
using System.Collections;

public class BGScroller : MonoBehaviour
{
	public float scrollSpeed;
	public float tileSizeZ;
	public GameObject otherBG;

	void Update ()
	{
		transform.Translate (Vector3.back * scrollSpeed * Time.deltaTime);
		if (transform.position.z < -400) {
			transform.position = new Vector3 (0, transform.position.y + 1, 530);
			//Overlap the other BG
			otherBG.transform.position = new Vector3 (0, otherBG.transform.position.y - 1, otherBG.transform.position.z);
		}
	}
}