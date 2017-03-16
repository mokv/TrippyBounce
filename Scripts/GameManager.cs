using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GoogleMobileAds.Api;

public class GameManager : MonoBehaviour {

	public Transform platformGenerator;
	private Vector3 platformStartPoint;
	public PlayerController thePlayer;
	private Vector3 playerStartPoint;
	public PhysicsMaterial2D bouncinessMaterial;
	public float bouncinessInGame;

	private PlatformDestroyer[] platformList;
	private ScoreManager theScoreManager;
	public DeathMenu theDeathScreen;
	private float moveSpeedStoreManager;
	private MainMenu theMainMenuInGame;
	public PauseMenu thePauseMenu;
	private float pointsPerSecondScoreStore;
	private long forReportScore;
	public string interstialAdID;
	private byte byteForAd;
	public byte triesBeforeAd;
	InterstitialAd interstitial;
	AdRequest request;
	public float pointsBeforeAddingAdsCount;
	private long hiScoreCountPrefs;
	private float hiScoreCountPrefsFloat;

	// Use this for initialization
	void Start () {
		PlayGamesPlatform.Activate();
		if (PlayerPrefs.HasKey("HighScore")) {
			hiScoreCountPrefsFloat = PlayerPrefs.GetFloat ("HighScore");
		}
		hiScoreCountPrefs = (long)hiScoreCountPrefsFloat;
		Social.localUser.Authenticate((bool success) => {
			if(success){
				Social.ReportScore(hiScoreCountPrefs, "CgkI-_nslMgKEAIQAQ", (bool successful) => {

				});
			}
		});
		Application.targetFrameRate = 60;
		//ads are from here

		#if UNITY_ANDROID
		string adUnitId = interstialAdID;
		#elif UNITY_IPHONE
		string adUnitId = "INSERT_IOS_INTERSTITIAL_AD_UNIT_ID_HERE";
		#else
		string adUnitId = "unexpected_platform";
		#endif

		// Initialize an InterstitialAd.
		interstitial = new InterstitialAd(adUnitId);
		// Create an empty ad request.
		request = new AdRequest.Builder().Build();
		//.AddTestDevice(AdRequest.TestDeviceSimulator)       // Simulator.
		//.AddTestDevice("0a91eafa-b30c-4187-914e-0b2172f6879c")  // My test device.
		//.Build();
		// Load the interstitial with the request.
		interstitial.LoadAd(request);

		//admob ends here
		platformStartPoint = platformGenerator.position;
		playerStartPoint = thePlayer.transform.position;
		theScoreManager = FindObjectOfType <ScoreManager>();
		pointsPerSecondScoreStore = theScoreManager.pointsPerSecond;
		moveSpeedStoreManager = thePlayer.moveSpeed;
		theScoreManager.scoreIncreasing = false;
		thePlayer.moveSpeed = 0f;
		theMainMenuInGame = FindObjectOfType<MainMenu> ();

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (theMainMenuInGame.isActiveAndEnabled) {
				Application.Quit ();
			} else if (theDeathScreen.isActiveAndEnabled) {
				MainPosition ();
			} else if (thePauseMenu.isActiveAndEnabled) {
				Application.Quit ();
			}
			else 
			{
				thePauseMenu.gameObject.SetActive (true);
				thePauseMenu.PauseGame ();
			}
		}
	}

	public void StartPlaying(){
		theScoreManager.scoreIncreasing = true;
		thePlayer.moveSpeed = moveSpeedStoreManager;
		bouncinessMaterial.bounciness = bouncinessInGame;
		thePlayer.GetComponent<CircleCollider2D> ().enabled = false;
		thePlayer.GetComponent<CircleCollider2D> ().enabled = true;

	}
	public void RestartGame()
	{
		theScoreManager.scoreIncreasing = false;
		forReportScore = (long)theScoreManager.scoreCount;
		Social.ReportScore(forReportScore, "CgkI-_nslMgKEAIQAQ", (bool success) => {

		});
		thePlayer.gameObject.SetActive (false);
		if (theScoreManager.scoreCount > pointsBeforeAddingAdsCount) {
			byteForAd++;
		}
		if (byteForAd >= triesBeforeAd) 
		{
			byteForAd = 0;
			if (interstitial.IsLoaded()) 
				{
					interstitial.Show();
				}
		}
		theDeathScreen.gameObject.SetActive(true);
		Handheld.Vibrate ();
		PlayerPrefs.SetFloat ("TotalRun", theScoreManager.totalRun);
		theDeathScreen.Activated ();

	}

	public void Reset(){
		interstitial.Destroy();
		theDeathScreen.gameObject.SetActive(false);
		platformList = FindObjectsOfType<PlatformDestroyer> ();
		for (int i = 0; i < platformList.Length; i++) {
			platformList [i].gameObject.SetActive (false);
		}
		thePlayer.transform.position = playerStartPoint;
		platformGenerator.position = platformStartPoint;
		thePlayer.gameObject.SetActive (true);
		theScoreManager.scoreIncreasing = true;
		theScoreManager.scoreCount = 0;
		theScoreManager.pointsPerSecond = pointsPerSecondScoreStore;
		interstitial.LoadAd(request);
		PlayerPrefs.SetFloat ("TotalRun", theScoreManager.totalRun);
	}

	public void MainPosition(){
		interstitial.Destroy();
		theDeathScreen.gameObject.SetActive(false);
		thePauseMenu.gameObject.SetActive (false);
		platformList = FindObjectsOfType<PlatformDestroyer> ();
		for (int i = 0; i < platformList.Length; i++) {
			platformList [i].gameObject.SetActive (false);
		}
		platformGenerator.position = platformStartPoint;
		bouncinessMaterial.bounciness = thePlayer.bouncingStart;
		thePlayer.GetComponent<CircleCollider2D> ().enabled = false;
		thePlayer.GetComponent<CircleCollider2D> ().enabled = true;
		thePlayer.transform.position = playerStartPoint;
		theScoreManager.scoreIncreasing = false;
		thePlayer.gameObject.SetActive (true);
		thePlayer.moveSpeed = 0f;
		thePlayer.GetComponent<PlayerController> ().enabled = false;
		theMainMenuInGame.gameObject.SetActive (true);
		theScoreManager.scoreCount = 0;
		theScoreManager.pointsPerSecond = pointsPerSecondScoreStore;
		interstitial.LoadAd(request);
		PlayerPrefs.SetFloat ("TotalRun", theScoreManager.totalRun);

	}

}
