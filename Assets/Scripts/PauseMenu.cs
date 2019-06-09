using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	public AudioSource pauseSound;
	public PlayerController thePlayerInMenu;

	public void PauseGame()
	{
		Time.timeScale = 0f;
	}

	public void ResumeGame()
	{
		Time.timeScale = 1f;
		gameObject.SetActive (false);
	}

	public void BackToMainMenuFromPause(){
		Time.timeScale = 1f;
		thePlayerInMenu.gameObject.SetActive (false);
		FindObjectOfType<GameManager> ().MainPosition ();
	}
}