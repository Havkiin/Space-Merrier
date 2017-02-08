using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {

	private int score;

	void Start () {

		score = 0;
		GetComponent<Text>().text = "Score: " + score;
	}
	
	void Update () {

		GetComponent<Text>().text = "Score: " + score;
	}

	public void addScore (int points)
	{
		score += points;
	}
}
