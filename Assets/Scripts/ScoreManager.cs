using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public Text scoreText;
	public Text totalRunText;
	public Text hiScoreText;

	public float scoreCount;
	public float hiScoreCount;

	public float pointsPerSecond;
	public bool scoreIncreasing;

	public bool newHiScore;
	public Text newHiScoreText;

	public float totalRun;

	// Use this for initialization
	void Start () {
		newHiScore = false;
		//PlayerPrefs.SetFloat ("HighScore", 0);
		if (PlayerPrefs.HasKey("HighScore")) {
			hiScoreCount = PlayerPrefs.GetFloat ("HighScore");
		}
		if (PlayerPrefs.HasKey ("TotalRun")) {
			totalRun = PlayerPrefs.GetFloat ("TotalRun");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (scoreIncreasing) {
			scoreCount += pointsPerSecond * Time.deltaTime;
			totalRun += pointsPerSecond * Time.deltaTime;
			newHiScore = false;
		}
		if(scoreCount > hiScoreCount)
		{
			newHiScore = true;
			hiScoreCount = scoreCount;
			PlayerPrefs.SetFloat ("HighScore", hiScoreCount);
		}
		if (newHiScore) {
			newHiScoreText.text = "New Record: " + Mathf.Round (hiScoreCount);
		} else {
			PlayerPrefs.SetFloat ("TotalRun", totalRun);
			newHiScoreText.text = "Score: " + Mathf.Round (scoreCount) + " | " + Mathf.Round (hiScoreCount);
		}
		scoreText.text = "Score: " + Mathf.Round(scoreCount);
		totalRunText.text = "Total Run: " + Mathf.Round(totalRun);
		hiScoreText.text = "Best Score: " + Mathf.Round (hiScoreCount);
	}
}
