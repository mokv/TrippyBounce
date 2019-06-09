using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class DeathMenu : MonoBehaviour {

	//From here is FadeEffect

	public float alphaValue;
	private float reverseAlphaValue;
	public bool check;
	public float fadeSpeed;
	public GameObject playAgainButton;
	public GameObject homeButton;
	public GameObject rateAndReviewButton;
	public Image theImage;
	private Image playAgainButtonImage;
	private Image homeButtonImage;
	private Image rateAndReviewImage;
	public GameObject ambientSourceObject;
	private AudioSource ambientSource;

	void Update(){
		if (check) {
			if (alphaValue >= 1f) {

			} else if (alphaValue >= 0f) {
				alphaValue = alphaValue + (Time.deltaTime * fadeSpeed);
			}
			playAgainButtonImage.color = new Color (1, 1, 1, alphaValue);
			homeButtonImage.color = new Color (1, 1, 1, alphaValue);
			rateAndReviewImage.color = new Color (1, 1, 1, alphaValue);
			if (reverseAlphaValue > 0f) {
				reverseAlphaValue -= Time.deltaTime * fadeSpeed;
				theImage.color = new Color (1, 1, 1, reverseAlphaValue);
			}
		}
	}
	public void Activated(){
		ambientSource = ambientSourceObject.GetComponent<AudioSource> ();
		playAgainButtonImage = playAgainButton.GetComponent<Image> ();
		homeButtonImage = homeButton.GetComponent<Image> ();
		rateAndReviewImage = rateAndReviewButton.GetComponent<Image> ();
		check = true;
		alphaValue = 0f;
		reverseAlphaValue = 1f;
		playAgainButtonImage.color = new Color (1, 1, 1, alphaValue);
		homeButtonImage.color = new Color (1, 1, 1, alphaValue);
		rateAndReviewImage.color = new Color (1, 1, 1, alphaValue);
		theImage.color = new Color (1, 1, 1, reverseAlphaValue);
		ambientSource.volume = 0.5f;
		//ambientSource.Stop ();
	}

	public void RestartGame(){
		//ambientSource.Play ();
		ambientSource.volume = 1f;;
		FindObjectOfType<GameManager> ().Reset ();
	}

	public void BackToMainMenu(){
		//ambientSource.Play ();
		ambientSource.volume = 1f;;
		FindObjectOfType<GameManager> ().MainPosition ();
	}

	public void RateAndReview(){
		Application.OpenURL("market://details?id=com.MokVProductions.TrippyBounce");
	}
}
