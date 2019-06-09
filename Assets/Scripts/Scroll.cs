using UnityEngine;
using System.Collections;

public class Scroll : MonoBehaviour {

	public float speedScroll;
	public PlayerController thePlayer;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (thePlayer.isActiveAndEnabled) {
			Vector2 offset = new Vector2 (Time.time * speedScroll, 0);
			GetComponent<Renderer> ().material.mainTextureOffset = offset;  
		}
	}
}
