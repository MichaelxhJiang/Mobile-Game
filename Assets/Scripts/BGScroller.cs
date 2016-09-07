using UnityEngine;
using System.Collections;

public class BGScroller : MonoBehaviour
{
	public float scrollSpeed;
	public float tileSizeZ;
	public GameObject otherBG;

	void Update ()
	{
		transform.Translate (Vector3.forward * scrollSpeed * Time.deltaTime);
		if (transform.position.z > 510) {
			transform.position = new Vector3 (0, transform.position.y - 1, -405);
			//Overlap the other BG
			otherBG.transform.position = new Vector3 (0, otherBG.transform.position.y + 1, otherBG.transform.position.z);
		}
	}
}