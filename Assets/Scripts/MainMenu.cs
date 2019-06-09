using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;

public class MainMenu : MonoBehaviour {
	public PlayerController thePlayerInMenu;
	public PauseMenu thePauseMenu;
	public Button theMuteButton;
	public Sprite muteOn;
	public Sprite muteOff;
	public TutorialMenu theTutorialMenu;
	public BackgroundColorStore theBackgroundColorStore;
	private int backgroundColorPrefs;


	void Start(){
		thePlayerInMenu = FindObjectOfType<PlayerController> ();
		thePlayerInMenu.GetComponent<PlayerController> ().enabled = false;
		thePauseMenu.GetComponent<PauseMenu> ().enabled = false;
		backgroundColorPrefs = PlayerPrefs.GetInt ("BackgroundColor");
		theBackgroundColorStore.mainCamera.backgroundColor = theBackgroundColorStore.levelColor [backgroundColorPrefs];
	}

	void Update(){
		
	}

	public void PlayGame(){
		thePlayerInMenu.GetComponent<PlayerController> ().enabled = true;
		FindObjectOfType<GameManager> ().StartPlaying ();
		thePauseMenu.GetComponent<PauseMenu> ().enabled = true;
		if (theTutorialMenu.isActiveAndEnabled) {
			theTutorialMenu.gameObject.SetActive (false);
		}
		if (theBackgroundColorStore.isActiveAndEnabled) {
			theBackgroundColorStore.gameObject.SetActive (false);
		}
		gameObject.SetActive (false);
	}

	public void MuteAudio(){
		if (AudioListener.pause) 
		{
			AudioListener.pause = false;
			theMuteButton.GetComponent<Image> ().sprite = muteOff;
		} 
		else 
		{
			AudioListener.pause = true;
			theMuteButton.GetComponent<Image> ().sprite = muteOn;
		}
	}


	public void Leaderboard(){
		//Social.localUser.Authenticate((bool success) => {
		//	// handle success or failure
		//	if(success)
		//	{
		//		PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkI-_nslMgKEAIQAQ");

		//	}
		//	else
		//		print ("Google Login Failed!");
		//});
	}


	public void Tutorial(){
		if (theTutorialMenu.isActiveAndEnabled) {
			theTutorialMenu.gameObject.SetActive (false);
		} else {
			theTutorialMenu.gameObject.SetActive (true);
		}
		if (theBackgroundColorStore.isActiveAndEnabled) {
			theBackgroundColorStore.gameObject.SetActive (false);
		}
	}

	public void BackgroundColorStore(){
		if (theBackgroundColorStore.isActiveAndEnabled) {
			theBackgroundColorStore.gameObject.SetActive (false);
		} else {
			theBackgroundColorStore.gameObject.SetActive (true);
		}
		if (theTutorialMenu.isActiveAndEnabled) {
			theTutorialMenu.gameObject.SetActive (false);
		}
		theBackgroundColorStore.BackgroundColorStoreActivated ();
	}

}
