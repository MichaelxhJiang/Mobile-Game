using UnityEngine;
using System.Collections;

public class ScoreVariables : MonoBehaviour {
	private int playerScore = 0;
	private int AIScore = 0;
	void OnGUI(){
		GUI.Box(new Rect(0, 0, 200, 30), "Score");
		GUI.Box(new Rect(0, 30, 200, 30), "Player:" + playerScore);
		GUI.Box(new Rect(0, 60, 200, 30), "AI:" + AIScore);
	}
	public void increasePlayerScore(){
		playerScore++;
	}

	public void increaseAIScore(){
		AIScore++;
	}
}
