using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	private float moveSpeedStore;
	public float jumpForce;
	public float speedMultiplier;
	public float speedIncreaseMilestone;
	private float speedMilestoneCount;
	private float speedMilestoneCountStore;
	private float speedIncreaseMilestoneStore;

	public float jumpTime;
	private float jumpTimeCounter;

	private Rigidbody2D myRigidbody;

	public bool grounded;
	public LayerMask whatIsGround;
	public Transform groundCheck;
	public float groundCheckRadius;
	public AudioSource deathSound;
	public PhysicsMaterial2D bouncinessMaterialPlayer;
	public float bouncingStart;
	private float pointsPerSecondStore;

	public bool canJump;
	public bool doubleJump;

	public GameManager theGameManager;
	private ScoreManager theScoreManager;
	private PlatformGenerator thePlatformGenerator;
	private float scaleChangeMinStore;
	private float scaleChangeMaxStore;
	public float scoreIncreaserPerMilesetone;

	// Use this for initialization
	void Start () {
		theScoreManager = FindObjectOfType<ScoreManager> ();
		pointsPerSecondStore = theScoreManager.pointsPerSecond;
		thePlatformGenerator = FindObjectOfType<PlatformGenerator> ();
		scaleChangeMinStore = thePlatformGenerator.scaleChangeMin;
		scaleChangeMaxStore = thePlatformGenerator.scaleChangeMax;
		myRigidbody = GetComponent<Rigidbody2D> ();

		jumpTimeCounter = jumpTime;
		speedMilestoneCount = speedIncreaseMilestone;

		moveSpeedStore = moveSpeed;
		speedMilestoneCountStore = speedMilestoneCount;
		speedIncreaseMilestoneStore = speedIncreaseMilestone;
		bouncinessMaterialPlayer.bounciness = bouncingStart;
		GetComponent<CircleCollider2D> ().enabled = false;
		GetComponent<CircleCollider2D> ().enabled = true;
		doubleJump = false;

	}
	
	// Update is called once per frame
	void Update () {
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

		if (transform.position.x > speedMilestoneCount) {
			speedMilestoneCount += speedIncreaseMilestone;
			speedIncreaseMilestone = speedIncreaseMilestone * speedMultiplier;
			moveSpeed = moveSpeed * speedMultiplier;
			theScoreManager.pointsPerSecond *= scoreIncreaserPerMilesetone;
			if (thePlatformGenerator.scaleChangeMin > 0.55f) {
				thePlatformGenerator.scaleChangeMin -= thePlatformGenerator.scaleChangeReducer;
				thePlatformGenerator.scaleChangeMax -= thePlatformGenerator.scaleChangeReducer;
			}
			if (thePlatformGenerator.obstacleOccurance > 1) {
				thePlatformGenerator.obstacleOccurance--;
			}
			thePlatformGenerator.wallObstacleBool = true;
			if(thePlatformGenerator.wallObstacleChanceRange > 5 ){
				thePlatformGenerator.wallObstacleChanceRange--;
			}
		}

		myRigidbody.velocity = new Vector2 (moveSpeed, myRigidbody.velocity.y);

		if(Input.GetMouseButtonDown(0))
		{
			if (canJump) 
			{
				myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, jumpForce);
				canJump = false;
				doubleJump = true;
			}
			else if (doubleJump) 
			{
				myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, jumpForce);
				doubleJump = false;
			}
		}

		if (Input.GetMouseButton (0)) {
			if (jumpTimeCounter > 0) {
				myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, jumpForce);
				jumpTimeCounter -= Time.deltaTime;
			}
		}

		if(Input.GetMouseButtonUp(0)){
			if (doubleJump) {
				jumpTimeCounter = jumpTime;
			} else {
				jumpTimeCounter = 0;
			}
		}
		if (grounded) 
		{
			jumpTimeCounter = jumpTime;
			canJump = true;
		}
			
	}

	void OnCollisionEnter2D ( Collision2D other)
	{
		if (other.gameObject.tag == "killbox") 
		{
			deathSound.Play ();
			theGameManager.RestartGame ();
			moveSpeed = moveSpeedStore;
			speedMilestoneCount = speedMilestoneCountStore;
			speedIncreaseMilestone = speedIncreaseMilestoneStore;
			theScoreManager.pointsPerSecond = pointsPerSecondStore;
			thePlatformGenerator.scaleChangeMax = scaleChangeMaxStore;
			thePlatformGenerator.scaleChangeMin = scaleChangeMinStore;
			thePlatformGenerator.obstacleOccurance = 9;
			thePlatformGenerator.wallObstacleBool = false;
			thePlatformGenerator.wallObstacleChanceRange = 19;
		}
	}
}
