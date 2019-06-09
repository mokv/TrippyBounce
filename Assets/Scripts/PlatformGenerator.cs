using UnityEngine;
using System.Collections;

public class PlatformGenerator : MonoBehaviour {

	public GameObject thePlatform;
	public Transform generationPoint;
	private float distanceBetween;
	public float distanceBetweenMin;
	public float distanceBetweenMax;
	public ObjectPooler[] theObjectPools;
	public Transform maxHeightPoint;

	private float platformWidth;
	//private int platformSelector;
	private float[] platformWidths;
	private float minHeight;
	private float maxHeight;
	public float heightChange;
	public float scaleChangeMin;
	public float scaleChangeMax;
	public float scaleChangeReducer;
	private float scaleChange;
	public int obstacleOccurance;
	public bool wallObstacleBool;
	private int wallObstacleChance;
	public int wallObstacleChanceRange;

	//public GameObject[] thePlatforms;
	// Use this for initialization
	void Start () {
		//platformWidth = thePlatform.GetComponent<BoxCollider2D> ().size.x;
		platformWidths = new float[theObjectPools.Length];

		for (int i = 0; i < theObjectPools.Length; i++) {
			platformWidths[i] = theObjectPools[i].pooledObject.GetComponent<BoxCollider2D> ().size.x;
		}

		minHeight = transform.position.y;
		maxHeight = maxHeightPoint.position.y;
		obstacleOccurance = 9;
		wallObstacleChanceRange = 19;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < generationPoint.position.x) 
		{
			distanceBetween = Random.Range (distanceBetweenMin, distanceBetweenMax);

			//platformSelector = Random.Range (0, theObjectPools.Length);

			scaleChange = Random.Range (scaleChangeMin, scaleChangeMax);

			heightChange =  Random.Range (minHeight, maxHeight);

			wallObstacleChance = Random.Range (1, wallObstacleChanceRange);

			transform.position = new Vector3 (transform.position.x + (platformWidths [0] / 2) + distanceBetween, heightChange, transform.position.z);

			GameObject newPlatform = theObjectPools[0].GetPooledObject ();
			newPlatform.transform.localScale = new Vector3(scaleChange, 1f, 1f);
			newPlatform.transform.position = transform.position;
			newPlatform.transform.rotation = transform.rotation;
			newPlatform.SetActive (true); 
			newPlatform.SendMessage ("ObstacleCheck", obstacleOccurance);

			if (wallObstacleBool) {
				if (wallObstacleChance == 1) {
					GameObject newWall = theObjectPools[1].GetPooledObject ();
					newWall.transform.position = new Vector3 (transform.position.x + platformWidths[1]  + distanceBetween + (distanceBetween/2), heightChange + (minHeight/2.4f), transform.position.z);
					newWall.transform.eulerAngles = new Vector3 (0, 0, 90);
					newWall.SetActive (true); 
				}

			}

			transform.position = new Vector3 (transform.position.x + (platformWidths [0] / 2), transform.position.y, transform.position.z);
		}
	}
}
