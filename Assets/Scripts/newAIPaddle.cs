using UnityEngine;
using System.Collections;

public class newAIPaddle : MonoBehaviour {
	public AudioClip hitSound;
	//Speed of AI paddle when returning to position
	public float speed;
	//destination point
	private Vector3 endPoint;
	//Bounce force for puck
	private float bounceForce;
	//The puck game object
	//public GameObject targ; DO YOU EVEN NEED THIS SH*T??????
	public Transform targTransform;
	private float targRadius = 2f;
	//This game object
	public Transform thisTransform;
	private float thisRadius;
	private PowerUp powerUp;

	//used to track which puck AI is following
	private bool followingClone;

	private AudioSource source;
	private float volHigh = 1f;
	private float volLow = .5f;

	bool stop = false;

	void Awake(){
		source = GetComponent<AudioSource> ();
	}
	// Use this for initialization
	void Start () {
		speed = GameStates.AIspeed;
		bounceForce = GameStates.AIbounceforce;
		thisRadius = thisTransform.localScale.z * GetComponent<CapsuleCollider> ().radius;
		powerUp = GameObject.FindGameObjectWithTag ("Player").GetComponent<PowerUp> ();
		followingClone = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float step = speed * Time.deltaTime;
		string pos = getPuckAndPaddlePosition ();
		float slope = getSlope (targTransform);
		float point = targTransform.position.z + 6;
		if (pos == "OtherSide")
			thisTransform.position = Vector3.MoveTowards (thisTransform.position, new Vector3 (Mathf.Clamp (targTransform.position.x, -6.0f, 6.0f), thisTransform.position.y, 30), step);
		else if (pos == "PuckInFront") {
			float dis = Mathf.Sqrt (Mathf.Abs((thisTransform.position.x - targTransform.position.x) + (thisTransform.position.z - targTransform.position.z)));
			if (dis < 5)
				step += (5 - dis) / 10;
			//float x = calculatePoints (slope, -45, targTransform.position.x, targTransform.position.z); FUCK THIS LINE IS SUCKS SHIT
			float x = (point+45)/slope;
			if (slope == 0)
				thisTransform.position = Vector3.MoveTowards (thisTransform.position, targTransform.position, step);
			else {
				thisTransform.position = Vector3.MoveTowards (thisTransform.position, new Vector3 (x, targTransform.position.y, targTransform.position.z), step);
				if (thisTransform.position.x < x) {
					thisTransform.position += Vector3.right * 0.12f;
				} else {
					thisTransform.position += Vector3.left * 0.12f;
				}
			}
		} else if (pos == "PuckBehind") {
			thisTransform.position = Vector3.MoveTowards (thisTransform.position, new Vector3 (targTransform.position.x / 2, targTransform.position.y, targTransform.position.z + (targRadius * 8)), step);
		}
		//if more than one puck
		if (powerUp.cloned) {
			findClosestPuck ();
		} else {
			//targTransform = GameObject.FindGameObjectWithTag ("Puck").transform;
			//Debug.Log ("reset puck target");
		}

		boundary ();
	}

	public void resetPuckTarget () {
		//reset targTransform to original puck 
		targTransform = GameObject.FindGameObjectWithTag ("Puck").transform;
	}

	//only run this if multiple pucks have been instantiated
	public void findClosestPuck () {
		GameObject[] pucks = GameObject.FindGameObjectsWithTag ("Puck");
		GameObject closestPuck = pucks [0];
		foreach (GameObject puck in pucks) {
			if (puck.transform.position.z > closestPuck.transform.position.z) {
				closestPuck = puck;
			}
		}

		//set target puck to be closest puck to AI net
		targTransform = closestPuck.transform;
		followingClone = true;
	}

	void boundary(){
		if (thisTransform.position.x - thisRadius < -24)
			thisTransform.position = new Vector3 (-24 + thisRadius, thisTransform.position.y, thisTransform.position.z);
		if (thisTransform.position.x + thisRadius > 23)
			thisTransform.position = new Vector3 (22.5f - thisRadius, thisTransform.position.y, thisTransform.position.z);
		if (thisTransform.position.z - thisRadius < 0)
			thisTransform.position = new Vector3 (thisTransform.position.x, thisTransform.position.y, 1 + thisRadius);
		if (thisTransform.position.z + thisRadius > 39)
			thisTransform.position = new Vector3 (thisTransform.position.x, thisTransform.position.y, 38 - thisRadius);
	}
	float getSlope(Transform a){
		if ((int)a.position.x == 0)
			return 0;
		float z = a.position.z;
		float x = a.position.x;
		return (z + 45) / x;
	}

	float calculatePoints(float m, float b, float h, float k){
		float bk = b - k;
		float x = 1 + Mathf.Pow (m, 2);
		float y = ((0-2)*h) + (2*m*bk);
		float z = Mathf.Pow (h, 2) + Mathf.Pow (b-k, 2) - (Mathf.Pow (targRadius, 2) * 8); //Remove *2 to make it acurrate
		return ((-y) + Mathf.Sqrt (Mathf.Pow (y, 2) - 4 * x * z)) / (2 * x);
	}
		
	string getPuckAndPaddlePosition(){
		
		//puck is on the other side
		if (stop) {
			StartCoroutine (Go (0.1f));
			return null;
		} 
		if (!powerUp.cloned && followingClone) {
			followingClone = false;
			targTransform = GameObject.FindGameObjectWithTag ("Puck").transform;
		}
		if (targTransform.position.z + targRadius < 0)
				return "OtherSide";
		//Puck is infront of paddle
		else if (targTransform.position.z < thisTransform.position.z)
				return "PuckInFront";
		//Puck is behind paddle
		else
			return "PuckBehind";
	}

	void OnCollisionEnter(Collision hit) {
		if (hit.collider.tag == "Puck") {
			float vol = Random.Range (volLow, volHigh);
			source.PlayOneShot(hitSound,vol);
			hit.rigidbody.AddForceAtPosition(-1 * hit.contacts [0].normal * bounceForce, hit.contacts[0].normal, ForceMode.Impulse);
			stop = true;
			//Debug.Log ("transform" + thisTransform.position.x + " " + thisTransform.position.z); SO ANNOYING >:(((((((
		}
	}

	IEnumerator Go (float waitTime) {
		yield return new WaitForSeconds (waitTime);
		stop = false;
	}
}
