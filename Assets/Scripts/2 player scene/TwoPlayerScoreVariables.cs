using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TwoPlayerScoreVariables : MonoBehaviour {
	private int playerScore = 0;
	private int player2Score = 0;

	private GUIStyle resultStyle;
	private GUIStyle scoreStyle;
	private GUIStyle mainMenuStyle;
	public Font resultFont;
	public Font mainMenuFont;
	public GUISkin textSkin;

	private bool gameEnd;

	void Start() {
		//Setup result screen variables and styling
		resultStyle = new GUIStyle ();
		scoreStyle = new GUIStyle ();
		mainMenuStyle = new GUIStyle ();
		resultStyle.font = resultFont;

		resultStyle.fontSize = (int)(40.0f * (float)(Screen.height) / 800);	//scale font size
		resultStyle.normal.textColor = Color.white;
		resultStyle.alignment = TextAnchor.MiddleCenter;

		scoreStyle.font = mainMenuFont;
		scoreStyle.fontSize = (int)(30.0f * (float)(Screen.height) / 800);	//scale font size
		scoreStyle.normal.textColor = Color.yellow;
		scoreStyle.alignment = TextAnchor.MiddleCenter;

		mainMenuStyle.font = mainMenuFont;
		mainMenuStyle.fontSize = (int)(30.0f * (float)(Screen.height) / 800);	//scale font size
		mainMenuStyle.normal.textColor = Color.white;
		mainMenuStyle.alignment = TextAnchor.MiddleCenter;

		gameEnd = false;
	}

	void OnGUI(){
		GUI.skin = textSkin;

		if (!gameEnd) {
			GUI.Label (new Rect (Screen.width * 3 / 8, Screen.height * 9 / 10, Screen.width / 4, Screen.height / 10), "SCORE:" + playerScore, scoreStyle);

			GUIUtility.RotateAroundPivot(180, new Vector2(Screen.width / 2, Screen.height / 2));

			GUI.Label (new Rect (Screen.width * 3 / 8, Screen.height * 9 / 10, Screen.width / 4, Screen.height / 10), "SCORE:" + player2Score, scoreStyle);

			GUIUtility.RotateAroundPivot(180, new Vector2(Screen.width / 2, Screen.height / 2));
		} else {
			//Semi transparent background
			GUI.Box (new Rect (0, 0, Screen.width, Screen.height), "");
			GameObject light = GameObject.FindGameObjectWithTag ("Main Light");
			light.GetComponent<Light> ().enabled = false;
			Time.timeScale = 0;
			if (player2Score == 7) {
				//Display messages and gui for user
				GUI.Label (new Rect (Screen.width / 2 - Screen.width / 4, Screen.height * 3 / 4 - Screen.height / 8, Screen.width / 2, Screen.height / 4), "YOU LOSE", resultStyle);
				GUI.Label (new Rect (Screen.width / 2 - Screen.width / 4, Screen.height * 3/ 4, Screen.width / 2, Screen.height / 4), "SCORE: " + playerScore, scoreStyle);

				GUIUtility.RotateAroundPivot(180, new Vector2(Screen.width / 2, Screen.height / 2));

				GUI.Label (new Rect (Screen.width / 2 - Screen.width / 4, Screen.height * 3 / 4 - Screen.height / 8, Screen.width / 2, Screen.height / 4), "YOU WIN", resultStyle);
				GUI.Label (new Rect (Screen.width / 2 - Screen.width / 4, Screen.height * 3/ 4, Screen.width / 2, Screen.height / 4), "SCORE: " + player2Score, scoreStyle);

				GUIUtility.RotateAroundPivot(180, new Vector2(Screen.width / 2, Screen.height / 2));
			} else {
				//Display messages and gui for user
				GUI.Label (new Rect (Screen.width / 2 - Screen.width / 4, Screen.height * 3 / 4 - Screen.height / 8, Screen.width / 2, Screen.height / 4), "YOU WIN", resultStyle);
				GUI.Label (new Rect (Screen.width / 2 - Screen.width / 4, Screen.height * 3/ 4, Screen.width / 2, Screen.height / 4), "SCORE: " + playerScore, scoreStyle);

				GUIUtility.RotateAroundPivot(180, new Vector2(Screen.width / 2, Screen.height / 2));

				GUI.Label (new Rect (Screen.width / 2 - Screen.width / 4, Screen.height * 3 / 4 - Screen.height / 8, Screen.width / 2, Screen.height / 4), "YOU LOSE", resultStyle);
				GUI.Label (new Rect (Screen.width / 2 - Screen.width / 4, Screen.height * 3/ 4, Screen.width / 2, Screen.height / 4), "SCORE: " + player2Score, scoreStyle);

				GUIUtility.RotateAroundPivot(180, new Vector2(Screen.width / 2, Screen.height / 2));
			}
			if (GUI.Button (new Rect (Screen.width * 3 / 8, Screen.height / 2 - Screen.height / 8 + Screen.height / 8, Screen.width  / 4, Screen.height / 4), "Main menu", mainMenuStyle)) {
				SceneManager.LoadScene (0); 
			}
		}
	}
	public void increasePlayer1Score(){
		playerScore++;
		if (playerScore == 7) {
			gameEnd = true;
		}
	}
	public void increasePlayer2Score(){
		player2Score++;
		if (player2Score == 7) {
			gameEnd = true;
		}
	}
}
