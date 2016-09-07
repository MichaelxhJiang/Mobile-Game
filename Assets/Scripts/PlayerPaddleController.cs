using UnityEngine;
using System.Collections;

public class PlayerPaddleController : MonoBehaviour {
	//flag to check if the user has tapped / clicked. 
	//Set to true on click. Reset to false on reaching destination
	public bool flag = false;
	//destination point
	private Vector3 endPoint;
	//alter this to change the speed of the movement of player / gameobject
	private float duration = 0.2f;
	//vertical position of the gameobject
	private float yAxis;
	//Bounce force for puck
	public float bounceForce = 65.0f;
	//test
	public float speed = 0;

	public AudioClip hitSound;

	private AudioSource source;

	private float volHigh = 1f;

	private float volLow = .5f;

	Vector3 lastPosition = Vector3.zero;

	void Awake(){
		source = GetComponent<AudioSource> ();
	}

	void Start(){
		//save the y axis value of gameobject
		yAxis = gameObject.transform.position.y;
	}

	// Update is called once per frame
	void FixedUpdate () {
		speed = (transform.position - lastPosition).magnitude;
		lastPosition = transform.position;
		//check if the screen is touched / clicked   
		if ((Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) || (Input.GetMouseButton (0))) {

			//
			// || (Input.GetMouseButton (0))
			//

			//declare a variable of RaycastHit struct
			RaycastHit hit;
			//Create a Ray on the tapped / clicked position
			Ray ray;
			//for unity editor
			#if UNITY_EDITOR
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			//for touch device
			#elif (UNITY_ANDROID || UNITY_IPHONE || UNITY_WP8)
			ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
			#endif

			//Check if the ray hits any collider
			if (Physics.Raycast (ray, out hit)) {
				//set a flag to indicate to move the gameobject
				if (hit.point.x > -23.0f && hit.point.x < 23.0f && hit.point.z > -39) {
					flag = true;
					//save the click / tap position
					endPoint = hit.point;
					//as we do not want to change the y axis value based on touch position, reset it to original y axis value
					endPoint.y = yAxis;
				}
			}
		} 
		//check if the flag for movement is true and the current gameobject position is not same as the clicked / tapped position
		if(flag && !Mathf.Approximately(gameObject.transform.position.magnitude, endPoint.magnitude)){ //&& !(V3Equal(transform.position, endPoint))){
			//move the gameobject to the desired position
			gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, endPoint, 1/(duration*(Vector3.Distance(gameObject.transform.position, endPoint))));
		}
		//set the movement indicator flag to false if the endPoint and current gameobject position are equal
		else if(flag && Mathf.Approximately(gameObject.transform.position.magnitude, endPoint.magnitude)) {
			flag = false;
		}
		//following code constrains the paddle from moving off the screen

		//Clamps half court
		if (transform.position.z + (GetComponent<CapsuleCollider> ().radius * transform.localScale.z) > 0) {
			float z = 0 - (GetComponent<CapsuleCollider> ().radius * transform.localScale.z);
			transform.position = new Vector3 (transform.position.x, transform.position.y, z);
		}
		//Clamps bottom
		if (transform.position.z + (GetComponent<CapsuleCollider> ().radius * transform.localScale.z) < -31) {
			float z = -31 - (GetComponent<CapsuleCollider> ().radius * transform.localScale.z);
			transform.position = new Vector3 (transform.position.x, transform.position.y, z);
		}
		//Clamps left
		if (transform.position.x + (GetComponent<CapsuleCollider> ().radius * transform.localScale.x) < -16) {
			float x = -16 - (GetComponent<CapsuleCollider> ().radius * transform.localScale.x);
			transform.position = new Vector3 (x, transform.position.y, transform.position.z);
		}
		//Clamps right
		if (transform.position.x + (GetComponent<CapsuleCollider> ().radius * transform.localScale.x) > 22) {
			float x = 22 - (GetComponent<CapsuleCollider> ().radius * transform.localScale.x);
			transform.position = new Vector3 (x, transform.position.y, transform.position.z);
		}
	}

	void OnCollisionEnter(Collision hit) {
		if (hit.gameObject.tag == "Puck") {
			if (speed > 0.7) {
				hit.rigidbody.AddForceAtPosition (-1 * hit.contacts [0].normal * (speed * bounceForce), hit.contacts [0].normal, ForceMode.Impulse);
				float vol = Random.Range (volLow, volHigh);
				source.PlayOneShot(hitSound,vol);
			} else {
				hit.rigidbody.AddForceAtPosition (-1 * hit.contacts [0].normal * speed, hit.contacts [0].normal, ForceMode.Impulse);
			}
		}
	}



	void OnCollisionStay(Collision hit) {
		OnCollisionEnter (hit);
	}
		
}
