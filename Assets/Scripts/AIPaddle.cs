using UnityEngine;
using System.Collections;

public class AIPaddle : MonoBehaviour {
	public AudioClip hitSound;
	//Speed of AI paddle when returning to position
	public float speed;
	//destination point
	private Vector3 endPoint;
	//Bounce force for puck
	private float bounceForce;
	//The puck game object
	private GameObject targ;
	//The player net game object
	public GameObject playerNet;
	//check if AI just recently touched the puck
	private bool hitPuck;

	private AudioSource source;
	private float volHigh = 1f;
	private float volLow = .5f;

	void Awake(){
		source = GetComponent<AudioSource> ();
	}

	void Start () {
		hitPuck = false;
		speed = GameStates.AIspeed;
		//speed = 70;
		bounceForce = GameStates.AIbounceforce;
		//bounceForce = 70;
		targ = GameObject.Find ("Puck");
	}

	// Update is called once per frame
	void Update () {
		float translation = speed * Time.deltaTime;

		if (targ.transform.position.x < 10 && targ.transform.position.x > -10) {
			//if puck is going towards center section of rink
			StartCoroutine (StraightShot ());
		} 

		if (targ.transform.position.z < 0) {
			//if the puck is on the other side
			transform.position = Vector3.MoveTowards (transform.position, new Vector3 (Mathf.Clamp(targ.transform.position.x, -5.0F, 5.0F), targ.transform.position.y, 30), translation);
		} 

		if (hitPuck) {
			//if we hit the puck away, wait
			StartCoroutine (WaitToChasePuck (0.5f));
		} else if (transform.position.z + (GetComponent<CapsuleCollider> ().radius * transform.localScale.x) > targ.transform.position.z + (targ.transform.localScale.z*1.5f)) {
			float dis = Mathf.Sqrt (Mathf.Abs((transform.position.x - targ.transform.position.x) + (transform.position.z - targ.transform.position.z)));
			Debug.Log ("Moving towards puck");
			if (dis < 5) 
				transform.position = Vector3.MoveTowards (transform.position, targ.transform.position, translation + ((5-dis)/10));
			else
				transform.position = Vector3.MoveTowards (transform.position, targ.transform.position, translation);
		} else {
			transform.position = Vector3.MoveTowards (transform.position, new Vector3 (targ.transform.position.x * 0.5f, targ.transform.position.y, targ.transform.position.z + (targ.transform.localScale.z*1.5f)), translation);
		}


	}

	void OnCollisionEnter(Collision hit) {
		if (hit.gameObject.tag == "Puck") {
			float vol = Random.Range (volLow, volHigh);
			source.PlayOneShot(hitSound,vol);
			hit.rigidbody.AddForceAtPosition(-1 * hit.contacts [0].normal * bounceForce, hit.contacts[0].normal, ForceMode.Impulse);
		}
	}
		

	void OnCollisionStay(Collision hit){
		hitPuck = true;
		//Debug.Log ("Glitch");
	}

	IEnumerator StopChasing(float waitTime){
		yield return new WaitForSeconds (waitTime);
		hitPuck = true;
	}

	IEnumerator WaitToChasePuck (float waitTime) {
		yield return new WaitForSeconds (waitTime);
		hitPuck = false;
	}


	IEnumerator StraightShot () {
		//if puck is to the right of player net
		if (targ.transform.position.x > playerNet.transform.position.x) {
			if (transform.position.x < targ.transform.position.x) {
				//if ai is to the left of puck
				//move ai to the right
				transform.Translate (Vector3.right * speed * Time.deltaTime);
			}
		} else {
		//else the puck is to the left of player net
			if (transform.position.x > targ.transform.position.x) {
				//if ai is to the right of puck
				//move ai to the left
				transform.Translate (Vector3.left * speed * Time.deltaTime);
			}
		}
		yield return null;
	}

}
	
/** TEMP TRASH
 * 
 * //store the difference between angle
float angle = Vector3.Angle (transform.position, targ.transform.position) - Vector3.Angle (targ.transform.position, playerNet.transform.position);

//if puck is to the left of both ai and player net
if (targ.transform.position.x < playerNet.transform.position.x - 2 && targ.transform.position.x < transform.position.x - 2) {
	transform.Translate (Vector3.left * translation);
	Debug.Log ("LEFT");
} else if (targ.transform.position.x > playerNet.transform.position.x + 2 && targ.transform.position.x > transform.position.x + 2) {
	//else if puck is to the right of both ai and player net
	transform.Translate (Vector3.right * translation);
	Debug.Log ("RIGHT");
} else {

	//if ai is to the left of puck
	if (targ.transform.position.x > transform.position.x) {
		//check if it is aiming towards net
		//if (Mathf.Abs (angle) <= 4) {
		//	//move towards puck and hit it
		//	transform.position = Vector3.MoveTowards (transform.position, targ.transform.position, translation);
		//	Debug.Log ("hitting puck");
		//} else 
		if (angle < 0.0f) {
			//puck needs to move to the left more
			transform.position = Vector3.MoveTowards (transform.position, new Vector3 (transform.position.x - 1, transform.position.y, transform.position.z), translation);
			Debug.Log ("moving left");
		} else {
			//puck needs to move to the right more
			transform.Translate (Vector3.right * 3 * translation);
			Debug.Log ("moving right");
		}
	} else {
		//if (Mathf.Abs (angle) <= 4) {
		//	//move towards puck and hit it
		//	transform.position = Vector3.MoveTowards (transform.position, targ.transform.position, translation);
		//} 
		//else 
		if (angle < 0) {
			//puck needs to move to the right more
			transform.Translate (Vector3.right * 3 * translation);
			Debug.Log ("moving right");
		} else {
			//puck needs to move to the left more
			transform.position = Vector3.MoveTowards (transform.position, new Vector3 (transform.position.x - 1, transform.position.y, transform.position.z), translation);
			Debug.Log ("moving left");
		}
	}
}
**/