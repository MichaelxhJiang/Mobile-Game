using UnityEngine;
using System.Collections;

public class TwoPlayerScoreVariables : MonoBehaviour {
	private int playerScore = 0;
	private int AIScore = 0;
	void OnGUI(){
		GUI.skin.box.fontSize = Screen.height/30;
		GUI.Box(new Rect(0, 0, Screen.width/3, Screen.height/20), "Score");
		GUI.Box(new Rect(0, 0+Screen.height/20, Screen.width/3, Screen.height/20), "Player 1:" + playerScore);
		GUI.Box(new Rect(0, 0+Screen.height/10, Screen.width/3, Screen.height/20), "Player 2:" + AIScore);
	}
	public void increasePlayer1Score(){
		playerScore++;
	}

	public void increasePlayer2Score(){
		AIScore++;
	}
}
