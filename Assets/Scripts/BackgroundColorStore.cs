using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BackgroundColorStore : MonoBehaviour {

	public bool[] levelBool;
	public float[] levelGoal;
	public Color[] levelColor;

	private ScoreManager theScoreManager;

	public Camera mainCamera;
	public GameObject cameraObject;

	public GameObject[] buttonsObjects;

	// Use this for initialization
	void Start () {
		theScoreManager = FindObjectOfType<ScoreManager> ();
		mainCamera = cameraObject.GetComponent<Camera> ();
		for (int i = 0; i < buttonsObjects.Length; i++) {
			if (theScoreManager.totalRun > levelGoal [i]) {
				levelBool [i] = true;
			}
		}
		for (int i = 0; i < buttonsObjects.Length; i++) {
			if (levelBool [i]) {
				levelColor [i].a = 1f;
				buttonsObjects [i].GetComponent<Image> ().color = levelColor [i];
			} else {
				buttonsObjects [i].GetComponent<Image> ().color = new Color (255, 255, 255, 0.90f);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void BackgroundColorStoreActivated(){
		for (int i = 0; i < buttonsObjects.Length; i++) {
			if (theScoreManager.totalRun > levelGoal [i]) {
				levelBool [i] = true;
			}
		}
		for (int i = 0; i < buttonsObjects.Length; i++) {
			if (levelBool [i]) {
				levelColor [i].a = 1f;
				buttonsObjects [i].GetComponent<Image> ().color = levelColor [i];
			} else {
				buttonsObjects [i].GetComponent<Image> ().color = new Color (255, 255, 255, 0.90f);
			}
		}
	}

	public void ZeroButton(){
		if (theScoreManager.totalRun > levelGoal [0]) {
			mainCamera.backgroundColor = levelColor [0];
			PlayerPrefs.SetInt ("BackgroundColor", 0);
		}
	}

	public void FirstButton(){
		if (theScoreManager.totalRun > levelGoal [1]) {
			mainCamera.backgroundColor = levelColor [1];
			PlayerPrefs.SetInt ("BackgroundColor", 1);
		}
	}

	public void SecondButton(){
		if (theScoreManager.totalRun > levelGoal [2]) {
			mainCamera.backgroundColor = levelColor [2];
			PlayerPrefs.SetInt ("BackgroundColor", 2);
		}
	}

	public void ThirdButton(){
		if (theScoreManager.totalRun > levelGoal [3]) {
			mainCamera.backgroundColor = levelColor [3];
			PlayerPrefs.SetInt ("BackgroundColor", 3);
		}
	}

	public void FourthButton(){
		if (theScoreManager.totalRun > levelGoal [4]) {
			mainCamera.backgroundColor = levelColor [4];
			PlayerPrefs.SetInt ("BackgroundColor", 4);
		}
	}

	public void FifthButton(){
		if (theScoreManager.totalRun > levelGoal [5]) {
			mainCamera.backgroundColor = levelColor [5];
			PlayerPrefs.SetInt ("BackgroundColor", 5);
		}
	}
}
