using UnityEngine;
using System.Collections;

public class BGScroller : MonoBehaviour
{
	public float scrollSpeed;
	public float tileSizeZ;
	public GameObject otherBG;

	private Vector3 startPosition;

	void Start ()
	{
		startPosition = gameObject.GetComponent<Transform>().position;
	}

	void Update ()
	{
		transform.Translate (Vector3.forward * scrollSpeed * Time.deltaTime);
		if (transform.position.z > 398) {
			transform.position = new Vector3 (0, transform.position.y, -293);
		}
	}
}