using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScoreVariables : MonoBehaviour {
	
	System.Random rand = new System.Random ();//using the random class from .NET
	private int playerScore;
	private int AIScore;
	private string nameToUse;

	private string[] nameArray = new string[]{"Michael","Bill","Yonglin","Jacob","Tyler","Emma","Olivia","Chloe", "Bob", "Joe"};
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

	//Result screen
	private GUIStyle resultStyle;
	private GUIStyle mainMenuStyle;
	public Font resultFont;
	public Font mainMenuFont;
	private bool gameEnd;

	void Start(){
		//random name for opponent
		nameToUse = "AI-" + nameArray[rand.Next (0, nameArray.Length)];

		//setup positioning of labels in game
		leftPos = Camera.main.WorldToScreenPoint (leftWall.transform.position);
		rightPos = Camera.main.WorldToScreenPoint (rightWall.transform.position);
		topPos = Camera.main.WorldToScreenPoint (topWall.transform.position);
		width = rightPos.x - leftPos.x;

		//Set up stop watch
		secondClock = 0;
		minuteClock = 0;
		scText = "00";

		//Setup result screen variables
		resultStyle = new GUIStyle ();
		mainMenuStyle = new GUIStyle ();
		resultStyle.font = resultFont;
		resultStyle.fontSize = 120;
		resultStyle.normal.textColor = Color.white;
		resultStyle.alignment = TextAnchor.MiddleCenter;
		mainMenuStyle.font = mainMenuFont;
		mainMenuStyle.fontSize = 48;
		mainMenuStyle.normal.textColor = Color.white;
		mainMenuStyle.alignment = TextAnchor.MiddleCenter;
		gameEnd = false;

		playerScore = 0;
		AIScore = 0;
	}

	void OnGUI(){
		GUI.skin = textSkin;
		//the score board background
		GUI.Box(new Rect(leftPos.x, 0, width, topPos.y / 4), "");
		//set the score variables
		GUI.Label(new Rect(leftPos.x + 25, 10, width, topPos.y / 8 - 10), "Player:" + playerScore);
		GUI.Label(new Rect(leftPos.x + 25, topPos.y / 8 - 5, width, topPos.y / 8 - 10), nameToUse + ":" + AIScore);
		GUI.Label (new Rect (rightPos.x - rightPos.x / 4.0f, 10, width, topPos.y / 8), minuteClock + ":" + scText);

		if (gameEnd) {
			//GUI.contentColor = Color.white;
			GameObject light = GameObject.FindGameObjectWithTag ("Main Light");
			light.GetComponent<Light> ().enabled = false;
			Time.timeScale = 0;
			if (AIScore == 7) {
				//Display messages and gui for user
				GUI.Box (new Rect (Screen.width / 2 - Screen.width / 4, Screen.height / 4 - Screen.height / 8, Screen.width / 2, Screen.height / 4), "YOU LOSE", resultStyle);
			} else {
				GUI.Box (new Rect (Screen.width / 2 - Screen.width / 4, Screen.height / 4 - Screen.height / 8, Screen.width / 2, Screen.height / 4), "YOU WIN", resultStyle);
			}
			if (GUI.Button (new Rect (Screen.width / 8.0f, Screen.height * 3 / 4 + Screen.height / 8, Screen.width * 3 / 4, Screen.height / 8), "Main menu", mainMenuStyle)) {
				SceneManager.LoadScene (0); 
			}
		}
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
		if (playerScore == 7) {
			gameEnd = true;
		}
	}

	public void increaseAIScore(){
		AIScore++;
		if (AIScore == 7) {
			gameEnd = true;
		}
	}
}
