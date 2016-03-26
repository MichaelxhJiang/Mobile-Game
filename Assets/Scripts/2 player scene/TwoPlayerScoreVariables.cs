using UnityEngine;
using System.Collections;

public class TwoPlayerScoreVariables : MonoBehaviour {
	private int playerScore = 0;
	private int AIScore = 0;
	void OnGUI(){
		GUI.Box(new Rect(0, 0, 200, 30), "Score");
		GUI.Box(new Rect(0, 30, 200, 30), "Player 1:" + playerScore);
		GUI.Box(new Rect(0, 60, 200, 30), "Player 2:" + AIScore);
	}
	public void increasePlayer1Score(){
		playerScore++;
	}

	public void increasePlayer2Score(){
		AIScore++;
	}
}
