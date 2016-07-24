using UnityEngine;
using System.Collections;

public class ScoreVariables : MonoBehaviour {
	
	System.Random rand = new System.Random ();//using the random class from .NET
	private int playerScore = 0;
	private int AIScore = 0;
	private int nameToUse = 0;

	private string[] nameArray = new string[]{"Michael","Bill","Yonglin","Jacob","Tyler","Emma","Olivia","Chloe"};
	//used for GUI setup
	public GameObject leftWall;
	public GameObject rightWall;
	public GameObject topWall;
	public Texture scoreBoard;
	public GUISkin textSkin;

	private float width;
	private Vector3 leftPos;
	private Vector3 rightPos;
	private Vector3 topPos;

	//timer for each round in game. Reset after goal
	private int secondClock;
	private int minuteClock;
	private string scText;

	void Start(){
		nameToUse = rand.Next (0, nameArray.Length);
		leftPos = Camera.main.WorldToScreenPoint (leftWall.transform.position);
		rightPos = Camera.main.WorldToScreenPoint (rightWall.transform.position);
		topPos = Camera.main.WorldToScreenPoint (topWall.transform.position);
		width = rightPos.x - leftPos.x;
		secondClock = 0;
		minuteClock = 0;
		scText = "00";
	}
	void OnGUI(){
		GUI.skin = textSkin;
		//the score board background
		GUI.Box(new Rect(leftPos.x, 0, width, topPos.y / 4), "");
		//set the score variables
		GUI.Label(new Rect(leftPos.x + 25, 10, width, topPos.y / 8 - 10), "Player:" + playerScore);
		GUI.Label(new Rect(leftPos.x + 25, topPos.y / 8 - 5, width, topPos.y / 8 - 10), nameArray[nameToUse] + ":" + AIScore);
		GUI.Label (new Rect (rightPos.x - rightPos.x / 4.0f, 10, width, topPos.y / 8), minuteClock + ":" + scText);
	}
	public void startClock () {
		CancelInvoke ();
		secondClock = 0;
		minuteClock = 0;
		scText = "00";
		InvokeRepeating ("addASecond", 1.0f, 1.0f);
	}

	public void addASecond () {
		secondClock++;
		if (secondClock == 60) {
			minuteClock++;
			secondClock = 0;
		} 
		if (secondClock < 10) {
			scText = "0" + secondClock;
		} else {
			scText = "" + secondClock;
		}
	}
	public void increasePlayerScore(){
		playerScore++;
	}

	public void increaseAIScore(){
		AIScore++;
	}
}
