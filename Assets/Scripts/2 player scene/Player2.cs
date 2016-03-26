using UnityEngine;
using System.Collections;

public class Player2 : MonoBehaviour {

	//flag to check if the user has tapped / clicked. 
	//Set to true on click. Reset to false on reaching destination
	private bool flag = false;
	//destination point
	private Vector3 endPoint;
	//alter this to change the speed of the movement of player / gameobject
	public float duration = 0.6f;
	//vertical position of the gameobject
	private float yAxis;
	//Bounce force for puck
	public float bounceForce = 40.0f;
	//test
	private float speed = 0;
	Vector3 lastPosition = Vector3.zero;

	void Start(){
		//save the y axis value of gameobject
		yAxis = gameObject.transform.position.y;
	}

	// Update is called once per frame
	void Update () {
		speed = (transform.position - lastPosition).magnitude;
		lastPosition = transform.position;
		//check if the screen is touched / clicked   
		if ((Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) || (Input.GetMouseButton (0))) {
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
				//Check if the contact is within the player 2 paddle's boundaries
				if (hit.point.z > 0.5f) {
					//set a flag to indicate to move the gameobject
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
		transform.position = new Vector3 (Mathf.Clamp(transform.position.x,-18.8f,18.8f), transform.position.y, transform.position.z);
		//this code constraints the paddle from moving off the screen
		if (transform.position.z + (GetComponent<CapsuleCollider> ().radius * transform.localScale.z) < 0.5f) {
			float z = 0 + (GetComponent<CapsuleCollider> ().radius * transform.localScale.z);
			transform.position = new Vector3 (transform.position.x, transform.position.y, z);
		}
	}

	void OnCollisionEnter(Collision hit) {
		if (hit.gameObject.tag == "Puck" && speed > 0.7) {
			hit.rigidbody.AddForceAtPosition (-1 * hit.contacts [0].normal * (speed * bounceForce), hit.contacts [0].normal, ForceMode.Impulse);
		} else {
			hit.rigidbody.AddForceAtPosition (-1 * hit.contacts [0].normal * speed, hit.contacts [0].normal, ForceMode.Impulse);
		}
	}

	void OnCollisionStay(Collision hit) {
		OnCollisionEnter (hit);
	}
}
