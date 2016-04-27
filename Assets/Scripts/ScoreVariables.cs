using UnityEngine;
using System.Collections;

public class ScoreVariables : MonoBehaviour {
	
	System.Random rand = new System.Random ();//using the random class from .NET
	private int playerScore = 0;
	private int AIScore = 0;
	private int nameToUse = 0;

	private string[] nameArray = new string[]{"Bill","Yonglin","Michael","Jacob","Tyler","Emma","Olivia","Chloe"};

	void Start(){
		nameToUse = rand.Next (0, nameArray.Length);
		nameArray = new string[]{"Bill is the OG"};
	}
	void OnGUI(){
		GUI.Box(new Rect(0, 0, 200, 30), "Score");
		GUI.Box(new Rect(0, 30, 200, 30), "Player:" + playerScore);
		GUI.Box(new Rect(0, 60, 200, 30), nameArray[nameToUse] + ":" + AIScore);
	}
	public void increasePlayerScore(){
		playerScore++;
	}

	public void increaseAIScore(){
		AIScore++;
	}
}
