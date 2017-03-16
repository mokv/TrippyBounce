using UnityEngine;
using System.Collections;

public class PlatformDestroyer : MonoBehaviour {

	public GameObject platformDestructionPoint;
	private int obstacleOrNot;
	private int obstacleOrNotRange;
	private bool obstacleGoUp;
	private bool obstacleGoDown;
	private float obstacleMovement;
	private float minHeight;
	private GameObject maxHeightPoint;
	private float maxHeight;
	private int upOrDown;
	private GameObject platformGenerator;

	// Use this for initialization
	void Start () {
		platformDestructionPoint = GameObject.Find ("PlatformDestructionPoint");
		platformGenerator = GameObject.Find ("PlatformGenerator");
		obstacleMovement = platformGenerator.transform.position.y;
		minHeight = transform.position.y;
		maxHeightPoint = GameObject.Find ("MaxHeightPoint");
		maxHeight = maxHeightPoint.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < platformDestructionPoint.transform.position.x) 
		{
			gameObject.SetActive(false);
		}
		if(obstacleOrNot == 1){
  			if (obstacleGoDown) 
  			{
  				obstacleMovement -= Time.deltaTime*2;
				gameObject.transform.position = new Vector3 (gameObject.transform.position.x, obstacleMovement, gameObject.transform.position.z);
  				if (obstacleMovement <= minHeight) {
  					obstacleGoUp = true;
  					obstacleGoDown = false;
  				}
  			} 
  			else if (obstacleGoUp) 
  			{
  				obstacleMovement += Time.deltaTime*2;
				gameObject.transform.position = new Vector3 (gameObject.transform.position.x, obstacleMovement, gameObject.transform.position.z);
  				if (obstacleMovement >= maxHeight) {
  					obstacleGoDown = true;
  					obstacleGoUp = false;
  				}
  			}



		}
	}

	void ObstacleCheck(int changeObstacleOccurance){
		obstacleOrNotRange = changeObstacleOccurance;
		obstacleOrNot = Random.Range (1, obstacleOrNotRange);
		//obstacleOrNot = 1;
		upOrDown = Random.Range (0, 1);
		if (upOrDown == 1) {
			obstacleGoDown = true;
		} else {
			obstacleGoUp = true;
		}
	}
}
